namespace PuzzleSolutions.Day08;

public class Puzzle08 : IPuzzleSolver
{
	public int Day => 8;
	
	public string SolveFirstPart(string[] input)
	{
		var visibleTrees = input.Length * 2 + input[0].Length * 2 - 4; // Edges are always visible
		for (var rowIndex = 1; rowIndex < input.Length - 1; rowIndex++)
		{
			for (var columnIndex = 1; columnIndex < input[0].Length - 1; columnIndex++)
			{
				if (IsVisibleFromOutside(rowIndex, columnIndex, input)) visibleTrees++;
			}
		}

		return visibleTrees.ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		var bestScore = 0;
		for (var rowIndex = 1; rowIndex < input.Length - 1; rowIndex++)
		{
			for (var columnIndex = 1; columnIndex < input[0].Length - 1; columnIndex++)
			{
				var score = GetScenicScore(rowIndex, columnIndex, input);
				if (score > bestScore) bestScore = score;
			}
		}
		
		return bestScore.ToString();
	}

	private static int GetScenicScore(int rowIndex, int columnIndex, string[] input)
	{
		var treeHeight = input[rowIndex][columnIndex];

		var visibleLeft = input[rowIndex][..columnIndex].Reverse().TakeWhile(c => c < treeHeight).Count();
		var visibleRight = input[rowIndex][(columnIndex + 1)..].TakeWhile(c => c < treeHeight).Count();
		var visibleUp = input[..rowIndex].Reverse().TakeWhile(s => s[columnIndex] < treeHeight).Count();
		var visibleDown = input[(rowIndex + 1)..].TakeWhile(s => s[columnIndex] < treeHeight).Count();

		// If we hit a tree before the edge, add one point for that tree
		if (visibleLeft < columnIndex - 1) visibleLeft++;
		if (visibleRight < input[0].Length - columnIndex - 1) visibleRight++;
		if (visibleUp < rowIndex - 1) visibleUp++;
		if (visibleDown < input.Length - rowIndex - 1) visibleDown++;
		
		return visibleLeft * visibleRight * visibleUp * visibleDown;
	}

	private static bool IsVisibleFromOutside(int rowIndex, int columnIndex, string[] input)
	{
		var treeHeight = input[rowIndex][columnIndex];

		var tallestLeft = input[rowIndex][..columnIndex].Max();
		var tallestRight = input[rowIndex][(columnIndex + 1)..].Max();
		var tallestUp = input[..rowIndex].Select(row => row[columnIndex]).Max();
		var tallestDown = input[(rowIndex + 1)..].Select(row => row[columnIndex]).Max();

		var shortestSide = new[] { tallestLeft, tallestRight, tallestUp, tallestDown }.Min();
		return treeHeight > shortestSide;
	}
}
