namespace AdventOfCode2022.Day4
{
    internal sealed class Day4 : IChallengeDay
    {
        public int DayNumber => 4;

        public void RunChallengeOne()
        {
            int fullAssignemntOverlapCount = 0;

            foreach (string line in File.ReadLines(@".\Day4\challenge-input.txt"))
            {
                int[] pairValues = ParsePairValues(line);

                var partner1 = new Range(pairValues[0], pairValues[1]);
                var partner2 = new Range(pairValues[2], pairValues[3]);

                // Partner 1 contains 2
                if (partner1.Start <= partner2.Start && partner1.End >= partner2.End)
                {
                    fullAssignemntOverlapCount++;
                }
                // Partner 2 contains 1
                else if (partner2.Start <= partner1.Start && partner2.End >= partner1.End)
                {
                    fullAssignemntOverlapCount++;
                }
            }

            Console.WriteLine($"The number of pairs with a full assignment overlap is {fullAssignemntOverlapCount}.");
        }

        public void RunChallengeTwo()
        {
            int anyAssignemntOverlapCount = 0;

            foreach (string line in File.ReadLines(@".\Day4\challenge-input.txt"))
            {
                int[] pairValues = ParsePairValues(line);

                var partner1 = new Range(pairValues[0], pairValues[1]);
                var partner2 = new Range(pairValues[2], pairValues[3]);

                // Any assignment overlap
                if (partner1.Start <= partner2.End && partner1.End >= partner2.Start)
                {
                    anyAssignemntOverlapCount++;
                }
            }

            Console.WriteLine($"The number of pairs with any assignment overlap is {anyAssignemntOverlapCount}.");
        }

        private static int[] ParsePairValues(string line)
        {
            var pairValues = line.Split('-', ',')
                .Select(v => int.TryParse(v, out int result) ? result : throw new NotSupportedException("The input row must be '0-0,0-0'."))
                .ToArray();

            if (pairValues.Length < 4)
            {
                throw new NotSupportedException("The input row must be '0-0,0-0'.");
            }

            return pairValues;
        }

        private sealed class Range
        {
            public Range(int start, int end)
            {
                Start = start;
                End = end;
            }

            public int Start { get; }
            public int End { get; }
        }
    }
}
