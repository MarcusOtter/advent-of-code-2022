namespace PuzzleSolutions.Day10;

public class Puzzle10 : IPuzzleSolver
{
	public int Day => 10;

	private class Instruction
	{
		public int CyclesRemaining;
		public int RegisterDelta;
	}

	public string SolveFirstPart(string[] input)
	{
		var registerSum = 1;
		var cycleCount = 0;
		var signalStrengths = new List<int>();
		var jobQueue = GetJobQueue(input);
		
		while (jobQueue.TryPeek(out var instruction))
		{
			if ((++cycleCount + 20) % 40 == 0) signalStrengths.Add(registerSum * cycleCount);
			if (--instruction.CyclesRemaining > 0) continue;
			
			jobQueue.Dequeue();
			registerSum += instruction.RegisterDelta;
		}

		return signalStrengths.Sum().ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		var registerSum = 1;
		var cycleCount = 0;
		var jobQueue = GetJobQueue(input);
		var output = "\n";

		while (jobQueue.TryPeek(out var instruction))
		{
			var hasVisiblePixel = Math.Abs(registerSum - cycleCount % 40) <= 1;
			output += hasVisiblePixel ? "#" : ".";

			if (++cycleCount % 40 == 0) output += "\n";
			if (--instruction.CyclesRemaining > 0) continue;
			
			jobQueue.Dequeue();
			registerSum += instruction.RegisterDelta;
		}

		return output;
	}

	private static Queue<Instruction> GetJobQueue(IEnumerable<string> input)
	{
		var jobQueue = new Queue<Instruction>();
		foreach (var instructionString in input)
		{
			jobQueue.Enqueue(new Instruction
			{
				CyclesRemaining = instructionString.StartsWith("addx") ? 2 : 1,
				RegisterDelta = instructionString.StartsWith("addx") ? int.Parse(instructionString.Split(" ")[1]) : 0
			});
		}

		return jobQueue;
	}
}
