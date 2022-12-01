namespace AdventOfCode2022
{
    internal static class ChallengeDayExtensions
    {
        public static void PrintResults(this IChallengeDay challengeDay)
        {
            Console.WriteLine($"Day {challengeDay.DayNumber}");

            Console.Write("Part 1: ");
            challengeDay.RunChallengeOne();

            Console.Write("Part 2: ");
            challengeDay.RunChallengeTwo();

            Console.WriteLine(Environment.NewLine);
        }
    }
}
