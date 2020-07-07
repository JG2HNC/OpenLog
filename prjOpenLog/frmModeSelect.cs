using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjOpenLog {
	public partial class frmModeSelect : Form {
		Dictionary<string, cMode> _dcModes;
		TextBox _txtResultMode;

		public frmModeSelect(Dictionary<string, cMode> Modes, TextBox Mode) {
			_dcModes = Modes; _txtResultMode = Mode;
			InitializeComponent();
		}

		private void frmModeSelect_Load(object sender, EventArgs e) {
			foreach(string sM in _dcModes.Keys) {
				lstModes.Items.Add(sM);
			}
		}

		private void cmdOK_Click(object sender, EventArgs e) {
			if (0 <= lstModes.SelectedIndex) {
				_txtResultMode.Text = lstModes.SelectedItem.ToString();
			}
			Close();
		}

		private void frmModeSelect_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}
	}
}
