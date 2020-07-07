using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace prjOpenLog {
	/// <summary>
	/// QSLカード印刷用
	/// </summary>
	class cPrintQSLDocument : PrintDocument {
		//コマンドパターン: $コマンド (パラメータ) データ
		const string sComPat = @"^\$\s*([^\(]+)\s*\(([^)]+)\)\s*(.*)";
		const string sDatPat = @"(%[^%]+\%)\s*(\[[\d+]\])*";
		List<cPrintCom> _lsCom;
		List<cQSO> _lsQSO;
		Dictionary<string, cMode> _dcMode;
		private int iCurIndex; //現在のページ番号(開始): 0 ～ _lsQSO.Count-1
		private int iTotalQSO;
		private int iQSOsParPage; //1ページあたりに表示するレコード数(同一コールサインの場合)
		private float fDx; 
		private float fDy;
		public List<string> PrintErrors { get; }


		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="QSOs">印刷対象の交信レコード</param>
		public cPrintQSLDocument(List<cQSO> QSOs, Dictionary<string,cMode> Modes) :base() {
			PrintErrors = new List<string>();
			_dcMode = Modes;
			
			//List並び替え(Call→日付
			IEnumerable<cQSO> sorted = QSOs.OrderBy(a => {
				string sCa = "ZZ" + a.CallQSL;
				if (a.DXCC == "JA" && Regex.IsMatch(a.CallQSL, "^7[K-N]")) { sCa = "JA" + a.CallQSL; }
				else if (a.DXCC == "JA" && Regex.IsMatch(a.CallQSL, "^8[J-N]")) { sCa = "JB" + a.CallQSL; }
				else if (a.DXCC == "JA") { sCa = "J" + a.CallQSL.Substring(2, 1) + a.CallQSL; }
				return (sCa);
			})
			.ThenBy(a => a.Date_S);
			_lsQSO = new List<cQSO>();
			foreach(cQSO q in sorted) { _lsQSO.Add(q); }

			_lsCom = new List<cPrintCom>();
			fDx = 0f;
			fDy = 0;
			iQSOsParPage = 1;
			iTotalQSO = _lsQSO.Count;
		}

		/// <summary>
		/// 印刷対象のQSLリスト
		/// </summary>
		public List<cQSO> QSOList { get { return (_lsQSO); } set { _lsQSO = value; } }

		/// <summary>
		/// 印刷コマンドファイルを読み込む→印刷位置微調整&1枚あたりQSO数取得
		/// </summary>
		/// <param name="PrintDefFile">印刷コマンドファイル</param>
		public void ReadPrintCommand(string PrintDefFile) {
			//構文パターン
			Regex rgCom = new Regex(sComPat);

			_lsCom.Clear();
			using (StreamReader sr = new StreamReader(PrintDefFile, Encoding.GetEncoding(932))) {
				int iL = 0;
				while (-1 < sr.Peek()) {
					iL++;
					string sLine = sr.ReadLine();
					if (rgCom.IsMatch(sLine)) {
						cPrintCom com = new cPrintCom(sLine, iL);
						if (com.Command == "delt") {
							if(com.Params.Length < 2) { throw new Exception("位置調整コマンドのパラメータは二つ必要です。"); }
							float.TryParse(com.Params[0], out fDx); float.TryParse(com.Params[1], out fDy);
							continue; //位置調整コマンドの時はデータに関係しないので飛ばす
						}
						if(com.Command == "nparpage") {
							if (com.Params.Length < 1) { throw new Exception("1枚あたりQSO数のパラメータは一つ必要です。"); }
							if(!int.TryParse(com.Params[0], out iQSOsParPage)) { iQSOsParPage = 1; }
							continue; //1枚あたりQSO数コマンドの時はデータに関係しないので飛ばす
						}
						_lsCom.Add(com);
					}
				}
			}
		}

		public void PrintQSLCard(object sender, PrintPageEventArgs e) {
			PrintErrors.Clear();
			Graphics g = e.Graphics;

			//ミリメートル単位に
			g.PageUnit = GraphicsUnit.Millimeter;

			//構文パターン & データパターン(フィールド取得)
			Regex rgCom = new Regex(sComPat);
			Regex rgDat = new Regex(sDatPat);

			System.Reflection.PropertyInfo[] piQSO = typeof(cQSO).GetProperties();
			Dictionary<string, string>[] dcDats = new Dictionary<string, string>[iQSOsParPage];
			int iStep = 0; //当該ページに載るQSOの数
			#region "QSOデータ取得"
			//まずは空の値を取得
			for (int i = 0; i < iQSOsParPage; i++) {
				dcDats[i] = new Dictionary<string, string>();
				for (int j = 0; j < piQSO.Length; j++) {
					string sPn = piQSO[j].Name;
					dcDats[i].Add(sPn, "");
				}
			}

			for (int i = 0; i < iQSOsParPage; i++) {
				if (_lsQSO.Count <= iCurIndex + i) { break; } //印刷QSOの範囲を超えているとき→データは無い
				if (_lsQSO[iCurIndex].Call != _lsQSO[iCurIndex + i].Call) { break; }
				iStep = i + 1;
				for (int j = 0; j < piQSO.Length; j++) {
					string sPn = piQSO[j].Name;
					System.Reflection.PropertyInfo pi = typeof(cQSO).GetProperty(sPn); //名前でアクセス
					cQSO q = _lsQSO[iCurIndex + i];
					string ss = pi.GetValue(q).ToString();
					dcDats[i][sPn] = pi.GetValue(q).ToString();
				}
			}
			#endregion

			for (int iC = 0; iC < _lsCom.Count; iC++) {
				string sCom = _lsCom[iC].Command;
				string[] sPrm = _lsCom[iC].Params;
				string sDat = _lsCom[iC].RawDate;

				try {
					#region "データ取得"
					{
					int iDp = 0; //データページ
					var mts = rgDat.Matches(sDat);
					for(int j = 0; j < mts.Count; j++) {
						if(mts[j].Groups.Count < 2) { continue; }
						string sFn = Regex.Replace(mts[j].Groups[1].Value, @"%([^\%]+)%", "$1");
						if (3 <= (mts[j].Groups.Count)) {
							if (int.TryParse(Regex.Replace(mts[j].Groups[2].Value, @"^\[(\d+)]", "$1"), out iDp)) { iDp--; }
						}
						if(dcDats.Length <= iDp) { continue; }
						if (!dcDats[iDp].ContainsKey(sFn)) { continue; }
						sDat = sDat.Replace(mts[j].Groups[0].Value, dcDats[iDp][sFn]);
					}
				}
				#endregion

					#region "コマンド実行"

					//パラメータのうち、第1・第2引数は位置(x,y)
					float fX, fY;
					if (!float.TryParse(sPrm[0], out fX)) { throw new Exception(string.Format("印刷コマンドの第1パラメータ(印刷位置X)は数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
					if (!float.TryParse(sPrm[1], out fY)) { throw new Exception(string.Format("印刷コマンドの第2パラメータ(印刷位置Y)は数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
					fX += fDx;
					fY += fDy;

					//印刷コマンド
					if (sCom == "line") { //線分 #line(x1, y1, x2, y2, Red, Green, Blue, Width)
						float fX2, fY2;
						if (!float.TryParse(sPrm[2], out fX2)) { throw new Exception(string.Format("印刷位置X2は数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						if (!float.TryParse(sPrm[3], out fY2)) { throw new Exception(string.Format("印刷位置Y2は数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						fX2 += fDx;
						fY2 += fDy;
						int iR, iG, iB;
						float fW;
						int.TryParse(sPrm[4], out iR); int.TryParse(sPrm[5], out iG); int.TryParse(sPrm[6], out iB); float.TryParse(sPrm[7], out fW);
						Pen pn = new Pen(Color.FromArgb(iR, iG, iB), fW);
						g.DrawLine(pn, fX, fY, fX2, fY2);
					}
					if (sCom == "line_ifblank") { //線分 #line(x1, y1, x2, y2, Red, Green, Blue, Width,対象とするQSO番号)
						int iNo = 0;
						if (9 <= sPrm.Length) { if (int.TryParse(sPrm[8], out iNo)) { iNo--; } }
						bool bFlg = false;
						if (_lsQSO.Count <= iCurIndex + iNo) { bFlg = true; }
						else {
							if (_lsQSO[iCurIndex].Call != _lsQSO[iCurIndex + iNo].Call) { bFlg = true; }
						}
						if (!bFlg) { continue; }

						float fX2, fY2;
						if (!float.TryParse(sPrm[2], out fX2)) { throw new Exception(string.Format("印刷位置X2は数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						if (!float.TryParse(sPrm[3], out fY2)) { throw new Exception(string.Format("印刷位置Y2は数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						fX2 += fDx;
						fY2 += fDy;
						int iR, iG, iB;
						float fW;
						int.TryParse(sPrm[4], out iR); int.TryParse(sPrm[5], out iG); int.TryParse(sPrm[6], out iB); float.TryParse(sPrm[7], out fW);
						Pen pn = new Pen(Color.FromArgb(iR, iG, iB), fW);
						g.DrawLine(pn, fX, fY, fX2, fY2);
					}
					if (sCom == "rect") { //長方形 #rect(x1, y1, w, h, Red, Green, Blue, Width)
						float fW, fH;
						if (!float.TryParse(sPrm[2], out fW)) { throw new Exception(string.Format("幅Wは数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						if (!float.TryParse(sPrm[3], out fH)) { throw new Exception(string.Format("高さHは数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						int iR, iG, iB;
						float fT;
						int.TryParse(sPrm[4], out iR); int.TryParse(sPrm[5], out iG); int.TryParse(sPrm[6], out iB); float.TryParse(sPrm[7], out fT);
						Pen pn = new Pen(Color.FromArgb(iR, iG, iB), fT);
						g.DrawRectangle(pn, fX, fY, fW, fH);
					}
					if (sCom == "text") { //横書きテキスト #text(x, y, フォント名, サイズ, Align) Data
						float fS;
						if (!float.TryParse(sPrm[3], out fS)) { throw new Exception(string.Format("フォントサイズは数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						Font ft = new Font(sPrm[2], fS);
						Brush bh = Brushes.Black;
						string sAlgn = "";
						if (4 < sPrm.Length) { sAlgn = sPrm[4]; GetTextOrg(sDat, sAlgn, ft, g, ref fX, ref fY); }
						g.DrawString(sDat, ft, bh, fX, fY);
					}
					if (sCom == "text_p") { //1文字テキスト #text_p(x, y, フォント名, サイズ, 開始位置:1～) Data
						float fS;
						if (!float.TryParse(sPrm[3], out fS)) { throw new Exception(string.Format("フォントサイズは数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						Font ft = new Font(sPrm[2], fS);
						Brush bh = Brushes.Black;
						string sAlgn = "";
						if (5 < sPrm.Length) { sAlgn = sPrm[5]; GetTextOrg(sDat, sAlgn, ft, g, ref fX, ref fY); }
						int iStart = int.Parse(sPrm[4]) - 1;
						if (sDat.Length < iStart + 1) { sDat = ""; }
						else { sDat = sDat.Substring(iStart, 1); }
						g.DrawString(sDat, ft, bh, fX, fY);
					}
					if (sCom == "text_rcvd") { //QSL受領時テキスト #text_rcvd(x, y, フォント名, サイズ, Align, QSO番号) Data
						int iNo = 0;
						if (6 <= sPrm.Length) { if (int.TryParse(sPrm[5], out iNo)) { iNo--; } }
						if (_lsQSO.Count <= iCurIndex + iNo) { continue; }
						if (_lsQSO[iCurIndex].Call != _lsQSO[iCurIndex + iNo].Call) { continue; }
						if (!_lsQSO[iCurIndex + iNo].Card_Resv) { continue; }
						float fS;
						if (!float.TryParse(sPrm[3], out fS)) { throw new Exception(string.Format("フォントサイズは数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						Font ft = new Font(sPrm[2], fS);
						Brush bh = Brushes.Black;
						string sAlgn = "";
						if (4 < sPrm.Length) { sAlgn = sPrm[4]; GetTextOrg(sDat, sAlgn, ft, g, ref fX, ref fY); }
						g.DrawString(sDat, ft, bh, fX, fY);
					}
					if (sCom == "text_unrcvd") { //QSL未受領時テキスト #text_unrcvd(x, y, フォント名, サイズ, Align, QSO番号) Data
						int iNo = 0;
						if (6 <= sPrm.Length) { if (int.TryParse(sPrm[5], out iNo)) { iNo--; } }
						if (_lsQSO.Count <= iCurIndex + iNo) { continue; }
						if (_lsQSO[iCurIndex].Call != _lsQSO[iCurIndex + iNo].Call) { continue; }
						if (_lsQSO[iCurIndex + iNo].Card_Resv) { continue; }

						float fS;
						if (!float.TryParse(sPrm[3], out fS)) { throw new Exception(string.Format("フォントサイズは数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						Font ft = new Font(sPrm[2], fS);
						Brush bh = Brushes.Black;
						string sAlgn = "";
						if (4 < sPrm.Length) { sAlgn = sPrm[4]; GetTextOrg(sDat, sAlgn, ft, g, ref fX, ref fY); }
						g.DrawString(sDat, ft, bh, fX, fY);
					}
					if (sCom == "text_myport") { //自局移動運用時Prefix(/エリア) #text_unrcvd(x, y, フォント名, サイズ, Align, QSO番号) Data
						int iNo = 0;
						if (6 <= sPrm.Length) { if (int.TryParse(sPrm[5], out iNo)) { iNo--; } }
						if (_lsQSO.Count <= iCurIndex + iNo) { continue; }
						if (_lsQSO[iCurIndex].Call != _lsQSO[iCurIndex + iNo].Call) { continue; }
						if (_lsQSO[iCurIndex + iNo].Prefix_My == "") { continue; }

						sDat = "/" + _lsQSO[iCurIndex + iNo].Prefix_My;
						float fS;
						if (!float.TryParse(sPrm[3], out fS)) { throw new Exception(string.Format("フォントサイズは数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						Font ft = new Font(sPrm[2], fS);
						Brush bh = Brushes.Black;
						string sAlgn = "";
						if (4 < sPrm.Length) { sAlgn = sPrm[4]; GetTextOrg(sDat, sAlgn, ft, g, ref fX, ref fY); }
						g.DrawString(sDat, ft, bh, fX, fY);
					}
					if (sCom == "text_date") { //年月日(x, y, フォント名, サイズ, Align, Format, QSO番号)
						int iNo = 0;
						if (7 <= sPrm.Length) { if (int.TryParse(sPrm[6], out iNo)) { iNo--; } }
						if (_lsQSO.Count <= iCurIndex + iNo) { continue; }
						if (_lsQSO[iCurIndex].Call != _lsQSO[iCurIndex + iNo].Call) { continue; }
						if (_lsQSO[iCurIndex + iNo].Prefix_My == "") { continue; }

						DateTime dtS = DateTime.FromBinary(_lsQSO[iCurIndex + iNo].Date_S).AddHours(_lsQSO[iCurIndex + iNo].TimeZone);
						string sFmt = "";
						if (5 < sPrm.Length) { sFmt = sPrm[5]; }
						string sDat2 = string.Format("{0:dd} {1}.", dtS, dtS.ToString("MMM", System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
						if (sFmt == "yyyy" || sFmt == "MM" || sFmt == "M" || sFmt == "dd" || sFmt == "d") { sDat2 = dtS.ToString(sFmt); }
						if (sFmt == "MMM") { sDat2 = dtS.ToString(sFmt, System.Globalization.CultureInfo.CreateSpecificCulture("en-US")); }
						float fS;
						if (!float.TryParse(sPrm[3], out fS)) { throw new Exception(string.Format("フォントサイズは数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						Font ft = new Font(sPrm[2], fS);
						Brush bh = Brushes.Black;
						string sAlgn = "";
						if (4 < sPrm.Length) { sAlgn = sPrm[4]; GetTextOrg(sDat2, sAlgn, ft, g, ref fX, ref fY); }
						g.DrawString(sDat2, ft, bh, fX, fY);
					}
					if (sCom == "text_ifdata") { //横書きテキスト #text_ifdata(x, y, フォント名, サイズ, Align, QSO番号, データの時のText, データ以外の時のText)
						int iNo = 0;
						if (6 <= sPrm.Length) { if (int.TryParse(sPrm[5], out iNo)) { iNo--; } }

						float fS;
						if (!float.TryParse(sPrm[3], out fS)) { throw new Exception(string.Format("フォントサイズは数値である必要があります。\n{0}行目、{1}", _lsCom[iC].LineNo, _lsCom[iC].Line)); }
						Font ft = new Font(sPrm[2], fS);
						Brush bh = Brushes.Black;
						string sAlgn = "";
						sDat = sPrm[7];
						string sMd = ""; if (dcDats[iNo].ContainsKey("Mode")) { sMd = dcDats[iNo]["Mode"]; }
						if (_dcMode.ContainsKey(sMd)) {
							string sCat = _dcMode[sMd].Type;
							if (sCat.ToUpper() == "DATA") { sDat = sPrm[6]; }
						}
						if (4 < sPrm.Length) { sAlgn = sPrm[4]; GetTextOrg(sDat, sAlgn, ft, g, ref fX, ref fY); }
						g.DrawString(sDat, ft, bh, fX, fY);
					}

					#endregion
				} catch(Exception ex) {
					//throw ex;
					PrintErrors.Add(ex.Message);
				}
			}

			iCurIndex += iStep;
			if (iCurIndex < _lsQSO.Count) { e.HasMorePages = true; }
			else { e.HasMorePages = false; iCurIndex = 0; }

		}

		//Alignmentに応じてテキストの原点を変更する
		private void GetTextOrg(string Str, string Align, Font FT, Graphics g, ref float X, ref float Y) {
			var pu = g.PageUnit;
			SizeF sz = g.MeasureString(Str, FT);
			if (Align.Length != 2) { return; }
			if (Align.Substring(0, 1).ToUpper() == "C") { X -= sz.Width / 2f; }
			else if (Align.Substring(0, 1).ToUpper() == "R") { X -= sz.Width; }

			if (Align.Substring(1, 1).ToUpper() == "M") { Y -= sz.Height / 2f; }
			else if (Align.Substring(1, 1).ToUpper() == "B") { Y -= sz.Height; }
		}

		private class cPrintCom {
			int _iL;
			string _sCom, _sDat, _sLine;
			public string[] _sPrms;
			public string Command { get { return (_sCom); } }
			public string[] Params { get { return (_sPrms); } }
			public string RawDate { get { return (_sDat); } }
			public int LineNo { get { return (_iL); } }
			public string Line { get { return (_sLine); } }

			public cPrintCom(string Line, int LineNo) {
				_iL = LineNo;
				_sLine = Line;
				Regex rgCom = new Regex(sComPat);
				Match mt = rgCom.Match(Line);
				if (!mt.Success) { throw new Exception("コマンド構文が不正です"); }
				if (mt.Groups.Count < 3) { throw new Exception("コマンド構文が足りません: $コマンド (パラメータ) データ"); }
				_sCom = mt.Groups[1].Value.Trim().ToLower();
				_sPrms = mt.Groups[2].Value.Split(',');
				for(int i = 0; i < _sPrms.Length; i++) { _sPrms[i] = _sPrms[i].Trim().Replace("\"", ""); }
				_sDat = "";
				if(4 <= mt.Groups.Count) { _sDat = mt.Groups[3].Value; }
			}
		}

	}
}
