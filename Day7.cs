using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AoC
{
	public static class Day7
	{
		class Bag
		{
			public Bag(string color)
			{
				Color = color;
				Bags = new List<Bag>();
			}

			public string Color { get; set; }

			public List<Bag> Bags { get; set; }
		}

		public static void Solve()
		{
			var rules = File.ReadAllLines(@"../../../data/day7.txt");

			Part1(rules);
			Part2(rules);
		}

		static List<Bag> ParseRules(string[] rules, bool bIncludeMultiples)
		{
			var bags = new List<Bag>();
			foreach (var rule in rules)
			{
				var split1 = rule.Split(" contain ");

				string parentColor = split1[0].Replace(" bags", "");

				Bag parentBag = bags.Find(b => b.Color == parentColor);
				if (parentBag == null)
				{
					parentBag = new Bag(parentColor);
					bags.Add(parentBag);
				}

				if (split1[1] != "no other bags.")
				{
					var split2 = split1[1].Split(", ");
					foreach (var split in split2)
					{
						var bagSplit = split.Split(' ');

						// Including multiple child bags will make the search in part one slower
						int numBags = bIncludeMultiples ? int.Parse(bagSplit[0]) : 1;
						string childColor = bagSplit[1] + " " + bagSplit[2];

						Bag childBag = bags.Find(b => b.Color == childColor);
						if (childBag == null)
						{
							childBag = new Bag(childColor);
							bags.Add(childBag);
						}

						for (int i = 0; i < numBags; ++i)
						{
							parentBag.Bags.Add(childBag);
						}
					}
				}
			}

			return bags;
		}

		static bool ContainsShinyGold(Bag bag)
		{
			foreach (var childBag in bag.Bags)
			{
				if (childBag.Color == "shiny gold")
				{
					return true;
				}
				else if (ContainsShinyGold(childBag))
				{
					return true;
				}
			}

			return false;
		}

		static void Part1(string[] rules)
		{
			List<Bag> bags = ParseRules(rules, false);

			int totalBags = 0;
			foreach (var bag in bags)
			{
				if (ContainsShinyGold(bag))
				{
					totalBags++;
				}
			}

			Console.WriteLine("7-1: {0}", totalBags);
		}

		static int CountChildBags(Bag bag)
		{
			int totalBags = bag.Bags.Count;
			foreach (var childBag in bag.Bags)
			{
				totalBags += CountChildBags(childBag);
			}

			return totalBags;
		}

		static void Part2(string[] rules)
		{
			List<Bag> bags = ParseRules(rules, true);

			var shinyGoldBag = bags.Find(b => b.Color == "shiny gold");
			int totalChildBags = CountChildBags(shinyGoldBag);

			Console.WriteLine("7-2: {0}", totalChildBags);
		}
	}
}