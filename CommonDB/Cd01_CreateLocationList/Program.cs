using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace Cd01_CreateLocationList {
	class Program {
		const string sInDX = @"D:\VCsharp\プロパティリスト\DXCC\T_DXCC.tsv";
		const string sInDM = @"D:\VCsharp\プロパティリスト\JCCG\T_JCCG.csv";
		const string sInBD = @"D:\VCsharp\プロパティリスト\BAND\T_BAND.csv";
		const string sInMD = @"D:\VCsharp\プロパティリスト\MODE\T_Mode.csv";
		const string sOut = @"D:\VCsharp\プロパティリスト\PropertyList.db";

		static void Main(string[] args) {
			Encoding sjis = Encoding.GetEncoding(932);
			if (File.Exists(sOut)) { File.Delete(sOut); }

			using (SQLiteConnection con = new SQLiteConnection(string.Format("Data Source={0};Version=3;", sOut))) {
				con.Open();
				using(SQLiteTransaction st = con.BeginTransaction())
				using (SQLiteCommand cmd = con.CreateCommand()) {
					cmd.CommandText = "CREATE TABLE IF NOT EXISTS [T_DXCC](DXCC text PRIMARY KEY, Name text, Pattern text, EntityCode integer);";// WITHOUT ROWID;";
					cmd.ExecuteNonQuery();
					using(StreamReader sr = new StreamReader(sInDX, sjis)) {
						sr.ReadLine();
						while(-1 < sr.Peek()) {
							string[] sL = sr.ReadLine().Split('\t');
							cmd.CommandText = string.Format("INSERT INTO [T_DXCC](DXCC, Pattern, Name, EntityCode) VALUES('{0}', '{1}', '{2}', '{3}');", sL[0], sL[1], sL[2], sL[3]);
							cmd.ExecuteNonQuery();
						}
					}

					cmd.CommandText = "CREATE TABLE IF NOT EXISTS [T_City](CityCode text PRIMARY KEY, JCCG text, Area text, Name text, Search text);";// WITHOUT ROWID;";
					cmd.ExecuteNonQuery();
					using (StreamReader sr = new StreamReader(sInDM, sjis)) {
						sr.ReadLine();
						while (-1 < sr.Peek()) {
							string[] sL = sr.ReadLine().Split(',');
							cmd.CommandText = string.Format("INSERT INTO [T_City](CityCode, JCCG, Area, Name, Search) VALUES('{0}', '{1}', '{2}', '{3}', '{4}');", sL[0], sL[1], sL[2], sL[3], sL[4]);
							cmd.ExecuteNonQuery();
						}
					}

					cmd.CommandText = "CREATE TABLE IF NOT EXISTS [T_Band](BandF text PRIMARY KEY, BandL text, Lower real, Upper real);";// WITHOUT ROWID;";
					cmd.ExecuteNonQuery();
					using (StreamReader sr = new StreamReader(sInBD, sjis)) {
						sr.ReadLine();
						while (-1 < sr.Peek()) {
							string[] sL = sr.ReadLine().Split(',');
							cmd.CommandText = string.Format("INSERT INTO [T_Band](BandF, BandL, Lower, Upper) VALUES('{0}', '{1}', '{2:f4}', '{3:f4}');", sL[0], sL[1], sL[2], sL[3]);
							cmd.ExecuteNonQuery();
						}
					}

					cmd.CommandText = "CREATE TABLE IF NOT EXISTS [T_Mode](Mode text PRIMARY KEY, Category text, Type text);";// WITHOUT ROWID;";
					cmd.ExecuteNonQuery();
					using (StreamReader sr = new StreamReader(sInMD, sjis)) {
						sr.ReadLine();
						while (-1 < sr.Peek()) {
							string[] sL = sr.ReadLine().Split(',');
							cmd.CommandText = string.Format("INSERT INTO [T_Mode](Mode, Category, Type) VALUES('{0}', '{1}', '{2}');", sL[0], sL[1], sL[2]);
							cmd.ExecuteNonQuery();
						}
					}

					st.Commit();
				}
				con.Close();
			}
		}
	}
}
