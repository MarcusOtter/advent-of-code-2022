namespace PuzzleSolutions.Day06;

public class Puzzle06 : IPuzzleSolver
{
	public int Day => 6;
	
	public string SolveFirstPart(string[] input)
	{
		return FindIndexAfterDistinctCharacters(input[0], 4).ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		return FindIndexAfterDistinctCharacters(input[0], 14).ToString();
	}

	private int FindIndexAfterDistinctCharacters(string input, int requiredLength)
	{
		for (var i = 0; i < input.Length - 3; i++)
		{
			var letters = input.Substring(i, requiredLength);
			var nonRepeating = new string(letters.Distinct().ToArray());

			if (string.Equals(letters, nonRepeating))
				return i + requiredLength;
		}

		return -1;
	}
}
