using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
	public static class Day11
	{
		public static void Solve()
		{
			var rows = File.ReadAllLines(@"../../../data/day11.txt");
			var seats = new List<List<char>>();
			foreach (var row in rows)
			{
				seats.Add(row.ToCharArray().ToList());
			}

			Part1(seats);
			Part2(seats);
		}

		static List<List<char>> CopySeats(List<List<char>> seats)
		{
			var copy = new List<List<char>>();
			foreach (var row in seats)
			{
				var newRow = new List<char>();
				newRow.AddRange(row);

				copy.Add(newRow);
			}

			return copy;
		}

		static int CountTotalOccupiedSeats(List<List<char>> seats)
		{
			int occupied = 0;
			foreach (var row in seats)
			{
				foreach (var seat in row)
				{
					if (seat == '#')
					{
						occupied++;
					}
				}
			}

			return occupied;
		}

		static int CountOccupiedAdjacentSeats(List<List<char>> seats, int row, int column)
		{
			int numRows = seats.Count;
			int numColumns = seats[row].Count;

			int startRow = Math.Clamp(row - 1, 0, numRows - 1);
			int endRow = Math.Clamp(row + 1, 0, numRows - 1);

			int startColumn = Math.Clamp(column - 1, 0, numColumns - 1);
			int endColumn = Math.Clamp(column + 1, 0, numColumns - 1);

			int occupied = 0;
			for (int i = startRow; i <= endRow; ++i)
			{
				for (int j = startColumn; j <= endColumn; ++j)
				{
					if ((i != row || j != column) && seats[i][j] == '#')
					{
						occupied++;
					}
				}
			}

			return occupied;
		}

		static int CheckDirection(List<List<char>> seats, int row, int column, int x, int y)
		{
			if (x == 0)
			{
				for (int i = row + y; i >= 0 && i < seats.Count; i += y)
				{
					if (seats[i][column] == '#')
					{
						return 1;
					}
					else if (seats[i][column] == 'L')
					{
						return 0;
					}
				}
			}
			else if (y == 0)
			{
				for (int j = column + x; j >= 0 && j < seats[row].Count; j += x)
				{
					if (seats[row][j] == '#')
					{
						return 1;
					}
					else if (seats[row][j] == 'L')
					{
						return 0;
					}
				}
			}
			else 
			{
				int i = i = row + y;
				int j = column + x;

				while (i >= 0 && i < seats.Count && j >= 0 && j < seats[i].Count)
				{
					if (seats[i][j] == '#')
					{
						return 1;
					}
					else if (seats[i][j] == 'L')
					{
						return 0;
					}

					i += y;
					j += x;
				}
			}

			return 0;
		}

		static int CountOccupiedVisibleSeats(List<List<char>> seats, int row, int column)
		{
			int occupied = 0;

			occupied += CheckDirection(seats, row, column, -1, -1);
			occupied += CheckDirection(seats, row, column,  0, -1);
			occupied += CheckDirection(seats, row, column,  1, -1);
			occupied += CheckDirection(seats, row, column,  1,  0);
			occupied += CheckDirection(seats, row, column,  1,  1);
			occupied += CheckDirection(seats, row, column,  0,  1);
			occupied += CheckDirection(seats, row, column, -1,  1);
			occupied += CheckDirection(seats, row, column, -1,  0);

			return occupied;
		}

		static bool ProcessRules(List<List<char>> seats, int threshold, Func<List<List<char>>, int, int, int> occupancyCheck)
		{
			var copy = CopySeats(seats);

			bool bStateChanged = false;
			for (int i = 0; i < seats.Count; ++i)
			{
				for (int j = 0; j < seats[i].Count; ++j)
				{
					int occupied = occupancyCheck(copy, i, j);
					if (copy[i][j] == 'L' && occupied == 0)
					{
						seats[i][j] = '#';
						bStateChanged = true;
					}
					else if (copy[i][j] == '#' && occupied >= threshold)
					{
						seats[i][j] = 'L';
						bStateChanged = true;
					}
				}
			}

			return bStateChanged;
		}

		

		static void Part1(List<List<char>> seats)
		{
			var copy = CopySeats(seats);

			bool bSeatsChanged = true;
			while (bSeatsChanged)
			{
				bSeatsChanged = ProcessRules(copy, 4, CountOccupiedAdjacentSeats);
			}

			int occupied = CountTotalOccupiedSeats(copy);

			Console.WriteLine("11-1: {0}", occupied);
		}

		static void Part2(List<List<char>> seats)
		{
			var copy = CopySeats(seats);

			bool bSeatsChanged = true;
			while (bSeatsChanged)
			{
				bSeatsChanged = ProcessRules(copy, 5, CountOccupiedVisibleSeats);
			}

			int occupied = CountTotalOccupiedSeats(copy);

			Console.WriteLine("11-2: {0}", occupied);
		}
	}
}