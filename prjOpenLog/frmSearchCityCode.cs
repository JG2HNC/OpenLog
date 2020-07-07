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
	public partial class frmSearchCityCode : Form {
		private Dictionary<string, cCity> _dcCityCode { get; }
		TextBox _txtResultCode, _txtResultName;

		/// <summary>
		/// 国内の地域(JCC/JCG)を検索する
		/// </summary>
		/// <param name="CityCode">JCC/JCG番号の一覧</param>
		/// <param name="ResultCode">結果を出力するテキストボックス(JCC/JCG)</param>
		/// <param name="ResultName">結果を出力するテキストボックス(市区町村名)</param>
		/// <param name="SearchKeyword">検索文字列(地名、前方一致)</param>
		public frmSearchCityCode(string SearchKeyword, string Area, Dictionary<string, cCity> CityCode, TextBox ResultCode, TextBox ResultName) {
			_dcCityCode = CityCode;
			_txtResultCode = ResultCode;
			_txtResultName = ResultName;
			InitializeComponent();
			txtKeyword.Text = SearchKeyword;
			txtArea.Text = Area;
		}

		private void frmSearchDomesticCode_Load(object sender, EventArgs e) {
			Search();
		}

		private void Search() {
			lstResult.Items.Clear();
			foreach(string sK in _dcCityCode.Keys) {
				string sA = _dcCityCode[sK].Area;
				string sN = _dcCityCode[sK].Name;
				string sPat = _dcCityCode[sK].SearchStr;
				if(sA != txtArea.Text) { continue; }
				if (chkExceptOld.Checked) {
					if(Regex.IsMatch(sN, @"^\*")) { continue; } //廃止を除外
				}
				if (txtKeyword.Text == "") { lstResult.Items.Add(string.Format("{0}: {1}", sK, sN)); }
				else {
					if (Regex.IsMatch(sPat, "^" + txtKeyword.Text)) {
						lstResult.Items.Add(string.Format("{0}: {1}", sK, sN));
					}
				}
			}

		}

		private void cmdSearch_Click(object sender, EventArgs e) {
			Search();
		}

		private void txtKeyword_TextChanged(object sender, EventArgs e) {
			cmdSearch_Click(sender, e);
		}


		private void cmdOK_Click(object sender, EventArgs e) {
			if(0 <= lstResult.SelectedIndex) {
				string[] sR = lstResult.SelectedItem.ToString().Split(':');
				if (_txtResultCode != null) { _txtResultCode.Text = sR[0]; }
				if (_txtResultName != null) { _txtResultName.Text = sR[1].Trim(); }
			}
			this.Close();
		}

		private void frmSearchCityCode_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}
	}
}
