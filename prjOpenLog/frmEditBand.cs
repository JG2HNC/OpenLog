using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace prjOpenLog {
	public partial class frmEditBand : Form {
		string _sDB;
		Dictionary<string, cBand> _dcBandP;
		BindingList<cBand> _blBand;
		BindingSource _bs;


		public frmEditBand(Dictionary<string, cBand> Bands, string sDB) {
			InitializeComponent();
			_sDB = sDB;
			_dcBandP = Bands;
			_blBand = new BindingList<cBand>();
			foreach(cBand bd in Bands.Values) { _blBand.Add(bd); }
			_bs = new BindingSource();
		}

		private void frmEditBand_Load(object sender, EventArgs e) {
			_bs.DataSource = _blBand;
			dgvBand.DataSource = _bs;
		}

		private void txtBandF_Leave(object sender, EventArgs e) {
			if (dgvBand.SelectedRows == null) { return; }
			if (dgvBand.SelectedRows.Count < 1) { return; }
			cBand bd = dgvBand.SelectedRows[0].DataBoundItem as cBand;
			if (bd == null) { return; }

			//重複チェック
			if (txtBandF.Text != bd.NameF) {
				foreach(cBand b in _blBand) {
					if(txtBandF.Text == b.NameF) { ErrMsg("入力された周波数帯名はすでに存在します(重複不可)。\n入力値:" + txtBandF.Text); return; }
				}
				bd.NameF = txtBandF.Text;
			}
		}

		private void txtBandL_Leave(object sender, EventArgs e) {
			if (dgvBand.SelectedRows == null) { return; }
			if (dgvBand.SelectedRows.Count < 1) { return; }
			cBand bd = dgvBand.SelectedRows[0].DataBoundItem as cBand;
			if (bd == null) { return; }
			bd.NameL = txtBandL.Text;
		}

		private void txtUpper_Leave(object sender, EventArgs e) {
			if (dgvBand.SelectedRows == null) { return; }
			if (dgvBand.SelectedRows.Count < 1) { return; }
			cBand bd = dgvBand.SelectedRows[0].DataBoundItem as cBand;
			if (bd == null) { return; }
			double dU;
			if(!double.TryParse(txtUpper.Text, out dU)) { ErrMsg("上限周波数の書式が不正です。\n入力値:" + txtUpper.Text); return; }
			bd.Upper = dU;
		}

		private void txtLower_Leave(object sender, EventArgs e) {
			if (dgvBand.SelectedRows == null) { return; }
			if (dgvBand.SelectedRows.Count < 1) { return; }
			cBand bd = dgvBand.SelectedRows[0].DataBoundItem as cBand;
			if (bd == null) { return; }
			double dL;
			if (!double.TryParse(txtLower.Text, out dL)) { ErrMsg("下限周波数の書式が不正です。\n入力値:" + txtLower.Text); return; }
			bd.Lower = dL;
		}

		private void cmdCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void CmdOK_Click(object sender, EventArgs e) {
			try {

				Dictionary<string, cBand> dcBand = new Dictionary<string, cBand>();
				#region "重複チェック&Dictionary更新"
				foreach(cBand bd in _blBand) {
					if (dcBand.ContainsKey(bd.NameF)) { ErrMsg("周波数帯名はすでに存在します(重複不可)。\n周波数帯名:" + bd.NameF); return; }
					dcBand.Add(bd.NameF, bd);
				}
				#endregion

				_dcBandP.Clear();
				using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", _sDB))) {
					con.Open();
					using (SQLiteTransaction st = con.BeginTransaction())
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText ="delete from [T_Band]";
						cmd.ExecuteNonQuery();

						var bds = dcBand.OrderBy((x) => x.Value.Lower);
						foreach (KeyValuePair<string,cBand> kvp in bds) {
							cmd.CommandText = string.Format("INSERT INTO [T_Band]([BandF],[BandL],[Lower],[Upper]) VALUES('{0}','{1}','{2}','{3}');", kvp.Key, kvp.Value.NameL, kvp.Value.Lower, kvp.Value.Upper);
							_dcBandP.Add(kvp.Key, kvp.Value);
							int iRes = cmd.ExecuteNonQuery();
						}
						st.Commit();
					}
					con.Close();
				}
				//DBを開放させるおまじない
				GC.Collect();
				GC.WaitForPendingFinalizers();

				Close();
			}
			catch (Exception ex) {
				ErrMsg(ex.Message);
			}
		}


		private void ErrMsg(string Message) {
			MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void dgvBand_SelectionChanged(object sender, EventArgs e) {
			if (dgvBand.SelectedRows == null) { return; }
			if (dgvBand.SelectedRows.Count < 1) { return; }
			cBand bd = dgvBand.SelectedRows[0].DataBoundItem as cBand;
			if (bd == null) { return; }
			txtBandF.Text = bd.NameF;
			txtBandL.Text = bd.NameL;
			txtLower.Text = bd.Lower.ToString();
			txtUpper.Text = bd.Upper.ToString();
		}

		private void cmdAddNew_Click(object sender, EventArgs e) {
			if(MessageBox.Show("新規周波数帯を追加しますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) { return; }
			_blBand.Add(new cBand("New MHz", "New m", 0d, 0d));
			dgvBand.CurrentCell = dgvBand.Rows[_blBand.Count - 1].Cells[0];
		}

		private void frmEditBand_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}
	}
}
