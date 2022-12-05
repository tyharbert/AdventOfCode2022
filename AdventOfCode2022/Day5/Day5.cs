using System.Text;

namespace AdventOfCode2022.Day5
{
    internal sealed class Day5 : IChallengeDay
    {
        public int DayNumber => 5;

        public void RunChallengeOne()
        {
            var crateStacks = ParseStartingCrates();
            var instructions = ParseProcedure();

            foreach (var instruciton in instructions)
            {
                for (int i = 0; i < instruciton.MoveQuantity; i++)
                {
                    var value = crateStacks[instruciton.FromStack].Pop();
                    crateStacks[instruciton.ToStack].Push(value);
                }
            }

            StringBuilder builder = new StringBuilder();
            foreach (var stack in crateStacks)
            {
                builder.Append(stack.Value.Peek());
            }

            Console.WriteLine($"The crates at the top of each stack are {builder}.");
        }

        public void RunChallengeTwo()
        {
            var crateStacks = ParseStartingCrates();
            var instructions = ParseProcedure();

            foreach (var instruciton in instructions)
            {
                var tempStack = new Stack<char>();

                for (int i = 0; i < instruciton.MoveQuantity; i++)
                {
                    tempStack.Push(crateStacks[instruciton.FromStack].Pop());
                }

                for (int i = 0; i < instruciton.MoveQuantity; i++)
                {
                    crateStacks[instruciton.ToStack].Push(tempStack.Pop());
                }                    
            }

            StringBuilder builder = new StringBuilder();
            foreach (var stack in crateStacks)
            {
                builder.Append(stack.Value.Peek());
            }

            Console.WriteLine($"The crates at the top of each stack are {builder}.");
        }

        private List<Instruction> ParseProcedure()
        {
            List<Instruction> instructions = new List<Instruction>();

            foreach (string line in File.ReadLines(@".\Day5\rearrangement-procedure.txt"))
            {
                var values = line.Replace("move ", "")
                    .Replace(" from ", ";")
                    .Replace(" to ", ";")
                    .Trim()
                    .Split(";")
                    .Select(x => int.TryParse(x, out int val) ? val : throw new NotSupportedException("The rearrangement-procedure.txt is in the wrong format."))
                    .ToArray();

                if (values.Length < 3)
                {
                    throw new NotSupportedException("The rearrangement-procedure.txt is in the wrong format.");
                }

                instructions.Add(new Instruction(values[0], values[1], values[2]));
            }

            return instructions;
        }

        private Dictionary<int, Stack<char>> ParseStartingCrates()
        {
            Dictionary<int, Stack<char>> crateStacks = new Dictionary<int, Stack<char>>();

            // Reverse loads the whole file up front so this could be bad in production
            foreach (string line in File.ReadLines(@".\Day5\starting-crates.txt").Reverse())
            {
                // Parse ids
                if (crateStacks.Count == 0)
                {
                    foreach (char charVal in line)
                    {
                        if (char.IsNumber(charVal))
                        {
                            crateStacks.Add((int)char.GetNumericValue(charVal), new Stack<char>());
                        }
                    }
                    continue;
                }

                // Parse crates
                for (int i = 0; i < crateStacks.Count; i++)
                {
                    var crateVal = line.Skip((i * 4) + 1).FirstOrDefault();

                    if (char.IsLetter(crateVal))
                    {
                        crateStacks[i + 1].Push(crateVal);
                    }
                }
            }

            return crateStacks;
        }

        private sealed class Instruction
        {
            public Instruction(int moveQuantity, int fromStack, int toStack)            {
                MoveQuantity = moveQuantity;
                FromStack = fromStack;
                ToStack = toStack;
            }

            public int MoveQuantity { get; }
            public int FromStack { get; }
            public int ToStack { get; }
        }
    }
}
