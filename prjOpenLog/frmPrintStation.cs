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
	public partial class frmPrintStation : Form {
		Dictionary<string, List<cQSO>> _dcQSO;
		Dictionary<string, cMode> _dcMode;

		public frmPrintStation(List<cQSO> QSOs, Dictionary<string, cMode> Modes) {
			_dcQSO = new Dictionary<string, List<cQSO>>();
			_dcMode = Modes;
			foreach(cQSO q in QSOs) {
				if (!_dcQSO.ContainsKey(q.Call)) { _dcQSO.Add(q.Call, new List<cQSO>()); }
				_dcQSO[q.Call].Add(q);
			}
			InitializeComponent();
		}

		private void frmPrintStation_Load(object sender, EventArgs e) {
			Dictionary<string, int> dcCallCount = new Dictionary<string, int>();
			foreach(string sC in _dcQSO.Keys) { dcCallCount.Add(sC, _dcQSO[sC].Count); }
			IOrderedEnumerable<KeyValuePair<string, int>> sorted = dcCallCount.OrderByDescending(pair => pair.Value);
			foreach (var p in sorted) {
				lstStationList.Items.Add(new cCall(p.Key, p.Value));
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e) {
			Close();
		}

		private class cCall {
			string _sC;
			int _iC;
			public cCall(string Call, int Count) { _sC = Call; _iC = Count; }
			public string Call { get { return (_sC); } }
			public int Count { get { return (_iC); } set { _iC = value; } }
			public override string ToString() {
				return string.Format("{0} ({1})", _sC, _iC);
			}

		}

		private void cmdDoPrint_Click(object sender, EventArgs e) {
			List<cQSO> lsPrint = new List<cQSO>();
			foreach(var itm in lstStationList.CheckedItems) {
				cCall c = itm as cCall;
				if(c == null) { continue; }
				if (!_dcQSO.ContainsKey(c.Call)) { continue; }
				foreach (cQSO q in _dcQSO[c.Call]) {
					lsPrint.Add(q);
				}
			}

			frmPrintCards fp = new frmPrintCards(lsPrint, _dcMode);
			fp.ShowDialog();
			Close();
		}

		private void frmPrintStation_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}
	}
}
