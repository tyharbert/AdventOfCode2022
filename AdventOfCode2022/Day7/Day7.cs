namespace AdventOfCode2022.Day7
{
    internal sealed class Day7 : IChallengeDay
    {
        public int DayNumber => 7;

        public void RunChallengeOne()
        {
            MyDirectory root = new MyDirectory(null);

            ParseFileSystemStructure(root);

            int minSize = 0;
            int maxSize = 100_000;

            int runningTotal = 0;
            CalculateDirectorySizeRecursive(root, s =>
            {
                if (s >= minSize && s <= maxSize)
                {
                    runningTotal += s;
                }
            });

            Console.WriteLine($"The sum of all of the directories under {maxSize} is: {runningTotal}.");
        }


        public void RunChallengeTwo()
        {
            MyDirectory root = new MyDirectory(null);

            ParseFileSystemStructure(root);

            var sizeCache = new List<int>();
            int totalUsed = CalculateDirectorySizeRecursive(root, s =>
            {
                sizeCache.Add(s);
            });

            int totalAvailable = 70_000_000;
            int targetSizeToFree = 30_000_000;
            int usedSpaceToFreed = totalUsed - (totalAvailable - targetSizeToFree);

            var directorySizeToDelete = sizeCache.Where(s => s >= usedSpaceToFreed)
                .OrderBy(s => s)
                .FirstOrDefault();

            Console.WriteLine($"The min size to reach an unused size greater than {targetSizeToFree} is : {directorySizeToDelete}.");
        }

        private static int CalculateDirectorySizeRecursive(MyDirectory currentDirectory, Action<int> onEachTotal)
        {
            int size = currentDirectory.Files
                .Sum(f => f.Value.FileSize);

            foreach (var directory in currentDirectory.Directories)
            {
                size += CalculateDirectorySizeRecursive(directory.Value, onEachTotal);
            }

            onEachTotal(size);

            return size;
        }

        private static void ParseFileSystemStructure(MyDirectory root)
        {
            MyDirectory current = root;

            foreach (var line in File.ReadLines(@".\Day7\challenge-input.txt"))
            {
                if (line.StartsWith("$"))
                {
                    var command = string.Concat(line.Skip(1))
                        .Trim()
                        .Split(' ');

                    // cd
                    if (command.FirstOrDefault() == "cd")
                    {
                        if (command.Length < 2)
                        {
                            throw new NotSupportedException($"The command '{line}' is in the wrong format.");
                        }

                        var cdInput = command[1];

                        // Go to parent
                        if (cdInput == "..")
                        {
                            current = current.Parent ?? throw new InvalidOperationException("Can't go back from the root Dir.");
                        }
                        // Go forward
                        else if (cdInput == "/")
                        {
                            current = root;
                        }
                        // Go to child
                        else
                        {
                            current = current.Directories[cdInput];
                        }
                    }
                    // ignore ls, we only care about the following lines in the outer else
                }
                // ls results, create these if they don't exist
                else
                {
                    var content = line.Trim()
                        .Split(' ');

                    if (content.Length < 2)
                    {
                        throw new NotSupportedException($"The content '{line}' is in the wrong format.");
                    }

                    // adding file
                    if (int.TryParse(content[0], out int fileSize))
                    {
                        string fileName = content[1];

                        if (!current.Files.ContainsKey(fileName))
                        {
                            current.Files.Add(fileName, new MyFile(fileSize));
                        }
                    }
                    // adding dir
                    else
                    {
                        string dirName = content[1];

                        if (!current.Directories.ContainsKey(dirName))
                        {
                            current.Directories.Add(dirName, new MyDirectory(current));
                        }
                    }
                }
            }
        }

        private sealed class MyDirectory
        {
            public MyDirectory? Parent { get; }

            public MyDirectory(MyDirectory? parent)
            {
                Parent = parent;
                Directories = new Dictionary<string, MyDirectory>();
                Files = new Dictionary<string, MyFile>();
            }

            public Dictionary<string, MyDirectory> Directories { get; }
            public Dictionary<string, MyFile> Files { get; }
        }

        private sealed class MyFile
        {
            public MyFile(int fileSize)
            {
                FileSize = fileSize;
            }

            public int FileSize { get; }
        }
    }
}
