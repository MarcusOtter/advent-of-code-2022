namespace PuzzleSolutions.Day01;

public class Puzzle01 : IPuzzleSolver
{
	public int Day => 1;
	
	public ValueTask<string> SolveFirstHalfAsync(IEnumerable<string> input)
	{
		var result = GetElfCalories(input)
			.First()
			.ToString();
		
		return ValueTask.FromResult(result);
	}

	public ValueTask<string> SolveSecondHalfAsync(IEnumerable<string> input)
	{
		var result = GetElfCalories(input)
			.Take(3)
			.Sum()
			.ToString();

		return ValueTask.FromResult(result);
	}

	// Descending order
	private static IEnumerable<int> GetElfCalories(IEnumerable<string> input)
	{
		var elvesCalories = new List<int>();
		var currentElfTotalCalories = 0;

		foreach (var line in input)
		{
			if (line == string.Empty)
			{
				elvesCalories.Add(currentElfTotalCalories);
				currentElfTotalCalories = 0;
				continue;
			}

			var calories = int.Parse(line);
			currentElfTotalCalories += calories;
		}
		
		return elvesCalories.OrderByDescending(x => x);
	}
}
