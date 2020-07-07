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
	public partial class frmEditDefaultRig : Form {
		Dictionary<string, cDefaultRig> _dcDF;
		string _sDB;
		BindingList<cDefaultRig> _blDF;
		BindingSource _bs;
		public frmEditDefaultRig(Dictionary<string, cDefaultRig> Default, string DB) {
			InitializeComponent();
			_dcDF = Default;
			_sDB = DB;
			_blDF = new BindingList<cDefaultRig>();
			foreach(cDefaultRig r in _dcDF.Values) { _blDF.Add(r); }
			_bs = new BindingSource();
			_bs.DataSource = _blDF;
		}

		private void frmEditDefaultRig_Load(object sender, EventArgs e) {
			dgvDefaultRig.RowHeadersVisible = false;
			dgvDefaultRig.DataSource = _bs;
			dgvDefaultRig.Columns["BandF"].ReadOnly = true;
			dgvDefaultRig.Columns["BandF"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
			dgvDefaultRig.Columns["BandF"].HeaderText = "周波数帯";
			dgvDefaultRig.Columns["RigHome"].HeaderText = "無線機(常置場所)";
			dgvDefaultRig.Columns["AntHome"].HeaderText = "アンテナ(常置場所)";
			dgvDefaultRig.Columns["RigMobile"].HeaderText = "無線機(移動先)";
			dgvDefaultRig.Columns["AntMobile"].HeaderText = "アンテナ(移動先)";
			dgvDefaultRig.Columns[0].Width = 70;
			for (int i = 1; i < dgvDefaultRig.Columns.Count; i++) { dgvDefaultRig.Columns[i].Width = 150; }
		}

		//セル変更→編集モード
		private void dgvDefaultRig_SelectionChanged(object sender, EventArgs e) {
			DataGridView dgv = sender as DataGridView;
			if (dgv == null) { return; }
			DataGridViewCell cell = dgv.CurrentCell;
			if (cell == null) { return; }
			SendKeys.Send("{F2}"); //編集モードにする
		}

		private void cmdOk_Click(object sender, EventArgs e) {
			dgvDefaultRig.EndEdit();

			Dictionary<string, cDefaultRig> dcNewDF = new Dictionary<string, cDefaultRig>();
			#region "重複チェック(念のため)"
			foreach(cDefaultRig df in _blDF) {
				if (dcNewDF.ContainsKey(df.BandF)) { ErrMsg("周波数帯が重複しています。\n周波数帯:" + df.BandF); return; }
				dcNewDF.Add(df.BandF, df);
			}
			#endregion

			try {
				using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", _sDB))) {
					con.Open();
					using (SQLiteTransaction st = con.BeginTransaction())
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = "delete from  [T_DefaultRig]";
						cmd.ExecuteNonQuery();
						_dcDF.Clear();

						foreach (cDefaultRig df in dcNewDF.Values) {
							cmd.CommandText = string.Format("INSERT INTO [T_DefaultRig]([BandF], [RigHome], [AntHome], [RigMobile], [AntMobile]) VALUES('{0}','{1}','{2}','{3}','{4}');", df.BandF, df.RigHome, df.AntHome, df.RigMobile, df.AntMobile);
							int iRes = cmd.ExecuteNonQuery();
							_dcDF.Add(df.BandF, df);
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

		private void cmdCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private void ErrMsg(string Message) {
			MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		//後始末
		private void frmEditDefaultRig_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}
	}
}
