namespace Utilities;

public record Point(int X, int Y)
{
    public Point[] GetNeighbors(bool includeDiagonals = false)
    {
        var points = new[]
        {
            this with {Y = Y - 1},
            this with {X = X + 1},
            this with {Y = Y + 1},
            this with {X = X - 1},
            new Point(X + 1, Y - 1),
            new Point(X + 1, Y + 1),
            new Point(X - 1, Y + 1),
            new Point(X - 1, Y - 1)
        };
        
        return includeDiagonals ? points : points[..4];
    }
}
