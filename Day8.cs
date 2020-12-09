using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC
{
	public static class Day8
	{
		class Instruction
		{
			public Instruction(string operation, int argument)
			{
				Operation = operation;
				Argument = argument;
				TimesExecuted = 0;
			}

			public string Operation { get; set; }

			public int Argument { get; set; }

			public int TimesExecuted { get; set; }
		}

		public static void Solve()
		{
			var lines = File.ReadAllLines(@"../../../data/day8.txt");
			
			Part1(lines);
			Part2(lines);
		}

		static void ExecuteInstruction(Instruction instruction, ref int pc, ref int accumulator)
		{
			instruction.TimesExecuted++;
			if (instruction.Operation == "acc")
			{
				accumulator += instruction.Argument;
				pc++;
			}
			else if(instruction.Operation == "jmp")
			{
				pc += instruction.Argument;
			}
			else if(instruction.Operation == "nop")
			{
				pc++;
			}
		}

		static void Part1(string[] lines)
		{
			var instructions = new List<Instruction>();
			foreach (var line in lines)
			{
				var split = line.Split(' ');
				instructions.Add(new Instruction(split[0], int.Parse(split[1])));
			}

			int accumulator = 0;
			int pc = 0;

			while (pc < instructions.Count)
			{
				if (instructions[pc].TimesExecuted == 1)
				{
					break;
				}

				ExecuteInstruction(instructions[pc], ref pc, ref accumulator);
			}


			Console.WriteLine("8-1: {0}", accumulator);
		}

		static void Part2(string[] lines)
		{
			var instructions = new List<Instruction>();
			foreach (var line in lines)
			{
				var split = line.Split(' ');
				instructions.Add(new Instruction(split[0], int.Parse(split[1])));
			}

			int accumulator = 0;
			for (int i = 0; i < instructions.Count; ++i)
			{
				if (instructions[i].Operation == "jmp")
				{
					instructions[i].Operation = "nop";
				}
				else if(instructions[i].Operation == "nop")
				{
					instructions[i].Operation = "jmp";
				}

				bool bLoopDetected = false;
				accumulator = 0;
				int pc = 0;

				while (pc < instructions.Count)
				{
					if (instructions[pc].TimesExecuted == 1)
					{
						bLoopDetected = true;
						break;
					}

					instructions[pc].TimesExecuted++;
					if (instructions[pc].Operation == "acc")
					{
						accumulator += instructions[pc].Argument;
						pc++;
					}
					else if (instructions[pc].Operation == "jmp")
					{
						pc += instructions[pc].Argument;
					}
					else if (instructions[pc].Operation == "nop")
					{
						pc++;
					}
				}

				if (!bLoopDetected)
				{
					break;
				}
				else
				{
					if (instructions[i].Operation == "jmp")
					{
						instructions[i].Operation = "nop";
					}
					else if (instructions[i].Operation == "nop")
					{
						instructions[i].Operation = "jmp";
					}
				}

				foreach (var instruction in instructions)
				{
					instruction.TimesExecuted = 0;
				}
			}
			

			Console.WriteLine("8-2: {0}", accumulator);
		}
	}
}