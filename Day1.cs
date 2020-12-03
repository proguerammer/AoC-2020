using System;
using System.IO;

namespace AoC
{
	public static class Day1
	{
		public static void Solve()
		{
			var lines = File.ReadAllLines(@"..\..\data\day1.txt");
			CheckForTwo(lines);
			CheckForThree(lines);
		}

		static void CheckForTwo(string[] lines)
		{
			for (int i = 0; i < lines.Length; ++i)
			{
				int v1 = int.Parse(lines[i]);
				for (int j = i + 1; j < lines.Length; ++j)
				{
					int v2 = int.Parse(lines[j]);
					if ((v1 + v2) == 2020)
					{
						Console.WriteLine("1-1: {0}", v1 * v2);
					}
				}
			}
		}

		static void CheckForThree(string[] lines)
		{
			for (int i = 0; i < lines.Length; ++i)
			{
				int v1 = int.Parse(lines[i]);
				for (int j = i + 1; j < lines.Length; ++j)
				{
					int v2 = int.Parse(lines[j]);
					for (int k = j + 1; k < lines.Length; ++k)
					{
						int v3 = int.Parse(lines[k]);
						if ((v1 + v2 + v3) == 2020)
						{
							Console.WriteLine("1-2: {0}", v1 * v2 * v3);
						}
					}

				}
			}
		}
	}
}
