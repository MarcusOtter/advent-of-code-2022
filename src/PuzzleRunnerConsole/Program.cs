using PuzzleSolutions;
using Utilities;

var puzzleDate = DateTime.Today;
var inputReader = new InputReader();

var input = await inputReader.ReadLinesAsync(puzzleDate);
var puzzle = PuzzleFactory.GetPuzzle(puzzleDate);

var firstHalfOutput = puzzle.SolveFirstPart(input);
Console.WriteLine($"First half: {firstHalfOutput}");

var secondHalfOutput = puzzle.SolveSecondPart(input);
Console.WriteLine($"Second half: {secondHalfOutput}");
