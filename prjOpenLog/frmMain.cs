using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.Data.SQLite;

namespace prjOpenLog {
	public partial class frmMain : Form {
		private Config _cfg;
		private BindingSource _BS;
		private BindingList<cQSO> _blQSOs;
		private Dictionary<string, cDxcc> _dcDXCC;
		private Dictionary<string, cCity> _dcCityCode;
		private Dictionary<string, cBand> _dcBand;
		private Dictionary<string, cMode> _dcMode;
		private Dictionary<string, cDefaultRig> _dcDefault;

		/// <summary>
		/// 正規表現パターンとDXCCエンティティ(Prefix)のペア
		/// </summary>
		public List<string[]> PatsDXCC { get; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="MyCallsign">自局コールサイン</param>
		public frmMain(string MyCallsign) {
			InitializeComponent();
			_blQSOs = new BindingList<cQSO>();
			_BS = new BindingSource();

			//ゆくゆくは設定ファイル・DBから取得する
			_cfg = new Config();
			_cfg.MyEntity = "JA"; //最終的には設定ファイルから読み取る
			_cfg.MyCall = MyCallsign;
			if(_cfg.MyCall == "") {
				frmInputCallsign f = new frmInputCallsign(_cfg);
				f.ShowDialog();
			}
			_cfg.StartTick = DateTime.UtcNow.Ticks;
			_cfg.DBpath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "db");
			_cfg.DPIscaleFactor = CreateGraphics().DpiX / 96d;
			_cfg.UseDefaultRig = true;

			//エンティティ、JCC/JCG、バンド、モード
			_dcDXCC = new Dictionary<string, cDxcc>();
			_dcCityCode = new Dictionary<string, cCity>();
			_dcBand = new Dictionary<string, cBand>();
			_dcMode = new Dictionary<string, cMode>();

			//初期設定のRig・Ant→コールサインDBから取得
			_dcDefault = new Dictionary<string, cDefaultRig>();
		}

		private void frmMain_Load(object sender, EventArgs e) {
			if(_cfg.MyCall == "") { ErrMsg("コールサインが未設定です。\n一旦アプリを終了します。アプリを再起動して、コールサインを入力してください。"); }


			#region "DXCC・JCC/JCG・バンド・モード取得"
			try {
				string sPropDb = Path.Combine(_cfg.DBpath, "PropertyList.db");
				if (!Directory.Exists(_cfg.DBpath)) { ErrMsg(string.Format("Error: Not exist Directory \"{0}\"", _cfg.DBpath)); return; }
				if (!File.Exists(sPropDb)) { ErrMsg("Error: Not exist \"PropertyList.db\""); return; }
				#region "DXCC, JCC/JCG, Band, Mode, DefaultRig(Key・空の値のみ)"
				using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", sPropDb))) {
					con.Open();

					//DXCC
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = "select [DXCC],[Name],[Pattern],[EntityCode] from [T_DXCC]";
						SQLiteDataReader dr = cmd.ExecuteReader();
						while (dr.Read()) {
							string sDxcc = dr["DXCC"].ToString();
							string sPats = dr["Pattern"].ToString();
							string sName = dr["Name"].ToString();
							int iECode;
							if(!int.TryParse(dr["EntityCode"].ToString(), out iECode)) { ErrMsg("Error: DXCC Entityのフォーマットが不正です。"); return; }

							_dcDXCC.Add(sDxcc, new cDxcc(sDxcc, sName, sPats, iECode));
						}
					}

					//JCC・JCG
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = "select [CityCode],[JCCG],[Area],[Name],[Search] from [T_City]";
						SQLiteDataReader dr = cmd.ExecuteReader();
						while (dr.Read()) {
							string sDc = dr["CityCode"].ToString();
							string sC2 = dr["JCCG"].ToString();
							string sA = dr["Area"].ToString();
							string sN = dr["Name"].ToString();
							string sS = dr["Search"].ToString();
							_dcCityCode.Add(sDc, new cCity(sDc, sC2, sA, sN, sS));
						}
					}

					//Band
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = "select [BandF],[BandL],[Lower],[Upper] from [T_Band] order by [Lower]";
						SQLiteDataReader dr = cmd.ExecuteReader();
						while (dr.Read()) {
							string sBF = dr["BandF"].ToString();
							string sBL = dr["BandL"].ToString();
							double dL = double.Parse(dr["Lower"].ToString());
							double dU = double.Parse(dr["Upper"].ToString());
							_dcBand.Add(sBF, new cBand(sBF, sBL, dL, dU));

							//初期設定のRig・Ant→Keyのみ登録
							_dcDefault.Add(sBF, new cDefaultRig(sBF));
						}
					}

					//Mode
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = "select [Mode],[Category],[Type] from [T_Mode]";
						SQLiteDataReader dr = cmd.ExecuteReader();
						while (dr.Read()) {
							string sMd = dr["Mode"].ToString();
							string sCt = dr["Category"].ToString();
							string sTp = dr["Type"].ToString();
							_dcMode.Add(sMd, new cMode(sMd, sCt, sTp));
						}
					}
				}
				#endregion

			}
			catch (Exception ex) {
				ErrMsg("Reading property.\n" + ex.Message);
				return;
			}
			#endregion

			string sQOsDb = Path.Combine(_cfg.DBpath, string.Format("{0}.db", _cfg.MyCall.ToUpper()));
			dgvMain.SuspendLayout();
			#region "QSO DB①"
			//DBなし→空のDBを作成
			if (!File.Exists(sQOsDb)) {
				try {
					CreateQsoDb();
				}
				catch(Exception ex) {
					ErrMsg("Error: While creating QSO db.\n" + ex.Message);
				}
			}
			else {
				try {
					//DBあり→QSOデータを読み取る
					List<string> lsFld = new List<string>(); //Select文→[]付き
					Dictionary<string, Type> dcFld = new Dictionary<string, Type>();
					#region "cQSOのPropertyInfo取得"
					{
						System.Reflection.PropertyInfo[] pi = typeof(cQSO).GetProperties();
						for (int i = 0; i < pi.Length; i++) {
							if (!pi[i].CanWrite) { continue; } //書込不可プロパティは飛ばす(他プロパティから表示用を生成)
							lsFld.Add(string.Format("[{0}]", pi[i].Name));
							dcFld.Add(pi[i].Name, pi[i].PropertyType);
						}
					}
					#endregion

					#region "DB→cQSO"
					using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", sQOsDb))) {
						con.Open();
						using (SQLiteCommand cmd = con.CreateCommand()) {
							cmd.CommandText = string.Format("select {0} from [T_QSO];", string.Join(", ", lsFld));

							SQLiteDataReader dr = cmd.ExecuteReader();
							while (dr.Read()) {
								cQSO q = new cQSO();

								#region "db→cQSO"
								foreach (string sP in dcFld.Keys) {
									var pp = typeof(cQSO).GetProperty(sP);
									string sVal = dr[sP].ToString();
									if(pp.PropertyType == typeof(int)) { pp.SetValue(q, int.Parse(sVal)); }
									else if (pp.PropertyType == typeof(long)) { pp.SetValue(q, long.Parse(sVal)); }
									else if (pp.PropertyType == typeof(double)) { pp.SetValue(q, double.Parse(sVal)); }
									else if (pp.PropertyType == typeof(string)) { pp.SetValue(q, sVal); }
									else if (pp.PropertyType == typeof(bool)) { pp.SetValue(q, Convert.ToBoolean(int.Parse(sVal))); }
								}
								#endregion
								_blQSOs.Add(q);
							}
						}
						con.Close();
					}
					//ファイルを開放させるおまじない
					//https://www.it-swarm.dev/ja/sqlite/systemdatasqlite-close%EF%BC%88%EF%BC%89%E3%81%8C%E3%83%87%E3%83%BC%E3%82%BF%E3%83%99%E3%83%BC%E3%82%B9%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%E3%82%92%E8%A7%A3%E6%94%BE%E3%81%97%E3%81%AA%E3%81%84/941181713/
					GC.Collect();
					GC.WaitForPendingFinalizers();
					#endregion
				}
				catch (Exception ex) {
					ErrMsg("Error: While reading QSO db.\n" + ex.Message);
				}
			}
			#endregion

			#region "QSO DB②→初期設定のRig・Ant"
			try {
				List<string> lsFlds = new List<string>();
				#region "cDefaultRigのPropertyInfo取得"
				{
					System.Reflection.PropertyInfo[] pi = typeof(cDefaultRig).GetProperties();
					for (int i = 0; i < pi.Length; i++) {
						if (pi[i].Name != "BandF") { lsFlds.Add(string.Format("[{0}]", pi[i].Name)); }
					}
				}
				#endregion

				string sQSODb = Path.Combine(_cfg.DBpath, string.Format("{0}.db", _cfg.MyCall.ToUpper()));
				using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", sQSODb))) {
					con.Open();
					using (SQLiteTransaction st = con.BeginTransaction())
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = string.Format("CREATE TABLE IF NOT EXISTS[T_DefaultRig]([BandF] text PRIMARY KEY, {0} text);", string.Join(" text, ", lsFlds));
						cmd.ExecuteNonQuery();
						st.Commit();

						System.Reflection.PropertyInfo[] pi = typeof(cDefaultRig).GetProperties();
						lsFlds.Add("[BandF]");
						foreach (string sB in _dcDefault.Keys) {
							cmd.CommandText = string.Format("select {0} from [T_DefaultRig] where [BandF] = '{1}';", string.Join(",", lsFlds), sB);
							using (SQLiteDataReader dr = cmd.ExecuteReader()) {
								while (dr.Read()) {
									string sT = dr.GetTableName(0);
									for (int i = 0; i < pi.Length; i++) {
										string sFn = pi[i].Name;
										string sVal = dr[sFn].ToString();
										if (!pi[i].CanWrite) { continue; } //書込不可プロパティは飛ばす(他プロパティから表示用を生成)
										var pp = typeof(cDefaultRig).GetProperty(sFn);
										pp.SetValue(_dcDefault[sB], sVal);
									}
								}
							}
						}
					}
				}
				//ファイルを開放させるおまじない
				//https://www.it-swarm.dev/ja/sqlite/systemdatasqlite-close%EF%BC%88%EF%BC%89%E3%81%8C%E3%83%87%E3%83%BC%E3%82%BF%E3%83%99%E3%83%BC%E3%82%B9%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB%E3%82%92%E8%A7%A3%E6%94%BE%E3%81%97%E3%81%AA%E3%81%84/941181713/
				GC.Collect();
				GC.WaitForPendingFinalizers();

			}
			catch (Exception ex) {
				ErrMsg("Error: While Read/Create DefaultRig on QSO db.\n" + ex.Message);
			}
			#endregion

			try {
				_BS.DataSource = _blQSOs;
				dgvMain.DataSource = _BS;

				#region "DataGridView制御"
				dgvMain.RowHeadersVisible = false;
				dgvMain.Columns["ID"].Width = (int)(_cfg.DPIscaleFactor * 55);
				dgvMain.Columns["ScreenQSLMethod"].Width = (int)(_cfg.DPIscaleFactor * 20);
				dgvMain.Columns["ScreenCardSend"].Width = (int)(_cfg.DPIscaleFactor * 20);
				dgvMain.Columns["ScreenCardReceive"].Width = (int)(_cfg.DPIscaleFactor * 20);
				dgvMain.Columns["ScreenDate"].Width = (int)(_cfg.DPIscaleFactor * 95);
				dgvMain.Columns["ScreenTime"].Width = (int)(_cfg.DPIscaleFactor * 75);
				dgvMain.Columns["ScreenTimeZone"].Width = (int)(_cfg.DPIscaleFactor * 35);
				dgvMain.Columns["ScreenCall"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["RS_His"].Width = (int)(_cfg.DPIscaleFactor * 40);
				dgvMain.Columns["RS_My"].Width = (int)(_cfg.DPIscaleFactor * 40);
				dgvMain.Columns["Freq"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["Mode"].Width = (int)(_cfg.DPIscaleFactor * 50);
				dgvMain.Columns["ScreenPwr_My"].Width = (int)(_cfg.DPIscaleFactor * 30);
				dgvMain.Columns["QRA"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["QTH"].Width = (int)(_cfg.DPIscaleFactor * 200);
				dgvMain.Columns["DXCC"].Width = (int)(_cfg.DPIscaleFactor * 60);
				dgvMain.Columns["CityCode"].Width = (int)(_cfg.DPIscaleFactor * 80);
				dgvMain.Columns["GL"].Width = (int)(_cfg.DPIscaleFactor * 60);
				dgvMain.Columns["QTH_h"].Width = (int)(_cfg.DPIscaleFactor * 200);
				dgvMain.Columns["QSLManager"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["Rig_His"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["Ant_His"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["ScreenPwr_His"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["Rig_My"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["Ant_My"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["QTH_My"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["Prefix_My"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["CityCode_My"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["GL_My"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["CardMsg"].Width = (int)(_cfg.DPIscaleFactor * 100);
				dgvMain.Columns["Remarks"].Width = (int)(_cfg.DPIscaleFactor * 100);

				dgvMain.Columns["ScreenQSLMethod"].HeaderText = "Q";
				dgvMain.Columns["ScreenCardSend"].HeaderText = "S";
				dgvMain.Columns["ScreenCardReceive"].HeaderText = "R";
				dgvMain.Columns["ScreenDate"].HeaderText = "Date";
				dgvMain.Columns["ScreenTime"].HeaderText = "Time";
				dgvMain.Columns["ScreenTimeZone"].HeaderText = "  ";
				dgvMain.Columns["ScreenCall"].HeaderText = "Callsign";
				dgvMain.Columns["RS_His"].HeaderText = "His";
				dgvMain.Columns["RS_My"].HeaderText = "My";
				dgvMain.Columns["Freq"].HeaderText = "Freq[MHz]";
				dgvMain.Columns["Mode"].HeaderText = "Mode";
				dgvMain.Columns["ScreenPwr_My"].HeaderText = "Power";
				dgvMain.Columns["QRA"].HeaderText = "Name";
				dgvMain.Columns["QTH"].HeaderText = "QTH";
				dgvMain.Columns["DXCC"].HeaderText = "DXCC";
				dgvMain.Columns["CityCode"].HeaderText = "JCC/JCG";
				dgvMain.Columns["GL"].HeaderText = "GL";
				dgvMain.Columns["QTH_h"].HeaderText = "Home";
				dgvMain.Columns["QSLManager"].HeaderText = "Manager";
				dgvMain.Columns["Rig_His"].HeaderText = "His Rig";
				dgvMain.Columns["Ant_His"].HeaderText = "His Ang";
				dgvMain.Columns["ScreenPwr_His"].HeaderText = "His Power";
				dgvMain.Columns["Rig_My"].HeaderText = "My Rig";
				dgvMain.Columns["Ant_My"].HeaderText = "My Ant";
				dgvMain.Columns["QTH_My"].HeaderText = "My QTH";
				dgvMain.Columns["Prefix_My"].HeaderText = "My Area";
				dgvMain.Columns["CityCode_My"].HeaderText = "My JCC/JCG";
				dgvMain.Columns["GL_My"].HeaderText = "My GL";
				dgvMain.Columns["CardMsg"].HeaderText = "Message";
				dgvMain.Columns["Remarks"].HeaderText = "Remarks";
				dgvMain.Columns["Prefix1"].HeaderText = "P1";
				dgvMain.Columns["Prefix1"].Visible = false;
				dgvMain.Columns["Prefix2"].Visible = false;
				dgvMain.Columns["Call"].Visible = false;
				dgvMain.Columns["Date_S"].Visible = false;
				dgvMain.Columns["Date_E"].Visible = false;
				dgvMain.Columns["TimeZone"].Visible = false;
				dgvMain.Columns["Band"].Visible = false;
				dgvMain.Columns["Pwr_My"].Visible = false;
				dgvMain.Columns["Pwr_His"].Visible = false;
				dgvMain.Columns["QSLMethod"].Visible = false;
				dgvMain.Columns["Card_Send"].Visible = false;
				dgvMain.Columns["Card_Resv"].Visible = false;
				dgvMain.Columns["Except"].Visible = false;
				dgvMain.Columns["LastUpdate"].Visible = false;
				dgvMain.Columns["CallQSL"].Visible = false;
				dgvMain.Columns["Time_HHmm"].Visible = false;

				if (0 < dgvMain.Rows.Count) { dgvMain.FirstDisplayedCell = dgvMain[0, dgvMain.Rows.Count - 1]; }
				#endregion

				CountCard(); //カード未発行枚数
			}
			catch (Exception ex) {
				ErrMsg("DataGridView.\n" + ex.Message);
				return;
			}
			dgvMain.ContextMenuStrip = cmsGrid;
			dgvMain.ResumeLayout();
		}

		private void frmMain_Activated(object sender, EventArgs e) {
			CountCard();
		}

		private void mnuAddNewQSO_Click(object sender, EventArgs e) {
			List<cQSO> lsAllQSO = new List<cQSO>();
			foreach(cQSO q in _blQSOs) { lsAllQSO.Add(q); }
			cQSO NewQSO;
			if(0 < _blQSOs.Count) { NewQSO = new cQSO(_blQSOs[_blQSOs.Count - 1]); }
			else { NewQSO = new cQSO(); }
			int[] iColW = new int[dgvMain.ColumnCount];
			string[] sColN = new string[dgvMain.ColumnCount];
			for (int i = 0; i < iColW.Length; i++) {
				if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
				else { iColW[i] = -1; sColN[i] = "N/A"; }
			}

			frmQSO f = new frmQSO(NewQSO, _blQSOs,  _dcDXCC, _dcCityCode, _dcBand, _dcMode, _dcDefault, iColW, sColN, _cfg);
			f.Show();
		}

		//QSOをダブルクリック
		private void dgvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
			try {
				if(dgvMain.SelectedRows == null) { return; }
				cQSO qso = dgvMain.SelectedRows[0].DataBoundItem as cQSO;
				if(qso == null) {
					if(_blQSOs.Count == 0) { qso = new cQSO(); }
					else { qso = new cQSO(_blQSOs[_blQSOs.Count - 1]); }
				}

				int[] iColW = new int[dgvMain.ColumnCount];
				string[] sColN = new string[dgvMain.ColumnCount];
				for (int i = 0; i < iColW.Length; i++) {
					if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
					else { iColW[i] = -1; sColN[i] = "N/A"; }
				}

				frmQSO fq = new frmQSO(qso, _blQSOs, _dcDXCC, _dcCityCode, _dcBand, _dcMode, _dcDefault, iColW, sColN, _cfg);
				fq.Show();
			}
			catch(Exception ex) {
				ErrMsg(ex.Message);
			}
		}

		private void mnuSaveDB_Click(object sender, EventArgs e) {
			SaveToDb();
		}
		private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
			SaveToDb();
		}

		private void mnuFilePrintCard_Click(object sender, EventArgs e) {
			List<cQSO> lsPrint = new List<cQSO>();
			foreach(cQSO q in _blQSOs) {
				if(!q.Card_Send && q.QSLMethod != (int)cQSO.enQSLMethod.N && q.QSLMethod != (int)cQSO.enQSLMethod.R) { lsPrint.Add(q); }
			}
			frmPrintCards fp = new frmPrintCards(lsPrint, _dcMode);
			fp.ShowDialog();
		}

		private void mnuFilePrintStation_Click(object sender, EventArgs e) {
			List<cQSO> lsPrint = new List<cQSO>();
			foreach (cQSO q in _blQSOs) {
				if (!q.Card_Send && q.QSLMethod != (int)cQSO.enQSLMethod.N && q.QSLMethod != (int)cQSO.enQSLMethod.R) { lsPrint.Add(q); }
			}
			frmPrintStation fs = new frmPrintStation(lsPrint, _dcMode);
			fs.Show();
		}

		#region "Context menu"
		private void cmsGrid_Received_Click(object sender, EventArgs e) {
			if (dgvMain.SelectedRows == null) { return; }
			if (dgvMain.SelectedRows.Count == 0) { return; }
			cQSO qso = dgvMain.SelectedRows[0].DataBoundItem as cQSO;
			if (qso == null) { return; }

			if (qso.Card_Resv) { qso.Card_Resv = false; }
			else { qso.Card_Resv = true; }
			qso.LastUpdate = DateTime.UtcNow.Ticks;
			SaveToDb();
		}

		private void cmsGrid_Sent_Click(object sender, EventArgs e) {
			if (dgvMain.SelectedRows == null) { return; }
			if (dgvMain.SelectedRows.Count == 0) { return; }
			cQSO qso = dgvMain.SelectedRows[0].DataBoundItem as cQSO;
			if (qso == null) { return; }

			if (qso.Card_Send) { qso.Card_Send = false; }
			else { qso.Card_Send = true; }
			qso.LastUpdate = DateTime.UtcNow.Ticks;
			SaveToDb();
			CountCard();
		}

		private void cmsGrid_Remove_Click(object sender, EventArgs e) {
			if (dgvMain.SelectedRows == null) { return; }
			if (dgvMain.SelectedRows.Count == 0) { return; }
			cQSO qso = dgvMain.SelectedRows[0].DataBoundItem as cQSO;
			if (qso == null) { return; }
			int iD = qso.ID;
			_blQSOs.Remove(qso);
			if(0 <= iD) {
				string sQSODb = Path.Combine(_cfg.DBpath, string.Format("{0}.db", _cfg.MyCall.ToUpper()));
				try {
					System.Reflection.PropertyInfo[] piQSO = typeof(cQSO).GetProperties();
					using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", sQSODb))) {
						con.Open();
						using (SQLiteTransaction st = con.BeginTransaction())
						using (SQLiteCommand cmd = con.CreateCommand()) {
							cmd.CommandText = string.Format("delete from [T_QSO] where [ID] = {0}", iD);
							cmd.ExecuteNonQuery();
							st.Commit();
						}
					}
				}
				catch (Exception ex) {
					ErrMsg("Error: Saving QSO to DB.\n" + ex.Message);
				}


			}
		}

		private void cmsGrid_Print_Click(object sender, EventArgs e) {
			if (dgvMain.SelectedRows == null) { return; }
			if (dgvMain.SelectedRows.Count == 0) { return; }
			cQSO qso = dgvMain.SelectedRows[0].DataBoundItem as cQSO;
			if (qso == null) { return; }

			frmPrintCards fp = new frmPrintCards(new List<cQSO>() { qso }, _dcMode);
			fp.ShowDialog();
		}


		#endregion

		private void mnuSearchCallsign_Click(object sender, EventArgs e) {
			int[] iColW = new int[dgvMain.ColumnCount];
			string[] sColN = new string[dgvMain.ColumnCount];
			for (int i = 0; i < iColW.Length; i++) {
				if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
				else { iColW[i] = -1; sColN[i] = "N/A"; }
			}

			frmSearchCallsign f = new frmSearchCallsign(_blQSOs, iColW, sColN);
			f.ShowDialog();
		}

		private void mnuMain_ToolsInportLogcs_Click(object sender, EventArgs e) {
			dgvMain.ClearSelection();
			string[] sHead = new string[dgvMain.Columns.Count];
			int[] iHead = new int[sHead.Length];
			for (int i = 0; i < dgvMain.Columns.Count; i++) {
				if (dgvMain.Columns[i].Visible) { iHead[i] = dgvMain.Columns[i].Width; }
				else { iHead[i] = -1; }
				sHead[i] = dgvMain.Columns[i].HeaderText;
			}
			frmInportLogcs f = new frmInportLogcs(_blQSOs, iHead, sHead, _dcBand);
			f.ShowDialog();
		}

		private void mnuMain_ToolsInport_Click(object sender, EventArgs e) {
			int[] iColW = new int[dgvMain.ColumnCount];
			string[] sColN = new string[dgvMain.ColumnCount];
			for (int i = 0; i < iColW.Length; i++) {
				if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
				else { iColW[i] = -1; sColN[i] = "N/A"; }
			}

			frmInport f = new frmInport(_blQSOs, _dcDXCC, _dcCityCode, _dcBand, _dcMode, _dcDefault, iColW, sColN, _cfg);
			f.ShowDialog();
		}

		private void mnuMain_ToolsSortDT_Click(object sender, EventArgs e) {
			string sQSODb = Path.Combine(_cfg.DBpath, string.Format("{0}.db", _cfg.MyCall.ToUpper()));
			string sOldDB = Path.Combine(_cfg.DBpath, string.Format("{0}_old.db", _cfg.MyCall.ToUpper()));
			List<cQSO> lsQSO = new List<cQSO>();
			#region "ソート準備"
			try {
				GC.Collect();
				GC.WaitForPendingFinalizers();
				if (File.Exists(sOldDB)) { File.Delete(sOldDB); }
				File.Move(sQSODb, sOldDB);

				bool bBandErr = false; //バンド名称エラー
				#region "ソート用リスト作成&バンドチェック"
				foreach (cQSO q in _blQSOs) {
					if (!_dcBand.ContainsKey(q.Band)) { bBandErr = true; }
					lsQSO.Add(q);
				}

				if (bBandErr) {
					if (MessageBox.Show("周波数帯を修正しますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
						foreach (cQSO q in lsQSO) {
							foreach (string sB in _dcBand.Keys) {
								if(_dcBand[sB].Lower <= q.Freq && q.Freq <= _dcBand[sB].Upper) { q.Band = sB; break; }
							}
						}
					}
				}
				#endregion
				lsQSO.Sort((a, b) => (int)((a.Date_S - b.Date_S) / 10000000));

				CreateQsoDb();
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			catch (Exception ex) {
				ErrMsg("ソート中のエラー\n" + ex.Message);
			}
			#endregion

			#region "ソート結果をBindingList & DBに登録"
			try {
				dgvMain.SuspendLayout();
				_blQSOs.Clear();
				for (int i = 0; i < lsQSO.Count; i++) {
					cQSO q = lsQSO[i];
					q.ID = -1;//i + 1;
					_blQSOs.Add(q);
				}
				SaveToDb();
			}
			catch (Exception ex) {
				ErrMsg("ソート反映中のエラー" + ex.Message);
			}
			#endregion

			#region "DefaultRigをDBに登録"
			try {
				using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", sQSODb))) {
					con.Open();
					using (SQLiteTransaction st = con.BeginTransaction())
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = "delete from  [T_DefaultRig]";
						cmd.ExecuteNonQuery();
						
						foreach (cDefaultRig df in _dcDefault.Values) {
							cmd.CommandText = string.Format("INSERT INTO [T_DefaultRig]([BandF], [RigHome], [AntHome], [RigMobile], [AntMobile]) VALUES('{0}','{1}','{2}','{3}','{4}');", df.BandF, df.RigHome, df.AntHome, df.RigMobile, df.AntMobile);
							int iRes = cmd.ExecuteNonQuery();
						}
						st.Commit();
					}
					con.Close();
				}
				//DBを開放させるおまじない
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			catch (Exception ex) {
				ErrMsg(ex.Message);
			}
			#endregion

			//用済みになったバックアップを削除
			if (MessageBox.Show("ソート前のバックアップを削除します。\nファイル名:" + sOldDB, "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK) {
				File.Delete(sOldDB);
			}
		}

		private void mnuMain_ToolsBackup_Click(object sender, EventArgs e) {
			SaveFileDialog sfd = new SaveFileDialog();

			//DBを開放させるおまじない
			GC.Collect();
			GC.WaitForPendingFinalizers();

			try {
				sfd.InitialDirectory = _cfg.DBpath;
				sfd.FileName = string.Format("{0}_{1:yyyyMMdd}-{1:HHmm}.zip", _cfg.MyCall, DateTime.Now);
				sfd.Filter = "Zipファイル(*.zip)|*.zip|すべてのファイル(*.*)|*.*";
				sfd.OverwritePrompt = true;
				if(sfd.ShowDialog() == DialogResult.OK) {
					GC.Collect();
					GC.WaitForPendingFinalizers();
					if (File.Exists(sfd.FileName)) { File.Delete(sfd.FileName); }
					using (ZipArchive za = ZipFile.Open(sfd.FileName, ZipArchiveMode.Create)) {
						za.CreateEntryFromFile(Path.Combine(_cfg.DBpath, _cfg.MyCall + ".db"), _cfg.MyCall + ".db", CompressionLevel.Fastest);
						za.CreateEntryFromFile(Path.Combine(_cfg.DBpath, "PropertyList.db"), "PropertyList.db", CompressionLevel.Fastest);
					}
				}
			}
			catch (Exception ex) {
				ErrMsg(ex.Message);
			}
		}

		private void mnuMain_ToolsEditBands_Click(object sender, EventArgs e) {
			frmEditBand fb = new frmEditBand(_dcBand, Path.Combine(_cfg.DBpath, "PropertyList.db"));
			fb.ShowDialog();
		}

		private void mnuMain_ToolsEditDxcc_Click(object sender, EventArgs e) {
			frmEditDXCC fd = new frmEditDXCC(_dcDXCC, Path.Combine(_cfg.DBpath, "PropertyList.db"));
			fd.ShowDialog();
		}

		private void mnuMain_ToolsEditCity_Click(object sender, EventArgs e) {
			frmEditCity fc = new frmEditCity(_dcCityCode, Path.Combine(_cfg.DBpath, "PropertyList.db"));
			fc.ShowDialog();
		}


		private void mnuMain_ToolsEditRigAnt_Click(object sender, EventArgs e) {
			string sDB = Path.Combine(_cfg.DBpath, string.Format("{0}.db", _cfg.MyCall.ToUpper()));
			frmEditDefaultRig fd = new frmEditDefaultRig(_dcDefault, sDB);
			fd.ShowDialog();
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}

		//エラーメッセージ
		public void ErrMsg(string Msg) {
			MessageBox.Show(Msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}


		/// <summary>
		/// 空のDBを作成する
		/// </summary>
		private void CreateQsoDb() {
			Dictionary<string, string> dcFType = new Dictionary<string, string>();
			#region "C#とSQLiteの型の対応"
			dcFType.Add("System.Int32", "integer");
			dcFType.Add("System.Int64", "integer");
			dcFType.Add("System.Boolean", "integer");
			dcFType.Add("System.Double", "real");
			dcFType.Add("System.String", "text");
			#endregion

			List<string> lsFld = new List<string>();
			#region "cQSOのPropertyInfoからSQLを生成する"
			{
				System.Reflection.PropertyInfo[] pi = typeof(cQSO).GetProperties();
				for (int i = 0; i < pi.Length; i++) {
					if (!pi[i].CanWrite) { continue; } //書込不可プロパティは飛ばす(他プロパティから表示用を生成)
					string sDef = string.Format("[{0}] {1}", pi[i].Name, dcFType[pi[i].PropertyType.ToString()]);
					if (pi[i].Name == "ID") { sDef = sDef + " PRIMARY KEY"; }
					lsFld.Add(sDef);
				}
			}
			#endregion

			string sQSODb = Path.Combine(_cfg.DBpath, string.Format("{0}.db", _cfg.MyCall.ToUpper()));
			#region "DBを作成する"
			using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", sQSODb))) {
				con.Open();
				using (SQLiteTransaction st = con.BeginTransaction())
				using (SQLiteCommand cmd = con.CreateCommand()) {
					cmd.CommandText = string.Format("CREATE TABLE IF NOT EXISTS[T_QSO]({0});", string.Join(", ", lsFld));
					cmd.ExecuteNonQuery();

					List<string> lsFlds = new List<string>();
					#region "cDefaultRigのPropertyInfo取得"
					{
						System.Reflection.PropertyInfo[] pi = typeof(cDefaultRig).GetProperties();
						for (int i = 0; i < pi.Length; i++) {
							if (pi[i].Name != "BandF") { lsFlds.Add(string.Format("[{0}]", pi[i].Name)); }
						}
					}
					#endregion


					cmd.CommandText = string.Format("CREATE TABLE IF NOT EXISTS[T_DefaultRig]([BandF] text PRIMARY KEY, {0} text);", string.Join(" text, ", lsFlds));
					cmd.ExecuteNonQuery();

					st.Commit();
				}
			}
			#endregion
		}

		/// <summary>
		/// DBへ保存
		/// </summary>
		private void SaveToDb() {
			List<cQSO> lsInsert = new List<cQSO>(); //新規
			List<cQSO> lsUpdate = new List<cQSO>(); //更新
			#region "新規・更新の抽出"
			foreach (cQSO q in _blQSOs) {
				if (q.ID < 0) { lsInsert.Add(q); }
				else if (_cfg.StartTick < q.LastUpdate) { lsUpdate.Add(q); }
			}
			#endregion

			string sQSODb = Path.Combine(_cfg.DBpath, string.Format("{0}.db", _cfg.MyCall.ToUpper()));
			try {
				System.Reflection.PropertyInfo[] piQSO = typeof(cQSO).GetProperties();
				using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", sQSODb))) {
					con.Open();
					using (SQLiteTransaction st = con.BeginTransaction())
					using (SQLiteCommand cmd = con.CreateCommand()) {
						#region "更新"
						try {
							foreach (cQSO q in lsUpdate) {
								List<string> lsPair = new List<string>();
								for (int i = 0; i < piQSO.Length; i++) {
									if (!piQSO[i].CanWrite) { continue; } //書けないProperty(表示用日付・Call等)→DBに無いため飛ばす
									System.Reflection.PropertyInfo p = typeof(cQSO).GetProperty(piQSO[i].Name); //名前でアクセス
									if (piQSO[i].PropertyType == typeof(bool)) {
										lsPair.Add(string.Format("[{0}]='{1}'", piQSO[i].Name, Convert.ToInt32(p.GetValue(q))));
									}
									else {
										lsPair.Add(string.Format("[{0}]='{1}'", piQSO[i].Name, p.GetValue(q).ToString()));
									}
								}
								cmd.CommandText = string.Format("update [T_QSO] set {0} where [ID]={1}", string.Join(", ", lsPair), q.ID);
								cmd.ExecuteNonQuery();
							}
						}
						catch (Exception ex) {
							ErrMsg("Error: Updating QSO db.\n" + ex.Message);
							return;
						}
						#endregion

						int iMaxID = 0;
						#region "ID最大値取得"
						try {
							cmd.CommandText = "select [ID] from [T_QSO]";
							SQLiteDataReader dr = cmd.ExecuteReader();
							while (dr.Read()) {
								int i; int.TryParse(dr["ID"].ToString(), out i);
								if (iMaxID < i) { iMaxID = i; }
							}
							dr.Close();
						}
						catch (Exception ex) {
							ErrMsg("Error: Getting maximum ID\n" + ex.Message);
							return;
						}
						#endregion

						#region "挿入(新規)"
						try {
							foreach (cQSO q in lsInsert) {
								q.ID = iMaxID + 1;
								iMaxID++;
								List<string> lsFld = new List<string>();
								List<string> lsVal = new List<string>();
								for (int i = 0; i < piQSO.Length; i++) {
									if (!piQSO[i].CanWrite) { continue; } //書けないProperty(表示用日付・Call等)→DBに無いため飛ばす
									System.Reflection.PropertyInfo p = typeof(cQSO).GetProperty(piQSO[i].Name); //名前でアクセス
									lsFld.Add(string.Format("[{0}]", piQSO[i].Name));
									if (piQSO[i].PropertyType == typeof(bool)) { lsVal.Add(string.Format("'{0}'", Convert.ToInt32(p.GetValue(q)))); }
									else {
										lsVal.Add(string.Format("'{0}'", p.GetValue(q).ToString()));
									}
								}
								cmd.CommandText = string.Format("INSERT INTO [T_QSO]({0}) VALUES({1});", string.Join(", ", lsFld), string.Join(", ", lsVal));
								cmd.ExecuteNonQuery();
							}
						}
						catch (Exception ex) {
							ErrMsg("Error: Insert new QSO db.\n" + ex.Message);
							return;
						}
						#endregion

						st.Commit();
						_cfg.StartTick = DateTime.UtcNow.Ticks;
					}
					con.Close();
				}
				//DBを開放させるおまじない
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			catch (Exception ex) {
				ErrMsg("Error: Saving QSO to DB.\n" + ex.Message);
			}
		}

		/// <summary>
		/// カード未発行を数える
		/// </summary>
		private void CountCard() {
			int iCn = 0;
			foreach (cQSO q in _blQSOs) {
				if (q.Card_Send) { continue; }
				if (q.QSLMethod == (int)cQSO.enQSLMethod.N) { continue; }
				if (q.QSLMethod == (int)cQSO.enQSLMethod.R) { continue; }
				iCn++;
			}
			stlQSL.Text = string.Format("カード未発行: {0}件", iCn);
		}
		private void dgvMain_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
			CountCard();
		}

	}

	/// <summary>
	/// 設定
	/// </summary>
	public class Config {
		/// <summary>
		/// 自局の所在するDXCC(日本→JA)
		/// </summary>
		public string MyEntity { get; set; }

		/// <summary>
		/// 自局コールサイン
		/// </summary>
		public string MyCall { get; set; }

		/// <summary>
		/// このアプリケーションを起動した時刻
		/// </summary>
		public long StartTick { get; set; }

		/// <summary>
		/// 更新データ等を保存するPath
		/// </summary>
		public string DBpath { get; set; }

		/// <summary>
		/// 画面のスケールファクタ
		/// </summary>
		public double DPIscaleFactor { get; set; }

		/// <summary>
		/// 周波数帯毎に登録した無線機・アンテナを自動入力するか
		/// </summary>
		public bool UseDefaultRig { get; set; }

	}



}
