namespace PuzzleSolutions;

public static class PuzzleFactory
{
	public static IPuzzleSolver GetPuzzle(DateTime date)
	{
		if (date.Year != 2022 || date.Month != 12)
		{
			throw new ArgumentException("Date needs to be in December 2022", nameof(date));
		}

		var puzzleSolvers = typeof(IPuzzleSolver).Assembly
			.GetTypes()
			.Where(p => p.IsClass && p.IsAssignableTo(typeof(IPuzzleSolver)))
			.Select(Activator.CreateInstance)
			.Cast<IPuzzleSolver>();

		var puzzle = puzzleSolvers.FirstOrDefault(x => x.Day == date.Day);
		if (puzzle == default)
		{
			throw new ArgumentException(
				$"Could not find an IPuzzleSolver for {date:yyyy-MM-dd} (Day = {date.Day})", nameof(date));
		}

		return puzzle;
	}
}
