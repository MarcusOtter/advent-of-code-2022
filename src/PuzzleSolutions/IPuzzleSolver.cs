namespace PuzzleSolutions;

public interface IPuzzleSolver
{
	public ValueTask<string> SolveFirstHalfAsync(IEnumerable<string> input);
	public ValueTask<string> SolveSecondHalfAsync(IEnumerable<string> input);

}
