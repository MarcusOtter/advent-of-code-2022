namespace PuzzleSolutions.Day03;

public class Puzzle03 : IPuzzleSolver
{
	public int Day => 3;

	public string SolveFirstPart(string[] input)
	{
		var sum = 0;
		foreach (var line in input)
		{
			var lineMiddleIndex = (int) Math.Ceiling(line.Length / 2d);
			var commonLetters = GetCommonLetters(line[..lineMiddleIndex], line[lineMiddleIndex..]);
			var priority = GetPriority(commonLetters[0]);

			sum += priority;
		}

		return sum.ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		var sum = 0;
		for (var i = 0; i < input.Length - 2; i++)
		{
			var commonLetters = GetCommonLetters(input[i], input[++i], input[++i]);
			var priority = GetPriority(commonLetters[0]);
			
			sum += priority;
		}
		
		return sum.ToString();
	}

	private static string GetCommonLetters(params string[] inputs)
	{
		if (inputs.Length == 0) return "";
		
		var common = inputs[0];
		foreach (var input in inputs)
		{
			common = new string(common.Intersect(input).ToArray());
		}
		
		return common;
	}

	private static int GetPriority(char c)
	{
		return " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(c);
	}
}
