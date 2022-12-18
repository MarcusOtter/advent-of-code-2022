using Utilities;

namespace PuzzleSolutions.Day12;

public class Puzzle12 : IPuzzleSolver
{
    public int Day => 12;
    
    public string SolveFirstPart(string[] input)
    {
        var grid = input.ToGrid();
        return GetDistance(grid.Find('S'), grid.Find('E'), grid).ToString();
    }
    
    public string SolveSecondPart(string[] input)
    {
        var grid = input.ToGrid();
        return grid
            .FindAll('a')
            .Select(point => GetDistance(point, grid.Find('E'), grid))
            .Min()
            .ToString();
    }

    private static int GetDistance(Point a, Point b, char[][] grid)
    {
        var frontier = new Queue<Point>();
        var cameFrom = new Dictionary<Point, Point>();

        frontier.Enqueue(a);
        cameFrom[a] = a;

        while (frontier.Any())
        {
            var current = frontier.Dequeue();
            foreach (var next in current.GetNeighbors())
            {
                if (cameFrom.ContainsKey(next)) continue; // Already visited
                if (next.X < 0 || next.X >= grid[0].Length) continue; // Out of bounds
                if (next.Y < 0 || next.Y >= grid.Length) continue; // Out of bounds
                
                var currentHeight = grid[current.Y][current.X];
                var nextHeight = grid[next.Y][next.X];
                
                if (currentHeight == 'S') currentHeight = 'a';
                if (nextHeight == 'E') nextHeight = 'z';
                
                if (nextHeight > currentHeight + 1) continue; // Cannot move to higher elevation
                
                frontier.Enqueue(next);
                cameFrom[next] = current;
            }
        }

        var path = new List<Point>();
        var currentPoint = b;
        while (currentPoint != a)
        {
            path.Add(currentPoint);
            if (!cameFrom.ContainsKey(currentPoint)) return int.MaxValue;
            currentPoint = cameFrom[currentPoint];
        }

        return path.Count;
    }
}