namespace AdventOfCode2022.Day3
{
    internal sealed class Day3 : IChallengeDay
    {
        public int DayNumber => 3;

        public void RunChallengeOne()
        {
            int runningTotal = 0;

            foreach (string line in File.ReadLines(@".\Day3\challenge-input.txt"))
            {
                var compartmentLength = line.Length / 2;

                var compartmentOne = line.Substring(0, compartmentLength);
                var compartmentTwo = line.Substring(compartmentLength, compartmentLength);

                var charVal = compartmentOne.Intersect(compartmentTwo).FirstOrDefault();

                var priority = char.IsLower(charVal) ? (int)charVal - 96 : (int)charVal - 38;

                runningTotal += priority;
            }

            Console.WriteLine($"The sum of the priorites is {runningTotal}.");
        }

        public void RunChallengeTwo()
        {
            int runningTotal = 0;

            var lines = File.ReadLines(@".\Day3\challenge-input.txt").ToList();
            int pageLines = 3;

            var pageCount = lines.Count() / pageLines;
            var pageNumber =  0;

            while (pageNumber < pageCount)
            {
                var groupLines = lines.Skip(pageNumber * pageLines).Take(pageLines);

                var charVal = InterativeIntersect(groupLines).FirstOrDefault();

                var priority = char.IsLower(charVal) ? (int)charVal - 96 : (int)charVal - 38;

                runningTotal += priority;

                pageNumber++;
            }

            Console.WriteLine($"The sum of the priorites is {runningTotal}.");
        }

        private IEnumerable<char> InterativeIntersect(IEnumerable<string> values)
        {
            var enumerator = values.GetEnumerator();
            enumerator.MoveNext();

            IEnumerable<char> result = enumerator.Current;

            while (enumerator.MoveNext())
            {
                result = result.Intersect(enumerator.Current);
            }

            return result;
        }
    }
}
