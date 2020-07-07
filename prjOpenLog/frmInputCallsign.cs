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
	public partial class frmInputCallsign : Form {
		private Config _cfg;
		public frmInputCallsign(Config cfg) {
			_cfg = cfg;
			InitializeComponent();
		}

		private void cmdInput_Click(object sender, EventArgs e) {
			if (Regex.IsMatch(txtCallsign.Text, "[A-Z0-9]+")) { _cfg.MyCall = txtCallsign.Text; Close(); }
			else {
				MessageBox.Show("コールサインが未入力または書式が不正です。\n再入力してください。\n入力値: ", txtCallsign.Text);
				return;
			}
		}

		private void txtCallsign_Leave(object sender, EventArgs e) {
			txtCallsign.Text = txtCallsign.Text.ToUpper();
		}

		private void frmInputCallsign_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}
	}
}
