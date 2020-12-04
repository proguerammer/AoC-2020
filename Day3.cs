using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC
{
	public static class Day3
	{
		public static void Solve()
		{
			var lines = File.ReadAllLines(@"..\..\data\day3.txt");
			Part1(lines);
			Part2(lines);
		}

		static int CheckSlope(string[] lines, int slopeX, int slopeY)
		{
			int width = lines[0].Length;
			int numTrees = 0;
			
			int x = slopeX;
			int y = slopeY;

			while (y < lines.Length)
			{				
				int wrapX = x % width;

				if (lines[y][wrapX] == '#')
				{
					numTrees++;
				}

				x += slopeX;
				y += slopeY;
			}

			return numTrees;
		}

		static void Part1(string[] lines)
		{
			int numTrees = CheckSlope(lines, 3, 1);
			Console.WriteLine("3-1: {0}", numTrees);
		}

		static void Part2(string[] lines)
		{
			long numTrees = CheckSlope(lines, 1, 1);
			numTrees *= CheckSlope(lines, 3, 1);
			numTrees *= CheckSlope(lines, 5, 1);
			numTrees *= CheckSlope(lines, 7, 1);
			numTrees *= CheckSlope(lines, 1, 2);

			Console.WriteLine("3-2: {0}", numTrees);
		}
	}
}
