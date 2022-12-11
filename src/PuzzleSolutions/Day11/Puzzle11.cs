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
		private readonly long _divisor;
		
		public Monkey(List<long> items, Func<long, long> operation, Func<long, int> test, long divisor)
		{
			Items = items;
			_operation = operation;
			_test = test;
			_divisor = divisor;
		}

		public (int monkeyIndex, long item)[] InspectItems(bool divideByThree)
		{
			var outputs = new List<(int monkeyIndex, long item)>();
			for (var i = 0; i < Items.Count; i++)
			{
				Items[i] = _operation(Items[i]);
				Items[i] /= divideByThree ? 3 : 1;
				
				// if (!divideByThree && Items[i] % _divisor == 0)
				// {
				// 	Items[i] /= _divisor;
				// }

				// I have no idea what I am doing and this does not work at all I think but idk
				if (!divideByThree && Items[i] >= 130 && Items[i].ToString()[^1] == '0')
				{
					Items[i] /= 10;
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
		// return GetMonkeyBusiness(20, true, input).ToString();
		return "";
	}

	public string SolveSecondPart(string[] input)
	{
		var demoInput = """
		Monkey 0:
		  Starting items: 79, 98
		  Operation: new = old * 19
		  Test: divisible by 23
		    If true: throw to monkey 2
		    If false: throw to monkey 3

		Monkey 1:
		  Starting items: 54, 65, 75, 74
		  Operation: new = old + 6
		  Test: divisible by 19
		    If true: throw to monkey 2
		    If false: throw to monkey 0

		Monkey 2:
		  Starting items: 79, 60, 97
		  Operation: new = old * old
		  Test: divisible by 13
		    If true: throw to monkey 1
		    If false: throw to monkey 3

		Monkey 3:
		  Starting items: 74
		  Operation: new = old + 3
		  Test: divisible by 17
		    If true: throw to monkey 0
		    If false: throw to monkey 1 
		""";
		input = demoInput.Split(Environment.NewLine);
		
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

			monkeys.Add(new Monkey(startingItems, operation, test, long.Parse(divisible[20..])));
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
