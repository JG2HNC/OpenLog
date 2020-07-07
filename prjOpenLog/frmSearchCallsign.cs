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
using System.Data.SQLite;

namespace prjOpenLog {
	public partial class frmSearchCallsign : Form {
		BindingList<cQSO> _blAllQSO;
		BindingList<cQSO> _blResult;
		BindingSource _bs;

		int[] _iColWidth;
		string[] _sColName;
		public frmSearchCallsign(BindingList<cQSO> AllQSO, int[] ColWidth, string[] ColName) {
			InitializeComponent();
			_blAllQSO = AllQSO;
			_blResult = new BindingList<cQSO>();
			_bs = new BindingSource();
			_bs.DataSource = _blResult;
			dgvSearch.DataSource = _bs;
			_iColWidth = ColWidth;
			_sColName = ColName;
		}

		private void frmSearchCallsign_Load(object sender, EventArgs e) {
			for (int i = 0; i < _iColWidth.Length; i++) {
				if (_iColWidth[i] < 0) { dgvSearch.Columns[i].Visible = false; }
				else { dgvSearch.Columns[i].Width = _iColWidth[i]; }
				dgvSearch.Columns[i].HeaderText = _sColName[i];
			}
			dgvSearch.ContextMenuStrip = cmsGrid;
		}

		private void cmdOK_Click(object sender, EventArgs e) {
			Close();
		}


		private void cmdSearch_Click(object sender, EventArgs e) {
			Search();
		}
		private void txtCall_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyData == Keys.Enter) { Search(); }
		}
		private void Search() {

			dgvSearch.SuspendLayout(); //描画を止める
			if (0 < _blResult.Count) { _blResult.Clear(); }

			List<cQSO> lsPast = new List<cQSO>(); //過去QSO一時置き場(ソート可能に)
			foreach (cQSO q in _blAllQSO) {
				if (txtCall.Text == q.Call) {
					lsPast.Add(q);
				}
			}

			//ソート
			lsPast.Sort((a, b) => a.Date_S.CompareTo(b.Date_S));
			foreach (cQSO q in lsPast) { _blResult.Add(q); }
			dgvSearch.ResumeLayout();

		}

		private void frmSearchCallsign_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}

		#region "Context menu"
		private void cmsGrid_Received_Click(object sender, EventArgs e) {
			if (dgvSearch.SelectedRows == null) { return; }
			if (dgvSearch.SelectedRows.Count == 0) { return; }
			cQSO qso = dgvSearch.SelectedRows[0].DataBoundItem as cQSO;
			if (qso == null) { return; }

			if (qso.Card_Resv) { qso.Card_Resv = false; }
			else { qso.Card_Resv = true; }
		}

		private void cmsGrid_Sent_Click(object sender, EventArgs e) {
			if (dgvSearch.SelectedRows == null) { return; }
			if (dgvSearch.SelectedRows.Count == 0) { return; }
			cQSO qso = dgvSearch.SelectedRows[0].DataBoundItem as cQSO;
			if (qso == null) { return; }

			if (qso.Card_Send) { qso.Card_Send = false; }
			else { qso.Card_Send = true; }
		}
		#endregion


		//Enterを押したときのビープ音を抑止する
		//https://dobon.net/vb/dotnet/control/tbsuppressbeep.html
		[System.Security.Permissions.UIPermission(System.Security.Permissions.SecurityAction.Demand, Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData) {
			if (keyData == Keys.Enter) { return (true); }
			else { return (base.ProcessDialogKey(keyData)); }
		}

	}
}
