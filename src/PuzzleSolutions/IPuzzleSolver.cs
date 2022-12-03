namespace PuzzleSolutions;

public interface IPuzzleSolver
{
	public int Day { get; }
	public string SolveFirstPart(string[] input);
	public string SolveSecondPart(string[] input);
}
