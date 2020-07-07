using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjOpenLog {
	public class cDxcc {
		string _sP, _sN, _sPat;
		int _iC;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="Prefix">DXCCエンティティの代表的なPrefix</param>
		/// <param name="Name">名称</param>
		/// <param name="Pattern">該当するPrefixのパターン(正規表現、前方一致:^は省略)</param>
		/// <param name="Code">DXCCのコード</param>
		public cDxcc(string Prefix, string Name, string Pattern, int Code) {
			_sP = Prefix; _sN = Name; _sPat = Pattern; _iC = Code;
		}

		public string Prefix { get { return (_sP); } set { _sP = value; } }
		public string PrefixSort {
			get {
				if (_sP.Substring(0, 1) == "*") { return ("Z" + _sP); }
				else { return ("A" + _sP); }
			}

		}
		public string Name { get { return (_sN); } set { _sN = value; } }
		public string Pattern { get { return (_sPat); } set { _sPat = value; } }
		public string[] Patterns { get { return (_sPat.Split(';')); } }
		public int Code { get { return (_iC); } set { _iC = value; } }

		public override string ToString() {
			return (string.Format("{0} {1}", _sP, _sN));
		}

	}

	public class cBand {
		string _sF, _sL;
		double _dL, _dU;
		/// <summary>
		/// 周波数帯のコンストラクタ
		/// </summary>
		/// <param name="NameF">名称(周波数)</param>
		/// <param name="NameL">名称(波長)</param>
		/// <param name="Lower">下限周波数[MHz]</param>
		/// <param name="Upper">上限周波数[MHz]</param>
		public cBand(string NameF, string NameL, double Lower, double Upper) {
			_sF = NameF; _sL = NameL; _dL = Lower; _dU = Upper;
		}
		public string NameF { get { return (_sF); } set { _sF = value; } }
		public string NameL { get { return (_sL); } set { _sL = value; } }
		public double Lower { get { return (_dL); } set { _dL = value; } }
		public double Upper { get { return (_dU); } set { _dU = value; } }
	}

	public class cCity {
		string _sCC, _sJCG, _sA, _sN, _sS;
		public cCity(string CityCode, string JCCG, string Area, string Name, string SearchStr) {
			_sCC = CityCode; _sJCG = JCCG; _sA = Area; _sN = Name; _sS = SearchStr;
		}
		public string CityCode { get { return (_sCC); }  set { _sCC = value; } }
		public string JCCG { get { return (_sJCG); } set { _sJCG = value; } }
		public string Area { get { return (_sA); } set { _sA = value; } }
		public string Name { get { return (_sN); } set { _sN = value; } }
		public string SearchStr { get { return (_sS); } set { _sS = value; } }
	}

	public class cMode {
		string _sM, _sC, _sT;
		public cMode(string Mode, string Category, string Type) { _sM = Mode; _sC = Category; _sT = Type; }
		public string Mode { get { return (_sM); } set { _sM = value; } }
		public string Category { get { return (_sC); } set { _sC = value; } }
		public string Type { get { return (_sT); } set { _sT = value; } }
	}

	public class cDefaultRig {
		string _sB, _sRh, _sAh, _sRm, _sAm;
		public cDefaultRig(string BandF, string RigHome, string AntHome, string RigMobile, string AntMobile) {
			_sB = BandF; _sRh = RigHome; _sAh = AntHome; _sRm = RigMobile; _sAm = AntMobile;
		}
		public cDefaultRig(string BandF) { _sB = BandF; _sRh = ""; _sAh = ""; _sRm = ""; _sAm = ""; }
		public string BandF { get { return (_sB); } }
		public string RigHome { get { return (_sRh); } set { _sRh = value; } }
		public string AntHome { get { return (_sAh); } set { _sAh = value; } }
		public string RigMobile { get { return (_sRm); } set { _sRm = value; } }
		public string AntMobile { get { return (_sAm); } set { _sAm = value; } }

	}

}
