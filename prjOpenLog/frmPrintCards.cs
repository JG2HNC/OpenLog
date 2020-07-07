using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Text.RegularExpressions;

namespace prjOpenLog {
	public partial class frmPrintCards : Form {
		cPrintQSLDocument _pq;
		bool _bIsEH;

		public frmPrintCards(List<cQSO> PrintQSOs, Dictionary<string, cMode> Modes) {
			_pq = new cPrintQSLDocument(PrintQSOs, Modes);
			_pq.EndPrint += PrintQSL_EndPrint;
			_bIsEH = false;
			InitializeComponent();
		}

		//プリンタ&用紙設定
		private void cmdSetPrint_Click(object sender, EventArgs e) {
			try {
				using (PrintDialog pdl = new PrintDialog()) {
					if (pdl.ShowDialog() == DialogResult.OK) {
						_pq.PrinterSettings = pdl.PrinterSettings;
						lblPaperSize.Text = string.Format("用紙: {0}({1:f1}mm×{2:f1}mm)\r\nプリンタ: {3}",
							_pq.DefaultPageSettings.PaperSize.PaperName, _pq.DefaultPageSettings.PaperSize.Width * 0.254d, _pq.DefaultPageSettings.PaperSize.Height * 0.254d,
							pdl.PrinterSettings.PrinterName);

						if (_bIsEH && File.Exists(txtSetPrintDef.Text)) {
							_pq.ReadPrintCommand(txtSetPrintDef.Text);
							ppcCard.Document = _pq;
							_pq.PrintPage -= new PrintPageEventHandler(_pq.PrintQSLCard);
							_pq.PrintPage += new PrintPageEventHandler(_pq.PrintQSLCard);

						}

					}
				}
			} catch(Exception ex) {
				ErrMeg(ex.Message);
			}

		}

		//印刷定義ファイル
		private void cmdSetPrintDef_Click(object sender, EventArgs e) {
			try {
				using (OpenFileDialog ofd = new OpenFileDialog()) {
					if (Directory.Exists(txtSetPrintDef.Text)) { ofd.InitialDirectory = txtSetPrintDef.Text; }
					ofd.Filter = "OpenLog印刷定義ファイル(*.pdef)|*.pdef|すべてのファイル(*.*)|*.*";
					if (ofd.ShowDialog() == DialogResult.OK) {
						txtSetPrintDef.Text = ofd.FileName;
						_pq.ReadPrintCommand(ofd.FileName);
					}
				}
				ppcCard.Document = _pq;
				if (_bIsEH) { _pq.PrintPage -= new PrintPageEventHandler(_pq.PrintQSLCard); }
				_pq.PrintPage += new PrintPageEventHandler(_pq.PrintQSLCard);
				_bIsEH = true;
				// cmdSetPrintDef.Enabled = false;
			}
			catch (Exception ex) {
				ErrMeg(ex.Message);
			}
		}

		private void cmdPgNext_Click(object sender, EventArgs e) {
			ppcCard.StartPage++;
		}

		private void cmdPgPrev_Click(object sender, EventArgs e) {
			if(ppcCard.StartPage == 0) { return; }
			ppcCard.StartPage--;
		}

		private void cmdPrint_Click(object sender, EventArgs e) {
			try {
				_pq.Print();
			} catch(Exception ex) {
				ErrMeg("印刷処理中にエラーが発生しました。\n" +ex.Message);
			}

			try {
				if(MessageBox.Show("カードを発送済みにしますか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
					foreach(cQSO q in _pq.QSOList) {
						q.Card_Send = true;
						q.LastUpdate = DateTime.UtcNow.Ticks;
					}
				}
			} catch(Exception ex) {
				ErrMeg("印刷終了後" + ex.Message);
			}

		}

		//エラー表示
		private void ErrMeg(string Message) {
			MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void PrintQSL_EndPrint(object sender,PrintEventArgs e) {
			if(_pq == null) { return; }
			if(0 < _pq.PrintErrors.Count) {
				ErrMeg(string.Format("印刷中に{0}個のエラーが発生しました。\n\nエラー内容:\n{1}", _pq.PrintErrors.Count, string.Join("\n", _pq.PrintErrors)));
			}
		}

		private void frmPrintCards_FormClosed(object sender, FormClosedEventArgs e) {
			Dispose();
		}

	}
}
