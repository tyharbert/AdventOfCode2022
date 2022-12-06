namespace AdventOfCode2022.Day6
{
    internal sealed class Day6 : IChallengeDay
    {
        public int DayNumber => 6;

        public void RunChallengeOne()
        {
            var line = File.ReadLines(@".\Day6\challenge-input.txt").First();

            var index = GetIndexAfterDistinctSequence(line, 4);

            Console.WriteLine($"First marker after character {index}.");
        }

        public void RunChallengeTwo()
        {
            var line = File.ReadLines(@".\Day6\challenge-input.txt").First();

            var index = GetIndexAfterDistinctSequence(line, 14);

            Console.WriteLine($"First marker after character {index}.");
        }

        private int GetIndexAfterDistinctSequence(string sequence, int distinctCharCount)
        {
            int index = 1;
            Queue<char> uniqueBuffer = new Queue<char>(distinctCharCount);

            var lineEnumerator = sequence.GetEnumerator();
            lineEnumerator.MoveNext();

            uniqueBuffer.Enqueue(lineEnumerator.Current);

            while (lineEnumerator.MoveNext())
            {
                // Get rid of duplicate values
                if (uniqueBuffer.Contains(lineEnumerator.Current))
                {
                    char trash = '\0';

                    while (trash != lineEnumerator.Current)
                    {
                        trash = uniqueBuffer.Dequeue();
                    }
                }

                // Add new value
                index++;
                uniqueBuffer.Enqueue(lineEnumerator.Current);

                // Break if there are distinctCharCount unique values
                if (uniqueBuffer.Count == distinctCharCount)
                {
                    break;
                }
            }

            return index;
        }
    }
}
