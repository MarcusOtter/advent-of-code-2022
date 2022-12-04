namespace PuzzleSolutions.Day04;

public class Puzzle04 : IPuzzleSolver
{
	public int Day => 4;
	
	public string SolveFirstPart(string[] input)
	{
		var count = 0;
		
		foreach (var line in input)
		{
			var (range1, range2) = GetNumberRangesFromLine(line);
			var overlappingNumbers = range1.Intersect(range2).ToArray();
			if (overlappingNumbers.SequenceEqual(range1) || overlappingNumbers.SequenceEqual(range2))
			{
				count++;
			}
		}

		return count.ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		var count = 0;
		
		foreach (var line in input)
		{
			var (range1, range2) = GetNumberRangesFromLine(line);
			var overlappingNumbers = range1.Intersect(range2).ToArray();
			if (overlappingNumbers.Length > 0)
			{
				count++;
			}
		}

		return count.ToString();
	}

	private static (int[] range1, int[] range2) GetNumberRangesFromLine(string line)
	{
		var firstRangeString = line.Split(",")[0];
		var secondRangeString = line.Split(",")[1];
		
		return (GetNumberRange(firstRangeString), GetNumberRange(secondRangeString));
	}

	private static int[] GetNumberRange(string range)
	{
		var rangeStart = int.Parse(range.Split("-")[0]);
		var rangeEnd = int.Parse(range.Split("-")[1]);
		
		return Enumerable.Range(rangeStart, rangeEnd - rangeStart + 1).ToArray();
	}
}
