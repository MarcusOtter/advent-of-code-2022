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
		// Prefer diagonal moves
		if (head.X > tail.X && head.Y > tail.Y) return new Position(tail.X + 1, tail.Y + 1);
		if (head.X > tail.X && head.Y < tail.Y) return new Position(tail.X + 1, tail.Y - 1);
		if (head.X < tail.X && head.Y > tail.Y) return new Position(tail.X - 1, tail.Y + 1);
		if (head.X < tail.X && head.Y < tail.Y) return new Position(tail.X - 1, tail.Y - 1);
		
		if (head.X > tail.X) return tail with { X = tail.X + 1 };
		if (head.X < tail.X) return tail with { X = tail.X - 1 };
		if (head.Y > tail.Y) return tail with { Y = tail.Y + 1 };
		if (head.Y < tail.Y) return tail with { Y = tail.Y - 1 };

		return tail;
	}
}
