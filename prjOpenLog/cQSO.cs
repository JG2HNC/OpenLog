using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //INotifyPropertyChanged実装用
using System.Runtime.CompilerServices;

namespace prjOpenLog {
	public class cQSO : INotifyPropertyChanged {
		#region "フィールド変数"
		bool _bCard_Send, _bCard_Resv, _bExcept;
		double _dFreq, _dPwr_My, _dPwr_His;
		int _id, _iRS_His, _iRS_My, _iTimeZone;
		enQSLMethod _eQSLMethod;
		long _lDate_S, _lDate_E, _lLastUpdate;
		string _sCall, _sPrefix1, _sPrefix2, _sQRA, _sBand, _sDXCC, _sCityCode, _sGL, _sQTH, _sQTH_h, _sMode, _sQSLManager;
		string _sRig_His, _sAnt_His, _sRig_My, _sAnt_My, _sQTH_My, _sPrefix_My, _sCityCode_My, _sGL_My, _sCardMsg, _sRemark;
		#endregion
		/// <summary>
		/// カード交換方式(B:Bureau, D:Direct(SASE), M:Manager, N:NoQSL, R:1way(Receive only), X:NA
		/// </summary>
		public enum enQSLMethod { B = 1, D = 2, M = 3, N = 4, R = 5, X = 0};

		#region "プロパティ"
		/// <summary>
		/// QSOを識別するためのID、新規の時は負の値とする(DB保存時に値を設定)
		/// </summary>
		public int ID { get { return (_id); } set { _id = value; RaisePropertyChanged(); } }

		/// <summary>
		/// QSL交換方法(表示用)
		/// </summary>
		public string ScreenQSLMethod { get { return (_eQSLMethod.ToString()); } }

		/// <summary>
		/// カード受領済み(表示用)
		/// </summary>
		public string ScreenCardSend { get {
				string s = "";
				if (_bCard_Send) { s = "*";}
				return (s);
			}
		}

		/// <summary>
		/// カード発送済み(表示用)
		/// </summary>
		public string ScreenCardReceive {
			get {
				string s = "";
				if (_bCard_Resv) { s = "*"; }
				return (s);
			}
		}

		/// <summary>
		/// 交信日付(表示用)
		/// </summary>
		public string ScreenDate {
			get {
				DateTime dt = DateTime.FromBinary(_lDate_S).AddHours(_iTimeZone);
				return (string.Format("{0:yyyy/MM/dd}", dt));
			}
		}

		/// <summary>
		/// 交信時刻(表示用)
		/// </summary>
		public string ScreenTime {
			get {
				DateTime dt = DateTime.FromBinary(_lDate_S).AddHours(_iTimeZone);
				return (string.Format("{0:HH}:{0:mm}:{0:ss}", dt));
			}
		}

		/// <summary>
		/// タイムゾーン(0→UTC、9→JST、それ以外→UTCとの時差)
		/// </summary>
		public string ScreenTimeZone {
			get {
				string sZ = _iTimeZone.ToString("+#;-#;");
				if (_iTimeZone == 9) { sZ = "JST"; }
				else if (_iTimeZone == 0) { sZ = "UTC"; }
				return (sZ);
			}
		}

		/// <summary>
		/// TimeZone(0:UTC, +9:JST)・・・制限:インド・ネパールは考慮していない
		/// </summary>
		public int TimeZone { get { return (_iTimeZone); } set { _iTimeZone = value; RaisePropertyChanged(); } }

		/// <summary>
		/// コールサイン(表示用)
		/// </summary>
		public string ScreenCall { get {
				string sCall = _sCall;
				if(_sPrefix1 != "") { sCall = _sPrefix1 + "/ " + sCall; }
				if (_sPrefix2 != "") { sCall = _sCall + " /" + _sPrefix2; }
				return (sCall);
			}
		}

		/// <summary>
		/// HisRST
		/// </summary>
		public int RS_His { get { return (_iRS_His); } set { _iRS_His = value; RaisePropertyChanged(); } }

		/// <summary>
		/// MyRST
		/// </summary>
		public int RS_My { get { return (_iRS_My); } set { _iRS_My = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 周波数[MHz]
		/// </summary>
		public double Freq { get { return (_dFreq); } set { _dFreq = value; RaisePropertyChanged(); } }

		/// <summary>
		/// Band
		/// </summary>
		public string Band { get { return (_sBand); } set { _sBand = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 電波形式
		/// </summary>
		public string Mode { get { return (_sMode); } set { _sMode = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 出力(自局)
		/// </summary>
		public string ScreenPwr_My {
			get {
				if (0d < _dPwr_My) { return (_dPwr_My.ToString()); }
				else { return (""); }
			}
		}

		/// <summary>
		/// 相手名前
		/// </summary>
		public string QRA { get { return (_sQRA); } set { _sQRA = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 相手局QTH(運用場所)
		/// </summary>
		public string QTH { get { return (_sQTH); } set { _sQTH = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 相手局のDXCCエンティティ(主要Prefix 例:JA)
		/// </summary>
		public string DXCC { get { return (_sDXCC); } set { _sDXCC = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 郡市区町村番号(JCC/JCG+町)
		/// </summary>
		public string CityCode { get { return (_sCityCode); } set { _sCityCode = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 相手局GL
		/// </summary>
		public string GL { get { return (_sGL); } set { _sGL = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 相手局常置場所
		/// </summary>
		public string QTH_h { get { return (_sQTH_h); } set { _sQTH_h = value; RaisePropertyChanged(); } }

		/// <summary>
		/// QSLマネージャー
		/// </summary>
		public string QSLManager { get { return (_sQSLManager); } set { _sQSLManager = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 相手RIG
		/// </summary>
		public string Rig_His { get { return (_sRig_His); } set { _sRig_His = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 相手Ant
		/// </summary>
		public string Ant_His { get { return (_sAnt_His); } set { _sAnt_His = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 出力(自局)
		/// </summary>
		public string ScreenPwr_His {
			get {
				if (0d < _dPwr_His) { return (_dPwr_His.ToString()); }
				else { return (""); }
			}
		}

		/// <summary>
		/// 自局Rig
		/// </summary>
		public string Rig_My { get { return (_sRig_My); } set { _sRig_My = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 自局Ant
		/// </summary>
		public string Ant_My { get { return (_sAnt_My); } set { _sAnt_My = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 自局QTH(移動地)
		/// </summary>
		public string QTH_My { get { return (_sQTH_My); } set { _sQTH_My = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 自局移動エリア
		/// </summary>
		public string Prefix_My { get { return (_sPrefix_My); } set { _sPrefix_My = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 自局JCC/JCG
		/// </summary>
		public string CityCode_My { get { return (_sCityCode_My); } set { _sCityCode_My = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 自局GL
		/// </summary>
		public string GL_My { get { return (_sGL_My); } set { _sGL_My = value; RaisePropertyChanged(); } }

		/// <summary>
		/// カードメッセージ
		/// </summary>
		public string CardMsg { get { return (_sCardMsg); } set { _sCardMsg = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 備考
		/// </summary>
		public string Remarks { get { return (_sRemark); } set { _sRemark = value; RaisePropertyChanged(); } }

		//メインフォームには表示しない項目(別Propertyを表示)
		/// <summary>
		/// 相手移動エリア(DXCC・前置き)
		/// </summary>
		public string Prefix1 { get { return (_sPrefix1); }set { _sPrefix1 = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ScreenCall)); }  }

		/// <summary>
		/// 相手移動エリア(後置き)
		/// </summary>
		public string Prefix2 { get { return (_sPrefix2); } set { _sPrefix2 = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ScreenCall)); } }

		/// <summary>
		/// 相手コールサイン
		/// </summary>
		public string Call { get { return (_sCall); } set { _sCall = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ScreenCall)); } }

		/// <summary>
		/// 開始時刻(Ticks, UTC)
		/// </summary>
		public long Date_S { get { return (_lDate_S); } set { _lDate_S = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ScreenDate)); } }

		/// <summary>
		/// 終了時刻(Ticks, UTC)
		/// </summary>
		public long Date_E { get { return (_lDate_E); } set { _lDate_E = value; RaisePropertyChanged(); } }
		
		/// <summary>
		/// カード交換方式
		/// </summary>
		public int QSLMethod {
			get { return ((int)_eQSLMethod); }
			set {
				if (1 <= value && value <= 4) { _eQSLMethod = (enQSLMethod)Enum.ToObject(typeof(enQSLMethod), value); }
				else { _eQSLMethod = enQSLMethod.X; }
				RaisePropertyChanged();
			}
		}

		/// <summary>
		/// 出力(自局)
		/// </summary>
		public double Pwr_My { get { return (_dPwr_My); } set { _dPwr_My = value; RaisePropertyChanged(); } }

		/// <summary>
		/// 相手出力
		/// </summary>
		public double Pwr_His { get { return (_dPwr_His); } set { _dPwr_His = value; RaisePropertyChanged(); } }

		///0:未発送、1:発送済
		public bool Card_Send { get { return (_bCard_Send); } set { _bCard_Send = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ScreenCardSend)); } }
		
		///0:未受領、1:受領済
		public bool Card_Resv { get { return (_bCard_Resv); } set { _bCard_Resv = value; RaisePropertyChanged(); RaisePropertyChanged(nameof(ScreenCardReceive)); } }
		
		///1:Wkd/Cfm除外
		public bool Except { get { return (_bExcept); } set { _bExcept = value; RaisePropertyChanged(); } }
		
		///最終更新時刻(Ticks, UTC)
		public long LastUpdate { get { return (_lLastUpdate); } set { _lLastUpdate = value; RaisePropertyChanged(); } }

		/// <summary>
		/// QSL転送欄のアドレス
		/// </summary>
		public string CallQSL {
			get {
				string sC = _sCall;
				if(QSLMethod == (int)enQSLMethod.M && _sQSLManager != "") { sC = _sQSLManager; }
				return (sC);
			}
		}

		/// <summary>
		/// 交信時刻(表示用)
		/// </summary>
		public string Time_HHmm {
			get {
				DateTime dt = DateTime.FromBinary(_lDate_S).AddHours(_iTimeZone);
				return (string.Format("{0:HH}:{0:mm}", dt));
			}
		}
		#endregion

		#region "コンストラクタ"
		public cQSO() {
			_id = -1;　_bCard_Send = false; _bCard_Resv = false; _bExcept = false;
			_iRS_His = 59;
			_iRS_My = 59;
			_sMode = "FM"; _dFreq = 430d; _dPwr_My = -1d;
			_iTimeZone = (DateTime.Now - DateTime.UtcNow).Hours; _eQSLMethod = enQSLMethod.B;
			_lDate_S = DateTime.UtcNow.Ticks; _lDate_E =-1; _lLastUpdate = DateTime.UtcNow.Ticks; _dPwr_His = -1d;
			_sPrefix1 = ""; _sCall = ""; _sPrefix2 = ""; _sQRA = ""; _sDXCC = ""; _sCityCode = ""; _sGL = ""; _sQTH = ""; _sQTH_h = ""; _sMode = ""; _sQSLManager = ""; _sRig_His = ""; _sAnt_His = "";  _sRig_My = ""; _sAnt_My = ""; _sQTH_My = ""; _sPrefix_My = ""; _sCityCode_My = ""; _sGL_My = ""; _sCardMsg = ""; _sRemark = "";
		}

		/// <summary>
		/// 最新QSOの周波数・形式・出力・自局情報を継承する
		/// </summary>
		/// <param name="LastQSO"></param>
		public cQSO(cQSO LastQSO) {
			_id = -1; _dFreq = LastQSO.Freq; _dPwr_My = LastQSO.Pwr_My; _sMode = LastQSO.Mode;
			_bCard_Send = false; _bCard_Resv = false; _bExcept = false;
			_iRS_His = 59;
			_iRS_My = 59;
			_iTimeZone = (DateTime.Now - DateTime.UtcNow).Hours; _eQSLMethod = enQSLMethod.B;
			_lDate_S = DateTime.UtcNow.Ticks; _lDate_E = -1 ; _lLastUpdate = DateTime.UtcNow.Ticks; ;
			_sPrefix1 = ""; _sCall = ""; _sPrefix2 = ""; _sQRA = ""; _sDXCC = ""; _sCityCode = ""; _sGL = ""; _sQTH = ""; _sQTH_h = ""; _sQSLManager = ""; _sRig_His = ""; _sAnt_His = "";
			_sRig_My = LastQSO.Rig_My; _sAnt_My = LastQSO.Ant_My; _sQTH_My = LastQSO.QTH_My; _sPrefix_My = LastQSO.Prefix_My; _sCityCode_My = LastQSO.CityCode_My; _sGL_My = LastQSO.GL_My; _sCardMsg = ""; _sRemark = "";
		}
		#endregion

		public void SetFreq(double Freq, Dictionary<string, cBand> Band) {
			_dFreq = Freq;
			_sBand = "N/A";
			foreach(string sB in Band.Keys) {
				if(Band[sB].Lower <= _dFreq && _dFreq <= Band[sB].Upper) { _sBand = sB; return; }
			}
		}

		#region "INotifyPropertyChanged実装(DataGridViewのリアルタイム更新)"
		//参考→https://qiita.com/soi/items/d0c83a0cc3a4b23237ef (C#5版+C#6版)
		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged([CallerMemberName]string propertyName = null) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion

	}
}
