using PuzzleSolutions;
using Utilities;

var puzzleDate = DateTime.Today;
var inputReader = new InputReader();

var input = await inputReader.ReadLinesAsync(puzzleDate);
var puzzle = PuzzleFactory.GetPuzzle(puzzleDate);

var firstHalfOutput = await puzzle.SolveFirstHalfAsync(input);
var secondHalfOutput = await puzzle.SolveSecondHalfAsync(input);

Console.WriteLine($"First half: {firstHalfOutput}");
Console.WriteLine($"Second half: {secondHalfOutput}");
