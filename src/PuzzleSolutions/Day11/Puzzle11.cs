namespace PuzzleSolutions.Day11;

public class Puzzle11 : IPuzzleSolver
{
	public int Day => 11;
	
	private class Monkey
	{
		public long InspectionCount { get; private set; }
		public List<long> Items { get; }
		
		private readonly Func<long, long> _operation;
		private readonly Func<long, int> _test;
		
		public Monkey(List<long> items, Func<long, long> operation, Func<long, int> test)
		{
			Items = items;
			_operation = operation;
			_test = test;
		}

		public (int monkeyIndex, long item)[] InspectItems(bool divideByThree)
		{
			var outputs = new List<(int monkeyIndex, long item)>();
			for (var i = 0; i < Items.Count; i++)
			{
				Items[i] = _operation(Items[i]);

				if (divideByThree)
				{
					Items[i] /= 3;
				}
				else
				{
					Items[i] %= 9_699_690;
				}
				

				outputs.Add((_test(Items[i]), Items[i]));
				InspectionCount++;
			}
			
			Items.Clear();
			return outputs.ToArray();
		}
	}

	public string SolveFirstPart(string[] input)
	{
		return GetMonkeyBusiness(20, true, input).ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		return GetMonkeyBusiness(10_000, false, input).ToString();
	}

	private static long GetMonkeyBusiness(int rounds, bool divideByThree, string[] input)
	{
		var monkeys = ParseMonkeys(input);
		for (var i = 0; i < rounds; i++)
		{
			foreach (var monkey in monkeys)
			{
				foreach (var (newOwner, item) in monkey.InspectItems(divideByThree))
				{
					monkeys[newOwner].Items.Add(item);
				}
			}
		}

		return monkeys
			.Select(m => m.InspectionCount)
			.OrderDescending()
			.Take(2)
			.Aggregate((a, b) => a * b);
	}
	
	private static Monkey[] ParseMonkeys(string[] input)
	{
		var monkeys = new List<Monkey>();
		for (var i = 1; i < input.Length; i+=3)
		{
			var startingItems = input[i].Split(':')[1].Split(',').Select(long.Parse).ToList();
			var operation = GetOperation(input[++i]);
			var divisible = input[++i];
			
			var test = GetTest(divisible, input[++i], input[++i]);

			monkeys.Add(new Monkey(startingItems, operation, test));
		}
		return monkeys.ToArray();
	}

	private static Func<long, long> GetOperation(string operation)
	{
		var parts = operation.Trim().Split(' ');
		var isNumber = long.TryParse(parts[5], out var num);
		return parts[4] switch
		{
			"+" => old => old + (isNumber ? num : old),
			"*" => old => old * (isNumber ? num : old),
			_ => throw new Exception("Unexpected operation")
		};
	}

	private static Func<long, int> GetTest(string divisible, string ifTrue, string ifFalse)
	{
		var divisor = long.Parse(divisible[20..]);
		var monkeyTrue = int.Parse(ifTrue[28..]);
		var monkeyFalse = int.Parse(ifFalse[29..]);
		return num => num % divisor == 0 ? monkeyTrue : monkeyFalse;
	}
}
