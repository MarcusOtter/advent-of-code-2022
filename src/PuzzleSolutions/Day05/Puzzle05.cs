using System.Text.RegularExpressions;

namespace PuzzleSolutions.Day05;

public class Puzzle05 : IPuzzleSolver
{
	public int Day => 5;
	
	private readonly Regex _regex = new Regex(@"move (\d+) from (\d+) to (\d+)");

	public string SolveFirstPart(string[] input)
	{
		var stacks = PopulateStacks(input);
		foreach (var line in input.SkipWhile(x => !x.Contains("move")))
		{
			var (boxesToMoveAmount, source, target) = ParseCraneMove(line, stacks);
			for (var i = 0; i < boxesToMoveAmount; i++)
			{
				var box = source.Pop();
				target.Push(box);
			}
		}

		var topBoxes = stacks.Aggregate("", (current, stack) => current + stack.Peek());
		return topBoxes;
	}

	public string SolveSecondPart(string[] input)
	{
		var stacks = PopulateStacks(input);
		foreach (var line in input.SkipWhile(x => !x.Contains("move")))
		{
			var (boxesToMoveAmount, source, target) = ParseCraneMove(line, stacks);
			var stack = new Stack<char>();
			for (var i = 0; i < boxesToMoveAmount; i++)
			{
				var box = source.Pop();
				stack.Push(box);
			}
			
			while (stack.Any())
			{
				target.Push(stack.Pop());
			}
		}

		var topBoxes = stacks.Aggregate("", (current, stack) => current + stack.Peek());
		return topBoxes;
	}

	private (int boxesToMoveAmount, Stack<char> source, Stack<char> target) ParseCraneMove(string line, Stack<char>[] stacks)
	{
		var match = _regex.Match(line);
		var amount = int.Parse(match.Groups[1].Value);
		var sourceStack = stacks[int.Parse(match.Groups[2].Value) - 1];
		var targetStack = stacks[int.Parse(match.Groups[3].Value) - 1];

		return (amount, sourceStack, targetStack);
	}

	private static Stack<char>[] PopulateStacks(string[] input)
	{
		var numberLabelsIndex = input
			.TakeWhile(line => !line.Any(char.IsDigit))
			.Count();
		
		var numberLabelsLine = input[numberLabelsIndex];
		var stackCount = int.Parse(numberLabelsLine.Trim().Last().ToString());
		var stacks = new int[stackCount].Select(_ => new Stack<char>()).ToArray();
		
		for (var i = numberLabelsIndex - 1; i >= 0; i--)
		{
			for (var j = 0; j < stackCount; j++)
			{
				var letter = input[i][1 + j * 4];
				if (string.IsNullOrWhiteSpace(letter.ToString())) continue;
				
				stacks[j].Push(letter);
			}
		}

		return stacks;
	}
}
