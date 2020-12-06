using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC
{
	public static class Day5
	{
		public static void Solve()
		{
			var lines = File.ReadAllLines(@"../../../data/day5.txt");
			CheckSeats(lines);
			FindMySeat(lines);
		}

		static int CalculateSeatId(string line)
		{
			string binstr = line.Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1');

			int row = 0;
			row += (int)(binstr[0] - '0') * 64;
			row += (int)(binstr[1] - '0') * 32;
			row += (int)(binstr[2] - '0') * 16;
			row += (int)(binstr[3] - '0') * 8;
			row += (int)(binstr[4] - '0') * 4;
			row += (int)(binstr[5] - '0') * 2;
			row += (int)(binstr[6] - '0') * 1;

			int column = 0;
			column += (int)(binstr[7] - '0') * 4;
			column += (int)(binstr[8] - '0') * 2;
			column += (int)(binstr[9] - '0') * 1;

			return (row * 8) + column;
		}

		static void CheckSeats(string[] lines)
		{
			int maxSeatId = 0;
			foreach(var line in lines)
			{
				maxSeatId = Math.Max(maxSeatId, CalculateSeatId(line));
			}

			Console.WriteLine("5-1: {0}", maxSeatId);
		}

		static void FindMySeat(string[] lines)
		{
			var seats = new List<int>();
			foreach (var line in lines)
			{
				seats.Add(CalculateSeatId(line));
			}

			seats.Sort();

			int seatId = 0;
			for (int i = 0; i < seats.Count - 1; ++i)
			{
				if ((seats[i] + 1) != seats[i + 1])
				{
					seatId = seats[i] + 1;
					break;
				}
			}

			Console.WriteLine("5-2: {0}", seatId);
		}
	}
}