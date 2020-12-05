using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC
{
	public static class Day4
	{
		public static void Solve()
		{
			var lines = File.ReadAllLines(@"..\..\..\data\day4.txt");
			CheckPassports(lines);
		}

		static bool PassportHasRequiredFields(Dictionary<string, string> passport)
		{
			bool bValid = true;

			bValid &= passport.Keys.Contains("byr");
			bValid &= passport.Keys.Contains("iyr");
			bValid &= passport.Keys.Contains("eyr");
			bValid &= passport.Keys.Contains("hgt");
			bValid &= passport.Keys.Contains("hcl");
			bValid &= passport.Keys.Contains("ecl");
			bValid &= passport.Keys.Contains("pid");

			return bValid;
		}

		static bool PassportIsValid(Dictionary<string, string> passport)
		{
			bool bValid = true;

			int byr = int.Parse(passport["byr"]);
			bValid &= (byr >= 1920 && byr <= 2002);

			int iyr = int.Parse(passport["iyr"]);
			bValid &= (iyr >= 2010 && iyr <= 2020);

			int eyr = int.Parse(passport["eyr"]);
			bValid &= (eyr >= 2020 && eyr <= 2030);

			string hgt = passport["hgt"];
			if (hgt.EndsWith("cm"))
			{
				int height = int.Parse(hgt.Replace("cm", ""));
				bValid &= (height >= 150 && height <= 193);
			}
			else if (hgt.EndsWith("in"))
			{
				int height = int.Parse(hgt.Replace("in", ""));
				bValid &= (height >= 59 && height <= 76);
			}
			else
			{
				bValid = false;
			}

			string hcl = passport["hcl"];
			Regex hairRegex = new Regex("^#[a-f0-9]{6,}$");
			bValid &= hairRegex.IsMatch(hcl);

			string ecl = passport["ecl"];
			Regex eyeRegex = new Regex("(amb|blu|brn|gry|grn|hzl|oth)");
			bValid &= eyeRegex.IsMatch(ecl);

			string pid = passport["pid"];
			Regex pidRegex = new Regex("^[0-9]{9}$");
			bValid &= pidRegex.IsMatch(pid);

			return bValid;
		}

		static void CheckPassports(string[] lines)
		{
			int i = 0;
			int numValidPassports = 0;
			int numStrictValidPassports = 0;
			var passport = new Dictionary<string, string>();

			while (i < lines.Length)
			{
				var line = lines[i];
				if (line != "")
				{
					string[] splits = line.Split(' ');
					foreach (var entry in splits)
					{
						string[] pair = entry.Split(':');
						passport.Add(pair[0], pair[1]);
					}
				}

				i++;
				if (line == "" || i == lines.Length)
				{
					if (PassportHasRequiredFields(passport))
					{
						numValidPassports++;

						if (PassportIsValid(passport))
						{
							numStrictValidPassports++;
						}
					}

					passport.Clear();
				}
			}

			Console.WriteLine("4-1: {0}", numValidPassports);
			Console.WriteLine("4-2: {0}", numStrictValidPassports);
		}
	}
}
