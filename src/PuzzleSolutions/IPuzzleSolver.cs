namespace PuzzleSolutions;

public interface IPuzzleSolver
{
	public int Day { get; }
	public ValueTask<string> SolveFirstHalfAsync(IEnumerable<string> input);
	public ValueTask<string> SolveSecondHalfAsync(IEnumerable<string> input);
}
