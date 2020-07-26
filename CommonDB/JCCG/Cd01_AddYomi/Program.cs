using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Cd01_AddYomi {
	class Program {
		const string sInPref = @"D:\VCsharp\prjOpenLog\地名リスト\JCCG\県番号_総務省JARL.csv";
		const string sInJARL = @"D:\VCsharp\prjOpenLog\地名リスト\JCCG\よみ_1JARL.csv";
		const string sInSomu = @"D:\VCsharp\prjOpenLog\地名リスト\JCCG\よみ_2総務省.csv";
		const string sInHamL = @"D:\VCsharp\prjOpenLog\地名リスト\JCCG\00_Hamlog.csv";
		const string sOut = @"D:\VCsharp\prjOpenLog\地名リスト\JCCG\T_JCCG.csv";

		static void Main(string[] args) {
			Encoding sjis = Encoding.GetEncoding(932);

			Dictionary<string, string> dcPCode = new Dictionary<string, string>(); //総務省→JARL
			Dictionary<string, string[]> dcPref = new Dictionary<string, string[]>();
			#region "都道府県"
			using (StreamReader sr = new StreamReader(sInPref, sjis)) {
				sr.ReadLine();
				while (-1 < sr.Peek()) {
					string[] sL = sr.ReadLine().Split(',');
					dcPCode.Add(sL[1], sL[2]);
					dcPref.Add(sL[2], new string[] { sL[0], sL[3] });
				}
			}
			#endregion

			Dictionary<string, string> dcJARL = new Dictionary<string, string>();
			#region "JARL"
			using (StreamReader sr = new StreamReader(sInJARL, sjis)) {
				sr.ReadLine();
				while (-1 < sr.Peek()) {
					string[] sL = sr.ReadLine().Split(',');
					if(sL[1] == "") { continue; }
					dcJARL.Add(sL[1], sL[2]);
				}
			}
			#endregion

			Dictionary<string, Dictionary<string, string>> dcSomu = new Dictionary<string, Dictionary<string, string>>();
			#region "総務省"
			using (StreamReader sr = new StreamReader(sInSomu, sjis)) {
				sr.ReadLine();
				while (-1 < sr.Peek()) {
					string[] sL = sr.ReadLine().Split(',');
					if(sL[2] == "") { continue; }
					string sP = dcPCode[sL[0].Substring(0, 2)];
					if (!dcSomu.ContainsKey(sP)) { dcSomu.Add(sP, new Dictionary<string, string>()); }
					if (!dcSomu[sP].ContainsKey(sL[1])) { dcSomu[sP].Add(sL[1], sL[2]); }
				}
			}
			#endregion

			int iL = 0;
			#region "Hamlog→読み"
			using(StreamWriter sw = new StreamWriter(sOut, false, sjis))
			using (StreamReader sr = new StreamReader(sInHamL, sjis)) {
				sr.ReadLine();
				sw.WriteLine("DCode,Code2,Prifix,Name,Search,Flag");
				Regex reOld = new Regex(@"([^\s]+)\s*\*(\d+)/(\d+)$");
				while (-1 < sr.Peek()) {
					string[] sL = sr.ReadLine().Split(',');
					string sDC = sL[0];
					string sC2 = sL[0]; if(Regex.IsMatch(sC2, "[A-Z]$")) { sC2 = sC2.Substring(0, sC2.Length- 1); }
					string sP = sL[0].Substring(0, 2);
					string sName = sL[1];
					string sName2 = sL[1];
					string sSearch = sL[2];
					string sArea = sL[3];
					Match mt = reOld.Match(sName);
					if (mt.Success) {
						//廃止
						int iY = int.Parse(mt.Groups[2].Value);
						if(iY < 30) { iY += 2000; }
						else { iY += 1900; }
						int iM = int.Parse(mt.Groups[3].Value);
						sName = string.Format("*{0} ({1}.{2})", mt.Groups[1].Value, iY, iM);
						sName2 = mt.Groups[1].Value;
					}

					int iFlg = 0;
					if (dcJARL.ContainsKey(sDC)) { iFlg = 1; sSearch = dcJARL[sDC]; }

					foreach (string sN in dcSomu[sP].Keys) {
						string s1st = dcSomu[sP][sN].Substring(0, 1);
						if (sL[2] != s1st) { continue; }
						if(sName2.Length < sN.Length) { continue; }
						string sCN = sName2.Substring(sName2.Length - sN.Length, sN.Length);
						if(string.Compare(sCN, sN) == 0) { iFlg = 2; sSearch = dcSomu[sP][sN]; }
					}

					//強制入力
					if(sDC == "03001A") { sSearch = "かねがさきちょう";  iFlg = 3; }
					if (sDC == "39004J") { sSearch = "ゆすはらちょう"; iFlg = 3; }

					sw.WriteLine("{0},{1},{2},{3},{4},{5}", sDC, sC2, sArea, sName, sSearch.Replace("'",""), iFlg);

				}
			}
			#endregion

		}
	}
}
