using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC
{
	public static class Day2
	{
		public static void Solve()
		{
			var lines = File.ReadAllLines(@"..\..\data\day2.txt");
			CheckPolicy1(lines);
			CheckPolicy2(lines);
		}

		static void CheckPolicy1(string[] lines)
		{
			int numValidPasswords = 0;
			foreach (var line in lines)
			{
				string[] split = line.Split(':');
				Debug.Assert(split.Length == 2);

				string[] policy = split[0].Replace('-', ' ').Split(' ');
				string password = split[1].TrimStart(' ');

				int min = int.Parse(policy[0]);
				int max = int.Parse(policy[1]);
				char letter = char.Parse(policy[2]);

				int numLetters = password.Count(x => x == letter);
				if((numLetters >= min) && (numLetters <= max))
				{
					numValidPasswords++;
				}
			}

			Console.WriteLine("2-1: {0}", numValidPasswords);
		}

		static void CheckPolicy2(string[] lines)
		{
			int numValidPasswords = 0;
			foreach (var line in lines)
			{
				string[] split = line.Split(':');
				Debug.Assert(split.Length == 2);

				string[] policy = split[0].Replace('-', ' ').Split(' ');
				string password = split[1].TrimStart(' ');

				int index1 = int.Parse(policy[0]);
				int index2 = int.Parse(policy[1]);
				char letter = char.Parse(policy[2]);

				if ((password[index1 - 1] == letter) ^ (password[index2 - 1] == letter))
				{
					numValidPasswords++;
				}
			}

			Console.WriteLine("2-2: {0}", numValidPasswords);
		}
	}
}
