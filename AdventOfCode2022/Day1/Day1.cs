namespace AdventOfCode2022.Day1
{
    internal sealed class Day1 : IChallengeDay
    {
        public int DayNumber => 1;

        /*
        The jungle must be too overgrown and difficult to navigate in vehicles or access from the air; the Elves' expedition traditionally goes on foot. As your boats approach land, the Elves begin taking inventory of their supplies. One important consideration is food - in particular, the number of Calories each Elf is carrying (your puzzle input).


        The Elves take turns writing down the number of Calories contained by the various meals, snacks, rations, etc.that they've brought with them, one item per line. Each Elf separates their own inventory from the previous Elf's inventory(if any) by a blank line.

        For example, suppose the Elves finish writing their items' Calories and end up with the following list:
         
        1000
        2000
        3000
         
        4000
         
        5000
        6000
         
        7000
        8000
        9000
         
        10000
        This list represents the Calories of the food carried by five Elves:



        The first Elf is carrying food with 1000, 2000, and 3000 Calories, a total of 6000 Calories.
        The second Elf is carrying one food item with 4000 Calories.
        The third Elf is carrying food with 5000 and 6000 Calories, a total of 11000 Calories.
        The fourth Elf is carrying food with 7000, 8000, and 9000 Calories, a total of 24000 Calories.
        The fifth Elf is carrying one food item with 10000 Calories.
        In case the Elves get hungry and need extra snacks, they need to know which Elf to ask: they'd like to know how many Calories are being carried by the Elf carrying the most Calories. In the example above, this is 24000 (carried by the fourth Elf).



        Find the Elf carrying the most Calories.How many total Calories is that Elf carrying?
        */
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

        /*
        By the time you calculate the answer to the Elves' question, they've already realized that the Elf carrying the most Calories of food might eventually run out of snacks.

        To avoid this unacceptable situation, the Elves would instead like to know the total Calories carried by the top three Elves carrying the most Calories. That way, even if one of those Elves runs out of snacks, they still have two backups.

        In the example above, the top three Elves are the fourth Elf (with 24000 Calories), then the third Elf(with 11000 Calories), then the fifth Elf(with 10000 Calories). The sum of the Calories carried by these three elves is 45000.

        Find the top three Elves carrying the most Calories.How many Calories are those Elves carrying in total?
        */

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
