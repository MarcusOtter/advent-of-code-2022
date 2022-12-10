namespace PuzzleSolutions.Day09;

public class Puzzle09 : IPuzzleSolver
{
	public int Day => 9;

	private record Position(int X, int Y)
	{
		public bool IsAdjacentOrEqual(Position other) => Math.Abs(X - other.X) <= 1 && Math.Abs(Y - other.Y) <= 1;
	}
	
	private readonly HashSet<Position> _visited = new() { new Position(0, 0) };
	
	public string SolveFirstPart(string[] input)
	{
		var head = new Position(0, 0);
		var tail = new Position(0, 0);
		
		foreach (var instruction in input)
		{
			var distance = int.Parse(instruction[2..]);
			for (var i = 0; i < distance; i++)
			{
				head = instruction[0] switch
				{
					'R' => head with { X = head.X + 1 },
					'L' => head with { X = head.X - 1 },
					'U' => head with { Y = head.Y + 1 },
					'D' => head with { Y = head.Y - 1 },
					_ => throw new ArgumentOutOfRangeException()
				};

				if (tail.IsAdjacentOrEqual(head)) continue;
			
				tail = MoveTowardsHead(head, tail);
				_visited.Add(tail);
			}
		}

		return _visited.Count.ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		return "";
	}

	private static Position MoveTowardsHead(Position head, Position tail)
	{
		var xSign = Math.Sign(head.X - tail.X);
		var ySign = Math.Sign(head.Y - tail.Y);
		var x = tail.X + xSign;
		var y = tail.Y + ySign;
	
		// Prefer diagonal moves
		if (xSign != 0 && ySign != 0) return new Position(x, y);
		if (xSign != 0) return tail with { X = x };
		if (ySign != 0) return tail with { Y = y };

		return tail;
	}
}
