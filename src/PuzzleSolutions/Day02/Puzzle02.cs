namespace PuzzleSolutions.Day02;

public class Puzzle02 : IPuzzleSolver
{
	public int Day => 2;

	public string SolveFirstPart(string[] input)
	{
		var score = 0;
		foreach (var line in input)
		{
			var opponentChoice = GetHandShape(line[0]);
			var playerChoice = GetHandShape(line[2]);

			score += (int) playerChoice;
			score += (int) GetGameOutcome(playerChoice, opponentChoice);
		}
		
		return score.ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		var score = 0;
		
		foreach (var line in input)
		{
			var opponentChoice = GetHandShape(line[0]);
			var desiredOutcome = GetDesiredGameOutcome(line[2]);
			var playerChoice = GetRequiredShape(opponentChoice, desiredOutcome);

			score += (int) playerChoice;
			score += (int) desiredOutcome;
		}
		
		return score.ToString();
	}

	private static HandShape GetRequiredShape(HandShape opponentChoice, GameOutcome desiredOutcome)
	{
		return desiredOutcome switch
		{
			GameOutcome.Draw => opponentChoice,

			GameOutcome.Win when opponentChoice is HandShape.Rock => HandShape.Paper,
			GameOutcome.Win when opponentChoice is HandShape.Paper => HandShape.Scissors,
			GameOutcome.Win when opponentChoice is HandShape.Scissors => HandShape.Rock,

			GameOutcome.Lose when opponentChoice is HandShape.Rock => HandShape.Scissors,
			GameOutcome.Lose when opponentChoice is HandShape.Paper => HandShape.Rock,
			GameOutcome.Lose when opponentChoice is HandShape.Scissors => HandShape.Paper,

			_ => 0,
		};
	}

	private static GameOutcome GetGameOutcome(HandShape playerChoice, HandShape opponentChoice)
	{
		if (playerChoice == opponentChoice) return GameOutcome.Draw;

		return playerChoice switch
		{
			HandShape.Rock when opponentChoice is HandShape.Scissors => GameOutcome.Win,
			HandShape.Paper when opponentChoice is HandShape.Rock => GameOutcome.Win,
			HandShape.Scissors when opponentChoice is HandShape.Paper => GameOutcome.Win,

			_ => GameOutcome.Lose
		};
	}

	private static HandShape GetHandShape(char c)
	{
		return c switch
		{
			'A' or 'X' => HandShape.Rock,
			'B' or 'Y' => HandShape.Paper,
			'C' or 'Z' => HandShape.Scissors,
			_ => 0
		};
	}

	private static GameOutcome GetDesiredGameOutcome(char c)
	{
		return c switch
		{
			'X' => GameOutcome.Lose,
			'Y' => GameOutcome.Draw,
			'Z' => GameOutcome.Win,
			_ => 0,
		};
	}

	private enum GameOutcome
	{
		Lose = 0,
		Draw = 3,
		Win = 6,
	}

	private enum HandShape
	{
		Rock = 1,
		Paper = 2,
		Scissors = 3,
	}
}
