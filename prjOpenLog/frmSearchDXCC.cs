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
	public partial class frmSearchDXCC : Form {
		TextBox _txtResultCode, _txtResultName;

		public Dictionary<string, cDxcc> _dcDXCC { get; }
		/// <summary>
		/// 正規表現パターンとDXCCエンティティ(Prefix)のペア
		/// </summary>

		/// <summary>
		/// DXCCエンティティの候補
		/// </summary>
		/// <param name="Prefix">検索対象コールサイン(Prefix)</param>
		/// <param name="DXCC">全DXCC</param>
		/// <param name="Patterns">Prefixの正規表現とエンティティ(Prefix)のペア</param>
		public frmSearchDXCC(string Prefix, Dictionary<string, cDxcc> DXCC, TextBox ResultCode, TextBox ResultName) {
			_dcDXCC = DXCC;
			InitializeComponent();
			_txtResultCode = ResultCode;
			_txtResultName = ResultName;
			if (Prefix != "") { Search(Prefix); }
			 
			else {
				foreach (string sPx in _dcDXCC.Keys) {
					lstDXCC.Items.Add(_dcDXCC[sPx]);
				}
			}
		}

		//PrefixからDXCCエンティティの候補をリストボックスに表示する
		private void Search(string Prefix) {
			Dictionary<string, cDxcc> dcCand = new Dictionary<string, cDxcc>();
			lstDXCC.Items.Clear();
			foreach(cDxcc dx in _dcDXCC.Values) {
				if (dcCand.ContainsKey(dx.Prefix)) { continue; }
				foreach (string sPt in dx.Patterns) {
					if(Regex.IsMatch(Prefix, "^" + sPt)) { dcCand.Add(dx.Prefix, dx); }
				}
			}
			foreach(cDxcc dx in dcCand.Values) { lstDXCC.Items.Add(dx); }

		}

		private void cmdOK_Click(object sender, EventArgs e) {
			if(lstDXCC.SelectedIndex < 0) { return; }
			cDxcc dx = lstDXCC.SelectedItem as cDxcc;
			if(dx == null) { return; }

			_txtResultCode.Text = dx.Prefix;
			_txtResultName.Text = dx.Name;
			Close();
		}

		private void frmSearchDXCC_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}

		private void cmdSearch_Click(object sender, EventArgs e) {
			Search(txtPrefix.Text);
		}
	}
}
