using System.Text;

namespace Utilities;

public class InputReader
{
	private readonly string _inputDirectory;
	
	public InputReader(string inputDirectory = "inputs")
	{
		_inputDirectory = inputDirectory;
	}

	public Task<string[]> ReadLinesAsync(DateTime date)
	{
		var expectedFileName = $"{date.Day:00}.txt";
		var path = Path.Join(_inputDirectory, expectedFileName);

		if (!File.Exists(path))
		{
			throw new FileNotFoundException("Could not find " + path);
		}

		return File.ReadAllLinesAsync(path, Encoding.UTF8);
	}
}
