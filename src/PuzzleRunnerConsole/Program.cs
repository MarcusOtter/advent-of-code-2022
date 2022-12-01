using PuzzleSolutions.Day01;
using Utilities;

var inputReader = new InputReader();
var input = await inputReader.ReadLinesAsync(DateTime.Today);

var puzzle = new Puzzle01();
var firstHalfOutput = await puzzle.SolveFirstHalfAsync(input);
var secondHalfOutput = await puzzle.SolveSecondHalfAsync(input);

Console.WriteLine($"First half: {firstHalfOutput}");
Console.WriteLine($"Second half: {secondHalfOutput}");
