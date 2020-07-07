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
using System.Text.RegularExpressions;

namespace prjOpenLog {
	public partial class frmInport : Form {
		private BindingList<cQSO> _blDest;
		private BindingList<cQSO> _blPreview;
		private BindingSource _bs;
		private Dictionary<string, cDxcc> _dcDXCC;
		private Dictionary<string, cCity> _dcJCCG;
		private Dictionary<string, cBand> _dcBand;
		private Dictionary<string, cMode> _dcMode;
		private Dictionary<string, cDefaultRig> _dcDefault;
		private List<string[]> _lsDXCCpats = new List<string[]>();
		private Config _cfg;
		private int[] _iColWidth;
		private string[] _sColName;

		public frmInport(BindingList<cQSO> Dest, Dictionary<string, cDxcc> DXCC, Dictionary<string, cCity> JCCG, Dictionary<string, cBand> Bands, Dictionary<string, cMode> Modes, Dictionary<string, cDefaultRig> DefaultRig,int[] ColWidths, string[] ColNames, Config Config) {
			_blDest = Dest; _dcDXCC = DXCC; _dcJCCG = JCCG; _dcBand = Bands; _dcMode = Modes; _dcDefault = DefaultRig; _cfg = Config;
			_iColWidth = ColWidths; _sColName = ColNames;
			_bs = new BindingSource();
			_blPreview = new BindingList<cQSO>();

			InitializeComponent();
		}

		private void frmInport_Load(object sender, EventArgs e) {
			_bs.DataSource = _blPreview;
			dgvInport.DataSource = _bs;

			#region "DataGridView制御"
			dgvInport.AllowUserToAddRows = false;
			dgvInport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvInport.Columns["Prefix1"].Visible = false;
			dgvInport.Columns["Prefix2"].Visible = false;
			dgvInport.Columns["Call"].Visible = false;
			dgvInport.Columns["Date_S"].Visible = false;
			dgvInport.Columns["Date_E"].Visible = false;
			dgvInport.Columns["TimeZone"].Visible = false;
			dgvInport.Columns["Pwr_My"].Visible = false;
			dgvInport.Columns["Pwr_His"].Visible = false;
			dgvInport.Columns["QSLMethod"].Visible = false;
			dgvInport.Columns["Card_Send"].Visible = false;
			dgvInport.Columns["Card_Resv"].Visible = false;
			dgvInport.Columns["Except"].Visible = false;
			dgvInport.Columns["LastUpdate"].Visible = false;
			for (int i = 0; i < _iColWidth.Length; i++) {
				dgvInport.Columns[i].Width = _iColWidth[i];
				dgvInport.Columns[i].HeaderText = _sColName[i];
			}
			#endregion

			//ファイルタイプ
			cboFileType.Items.Add(new cType("wsjt-x", "WSJT-X Logfile", "WSJT-X Logfile(*.log)|*.log"));
			cboFileType.SelectedIndex = 0;

			//QSL交換方法
			cboQSL.Items.Add(new cQSL((int)cQSO.enQSLMethod.B, "Bureau(JARL)"));
			cboQSL.Items.Add(new cQSL((int)cQSO.enQSLMethod.N, "No QSL"));
			cboQSL.Items.Add(new cQSL((int)cQSO.enQSLMethod.R, "1 way"));
			cboQSL.SelectedIndex = 1;
		}

		private void cmdQTH_My_Click(object sender, EventArgs e) {
			string sA = "";
			if (3 <= _cfg.MyCall.Length) { sA = _cfg.MyCall.Substring(2, 1); }
			if (Regex.IsMatch(_cfg.MyCall, "^7[K-N]")) { sA = "1"; }
			if (txtPrefix_My.Text != "") { sA = txtPrefix_My.Text; }
			frmSearchCityCode f = new frmSearchCityCode(txtQTH_My.Text, sA, _dcJCCG, txtDcode, txtQTH_My);
			f.ShowDialog();
		}

		//WSJT-XのLogプレビュー作成
		private void PreviewWSJT() {
			List<string> lsErr = new List<string>();
			int iDc = 0; //Dupeの件数
			int iL = 0;
			using(StreamReader sr = new StreamReader(txtSetInCSV.Text, Encoding.ASCII)) {
				while(-1 < sr.Peek()) {
					iL++;
					cQSO q = new cQSO();

					//自局情報
					if(txtPrefix_My.Text != "") { q.Prefix_My = txtPrefix_My.Text; }
					if (txtQTH_My.Text != "") { q.QTH_My = txtQTH_My.Text; }
					if (txtDcode.Text != "") { q.CityCode_My = txtDcode.Text; }
					if(txtGL.Text != "") { q.GL_My = txtGL.Text; }
					if(0 <= cboQSL.SelectedIndex) { cQSL qs = (cQSL)cboQSL.SelectedItem; q.QSLMethod = qs.QSLMethod; }

					//QSO情報
					string[] sL = sr.ReadLine().Split(',');
					DateTime dtS, dtE;
					int iV; double dV;
					if (!DateTime.TryParse(sL[0].Replace("-", "/") + " " + sL[1], out dtS)) { lsErr.Add(string.Format("Line:{0} 開始時刻の書式が不正", iL)); }
					if (!DateTime.TryParse(sL[2].Replace("-", "/") + " " + sL[3], out dtE)) { lsErr.Add(string.Format("Line:{0} 終了時刻の書式が不正", iL)); }
					q.Date_S = dtS.Ticks;
					q.Date_E = dtE.Ticks;
					q.TimeZone = 0;
					q.Call = sL[4];
					q.GL = sL[5];
					if(double.TryParse(sL[6], out dV)) { q.SetFreq(dV, _dcBand); }
					else { lsErr.Add(string.Format("Line:{0} 周波数の書式が不正", iL)); }
					q.Mode = sL[7];

					//デフォルト設定のリグ
					if(chkUseDefault.Checked && _dcDefault.ContainsKey(q.Band)) {
						if(q.Prefix_My == "") { q.Rig_My = _dcDefault[q.Band].RigHome; q.Ant_My = _dcDefault[q.Band].AntHome; }
						else { q.Rig_My = _dcDefault[q.Band].RigMobile; q.Ant_My = _dcDefault[q.Band].AntMobile; }
					}

					if (int.TryParse(sL[8], out iV)) { q.RS_His = iV; }
					else { lsErr.Add(string.Format("Line:{0} 相手RSの書式が不正", iL)); }

					if (int.TryParse(sL[9], out iV)) { q.RS_My = iV; }
					else { lsErr.Add(string.Format("Line:{0} 自局RSの書式が不正", iL)); }

					if ( double.TryParse(sL[10], out dV)) { q.Pwr_My = dV; }
					else { if (sL[10] != "") { lsErr.Add(string.Format("Line:{0} 出力の書式が不正", iL)); } }

					q.Remarks = sL[11];
					q.QRA = sL[12];

					//重複判別
					bool bDupe = false;
					foreach(cQSO pq in _blDest) {
						if (pq.Date_S == q.Date_S && pq.Call == pq.Call) { bDupe = true; iDc++; }
					}
					if(bDupe && chkExceptDupe.Checked) { continue; }

					#region "DXCC"
					{
						string sPrefix = q.Call;
						if (1 < q.Prefix1.Length) { sPrefix = q.Prefix1; }
						if (1 < q.Prefix2.Length) { sPrefix = q.Prefix2; }
						Dictionary<string, string> dcDx = GetDXCC(sPrefix);

						if (dcDx.Count == 1) {
							var kv = dcDx.FirstOrDefault();
							q.DXCC = kv.Key;
						}
					}
					#endregion

					_blPreview.Add(q);
				}
			}

			if(lsErr.Count != 0) {
				string sErr = string.Join("\n", lsErr);
				MessageBox.Show("エラーがありました。\n\n" + sErr, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			if(iDc != 0) {
				MessageBox.Show("重複件数: " + iDc.ToString(), "重複がありました", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void cmdSetInCSV_Click(object sender, EventArgs e) {
			cType tp = (cType)cboFileType.SelectedItem;


			OpenFileDialog ofd = new OpenFileDialog();
			if (Directory.Exists(txtSetInCSV.Text)) { ofd.InitialDirectory = txtSetInCSV.Text; }
			ofd.Filter = tp.Filter + "|All files(*.*)|*.*";
			if (ofd.ShowDialog() != DialogResult.OK) { return; }
			txtSetInCSV.Text = ofd.FileName;

			if(tp.ID == "wsjt-x") { PreviewWSJT(); }

		}

		private class cType {
			string _Screen, _Filter, _sID;
			public cType(string ID, string Screen, string Filter) {
				_sID = ID; _Screen = Screen; _Filter = Filter;
			}
			public string Screen { get { return (_Screen); } }
			public string Filter { get { return (_Filter); } }
			public string ID { get { return (_sID); } }
			public override string ToString() {
				return(Screen);
			}
		}

		private class cQSL {
			string _Screen;
			int _iM;
			public cQSL(int QSLMethod, string Screen) { _iM = QSLMethod; _Screen = Screen; }
			public string Screen { get { return (_Screen); } }
			public int QSLMethod { get { return (_iM); } }
			public override string ToString() {
				return (_Screen);
			}
		}

		#region "KeyUPイベント(Enterで次のコントロールへ)"
		private void cmdInport_Click(object sender, EventArgs e) {
			foreach (cQSO q in _blPreview) { _blDest.Add(q); }

			MessageBox.Show("インポートが完了しました。", "完了", MessageBoxButtons.OK);
			Close();
		}

		private void txtPrefix_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtQTH_My_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				string sA = "";
				if (3 <= _cfg.MyCall.Length) { sA = _cfg.MyCall.Substring(2, 1); }
				if (Regex.IsMatch(_cfg.MyCall, "^7[K-N]")) { sA = "1"; }
				if (txtPrefix_My.Text != "") { sA = txtPrefix_My.Text; }
				frmSearchCityCode f = new frmSearchCityCode(txtQTH_My.Text, sA, _dcJCCG, txtDcode, txtQTH_My);
				f.ShowDialog();
			}
		}

		private void txtDcode_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}

		private void txtGL_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) { ProcessTabKey(true); }
		}


		#endregion

		#region "Leaveイベント(入力チェック)"
		private void txtPrefix_My_Leave(object sender, EventArgs e) {
			int iA;
			if (txtPrefix_My.Text != "" && !int.TryParse(txtPrefix_My.Text, out iA)) { MessageBox.Show("エリアの書式が不正です(数値で入れてください)", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error); txtPrefix_My.Text = ""; }
		}
		#endregion

		/// <summary>
		/// 入力されたPrefixからDXCCエンティティの候補を出力する
		/// </summary>
		/// <param name="Prefix">コールサイン(プリフィックス)</param>
		/// <returns>DXCCエンティティの候補</returns>
		private Dictionary<string, string> GetDXCC(string Prefix) {
			Dictionary<string, string> dcDXCCcand = new Dictionary<string, string>();

			foreach (cDxcc dx in _dcDXCC.Values) {
				foreach (string sPt in dx.Patterns) {
					if (Regex.IsMatch(Prefix, "^" + sPt)) {
						if (!dcDXCCcand.ContainsKey(dx.Prefix)) { dcDXCCcand.Add(dx.Prefix, dx.Name); }
					}
				}
			}
			return (dcDXCCcand);
		}

		private void frmInport_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}
	}
}
