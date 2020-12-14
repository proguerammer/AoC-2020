using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public static class Day10
	{
		public static void Solve()
		{
			var adapters = File.ReadAllLines(@"../../../data/day10.txt").Select(n => int.Parse(n)).OrderBy(n => n).ToList();
			adapters.Add(adapters[adapters.Count - 1] + 3);

			Part1(adapters);
			Part2(adapters);
		}

		static void Part1(List<int> adapters)
		{
			int[] diffs = { 0, 0, 0, 0 };
			int previous = 0;

			for (int i = 0; i < adapters.Count; ++i)
			{
				int diff = adapters[i] - previous;
				diffs[diff]++;
				previous = adapters[i];
			}

			Console.WriteLine("10-1: {0}", diffs[1] * diffs[3]);
		}

		static long CountPaths(int adapter, List<int> adapters, int index, Dictionary<int, long> pathCache)
		{
			long numPaths = 0;

			if (pathCache.ContainsKey(adapter))
			{
				return pathCache[adapter];
			}

			for (int i = index; i < adapters.Count; ++i)
			{
				if ((adapters[i] - adapter) <= 3)
				{
					var next = adapters[i];
					if (next == adapters.Last())
					{
						numPaths = 1;
					}
					else
					{
						numPaths += CountPaths(next, adapters, i + 1, pathCache);
					}
				}
			}

			pathCache[adapter] = numPaths;

			return numPaths;
		}

		static void Part2(List<int> adapters)
		{
			var pathCache = new Dictionary<int, long>();
			pathCache[0] = CountPaths(0, adapters, 0, pathCache);

			Console.WriteLine("10-2: {0}", pathCache[0]);
		}
	}
}