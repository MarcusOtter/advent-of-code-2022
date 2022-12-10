namespace PuzzleSolutions.Day09;

public class Puzzle09 : IPuzzleSolver
{
	public int Day => 9;

	private record Position(int X, int Y)
	{
		public bool IsAdjacentOrEqual(Position other) => Math.Abs(X - other.X) <= 1 && Math.Abs(Y - other.Y) <= 1;
	}
	
	public string SolveFirstPart(string[] input)
	{
		return GetTailVisitedCount(input, 2).ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		return GetTailVisitedCount(input, 10).ToString();
	}

	private static int GetTailVisitedCount(IEnumerable<string> input, int ropeLength)
	{
		var visited = new HashSet<Position> { new(0, 0) };
		var ropeKnots = Enumerable.Repeat(new Position(0, 0), ropeLength).ToArray();

		foreach (var instruction in input)
		{
			var distance = int.Parse(instruction[2..]);
			for (var i = 0; i < distance; i++)
			{
				ropeKnots[0] = GetHeadPosition(ropeKnots[0], instruction[0]);
				var tailPosition = UpdateRopeTail(ropeKnots);
				visited.Add(tailPosition);
			}
		}

		return visited.Count;
	}

	private static Position UpdateRopeTail(IList<Position> ropeKnots)
	{
		for (var j = 1; j < ropeKnots.Count; j++)
		{
			var targetKnot = ropeKnots[j - 1];
			var knot = ropeKnots[j];
			
			if (knot.IsAdjacentOrEqual(targetKnot)) break;
			ropeKnots[j] = MoveTowardsPosition(knot, targetKnot);
		}

		return ropeKnots[^1]; // Return tail position
	}

	private static Position GetHeadPosition(Position head, char direction)
	{
		return direction switch
		{
			'R' => head with { X = head.X + 1 },
			'L' => head with { X = head.X - 1 },
			'U' => head with { Y = head.Y + 1 },
			'D' => head with { Y = head.Y - 1 },
			_ => throw new ArgumentOutOfRangeException()
		};
	}

	private static Position MoveTowardsPosition(Position source, Position target)
	{
		var xSign = Math.Sign(target.X - source.X);
		var ySign = Math.Sign(target.Y - source.Y);
		var x = source.X + xSign;
		var y = source.Y + ySign;

		// Prefer diagonal moves
		if (xSign != 0 && ySign != 0) return new Position(x, y);
		if (xSign != 0) return source with { X = x };
		if (ySign != 0) return source with { Y = y };

		return source;
	}
}
