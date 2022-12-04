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
                var pairValues = line.Split('-', ',')
                    .Select(v => int.TryParse(v, out int result) ? result : throw new NotSupportedException("The input row must be '0-0,0-0'."))
                    .ToArray();

                if (pairValues.Length < 4)
                {
                    throw new NotSupportedException("The input row must be '0-0,0-0'.");
                }

                // Partner 1 contains 2
                if (pairValues[0] <= pairValues[2] && pairValues[1] >= pairValues[3])
                {
                    fullAssignemntOverlapCount++;
                }
                // Partner 2 contains 1
                else if (pairValues[2] <= pairValues[0] && pairValues[3] >= pairValues[1])
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
                var pairValues = line.Split('-', ',')
                    .Select(v => int.TryParse(v, out int result) ? result : throw new NotSupportedException("The input row must be '0-0,0-0'."))
                    .ToArray();

                if (pairValues.Length < 4)
                {
                    throw new NotSupportedException("The input row must be '0-0,0-0'.");
                }

                // Any assignment overlap
                if (pairValues[0] <= pairValues[3] && pairValues[1] >= pairValues[2])
                {
                    anyAssignemntOverlapCount++;
                }
            }

            Console.WriteLine($"The number of pairs with any assignment overlap is {anyAssignemntOverlapCount}.");
        }
    }
}
