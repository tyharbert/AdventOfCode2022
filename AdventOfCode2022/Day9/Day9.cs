namespace AdventOfCode2022.Day9
{
    internal sealed class Day9 : IChallengeDay
    {
        public int DayNumber => 9;

        public void RunChallengeOne()
        {
            HashSet<string> tailHistory = new HashSet<string>();

            Index h = new Index { X = 0, Y = 0 };
            Index t = new Index { X = 0, Y = 0 };

            foreach (var line in File.ReadLines(@".\Day9\challenge-input.txt"))
            {
                var rule = line.Split(' ')
                    .ToArray();

                var moveCommand = rule[0].FirstOrDefault();
                var moveAmount = int.Parse(rule[1]);

                for (var i = 0; i < moveAmount; i++)
                {

                    switch (moveCommand)
                    {
                        case 'U':
                            MoveUp(h);
                            break;
                        case 'D':
                            MoveDown(h);
                            break;
                        case 'L':
                            MoveLeft(h);
                            break;
                        case 'R':
                            MoveRight(h);
                            break;
                        default:
                            throw new NotSupportedException();
                    }

                    FollowTheLeader(h, t);
                    MarkTailWasHere(tailHistory, t);
                }
            }

            Console.WriteLine($"The key was in this many locations: {tailHistory.Count()}");
        }

        public void RunChallengeTwo()
        {
            HashSet<string> tailHistory = new HashSet<string>();

            Index h = new Index { X = 0, Y = 0 };
            Index[] tailKnots = Enumerable.Repeat(0, 9).Select(x => new Index()).ToArray();

            foreach (var line in File.ReadLines(@".\Day9\challenge-input.txt"))
            {
                var rule = line.Split(' ')
                    .ToArray();

                var moveCommand = rule[0].FirstOrDefault();
                var moveAmount = int.Parse(rule[1]);

                for (var i = 0; i < moveAmount; i++)
                {

                    switch (moveCommand)
                    {
                        case 'U':
                            MoveUp(h);
                            break;
                        case 'D':
                            MoveDown(h);
                            break;
                        case 'L':
                            MoveLeft(h);
                            break;
                        case 'R':
                            MoveRight(h);
                            break;
                        default:
                            throw new NotSupportedException();
                    }

                    FollowTheLeader(h, tailKnots.First());

                    for (int k = 1; k < tailKnots.Length; k++)
                    {
                        FollowTheLeader(tailKnots[k - 1], tailKnots[k]);
                    }

                    MarkTailWasHere(tailHistory, tailKnots.Last());
                }
            }

            Console.WriteLine($"The key was in this many locations: {tailHistory.Count()}");
        }

        private void FollowTheLeader(Index leader, Index follower)
        {
            int changeX = 0;
            int changeY = 0;

            if (leader.X == follower.X)
            {
                changeY = leader.Y - follower.Y;
            }
            else if (leader.Y == follower.Y)
            {
                changeX = leader.X - follower.X;
            }
            else
            {
                changeX = leader.X - follower.X;
                changeY = leader.Y - follower.Y;
            }

            
            if (Math.Abs(changeX) == 2)
            {
                // Move right
                if (changeX > 0)
                {
                    MoveRight(follower);
                }
                // Move left
                else if (changeX < 0)
                {
                    MoveLeft(follower);
                }

                // Move up (diagonal)
                if (changeY > 0)
                {
                    MoveUp(follower);
                }
                // Move down (diagonal)
                else if (changeY < 0)
                {
                    MoveDown(follower);
                }
            }
            else if(Math.Abs(changeY) == 2)
            {
                // Move up
                if (changeY > 0)
                {
                    MoveUp(follower);
                }
                // Move down
                else if (changeY < 0)
                {
                    MoveDown(follower);
                }

                // Move right (diagonal)
                if (changeX > 0)
                {
                    MoveRight(follower);
                }
                // Move left (diagonal)
                else if (changeX < 0)
                {
                    MoveLeft(follower);
                }
            }
        }

        private Index MoveRight(Index index)
        {
            index.X += 1;

            return index;
        }

        private Index MoveLeft(Index index)
        {
            index.X -= 1;

            return index;
        }

        private Index MoveUp(Index index)
        {
            index.Y += 1;

            return index;
        }

        private Index MoveDown(Index index)
        {
            index.Y -= 1;

            return index;
        }

        private void MarkTailWasHere(HashSet<string> tailHistory, Index tail)
        {
            var key = $"{tail.X}{tail.Y}";

            if (!tailHistory.Contains(key))
            {
                tailHistory.Add(key);
            }
        }

        private sealed class Index
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
