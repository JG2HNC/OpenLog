using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace prjOpenLog {
	public partial class frmQSO : Form {
		private cQSO _QSO;
		private BindingList<cQSO> _blAllQSO;
		private Dictionary<string, cDxcc> _dcDXCC;
		private Dictionary<string, cCity> _dcCityCode;
		private Dictionary<string, cBand> _dcBand;
		private Dictionary<string, cMode> _dcMode;
		private Dictionary<string, cDefaultRig> _dcDefault;
		private BindingSource _bsPast; 
		private BindingList<cQSO> _blPastQSO;
		private Config _cfg;
		private int[] _iColWidth;
		private string[] _sColName;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="QSO">編集・新規QSOオブジェクト</param>
		/// <param name="AllQSO">全更新履歴</param>
		/// <param name="DXCC">DXCCリスト</param>
		/// <param name="CityCode">JCC/JCG+町村リスト</param>
		/// <param name="Bands">バンド一覧</param>
		/// <param name="Modes">モード一覧</param>
		/// <param name="Config">設定</param>
		public frmQSO(cQSO QSO, BindingList<cQSO> AllQSO, Dictionary<string, cDxcc> DXCC, Dictionary<string, cCity> CityCode, Dictionary<string, cBand> Bands, Dictionary<string, cMode> Modes, Dictionary<string, cDefaultRig> Default,int[] ColWidths, string[] ColNames, Config Config) {
			_QSO = QSO; _blAllQSO = AllQSO; _dcDXCC = DXCC; _dcCityCode = CityCode; _dcBand = Bands; _dcMode = Modes; _dcDefault = Default; _cfg = Config;
			_iColWidth = ColWidths; _sColName = ColNames;
			InitializeComponent();
		}

		private void frmQSO_Load(object sender, EventArgs e) {
			RefleshForm();
			_blPastQSO = new BindingList<cQSO>();
			_bsPast = new BindingSource();
			_bsPast.DataSource = _blPastQSO;
			dgvPastQSO.DataSource = _bsPast;
			if(_QSO.Band != "") { lblBand.Text = _QSO.Band; }


			#region "DataGridView制御"
			dgvPastQSO.Columns["Prefix1"].Visible = false;
			dgvPastQSO.Columns["Prefix2"].Visible = false;
			dgvPastQSO.Columns["Call"].Visible = false;
			dgvPastQSO.Columns["Date_S"].Visible = false;
			dgvPastQSO.Columns["Date_E"].Visible = false;
			dgvPastQSO.Columns["TimeZone"].Visible = false;
			dgvPastQSO.Columns["Band"].Visible = false;
			dgvPastQSO.Columns["Pwr_My"].Visible = false;
			dgvPastQSO.Columns["Pwr_His"].Visible = false;
			dgvPastQSO.Columns["QSLMethod"].Visible = false;
			dgvPastQSO.Columns["Card_Send"].Visible = false;
			dgvPastQSO.Columns["Card_Resv"].Visible = false;
			dgvPastQSO.Columns["Except"].Visible = false;
			dgvPastQSO.Columns["LastUpdate"].Visible = false;
			dgvPastQSO.Columns["CallQSL"].Visible = false;
			dgvPastQSO.Columns["Time_HHmm"].Visible = false;
			for (int i = 0; i < _iColWidth.Length; i++) {
				dgvPastQSO.Columns[i].Width = _iColWidth[i];
				dgvPastQSO.Columns[i].HeaderText = _sColName[i];
			}
			#endregion

			ActiveControl = txtCall;
		}

		//エラーメッセージ
		public void ErrMsg(string Msg) {
			MessageBox.Show(Msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		//警告メッセージ
		public void WarnMsg(string Msg) {
			MessageBox.Show(Msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// FormにQSOの内容を反映する
		/// </summary>
		private void RefleshForm() {
			txtPrefix1.Text = _QSO.Prefix1;
			txtPrefix2.Text = _QSO.Prefix2;
			txtCall.Text = _QSO.Call;
			txtQRA.Text = _QSO.QRA;
			txtDXCC.Text = _QSO.DXCC;
			txtCityCode.Text = _QSO.CityCode;
			txtGL.Text = _QSO.GL;
			txtQTH.Text = _QSO.QTH;
			txtQTH_h.Text = _QSO.QTH_h;
			txtRS_His.Text = _QSO.RS_His.ToString();
			txtRS_My.Text = _QSO.RS_My.ToString(); ;
			txtFreq.Text = string.Format("{0:f3}", _QSO.Freq);
			txtMode.Text = _QSO.Mode;
			if (0d < _QSO.Pwr_My) { txtPwr_My.Text = _QSO.Pwr_My.ToString(); }

			DateTime dtS = DateTime.FromBinary(_QSO.Date_S).AddHours(_QSO.TimeZone);
			txtDate_S.Text = string.Format("{0:yyyy/MM/dd}", dtS);
			if(dtS.Second == 0) { txtTime_S.Text = string.Format("{0:HH}:{0:mm}", dtS); }
			else { txtTime_S.Text = string.Format("{0:HH}:{0:mm}:{0:ss}", dtS); }
			
			#region "交信終了時刻"
			if (_QSO.Date_E < 0) { txtTime_E.Text = ""; }
			else {
				DateTime dtE = DateTime.FromBinary(_QSO.Date_E).AddHours(_QSO.TimeZone);
				txtTime_E.Text = string.Format("{0:HH}:{0:mm}", dtE);
			}
			#endregion

			#region "TimeZone"
			if (_QSO.TimeZone == 9) { txtTimeZone.Text = "JST"; }
			else if (_QSO.TimeZone == 0) { txtTimeZone.Text = "UTC"; }
			else { txtTimeZone.Text = _QSO.TimeZone.ToString("+#;-#;"); }
			#endregion

			#region "QSL交換方式"
			if (_QSO.QSLMethod == 1) { rdoBureau.Checked = true; }
			else if (_QSO.QSLMethod == 2) { rdoDirect.Checked = true; }
			else if (_QSO.QSLMethod == 3) { rdoManager.Checked = true; }
			else { rdoNone.Checked = true; }
			txtQSLManager.Text = _QSO.QSLManager;
			#endregion

			txtRig_His.Text = _QSO.Rig_His;
			txtAnt_His.Text = _QSO.Ant_His;
			if (0 < _QSO.Pwr_His) { txtPwr_His.Text = _QSO.Pwr_His.ToString(); }
			else { txtPwr_His.Text = ""; }
			txtRig_My.Text = _QSO.Rig_My;
			txtAnt_My.Text = _QSO.Ant_My;
			txtQTH_My.Text = _QSO.QTH_My;
			txtPrefix_My.Text = _QSO.Prefix_My;
			txtCityCode_My.Text = _QSO.CityCode_My;
			txtGL_My.Text = _QSO.GL_My;
			txtCardMsg.Text = _QSO.CardMsg;
			txtRemark.Text = _QSO.Remarks;
			chkCard_Send.Checked = _QSO.Card_Send;
			chkCard_Resv.Checked = _QSO.Card_Resv;
			chkExcept.Checked = _QSO.Except;
		}

		/// <summary>
		/// 入力されたPrefixからDXCCエンティティの候補を出力する
		/// </summary>
		/// <param name="Prefix">コールサイン(プリフィックス)</param>
		/// <returns>DXCCエンティティの候補</returns>
		private Dictionary<string, string> GetDXCC(string Prefix) {
			Dictionary<string, string> dcDXCCcand = new Dictionary<string, string>();
			foreach(cDxcc dx in _dcDXCC.Values) {
				foreach(string sPt in dx.Patterns) {
					if (Regex.IsMatch(dx.Prefix, @"^\*")) { continue; } //消滅エンティティは対象外
					if (Regex.IsMatch(Prefix, "^" + sPt)) { 
						if (!dcDXCCcand.ContainsKey(dx.Prefix)) { dcDXCCcand.Add(dx.Prefix, dx.Name); }
					}
				}
			}
			return (dcDXCCcand);
		}

		//時刻更新
		private void cmdDate_Click(object sender, EventArgs e) {
			DateTime dtU = DateTime.UtcNow;
			if (txtTimeZone.Text != "") {
				int iZ;
				if (txtTimeZone.Text.ToUpper() == "JST") { iZ = 9; }
				else if (txtTimeZone.Text.ToUpper() == "UTC") { iZ = 0; }
				else { int.TryParse(txtTimeZone.Text, out iZ); }
				txtDate_S.Text = string.Format("{0:yyyy/MM/dd}", dtU.AddHours(iZ));
				txtTime_S.Text = string.Format("{0:HH}:{0:mm}", dtU.AddHours(iZ));
			}

			else {
				TimeSpan ts = DateTime.Now - dtU;
				if ((int)ts.TotalHours == 9) { txtTimeZone.Text = "JST"; }
				else if ((int)ts.TotalHours == 0) { txtTimeZone.Text = "UTC"; }
				else { txtTimeZone.Text = ts.TotalHours.ToString("+#;-#;"); }
				txtDate_S.Text = string.Format("{0:yyyy/MM/dd}", dtU.AddHours(ts.TotalHours));
				txtTime_S.Text = string.Format("{0:HH}:{0:mm}", dtU.AddHours(ts.TotalHours));
			}
		}

		#region "Formに入力された文字列のチェック"

		//日付Format
		private void txtDate_S_Leave(object sender, EventArgs e) {
			string s = txtDate_S.Text;
			s.Replace('-', '/');
			if (Regex.IsMatch(s, @"\d{8}")) { s = string.Format("{0}/{1}/{2}", s.Substring(0, 4), s.Substring(4, 2), s.Substring(6, 2)); }
			txtDate_S.Text = s;

			DateTime dt;
			if (!DateTime.TryParse(txtDate_S.Text + " 00:00", out dt)) { ErrMsg("Invalid date formatting.\n日付の書式が不正です。"); txtDate_S.Text = string.Format("{0:yyyy/MM/dd}", DateTime.Now); }
		}

		//時刻Format
		private void txtTime_S_Leave(object sender, EventArgs e) {
			string s = txtTime_S.Text;
			if (Regex.IsMatch(s, @"\d{4}")) { s = string.Format("{0}:{1}", s.Substring(0, 2), s.Substring(2, 2)); }
			txtTime_S.Text = s;

			DateTime dt;
			if (!DateTime.TryParse(txtDate_S.Text + " " + txtTime_S.Text, out dt)) { ErrMsg("Invalid time formatting.\n時刻の書式が不正です。"); txtTime_S.Text = string.Format("{0:hh}:{0:mm}", DateTime.Now); }
		}

		//タイムゾーンFormat
		private void txtTimeZone_Leave(object sender, EventArgs e) {
			int iZ;
			if(txtTimeZone.Text.ToUpper() == "JST") { iZ = 9; }
			else if (txtTimeZone.Text.ToUpper() == "UTC") { iZ = 0; }
			else if (int.TryParse(txtTimeZone.Text, out iZ)) {
				txtTimeZone.Text = iZ.ToString("+#;-#;");
			}
			else {
				ErrMsg("Invalid timezone formatting.\nタイムゾーンの書式が不正です(JST:+9、UTC:0)。");
				iZ = 9;
			}
			_QSO.TimeZone = iZ;
			
		}

		//コールサイン
		private void txtPrefix1_Leave(object sender, EventArgs e) {
			txtPrefix1.Text = txtPrefix1.Text.ToUpper();
			if (!Regex.IsMatch(txtPrefix1.Text, @"[A-Z0-1]*")) { WarnMsg("Check the call sign format.\nコールサインの書式を確認してください。\n" + txtPrefix1.Text); }
		}
		private void txtCall_Leave(object sender, EventArgs e) {
			txtCall.Text = txtCall.Text.ToUpper();
			if(txtCall.Text == "") { return; }

			//コールサインの書式
			if (!Regex.IsMatch(txtCall.Text, @"[A-Z0-1]*")) {
				WarnMsg("Check the call sign format.\nコールサインの書式を確認してください。\n" + txtCall.Text); return;
			}

			#region "DXCC"
			{
				string sPrefix = txtCall.Text;
				if (1 < txtPrefix1.Text.Length) { sPrefix = txtPrefix1.Text; }
				if (1 < txtPrefix2.Text.Length) { sPrefix = txtPrefix2.Text; }
				Dictionary<string, string> dcDx = GetDXCC(sPrefix);

				if (dcDx.Count == 1) {
					var kv = dcDx.FirstOrDefault();
					txtDXCC.Text = kv.Key;
					if(txtQTH.Text == "" && kv.Key != "JA") { txtQTH.Text = kv.Value; }
				}
			}
			#endregion

			if(txtDXCC.Text != "JA" && txtDXCC.Text != "" && txtTimeZone.Text == "JST") {
				string sDT = txtDate_S.Text + " " + txtTime_S.Text;
				DateTime dt;
				if(DateTime.TryParse(sDT, out dt)) {
					dt = dt.AddHours(-9d);
					txtTimeZone.Text = "UTC";
					txtDate_S.Text = string.Format("{0:yyyy/MM/dd}", dt);
					txtTime_S.Text = string.Format("{0:HH:mm}", dt);
				}
			}

			string sQRA = ""; //名前(Hitした中で一番長い名前を取得する→コンテスト等を考慮)
			#region "過去のQSO検索"
			dgvPastQSO.SuspendLayout(); //描画を止める
			if (0 < _blPastQSO.Count) { _blPastQSO.Clear(); }

			List<cQSO> lsPast = new List<cQSO>(); //過去QSO一時置き場(ソート可能に)
			foreach (cQSO q in _blAllQSO) {
				if (txtCall.Text == q.Call) {
					if (sQRA.Length < q.QRA.Length) { sQRA = q.QRA; }
					lsPast.Add(q);
				}
			}
			txtQRA.Text = sQRA;

			//ソート
			lsPast.Sort((a, b) => a.Date_S.CompareTo(b.Date_S));
			foreach (cQSO q in lsPast) { _blPastQSO.Add(q); }
			if (0 < _blPastQSO.Count) {
				dgvPastQSO.FirstDisplayedCell = dgvPastQSO[0, _blPastQSO.Count - 1];
				cQSO ql = _blPastQSO[_blPastQSO.Count - 1];
				lblLastQSO.Text = string.Format("Last QSO: {0:yyyy/MM/dd} {1}({2})",
					DateTime.FromBinary(ql.Date_S).AddHours(ql.TimeZone), ql.Band, ql.Mode);
			}
			else { lblLastQSO.Text = "Last QSO: N/A (1st QSO)"; }
			dgvPastQSO.ResumeLayout();
			#endregion
		}
		private void txtPrefix2_Leave(object sender, EventArgs e) {
			txtPrefix2.Text = txtPrefix2.Text.ToUpper();
			if (!Regex.IsMatch(txtPrefix2.Text, @"[A-Z0-1]*")) { WarnMsg("Check the call sign format.\nコールサインの書式を確認してください。\n" + txtPrefix2.Text); }
		}

		//RS
		private void txtRS_His_Leave(object sender, EventArgs e) {
			int i;
			if (!int.TryParse(txtRS_His.Text, out i)) { WarnMsg("Check the RS format.\nRSレポートの書式を確認してください。\n" + txtRS_His.Text); }
		}
		private void txtRS_My_Leave(object sender, EventArgs e) {
			int i;
			if (!int.TryParse(txtRS_My.Text, out i)) { WarnMsg("Check the RS format.\nRSレポートの書式を確認してください。\n" + txtRS_My.Text); }

		}

		//周波数
		private void txtFreq_Leave(object sender, EventArgs e) {
			double dFreq;
			if (!double.TryParse(txtFreq.Text, out dFreq)) { ErrMsg("Invalid frequency formatting.\n周波数の書式が不正です。\n" + txtFreq.Text); return; }

			bool bFlg = false;
			foreach (string sB in _dcBand.Keys) { if (_dcBand[sB].Lower <= dFreq && dFreq <= _dcBand[sB].Upper) { lblBand.Text = sB;  bFlg=true; } }
			if (!bFlg) { WarnMsg("The input frequency is out of range.\n入力された周波数が範囲外です。\n" + txtFreq.Text + "[MHz]"); lblBand.Text = "N/A"; }
			

			//RIG入力
			if(_cfg.UseDefaultRig && _dcDefault.ContainsKey(lblBand.Text)){
				if (txtPrefix_My.Text == "") { txtRig_My.Text = _dcDefault[lblBand.Text].RigHome; txtAnt_My.Text = _dcDefault[lblBand.Text].AntHome; }
				else { txtRig_My.Text = _dcDefault[lblBand.Text].RigMobile; txtAnt_My.Text = _dcDefault[lblBand.Text].AntMobile; }
			}

		}

		//出力
		private void txtPwr_My_Leave(object sender, EventArgs e) {
			if (txtPwr_My.Text == "") { return; }
			double dPwr;
			if (!double.TryParse(txtPwr_My.Text, out dPwr)) { ErrMsg("Invalid power formatting.\n出力の書式が不正です。\n" + txtPwr_My.Text); return; }
		}

		//モード
		private void txtMode_Leave(object sender, EventArgs e) {
			if (!_dcMode.ContainsKey(txtMode.Text)) {
				frmModeSelect fm = new frmModeSelect(_dcMode, txtMode);
				fm.ShowDialog();
			}
		}


		private void txtPwr_His_Leave(object sender, EventArgs e) {
			if (txtPwr_His.Text == "") { return; }
			double dPwr;
			if (!double.TryParse(txtPwr_His.Text, out dPwr)) { ErrMsg("Invalid power formatting.\n出力の書式が不正です。\n" + txtPwr_His.Text); return; }
		}

		//終了時刻
		private void txtTime_E_Leave(object sender, EventArgs e) {
			string s = txtTime_E.Text;
			if (Regex.IsMatch(s, @"\d{4}")) { s = string.Format("{0}:{1}", s.Substring(0, 2), s.Substring(2, 2)); }
			txtTime_E.Text = s;

			DateTime dt;
			if (txtTime_E.Text != "" && !DateTime.TryParse(txtDate_S.Text + " " + txtTime_S.Text, out dt)) { ErrMsg("Invalid time formatting.\n時刻の書式が不正です。"); txtTime_S.Text = string.Format("{0:hh}:{0:mm}", DateTime.Now); }
		}

		#endregion

		#region"過去QSOのQTHをToolTipに表示する"
		private void dgvPastQSO_SelectionChanged(object sender, EventArgs e) {
			//ShowTooltip();
		}

		private void dgvPastQSO_CellClick(object sender, DataGridViewCellEventArgs e) {
			ShowTooltip();
		}

		private void ShowTooltip() {
			try {
				if (dgvPastQSO.SelectedRows == null) { return; }
				if (dgvPastQSO.SelectedRows.Count == 0) { return; }
				cQSO qso = dgvPastQSO.SelectedRows[0].DataBoundItem as cQSO;
				if (qso == null) { return; }
				ToolTip tlp = new ToolTip();
				tlp.InitialDelay = 0;
				tlp.AutoPopDelay = 10000;
				string sQTH = string.Format("His:{0}\nMy :{1}", qso.QTH, qso.QTH_My);
				tlp.SetToolTip(dgvPastQSO, sQTH);
			}
			catch (Exception ex) {
				ErrMsg("Tooltip(Past QSO)\n" + ex.Message);
			}

		}
		#endregion

		/// <summary>
		/// 内容反映
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdRegist_Click(object sender, EventArgs e) {
			RegistQSO();
		}
		private void RegistQSO() {
			int iTmp; double dTmp;
			#region "交信開始時刻"
			{
				int iTz;
				if (txtTimeZone.Text.ToUpper() == "JST") { iTz = 9; }
				else if (txtTimeZone.Text.ToUpper() == "UTC") { iTz = 0; }
				else if (!int.TryParse(txtTimeZone.Text, out iTz)) {
					//DateTime dtU = DateTime.UtcNow;
					//TimeSpan ts = DateTime.Now - dtU;
					ErrMsg("Invalid timezone formatting.\nタイムゾーンの書式が不正です(JST:+9、UTC:0)。");
				}

				DateTime dtS;
				if (!DateTime.TryParse(txtDate_S.Text + " " + txtTime_S.Text, out dtS)) { ErrMsg("Invalid date formatting.\n日付の書式が不正です。"); return; }
				dtS = dtS.AddHours(-iTz);
				_QSO.Date_S = dtS.Ticks;
			}
			#endregion
			_QSO.Prefix1 = txtPrefix1.Text;
			_QSO.Prefix2 = txtPrefix2.Text;
			if (txtCall.Text == "") { ErrMsg("Error:The callsign is blank.\nコールサインが空白です。"); return; }
			else { _QSO.Call = txtCall.Text; }

			_QSO.QRA = txtQRA.Text;
			_QSO.DXCC = txtDXCC.Text;
			_QSO.CityCode = txtCityCode.Text;
			_QSO.GL = txtGL.Text;
			_QSO.QTH = txtQTH.Text;
			_QSO.QTH_h = txtQTH_h.Text;
			if (int.TryParse(txtRS_His.Text, out iTmp)) { _QSO.RS_His = iTmp; } else { ErrMsg("Error: RS_His"); return; }
			if (int.TryParse(txtRS_My.Text, out iTmp)) { _QSO.RS_My = iTmp; } else { ErrMsg("Error: RS_My"); return; }
			if (double.TryParse(txtFreq.Text, out dTmp)) { _QSO.SetFreq(dTmp, _dcBand); } else { ErrMsg("Error: Freq"); return; }
			_QSO.Mode = txtMode.Text;
			if (double.TryParse(txtPwr_My.Text, out dTmp)) { _QSO.Pwr_My = dTmp; } else { ErrMsg("Error: Pwr_My"); return; }
			#region "交信終了時刻"
			{
				if (txtTime_E.Text == "") { _QSO.Date_E = -1; }
				else {
					DateTime dtTmp;
					if (DateTime.TryParse(txtDate_S.Text + " " + txtTime_E.Text, out dtTmp)) {
						_QSO.Date_E = dtTmp.AddHours(-_QSO.TimeZone).Ticks;
						if (_QSO.Date_E < _QSO.Date_S) { _QSO.Date_E = dtTmp.AddHours(24 - _QSO.TimeZone).Ticks; } //零時またぎのQSO
					}
					else { ErrMsg("Error: Date_E or Time_E"); return; }
				}
			}
			#endregion
			_QSO.QSLManager = txtQSLManager.Text;
			_QSO.Rig_His = txtRig_His.Text;
			_QSO.Ant_His = txtAnt_His.Text;
			#region "相手局出力"
			if (txtPwr_His.Text == "") { _QSO.Pwr_His = -1d; }
			else {
				if (double.TryParse(txtPwr_His.Text, out dTmp)) { _QSO.Pwr_His = dTmp; } else { ErrMsg("Error: Pwr_His"); return; }
			}
			#endregion
			_QSO.Rig_My = txtRig_My.Text;
			_QSO.Ant_My = txtAnt_My.Text;
			_QSO.QTH_My = txtQTH_My.Text;
			_QSO.Prefix_My = txtPrefix_My.Text;
			_QSO.CityCode_My = txtCityCode_My.Text;
			_QSO.GL_My = txtGL_My.Text;
			_QSO.CardMsg = txtCardMsg.Text;
			_QSO.Remarks = txtRemark.Text;
			_QSO.Card_Send = chkCard_Send.Checked;
			_QSO.Card_Resv = chkCard_Resv.Checked;
			_QSO.Except = chkExcept.Checked;

			_QSO.LastUpdate = DateTime.UtcNow.Ticks;
			#region "QSLカード交換方法"
			if (rdoBureau.Checked) { _QSO.QSLMethod = (int)cQSO.enQSLMethod.B; }
			else if (rdoDirect.Checked) { _QSO.QSLMethod = (int)cQSO.enQSLMethod.D; }
			else if (rdoManager.Checked) { _QSO.QSLMethod = (int)cQSO.enQSLMethod.M; }
			else if (rdoNone.Checked) { _QSO.QSLMethod = (int)cQSO.enQSLMethod.N; }
			else if (rdo1way.Checked) { _QSO.QSLMethod = (int)cQSO.enQSLMethod.R; }
			else { _QSO.QSLMethod = (int)cQSO.enQSLMethod.X; }
			#endregion

			//新規QSO→更新履歴に追加する
			if (!_blAllQSO.Contains(_QSO) && _QSO.Call != "") { _blAllQSO.Add(_QSO); }

			//QSOを新規に
			_QSO = new cQSO(_QSO);
			RefleshForm();
		}

		#region "コマンドボタン"
		private void cmdCancel_Click(object sender, EventArgs e) {
			_QSO = new cQSO(_QSO);
			_blPastQSO.Clear();
			lblLastQSO.Text = "LastQSO: N/A";
			RefleshForm();
		}

		private void frmQSO_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}

		private void cmdMode_Click(object sender, EventArgs e) {
			frmModeSelect fm = new frmModeSelect(_dcMode, txtMode);
			fm.ShowDialog();
		}

		private void cmdDxcc_Click(object sender, EventArgs e) {
			string sPrefix = txtCall.Text;
			if (1 < txtPrefix1.Text.Length) { sPrefix = txtPrefix1.Text; }
			if (1 < txtPrefix2.Text.Length) { sPrefix = txtPrefix2.Text; }

			frmSearchDXCC f = new frmSearchDXCC(sPrefix, _dcDXCC, txtDXCC, txtQTH);
			f.ShowDialog();
		}

		private void cmdQTH_Click(object sender, EventArgs e) {
			if (txtDXCC.Text == _cfg.MyEntity) {
				string sA = "";
				if (3 <= txtCall.Text.Length) { sA = txtCall.Text.Substring(2, 1); }
				if (Regex.IsMatch(txtCall.Text, "^7[K-N]")) { sA = "1"; }
				if (txtPrefix2.Text != "") { sA = txtPrefix2.Text; }
				frmSearchCityCode f = new frmSearchCityCode(txtQTH.Text, sA, _dcCityCode, txtCityCode, txtQTH);
				f.ShowDialog();
			}
			else {
				string sPrefix = txtCall.Text;
				if (1 < txtPrefix1.Text.Length) { sPrefix = txtPrefix1.Text; }
				if (1 < txtPrefix2.Text.Length) { sPrefix = txtPrefix2.Text; }
				frmSearchDXCC f = new frmSearchDXCC(sPrefix, _dcDXCC, txtDXCC, txtQTH);
				f.ShowDialog();
			}
		}

		private void cmdQTH_h_Click(object sender, EventArgs e) {
			string sA = "";
			if (3 <= txtCall.Text.Length) { sA = txtCall.Text.Substring(2, 1); }
			if (Regex.IsMatch(txtCall.Text, "^7[K-N]")) { sA = "1"; }
			frmSearchCityCode f = new frmSearchCityCode(txtQTH_h.Text, sA, _dcCityCode, null, txtQTH_h);
			f.ShowDialog();
		}

		private void cmdCityCode_Click(object sender, EventArgs e) {
			cmdQTH_Click(sender, e);
		}

		private void cmdQTH_My_Click(object sender, EventArgs e) {
			frmSearchCityCode f = new frmSearchCityCode(txtQTH_My.Text, txtPrefix_My.Text, _dcCityCode, txtCityCode_My, txtQTH_My);
			f.ShowDialog();
		}

		private void cmdCityCode_My_Click(object sender, EventArgs e) {
			cmdQTH_My_Click(sender, e);
		}
		#endregion

		//Enterを押したときのビープ音を抑止する
		//https://dobon.net/vb/dotnet/control/tbsuppressbeep.html
		[System.Security.Permissions.UIPermission(System.Security.Permissions.SecurityAction.Demand, Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData) {
			if (keyData == Keys.Enter) { return (true); }
			else { return (base.ProcessDialogKey(keyData)); }
		}

		#region "KeyUP Shift+Enger→登録、Enter→次のフォーカス"
		private void txtDate_S_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtTime_S_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtTimeZone_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtPrefix1_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtCall_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtPrefix2_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }

		}

		private void txtRS_His_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtRS_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtMode_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtFreq_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtPwr_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtQTH_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode != Keys.Enter) { return; }
			else if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (txtDXCC.Text == _cfg.MyEntity && Regex.IsMatch(txtQTH.Text, @"^\p{IsHiragana}+$")) {
				string sA = "";
				if (3 <= txtCall.Text.Length) { sA = txtCall.Text.Substring(2, 1); }
				if (Regex.IsMatch(txtCall.Text, "^7[K-N]")) { sA = "1"; }
				if (txtPrefix2.Text != "") { sA = txtPrefix2.Text; }
				frmSearchCityCode f = new frmSearchCityCode(txtQTH.Text, sA, _dcCityCode, txtCityCode, txtQTH);
				f.ShowDialog();
			}
		}

		private void txtQTH_h_KeyUp(object sender, KeyEventArgs e) {
			if(e.KeyCode != Keys.Enter) { return; }
			else if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (txtDXCC.Text == _cfg.MyEntity && Regex.IsMatch(txtQTH_h.Text, @"^\p{IsHiragana}+$")) {
				string sA = "";
				if (3 <= txtCall.Text.Length) { sA = txtCall.Text.Substring(2, 1); }
				if (Regex.IsMatch(txtCall.Text, "^7[K-N]")) { sA = "1"; }
				frmSearchCityCode f = new frmSearchCityCode(txtQTH_h.Text, sA, _dcCityCode, null, txtQTH_h);
				f.ShowDialog();
			}
		}

		private void txtDXCC_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtDcode_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtGL_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtPwr_His_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtQRA_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
		}

		private void txtRig_His_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
		}

		private void txtAnt_His_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
		}

		private void txtQSLManager_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtPrefix_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtQTH_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode != Keys.Enter) { return; }
			else if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (Regex.IsMatch(txtQTH_My.Text, @"^\p{IsHiragana}+$")) {
				string sA = "";
				if (3 <= _cfg.MyCall.Length) { sA = _cfg.MyCall.Substring(2, 1); }
				if (Regex.IsMatch(_cfg.MyCall, "^7[K-N]")) { sA = "1"; }
				if (txtPrefix_My.Text != "") { sA = txtPrefix_My.Text; }
				frmSearchCityCode f = new frmSearchCityCode(txtQTH_My.Text, sA, _dcCityCode, txtCityCode_My, txtQTH_My);
				f.ShowDialog();
			}
		}

		private void txtDcode_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtRig_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
		}

		private void txtAnt_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
		}

		private void txtGL_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtCardMsg_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
		}

		private void txtRemark_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
		}

		private void txtTime_E_KeyUp(object sender, KeyEventArgs e) {
			if (e.Shift && e.KeyCode == Keys.Enter) { RegistQSO(); }
			else if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}
		#endregion

	}

}
