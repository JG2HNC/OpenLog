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

		public const string CommonDbFile = "Common.db";

		private BindingSource _BS;

		/// <summary>
		/// Config
		/// </summary>
		public Config Config;
		/// <summary>
		/// 全QSO→メインフォーム(frmMain)のグリッドの内容
		/// </summary>
		public BindingList<cQSO> AllQSO { get; set; }

		/// <summary>
		/// DXCC一覧(Key:主要Prefix)
		/// </summary>
		public Dictionary<string, cDxcc> DXCCList { get; set; }

		/// <summary>
		/// 市区町村一覧(Key:市区町村番号; JCC/JCG+町村記号)
		/// </summary>
		public Dictionary<string, cCity> CityList { get; set; }

		/// <summary>
		/// 周波数帯一覧(Key:周波数名)
		/// </summary>
		public Dictionary<string, cBand> BandList { get; set; }

		/// <summary>
		/// 電波形式一覧(Key:電波形式 ex:CW)
		/// </summary>
		public Dictionary<string, cMode> ModeList { get; set; }

		/// <summary>
		/// 周波数帯ごとのDefaultのリグ・アンテナ(Key:周波数名)
		/// </summary>
		public Dictionary<string, cDefaultRig> DefaultRigList { get; set; }

		/// <summary>
		/// 正規表現パターンとDXCCエンティティ(Prefix)のペア
		/// </summary>
		public List<string[]> PatsDXCC { get; }

		/// <summary>
		/// MainFormのGridの列ヘッダ(文字列)
		/// </summary>
		public string[] GridColNames {
			get {
				int[] iColW = new int[dgvMain.ColumnCount];
				string[] sColN = new string[dgvMain.ColumnCount];
				for (int i = 0; i < iColW.Length; i++) {
					if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
					else { iColW[i] = -1; sColN[i] = "N/A"; }
				}
				return (sColN);
			}
		}

		/// <summary>
		/// MainFormのGridの列ヘッダ(幅)
		/// </summary>
		public int[] GridColWidth {
			get {
				int[] iColW = new int[dgvMain.ColumnCount];
				string[] sColN = new string[dgvMain.ColumnCount];
				for (int i = 0; i < iColW.Length; i++) {
					if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
					else { iColW[i] = -1; sColN[i] = "N/A"; }
				}
				return (iColW);
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="MyCallsign">自局コールサイン</param>
		public frmMain(string MyCallsign) {
			InitializeComponent();
			AllQSO = new BindingList<cQSO>();
			_BS = new BindingSource();

			//ゆくゆくは設定ファイル・DBから取得する
			Config = new Config();
			Config.MyEntity = "JA"; //最終的には設定ファイルから読み取る
			Config.MyCall = MyCallsign;
			if(Config.MyCall == "") {
				frmInputCallsign f = new frmInputCallsign(Config);
				f.ShowDialog();
			}
			Config.StartTick = DateTime.UtcNow.Ticks;
			Config.DBpath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "db");
			Config.DPIscaleFactor = CreateGraphics().DpiX / 96d;
			Config.UseDefaultRig = true;

			//エンティティ、JCC/JCG、バンド、モード
			DXCCList = new Dictionary<string, cDxcc>();
			CityList = new Dictionary<string, cCity>();
			BandList = new Dictionary<string, cBand>();
			ModeList = new Dictionary<string, cMode>();

			//初期設定のRig・Ant→コールサインDBから取得
			DefaultRigList = new Dictionary<string, cDefaultRig>();
		}

		private void frmMain_Load(object sender, EventArgs e) {
			if(Config.MyCall == "") { ErrMsg("コールサインが未設定です。\n一旦アプリを終了します。アプリを再起動して、コールサインを入力してください。"); }


			#region "DXCC・JCC/JCG・バンド・モード取得"
			try {
				string sPropDb = Path.Combine(Config.DBpath, CommonDbFile);
				if (!Directory.Exists(Config.DBpath)) { ErrMsg(string.Format("Error: Not exist Directory \"{0}\"", Config.DBpath)); return; }
				if (!File.Exists(sPropDb)) { ErrMsg(string.Format("Error: Not exist \"{0}\"", frmMain.CommonDbFile)); return; }
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

							DXCCList.Add(sDxcc, new cDxcc(sDxcc, sName, sPats, iECode));
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
							CityList.Add(sDc, new cCity(sDc, sC2, sA, sN, sS));
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
							BandList.Add(sBF, new cBand(sBF, sBL, dL, dU));

							//初期設定のRig・Ant→Keyのみ登録
							DefaultRigList.Add(sBF, new cDefaultRig(sBF));
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
							ModeList.Add(sMd, new cMode(sMd, sCt, sTp));
						}
					}
				}
				#endregion

			}
			catch (Exception ex) {
				ErrMsg("Reading CommonDB.\n" + ex.Message);
				return;
			}
			#endregion

			string sQOsDb = Path.Combine(Config.DBpath, string.Format("{0}.db", Config.MyCall.ToUpper()));
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
								AllQSO.Add(q);
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

				string sQSODb = Path.Combine(Config.DBpath, string.Format("{0}.db", Config.MyCall.ToUpper()));
				using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", sQSODb))) {
					con.Open();
					using (SQLiteTransaction st = con.BeginTransaction())
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = string.Format("CREATE TABLE IF NOT EXISTS[T_DefaultRig]([BandF] text PRIMARY KEY, {0} text);", string.Join(" text, ", lsFlds));
						cmd.ExecuteNonQuery();
						st.Commit();

						System.Reflection.PropertyInfo[] pi = typeof(cDefaultRig).GetProperties();
						lsFlds.Add("[BandF]");
						foreach (string sB in DefaultRigList.Keys) {
							cmd.CommandText = string.Format("select {0} from [T_DefaultRig] where [BandF] = '{1}';", string.Join(",", lsFlds), sB);
							using (SQLiteDataReader dr = cmd.ExecuteReader()) {
								while (dr.Read()) {
									string sT = dr.GetTableName(0);
									for (int i = 0; i < pi.Length; i++) {
										string sFn = pi[i].Name;
										string sVal = dr[sFn].ToString();
										if (!pi[i].CanWrite) { continue; } //書込不可プロパティは飛ばす(他プロパティから表示用を生成)
										var pp = typeof(cDefaultRig).GetProperty(sFn);
										pp.SetValue(DefaultRigList[sB], sVal);
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
				_BS.DataSource = AllQSO;
				dgvMain.DataSource = _BS;

				#region "DataGridView制御"
				dgvMain.RowHeadersVisible = false;
				dgvMain.Columns["ID"].Width = (int)(Config.DPIscaleFactor * 55);
				dgvMain.Columns["ScreenQSLMethod"].Width = (int)(Config.DPIscaleFactor * 20);
				dgvMain.Columns["ScreenCardSend"].Width = (int)(Config.DPIscaleFactor * 20);
				dgvMain.Columns["ScreenCardReceive"].Width = (int)(Config.DPIscaleFactor * 20);
				dgvMain.Columns["ScreenDate"].Width = (int)(Config.DPIscaleFactor * 95);
				dgvMain.Columns["ScreenTime"].Width = (int)(Config.DPIscaleFactor * 75);
				dgvMain.Columns["ScreenTimeZone"].Width = (int)(Config.DPIscaleFactor * 35);
				dgvMain.Columns["ScreenCall"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["RS_His"].Width = (int)(Config.DPIscaleFactor * 40);
				dgvMain.Columns["RS_My"].Width = (int)(Config.DPIscaleFactor * 40);
				dgvMain.Columns["Freq"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["Mode"].Width = (int)(Config.DPIscaleFactor * 50);
				dgvMain.Columns["ScreenPwr_My"].Width = (int)(Config.DPIscaleFactor * 30);
				dgvMain.Columns["QRA"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["QTH"].Width = (int)(Config.DPIscaleFactor * 200);
				dgvMain.Columns["DXCC"].Width = (int)(Config.DPIscaleFactor * 60);
				dgvMain.Columns["CityCode"].Width = (int)(Config.DPIscaleFactor * 80);
				dgvMain.Columns["GL"].Width = (int)(Config.DPIscaleFactor * 60);
				dgvMain.Columns["QTH_h"].Width = (int)(Config.DPIscaleFactor * 200);
				dgvMain.Columns["QSLManager"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["Rig_His"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["Ant_His"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["ScreenPwr_His"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["Rig_My"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["Ant_My"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["QTH_My"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["Prefix_My"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["CityCode_My"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["GL_My"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["CardMsg"].Width = (int)(Config.DPIscaleFactor * 100);
				dgvMain.Columns["Remarks"].Width = (int)(Config.DPIscaleFactor * 100);

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

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
			SaveToDb();
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}

		#region "Menu-File"
		private void mnuAddNewQSO_Click(object sender, EventArgs e) {
			List<cQSO> lsAllQSO = new List<cQSO>();
			foreach (cQSO q in AllQSO) { lsAllQSO.Add(q); }
			cQSO NewQSO;
			if (0 < AllQSO.Count) { NewQSO = new cQSO(AllQSO[AllQSO.Count - 1]); }
			else { NewQSO = new cQSO(); }
			int[] iColW = new int[dgvMain.ColumnCount];
			string[] sColN = new string[dgvMain.ColumnCount];
			for (int i = 0; i < iColW.Length; i++) {
				if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
				else { iColW[i] = -1; sColN[i] = "N/A"; }
			}

			frmQSO f = new frmQSO(NewQSO, this);
			f.Show();
		}

		private void mnuSaveDB_Click(object sender, EventArgs e) {
			SaveToDb();
		}

		private void mnuFilePrintCard_Click(object sender, EventArgs e) {
			List<cQSO> lsPrint = new List<cQSO>();
			foreach (cQSO q in AllQSO) {
				if (!q.Card_Send && q.QSLMethod != (int)cQSO.enQSLMethod.N && q.QSLMethod != (int)cQSO.enQSLMethod.R) { lsPrint.Add(q); }
			}
			frmPrintCards fp = new frmPrintCards(lsPrint, this);
			fp.ShowDialog();
		}

		private void mnuFilePrintStation_Click(object sender, EventArgs e) {
			List<cQSO> lsPrint = new List<cQSO>();
			foreach (cQSO q in AllQSO) {
				if (!q.Card_Send && q.QSLMethod != (int)cQSO.enQSLMethod.N && q.QSLMethod != (int)cQSO.enQSLMethod.R) { lsPrint.Add(q); }
			}
			frmPrintStation fs = new frmPrintStation(lsPrint, this);
			fs.Show();
		}

		#endregion

		#region "Menu-Search"
		private void mnuSearchCallsign_Click(object sender, EventArgs e) {
			int[] iColW = new int[dgvMain.ColumnCount];
			string[] sColN = new string[dgvMain.ColumnCount];
			for (int i = 0; i < iColW.Length; i++) {
				if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
				else { iColW[i] = -1; sColN[i] = "N/A"; }
			}

			frmSearchCallsign f = new frmSearchCallsign(this);
			f.ShowDialog();
		}

		#endregion

		#region "Menu-Tool"
		private void mnuMain_ToolsInportLogcs_Click(object sender, EventArgs e) {
			dgvMain.ClearSelection();
			string[] sHead = new string[dgvMain.Columns.Count];
			int[] iHead = new int[sHead.Length];
			for (int i = 0; i < dgvMain.Columns.Count; i++) {
				if (dgvMain.Columns[i].Visible) { iHead[i] = dgvMain.Columns[i].Width; }
				else { iHead[i] = -1; }
				sHead[i] = dgvMain.Columns[i].HeaderText;
			}
			frmInportLogcs f = new frmInportLogcs(this);
			f.ShowDialog();
		}

		private void mnuMain_ToolsInport_Click(object sender, EventArgs e) {
			int[] iColW = new int[dgvMain.ColumnCount];
			string[] sColN = new string[dgvMain.ColumnCount];
			for (int i = 0; i < iColW.Length; i++) {
				if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
				else { iColW[i] = -1; sColN[i] = "N/A"; }
			}

			frmInport f = new frmInport(this);
			f.ShowDialog();
		}

		private void mnuMain_ToolsSortDT_Click(object sender, EventArgs e) {
			string sQSODb = Path.Combine(Config.DBpath, string.Format("{0}.db", Config.MyCall.ToUpper()));
			string sOldDB = Path.Combine(Config.DBpath, string.Format("{0}_old.db", Config.MyCall.ToUpper()));
			List<cQSO> lsQSO = new List<cQSO>();
			#region "ソート準備"
			try {
				GC.Collect();
				GC.WaitForPendingFinalizers();
				if (File.Exists(sOldDB)) { File.Delete(sOldDB); }
				File.Move(sQSODb, sOldDB);

				bool bBandErr = false; //バンド名称エラー
				#region "ソート用リスト作成&バンドチェック"
				foreach (cQSO q in AllQSO) {
					if (!BandList.ContainsKey(q.Band)) { bBandErr = true; }
					lsQSO.Add(q);
				}

				if (bBandErr) {
					if (MessageBox.Show("周波数帯を修正しますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
						foreach (cQSO q in lsQSO) {
							foreach (string sB in BandList.Keys) {
								if (BandList[sB].Lower <= q.Freq && q.Freq <= BandList[sB].Upper) { q.Band = sB; break; }
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
				AllQSO.Clear();
				for (int i = 0; i < lsQSO.Count; i++) {
					cQSO q = lsQSO[i];
					q.ID = -1;//i + 1;
					AllQSO.Add(q);
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

						foreach (cDefaultRig df in DefaultRigList.Values) {
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
				sfd.InitialDirectory = Config.DBpath;
				sfd.FileName = string.Format("{0}_{1:yyyyMMdd}-{1:HHmm}.zip", Config.MyCall, DateTime.Now);
				sfd.Filter = "Zipファイル(*.zip)|*.zip|すべてのファイル(*.*)|*.*";
				sfd.OverwritePrompt = true;
				if (sfd.ShowDialog() == DialogResult.OK) {
					GC.Collect();
					GC.WaitForPendingFinalizers();
					if (File.Exists(sfd.FileName)) { File.Delete(sfd.FileName); }
					using (ZipArchive za = ZipFile.Open(sfd.FileName, ZipArchiveMode.Create)) {
						za.CreateEntryFromFile(Path.Combine(Config.DBpath, Config.MyCall + ".db"), Config.MyCall + ".db", CompressionLevel.Fastest);
						za.CreateEntryFromFile(Path.Combine(Config.DBpath, CommonDbFile), CommonDbFile, CompressionLevel.Fastest);
					}
				}
			}
			catch (Exception ex) {
				ErrMsg(ex.Message);
			}
		}

		private void mnuMain_ToolsEditBands_Click(object sender, EventArgs e) {
			frmEditBand fb = new frmEditBand(this);
			fb.ShowDialog();
		}

		private void mnuMain_ToolsEditDxcc_Click(object sender, EventArgs e) {
			frmEditDXCC fd = new frmEditDXCC(this);
			fd.ShowDialog();
		}

		private void mnuMain_ToolsEditCity_Click(object sender, EventArgs e) {
			frmEditCity fc = new frmEditCity(this);
			fc.ShowDialog();
		}


		private void mnuMain_ToolsEditRigAnt_Click(object sender, EventArgs e) {
			frmEditDefaultRig fd = new frmEditDefaultRig(this);
			fd.ShowDialog();
		}

		#endregion

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
			AllQSO.Remove(qso);
			if (0 <= iD) {
				string sQSODb = Path.Combine(Config.DBpath, string.Format("{0}.db", Config.MyCall.ToUpper()));
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

			frmPrintCards fp = new frmPrintCards(new List<cQSO>() { qso }, this);
			fp.ShowDialog();
		}


		#endregion


		/// <summary>
		/// QSO(行)をダブルクリック→QSO編集フォームを開く
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dgvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
			try {
				if(dgvMain.SelectedRows == null) { return; }
				cQSO qso = dgvMain.SelectedRows[0].DataBoundItem as cQSO;
				if(qso == null) {
					if(AllQSO.Count == 0) { qso = new cQSO(); }
					else { qso = new cQSO(AllQSO[AllQSO.Count - 1]); }
				}

				int[] iColW = new int[dgvMain.ColumnCount];
				string[] sColN = new string[dgvMain.ColumnCount];
				for (int i = 0; i < iColW.Length; i++) {
					if (dgvMain.Columns[i].Visible) { iColW[i] = dgvMain.Columns[i].Width; sColN[i] = dgvMain.Columns[i].HeaderText; }
					else { iColW[i] = -1; sColN[i] = "N/A"; }
				}

				frmQSO fq = new frmQSO(qso, this);
				fq.Show();
			}
			catch(Exception ex) {
				ErrMsg(ex.Message);
			}
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

			string sQSODb = Path.Combine(Config.DBpath, string.Format("{0}.db", Config.MyCall.ToUpper()));
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
		public void SaveToDb() {
			List<cQSO> lsInsert = new List<cQSO>(); //新規
			List<cQSO> lsUpdate = new List<cQSO>(); //更新
			#region "新規・更新の抽出"
			foreach (cQSO q in AllQSO) {
				if (q.ID < 0) { lsInsert.Add(q); }
				else if (Config.StartTick < q.LastUpdate) { lsUpdate.Add(q); }
			}
			#endregion

			string sQSODb = Path.Combine(Config.DBpath, string.Format("{0}.db", Config.MyCall.ToUpper()));
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
						Config.StartTick = DateTime.UtcNow.Ticks;
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
			foreach (cQSO q in AllQSO) {
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
