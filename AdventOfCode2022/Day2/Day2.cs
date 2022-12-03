using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Day2
{
    internal class Day2 : IChallengeDay
    {
        public int DayNumber => 2;

        public void RunChallengeOne()
        {
            int totalScore = 0;

            foreach (string line in File.ReadLines(@".\Day2\challenge-input.txt"))
            {
                var opponentPlay = line.First();
                var myPlay = line.Last();

                var opponentScore = _scores[opponentPlay];
                var myScore = _scores[myPlay];

                totalScore += myScore;

                if (opponentScore == myScore)
                {
                    totalScore += 3;
                }
                else if (DecrementScore(myScore) == opponentScore)
                {
                    totalScore += 6;
                }                
            }

            Console.WriteLine($"The total score would be {totalScore}.");
        }

        private int DecrementScore(int score)
        {
            var temp = score - 1;

            return temp == 0 ? 3 : temp;
        }

        private int IncrementScore(int score)
        {
            var temp = score + 1;

            return temp == 4 ? 1 : temp;
        }

        // Scores
        private readonly IDictionary<char, int> _scores = new Dictionary<char, int> { { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'X', 1 }, { 'Y', 2 }, { 'Z', 3 } };

        public void RunChallengeTwo()
        {
            int totalScore = 0;

            foreach (string line in File.ReadLines(@".\Day2\challenge-input.txt"))
            {
                var opponentPlay = line.First();
                var decision = line.Last();

                var opponentScore = _scores[opponentPlay];
                var decisionScore = _scores[decision];

                // Tie
                if (decisionScore == 2)
                {
                    totalScore += 3 + opponentScore;
                }
                // Win
                else if (decisionScore == 3)
                {
                    totalScore += 6 + IncrementScore(opponentScore);
                }
                else
                {
                    totalScore += DecrementScore(opponentScore);
                }
            }

            Console.WriteLine($"The total score would be {totalScore}.");
        }
    }
}
