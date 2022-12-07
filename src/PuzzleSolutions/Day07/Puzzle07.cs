namespace PuzzleSolutions.Day07;

public class Puzzle07 : IPuzzleSolver
{
	public int Day => 7;
	
	public record Directory(string Name, Directory? Parent)
	{
		public List<Directory> Subdirectories { get; } = new();
		public List<File> Files { get; } = new();
	}
	public record File(string Name, int Size);

	private Directory _rootDirectory = new("/", null);

	public string SolveFirstPart(string[] input)
	{
		PopulateRootDirectory(input);

		var descendantDirs = GetDescendantDirectories(_rootDirectory);
		var fileSizeSum = descendantDirs
			.Select(dir => GetDescendantFiles(dir).Sum(x => x.Size))
			.Where(dirSize => dirSize <= 100_000)
			.Sum();

		return fileSizeSum.ToString();
	}

	public string SolveSecondPart(string[] input)
	{
		PopulateRootDirectory(input);
		
		const int totalSpace = 70_000_000;
		const int requiredSpace = 30_000_000;
		var usedSpace = GetDescendantFiles(_rootDirectory).Sum(x => x.Size);
		var availableSpace = totalSpace - usedSpace;
		var missingSpace = Math.Abs(availableSpace - requiredSpace);

		var descendantDirs = GetDescendantDirectories(_rootDirectory);
		var directorySize = descendantDirs
			.Select(dir => GetDescendantFiles(dir).Sum(x => x.Size))
			.Where(dirSize => dirSize > missingSpace)
			.Order()
			.FirstOrDefault();

		return directorySize.ToString();
	}

	private void PopulateRootDirectory(IReadOnlyList<string> input)
	{
		_rootDirectory = new Directory("/", null);
		var currentDirectory = _rootDirectory;
		for (var i = 1; i < input.Count; i++)
		{
			var line = input[i];
			if (line.StartsWith("$ cd"))
			{
				currentDirectory = ChangeDirectory(line, currentDirectory);
			}
			else if (line.StartsWith("$ ls"))
			{
				var directoryContents = PopulateDirectoryContents(input.Skip(i + 1), currentDirectory);
				i += directoryContents.Length;
			}
		}
	}

	private Directory? ChangeDirectory(string line, Directory? currentDirectory)
	{
		var targetDirName = line.Split(" ")[2];
		switch (targetDirName)
		{
			case "..": return currentDirectory?.Parent;
			case "/": return _rootDirectory;
		}

		var targetDirectory = currentDirectory?.Subdirectories.FirstOrDefault(x => x.Name == targetDirName);
		if (targetDirectory != default)
		{
			return targetDirectory;
		}
		
		targetDirectory = new Directory(targetDirName, currentDirectory);
		currentDirectory?.Subdirectories.Add(targetDirectory);
		return targetDirectory;
	}

	private static string[] PopulateDirectoryContents(IEnumerable<string> input, Directory? directory)
	{
		var directoryContents = input.TakeWhile(l => !l.StartsWith("$")).ToArray();
		foreach (var entry in directoryContents)
		{
			if (entry.StartsWith("dir")) continue;

			var fileName = entry.Split(" ")[1];
			var fileSize = int.Parse(entry.Split(" ")[0]);

			// Don't duplicate files
			if (directory?.Files.Any(x => x.Name == fileName) ?? false) continue;

			directory?.Files.Add(new File(fileName, fileSize));
		}

		return directoryContents;
	}

	private static IEnumerable<File> GetDescendantFiles(Directory directory)
	{
		return directory.Files.Concat(directory.Subdirectories.SelectMany(GetDescendantFiles));
	}

	private static IEnumerable<Directory> GetDescendantDirectories(Directory directory)
	{
		return directory.Subdirectories.Concat(directory.Subdirectories.SelectMany(GetDescendantDirectories));
	}
}
