namespace EverybodyCodes.Quests
{
    public static class Q20P1 
    {
        private static void Print(string[] s)
        {
            foreach(string c in s)
            {
                Console.Write(c);
            }
            Console.WriteLine();
        }

        private static long Evaluate(string[] expression)
        {
            if(expression.Length != 3)
            {
                throw new ArgumentException($"Element of Length {expression.Length}");
            }
            

            string symbol = expression[1];
            switch (symbol)
            {
                case "+":
                    return long.Parse(expression[0]) + long.Parse(expression[2]);
                case "-":
                    return long.Parse(expression[0]) - long.Parse(expression[2]);
                default:
                    return 0;
            }
        }

        private static long CountSegments(char c)
        {
            switch(c)
            {
                case '0':
                    return 6;
                case '1':
                    return 2;
                case '2':
                    return 5;
                case '3':
                    return 5;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 3;
                case '8':
                    return 7;
                case '9':
                    return 6;
                case '-':
                    return 1;
                case '+':
                    return 2;
                case '=':
                    return 2;
                default:
                    return 0;
            }
        }

        private static long CountSegments(string[] expression)
        {
            long segments = 0;
            foreach(string element in expression)
            {
                if(element.Length > 1)
                {
                    throw new ArgumentException($"Element of Length {element.Length}");
                }
                segments += CountSegments(element[0]);
            }
            return segments;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Matchmatics\Part 1\everybody_codes_e2026_q20_p1.txt");
            string[] lines = input.Split('\n');
            long ans = 0;
            foreach(string line in lines)
            {
                string[] lhs = (line.Split("=")[0].Trim()).Split(" ");
                string[] rhs = (line.Split("=")[1].Trim()).Split(" ");
                if(Evaluate(lhs) == Evaluate(rhs))
                {
                    ans += CountSegments(lhs) + CountSegments(rhs) + 2;
                }
            }

            Console.WriteLine(ans);

        }
    }
}
