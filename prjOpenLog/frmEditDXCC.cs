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
	public partial class frmEditDXCC : Form {
		Dictionary<string, cDxcc> _dcDXCC;
		BindingList<cDxcc> _blDxcc;
		BindingSource _bs;
		string _sDB;

		public frmEditDXCC(Dictionary<string, cDxcc> DXCCs, string sDB) {
			InitializeComponent();
			_sDB = sDB;
			_blDxcc = new BindingList<cDxcc>();
			_bs = new BindingSource();
			_dcDXCC = DXCCs;
		}

		private void frmEditDXCC_Load(object sender, EventArgs e) {
			foreach (cDxcc dx in _dcDXCC.Values) {
				_blDxcc.Add(dx);
			}
			_bs.DataSource = _blDxcc;
			dgvDxcc.DataSource = _bs;
			dgvDxcc.Columns["PrefixSort"].Visible = false;
		}

		private void dgvDxcc_SelectionChanged(object sender, EventArgs e) {
			if (dgvDxcc.SelectedRows == null) { return; }
			if (dgvDxcc.SelectedRows.Count < 1) { return; }
			cDxcc dx = dgvDxcc.SelectedRows[0].DataBoundItem as cDxcc;
			if (dx == null) { return; }
			txtPrefix.Text = dx.Prefix;
			txtName.Text = dx.Name;
			txtPattern.Text = dx.Pattern;
			txtCode.Text = dx.Code.ToString();

		}

		private void cmdCreateNew_Click(object sender, EventArgs e) {
			cDxcc dxNew = new cDxcc("New", "NewEntity", "", -999);
			_blDxcc.Add(dxNew);
			dgvDxcc.CurrentCell = dgvDxcc.Rows[_blDxcc.Count - 1].Cells[0];
		}

		private void txtPrefix_Leave(object sender, EventArgs e) {
			if (dgvDxcc.SelectedRows == null) { return; }
			if (dgvDxcc.SelectedRows.Count < 1) { return; }
			cDxcc dx = dgvDxcc.SelectedRows[0].DataBoundItem as cDxcc;
			if (dx == null) { return; }
			if(txtPrefix.Text != dx.Prefix) {
				//重複チェック
				foreach(cDxcc d in _blDxcc) {
					if(d.Prefix == txtPrefix.Text) { ErrMsg("Prefixに重複がありました\n。処理を終了します。\n Prefix:"+ txtPrefix.Text); return; }
				}
				dx.Prefix = txtPrefix.Text;
			}
		}

		private void txtName_Leave(object sender, EventArgs e) {
			if (dgvDxcc.SelectedRows == null) { return; }
			if (dgvDxcc.SelectedRows.Count < 1) { return; }
			cDxcc dx = dgvDxcc.SelectedRows[0].DataBoundItem as cDxcc;
			if (dx == null) { return; }
			dx.Name = txtName.Text;
		}

		private void txtPattern_Leave(object sender, EventArgs e) {
			if (dgvDxcc.SelectedRows == null) { return; }
			if (dgvDxcc.SelectedRows.Count < 1) { return; }
			cDxcc dx = dgvDxcc.SelectedRows[0].DataBoundItem as cDxcc;
			if (dx == null) { return; }
			dx.Pattern = txtPattern.Text;

		}

		private void txtCode_Leave(object sender, EventArgs e) {
			int iC;
			if (!int.TryParse(txtCode.Text, out iC)) { ErrMsg("Entity Codeは数字(整数)を入力してください。\n入力された文字列: " + txtCode.Text); }
			if (dgvDxcc.SelectedRows == null) { return; }
			if (dgvDxcc.SelectedRows.Count < 1) { return; }
			cDxcc dx = dgvDxcc.SelectedRows[0].DataBoundItem as cDxcc;
			if (dx == null) { return; }
			dx.Code = iC;
		}

		private void CmdOK_Click(object sender, EventArgs e) {
			Dictionary<string,string> dcDupe = new Dictionary<string, string>();
			#region "Prefix重複・EntityCode無効チェック"
			foreach(cDxcc dx in _blDxcc) {
				if (dcDupe.ContainsKey(dx.Prefix)) { ErrMsg(string.Format("Prefix「{0}」が重複しています。", dx.Prefix)); return; }
				if (dx.Code < 0) { ErrMsg(string.Format("EntityCodeが負の値です\n{0} {1} Code={2}。", dx.Prefix, dx.Name, dx.Code)); return; }
			}
			#endregion

			try {
				_dcDXCC.Clear();
				using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", _sDB))) {
					con.Open();
					using (SQLiteTransaction st = con.BeginTransaction())
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = "delete from [T_DXCC]";
						cmd.ExecuteNonQuery();

						var dxs = _blDxcc.OrderBy((x) => x.PrefixSort);
						foreach (cDxcc dx in dxs) {
							cmd.CommandText = string.Format("INSERT INTO [T_DXCC]([DXCC],[Name],[Pattern],[EntityCode]) VALUES('{0}','{1}','{2}','{3}');", dx.Prefix, dx.Name, dx.Pattern, dx.Code);
							int iRes = cmd.ExecuteNonQuery();
							_dcDXCC.Add(dx.Prefix, dx);
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

		private void frmEditDXCC_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}
	}
}
