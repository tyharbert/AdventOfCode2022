namespace AdventOfCode2022.Day1
{
    internal sealed class Day1 : IChallengeDay
    {
        public int DayNumber => 1;

        public void RunChallengeOne()
        {
            int highestCalorieCount = 0;
            int currentElfCalorieTotal = 0;

            foreach (string line in File.ReadLines(@".\Day1\challenge-input.txt"))
            {
                
                if (int.TryParse(line, out int calorieCount))
                {
                    currentElfCalorieTotal += calorieCount;
                }
                else
                {
                    if (currentElfCalorieTotal > highestCalorieCount)
                    {
                        highestCalorieCount = currentElfCalorieTotal;
                    }

                    currentElfCalorieTotal = 0;
                }
            }

            Console.WriteLine($"The highest calorie total is: {highestCalorieCount}.");
        }

        public void RunChallengeTwo()
        {
            int[] elfCalorieCountLeaderBoard = new int[3];
            int currentElfCalorieTotal = 0;

            foreach (string line in File.ReadLines(@".\Day1\challenge-input.txt"))
            {

                if (int.TryParse(line, out int calorieCount))
                {
                    currentElfCalorieTotal += calorieCount;
                }
                else
                {
                    for (int i = 0; i < elfCalorieCountLeaderBoard.Length; i++)
                    {
                        if (currentElfCalorieTotal > elfCalorieCountLeaderBoard[i])
                        {
                            InsertIntoLeaderboardAtIndex(elfCalorieCountLeaderBoard, i, currentElfCalorieTotal);
                            break;
                        }
                    }

                    currentElfCalorieTotal = 0;
                }
            }

            Console.WriteLine($"The sum of the 3 highest calorie totals is: {elfCalorieCountLeaderBoard.Sum()} ({String.Join(", ", elfCalorieCountLeaderBoard)}).");
        }

        private void InsertIntoLeaderboardAtIndex(int[] leaderBoard, int index, int value)
        {
            // Shift lower scores to the right
            for (int i = leaderBoard.Length - 1; i > index ; i--)
            {
                leaderBoard[i] = leaderBoard[i - 1];
            }

            // Insert the new score
            leaderBoard[index] = value;
        }
    }
}
