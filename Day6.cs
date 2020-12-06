using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC
{
	public static class Day6
	{
		public static void Solve()
		{
			var text = File.ReadAllText(@"../../../data/day6.txt");
			var groups = text.Split("\r\n\r\n");

			CheckGroups(groups);
		}

		static void CheckGroups(string[] groups)
		{
			int totalAnswers = 0;
			int totalAnsweredByAll = 0;

			foreach (var group in groups)
			{
				var entries = group.Split("\r\n");
				var answers = new Dictionary<char, int>();

				foreach (var entry in entries)
				{
					for (int i = 0; i < entry.Length; ++i)
					{
						char letter = entry[i];
						if (!answers.ContainsKey(letter))
						{
							answers[letter] = 1;
						}
						else
						{
							answers[letter]++;
						}
					}
				}

				totalAnswers += answers.Keys.Count;
				totalAnsweredByAll += answers.Count(a => a.Value == entries.Length);
			}

			Console.WriteLine("6-1: {0}", totalAnswers);
			Console.WriteLine("6-2: {0}", totalAnsweredByAll);
		}
	}
}