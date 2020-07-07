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
	public partial class frmInportLogcs : Form {
		string[] _sColN;
		int[] _iColW;
		BindingList<cQSO> _blDest;
		BindingList<cQSO> _blPreview;
		BindingSource _bs;
		Dictionary<string, cBand> _dcBand;

		public frmInportLogcs(BindingList<cQSO> Dest, int[] HeaderWidths, string[] HeaderTexts, Dictionary<string, cBand> Band) {
			InitializeComponent();
			_blDest = Dest;
			_sColN = HeaderTexts;
			_iColW = HeaderWidths;
			_blPreview = new BindingList<cQSO>();
			_bs = new BindingSource();
			_bs.DataSource = _blPreview;
			dgvInport.DataSource = _bs;
			_dcBand = Band;
		}

		private void frmInportLogcs_Load(object sender, EventArgs e) {
			dgvInport.SuspendLayout();
			for (int i = 0; i < dgvInport.Columns.Count; i++) {
				dgvInport.Columns[i].HeaderText = _sColN[i];
				if (_iColW[i] < 0) { dgvInport.Columns[i].Visible = false; }
				else { dgvInport.Columns[i].Width = _iColW[i]; }
			}
			dgvInport.ResumeLayout();

		}

		private void cmdSetInCSV_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			if (Directory.Exists(txtSetInCSV.Text)) { ofd.InitialDirectory = txtSetInCSV.Text; }
			ofd.Filter = "Logcs CSV file(*.csv)|*.csv|All files(*.*)|*.*";
			if (ofd.ShowDialog() != DialogResult.OK) { return; }
			txtSetInCSV.Text = ofd.FileName;

			List<string> lsErr = new List<string>();

			dgvInport.SuspendLayout();
			using (StreamReader sr = new StreamReader(txtSetInCSV.Text, Encoding.GetEncoding(932))) {
				int iL = 0;
				while (-1 < sr.Peek()) {
					iL++;
					string[] sL = sr.ReadLine().Split(',');

					List<string> lsL = new List<string>();
					#region "カンマ区切り対策"
					lsL.AddRange(sL);

					// 項目分繰り返す
					for (int i = 0; i < lsL.Count; ++i) {
						//先頭のスペースを除去して、ダブルクォーテーションが入っていないか判定する
						if (lsL[i] != string.Empty && lsL[i].TrimStart()[0] == '"') {
							// もう一回ダブルクォーテーションが出てくるまで要素を結合
							while (lsL[i].TrimEnd()[lsL[i].TrimEnd().Length - 1] != '"') {
								lsL[i] = lsL[i] + "," + lsL[i + 1];

								//結合したら要素を削除する
								lsL.RemoveAt(i + 1);
							}
						}
					}
					for (int i = 0; i < lsL.Count; i++) { lsL[i] = lsL[i].Trim().Replace("\"", ""); }
					#endregion

					cQSO q = new cQSO();

					int iV;
					double dV;
					q.Prefix1 = lsL[0];
					q.Call = lsL[1];
					q.Prefix2 = lsL[2];
					q.CityCode = lsL[3];
					q.QTH = lsL[4];
					q.QTH_h = lsL[6];
					q.QRA = lsL[7];
					if (int.TryParse(lsL[8], out iV)) { q.RS_His = iV; } else { lsErr.Add(string.Format("{0} RS_His={1}",iL, lsL[8])); }
					if (int.TryParse(lsL[9], out iV)) { q.RS_My = iV; } else { lsErr.Add(string.Format("{0} RS_My={1}", iL, lsL[9])); }
					if (double.TryParse(lsL[10], out dV)) { q.SetFreq(dV, _dcBand); }
					q.Mode = lsL[11];
					if (double.TryParse(lsL[12], out dV)) { q.Pwr_My = dV; } else { q.Pwr_My = -1d; }
					
					DateTime dtS;
					if(lsL[16] == "J") { q.TimeZone = 9; } else if(lsL[16] == "U") { q.TimeZone = 0; } else { lsErr.Add(string.Format("{0} TimeZone={1}", iL, lsL[16])); }
					if (!DateTime.TryParse(string.Format("{0} {1}", lsL[13].Replace("-", "/"), lsL[14]), out dtS)) { lsErr.Add(string.Format("{0} Day={1} Time={2}", iL, lsL[13], lsL[14])); }
					else { q.Date_S = dtS.AddHours(-q.TimeZone).Ticks; }

					q.QTH_My = lsL[21];
					q.Prefix_My = lsL[22];
					if(Regex.IsMatch(lsL[23], @"^\d{4}")) { q.DXCC = "JA"; q.CityCode_My = lsL[23]; }
					else { q.DXCC = lsL[23]; }
					q.GL_My = lsL[24];
					q.Rig_My = lsL[25];
					q.Ant_My = lsL[26];

					#region "カード交換方式"
					q.QSLMethod = (int)cQSO.enQSLMethod.N;
					if(lsL[29] == "J") { q.QSLMethod = (int)cQSO.enQSLMethod.B; }
					else if(lsL[29] == "D") { q.QSLMethod = (int)cQSO.enQSLMethod.D; }
					else if (lsL[29] == "M") { q.QSLMethod = (int)cQSO.enQSLMethod.M; }
					#endregion
					if (lsL[30] == "*") { q.Card_Send = true; } else { q.Card_Send = false; }
					if (lsL[31] == "*") { q.Card_Resv = true; } else { q.Card_Resv = false; }

					_blPreview.Add(q);

				}
				dgvInport.ResumeLayout();
			}
			MessageBox.Show("プレビューされた交信リストを確認のうえ、問題が無ければ登録を押してください。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void cmdInport_Click(object sender, EventArgs e) {
			foreach (cQSO q in _blPreview) { _blDest.Add(q); }
			Close();
		}

		private void frmInportLogcs_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}
	}
}
