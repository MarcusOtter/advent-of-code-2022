namespace Utilities;

public static class InputHelpers
{
    public static char[][] ToGrid(this string[] input)
    {
        return input.Select(x => x.ToCharArray()).ToArray();
    }
    
    public static Point Find(this char[][] grid, char c)
    {
        return Find(grid, c, true).FirstOrDefault() ?? new Point(-1, -1);
    }

    public static Point[] FindAll(this char[][] grid, char c)
    {
        return Find(grid, c, false);
    }
    
    private static Point[] Find(char[][] grid, char c, bool quitAfterFirst)
    {
        var points = new List<Point>();
        for (var y = 0; y < grid.Length; y++)
        {
            for (var x = 0; x < grid[y].Length; x++)
            {
                if (grid[y][x] != c) continue;
                
                points.Add(new Point(x, y));
                if (quitAfterFirst)
                {
                    return points.ToArray();
                }
            }
        }

        return points.ToArray();
    }
}