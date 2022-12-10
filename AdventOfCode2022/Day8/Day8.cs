namespace AdventOfCode2022.Day8
{
    internal sealed class Day8 : IChallengeDay
    {
        public int DayNumber => 8;

        public void RunChallengeOne()
        {
            var trees = ParseTrees();

            var rowCount = trees.GetLength(0);
            var colCount = trees.GetLength(1);

            var visible = new bool[rowCount - 2, colCount - 2];

            CheckMiddleVisibilityTop(trees, visible);
            CheckMiddleVisibilityBottom(trees, visible);
            CheckMiddleVisibilityLeft(trees, visible);
            CheckMiddleVisibilityRight(trees, visible);

            int count = CountMiddleVisibility(visible);

            // Add exterior visibility
            count += (rowCount * 2) + (colCount * 2) - 4;

            Console.WriteLine($"The count of visible trees is {count}.");
        }

        public void RunChallengeTwo()
        {
            var trees = ParseTrees();

            var rowCount = trees.GetLength(0);
            var colCount = trees.GetLength(1);

            int highScore = 0;

            for (var row = 1; row < rowCount - 1; row++)
            {
                for (var col = 1; col < colCount - 1; col++)
                {
                    int score = GetScoreUp(trees, row, col);
                    score *= GetScoreDown(trees, row, col);
                    score *= GetScoreLeft(trees, row, col);
                    score *= GetScoreRight(trees, row, col);

                    if (score > highScore)
                    {
                        highScore = score;
                    }
                }
            }

            Console.WriteLine($"The highest scenic score is {highScore}.");
        }

        private int GetScoreUp(int[,] trees, int rowStart, int col)
        {
            var compareValue = trees[rowStart, col];
            var score = 0;

            for (var row = rowStart - 1; row >= 0; row--)
            {
                score++;

                if (trees[row, col] >= compareValue)
                {
                    break;
                }
            }

            return score;
        }

        private int GetScoreDown(int[,] trees, int rowStart, int col)
        {
            var compareValue = trees[rowStart, col];
            var score = 0;

            for (var row = rowStart + 1; row < trees.GetLength(0); row++)
            {
                score++;

                if (trees[row, col] >= compareValue)
                {
                    break;
                }
            }

            return score;
        }

        private int GetScoreLeft(int[,] trees, int row, int colStart)
        {
            var compareValue = trees[row, colStart];
            var score = 0;

            for (var col = colStart - 1; col >= 0; col--)
            {
                score++;

                if (trees[row, col] >= compareValue)
                {
                    break;
                }
            }

            return score;
        }

        private int GetScoreRight(int[,] trees, int row, int colStart)
        {
            var compareValue = trees[row, colStart];
            var score = 0;

            for (var col = colStart + 1; col < trees.GetLength(1); col++)
            {
                score++;

                if (trees[row, col] >= compareValue)
                {
                    break;
                }
            }

            return score;
        }

        private static int CountMiddleVisibility(bool[,] visible)
        {
            int count = 0;
            for (int i = 0; i < visible.GetLength(0); i++)
            {
                for (int j = 0; j < visible.GetLength(1); j++)
                {
                    if (visible[i, j])
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static void CheckMiddleVisibilityTop(int[,] trees, bool[,] visible)
        {
            var rowCount = trees.GetLength(0);
            var colCount = trees.GetLength(1);

            int min;

            for (int col = 1; col < colCount - 1; col++)
            {
                min = trees[0, col];

                for (int row = 1; row < rowCount - 1; row++)
                {
                    if (trees[row, col] > min)
                    {
                        min = trees[row, col];
                        visible[row - 1, col - 1] = true;
                    }
                }
            }
        }

        private static void CheckMiddleVisibilityBottom(int[,] trees, bool[,] visible)
        {
            var rowCount = trees.GetLength(0);
            var colCount = trees.GetLength(1);

            int min;

            for (int col = 1; col < colCount - 1; col++)
            {
                min = trees[rowCount - 1, col];

                for (int row = rowCount - 2; row >= 1; row--)
                {
                    if (trees[row, col] > min)
                    {
                        min = trees[row, col];
                        visible[row - 1, col - 1] = true;
                    }
                }
            }
        }

        private static void CheckMiddleVisibilityLeft(int[,] trees, bool[,] visible)
        {
            var rowCount = trees.GetLength(0);
            var colCount = trees.GetLength(1);

            int min;

            for (int row = 1; row < rowCount - 1; row++)
            {
                min = trees[row, 0];

                for (int col = 1; col < colCount - 1; col++)
                {
                    if (trees[row, col] > min)
                    {
                        min = trees[row, col];
                        visible[row - 1, col - 1] = true;
                    }
                }
            }
        }

        private static void CheckMiddleVisibilityRight(int[,] trees, bool[,] visible)
        {
            var rowCount = trees.GetLength(0);
            var colCount = trees.GetLength(1);

            int min;

            for (int row = 1; row < rowCount - 1; row++)
            {
                min = trees[row, colCount - 1];

                for (int col = colCount - 2; col >= 1; col--)
                {
                    if (trees[row, col] > min)
                    {
                        min = trees[row, col];
                        visible[row - 1, col - 1] = true;
                    }
                }
            }
        }

        private static int[,] ParseTrees()
        {
            var lines = File.ReadLines(@".\Day8\challenge-input.txt").ToList();

            var rowCount = lines.Count;
            var colCount = lines.First().Length;

            var trees = new int[rowCount, colCount];

            for (var row = 0; row < rowCount; row++)
            {
                var rowValues = lines[row]
                        .Select(c => (int)char.GetNumericValue(c))
                        .ToArray();

                for (var col = 0; col < colCount; col++)
                {
                    trees[row, col] = rowValues[col];
                }
            }

            return trees;
        }
    }
}
