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
using System.Data.SQLite;

namespace prjOpenLog {
	public partial class frmEditCity : Form {
		Dictionary<string, cCity> _dcCity;
		BindingList<cCity> _blCity;
		BindingSource _bs;
		string _sDB;

		public frmEditCity(Dictionary<string, cCity> City, string DB) {
			_dcCity = City;
			_blCity = new BindingList<cCity>();
			_bs = new BindingSource();
			_sDB = DB;
			foreach (cCity c in _dcCity.Values) { _blCity.Add(c); }
			InitializeComponent();
		}

		private void frmEditCity_Load(object sender, EventArgs e) {
			_bs.DataSource = _blCity;
			dgvCity.RowHeadersVisible = false;
			dgvCity.DataSource = _bs;
		}

		private void frmEditCity_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}

		private void cmdSearch_Click(object sender, EventArgs e) {
			int iStartIndex = 0;
			if(dgvCity.CurrentCell != null) {
				if(0 <= dgvCity.CurrentCell.RowIndex) { iStartIndex = dgvCity.CurrentCell.RowIndex; }
			}
			if(dgvCity.Rows.Count -1 <= iStartIndex) { MessageBox.Show("最後まで検索しました。", "検索終了", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

			//検索本体
			string sPat = "^" + txtSearch.Text;
			for(int i  = iStartIndex; i < _blCity.Count; i++) {
				cCity c = _blCity[i];
				if(Regex.IsMatch(c.SearchStr, sPat)) {
					dgvCity.FirstDisplayedScrollingRowIndex = i;
					dgvCity.CurrentCell = dgvCity[4, i];
					return;
				}

			}


		}

		private void cmdAddNew_Click(object sender, EventArgs e) {
			if (MessageBox.Show("新規市町村を追加しますか?\n(一度追加を反映すると、データベースを外部ツールで直接編集しないと削除できません)", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No) { return; }
			cCity c = new cCity("9999", "9999", "99", "新規市区町村", "しんき");
			_blCity.Add(c);
			dgvCity.FirstDisplayedScrollingRowIndex = _blCity.Count - 1;
			dgvCity.CurrentCell = dgvCity[4, _blCity.Count - 1];
		}

		private void cmdCancel_Click(object sender, EventArgs e) {
			Close();
		}

		//セル選択→編集モード
		private void dgvCity_SelectionChanged(object sender, EventArgs e) {
			DataGridView dgv = sender as DataGridView;
			if (dgv == null) { return; }
			DataGridViewCell cell = dgv.CurrentCell;
			if (cell == null) { return; }
			SendKeys.Send("{F2}"); //編集モードにする

		}

		private void cmdOK_Click(object sender, EventArgs e) {
			Dictionary<string, cCity> dcNew = new Dictionary<string, cCity>();
			#region "重複チェック"
			foreach(cCity c in _blCity) {
				if (dcNew.ContainsKey(c.CityCode)) {
					string sMsg = string.Format("新規市町村番号「{0}」に重複があるためDBへの保存が出来ません。\n重複を解消するか、一旦アプリを再起動してください。\n名称読み①:{1}\n名称読み②:{2}",
						c.CityCode, dcNew[c.CityCode].SearchStr, c.SearchStr);
					MessageBox.Show(sMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				dcNew.Add(c.CityCode, c);
			}
			#endregion

			try {
				using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", _sDB))) {
					con.Open();
					using (SQLiteTransaction st = con.BeginTransaction())
					using (SQLiteCommand cmd = con.CreateCommand()) {
						cmd.CommandText = "delete from  [T_City]";
						cmd.ExecuteNonQuery();
						_dcCity.Clear();

						foreach (cCity c in dcNew.Values) {
							cmd.CommandText = string.Format("INSERT INTO [T_City]([CityCode], [JCCG], [Area], [Name], [Search]) VALUES('{0}','{1}','{2}','{3}','{4}');", c.CityCode, c.JCCG, c.Area, c.Name, c.SearchStr);
							int iRes = cmd.ExecuteNonQuery();
							_dcCity.Add(c.CityCode, c);
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
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}
	}
}
