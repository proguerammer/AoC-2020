using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC
{
	public static class Day9
	{
		public static void Solve()
		{
			var numbers = File.ReadAllLines(@"../../../data/day9.txt").Select(n => long.Parse(n)).ToArray();

			long invalidNumber = Part1(numbers);
			long weakness = Part2(numbers, invalidNumber);
		}

		static long Part1(long[] numbers)
		{
			long invalidNumber = 0;
			for (int i = 25; i < numbers.Length; ++i)
			{
				bool bFoundSum = false;
				for (int j = i - 25; j < i; ++j)
				{
					for (int k = j + 1; k < i; ++k)
					{
						bFoundSum |= (numbers[j] + numbers[k]) == numbers[i];
					}
				}

				if (!bFoundSum)
				{
					invalidNumber = numbers[i];
					break;
				}
			}

			Console.WriteLine("9-1: {0}", invalidNumber);

			return invalidNumber;
		}

		static long Part2(long[] numbers, long invalidNumber)
		{
			long weakness = 0;
			for (int i = 25; i < numbers.Length; ++i)
			{
				for (int j = i + 2; j < numbers.Length; ++j)
				{
					long sum = 0;
					long min = numbers[i];
					long max = 0;

					for (int k = i; k < j; ++k)
					{
						sum += numbers[k];
						min = Math.Min(min, numbers[k]);
						max = Math.Max(max, numbers[k]);
					}

					if (sum == invalidNumber)
					{
						weakness = min + max;
					}
				}
			}

			Console.WriteLine("9-2: {0}", weakness);

			return weakness;
		}
	}
}