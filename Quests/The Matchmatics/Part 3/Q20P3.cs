namespace EverybodyCodes.Quests
{
    using System.Text;

    public static class Q20P3 
    {
        private static void Print(string[] s)
        {
            foreach(string c in s)
            {
                Console.Write(c);
            }
            Console.WriteLine();
        }

        private static long Power(long x, long n)
        {
            if (n == 0)
                return 1;
            
            if(n % 2 == 0)
                return Power(x * x, n / 2);
            return x * Power(x * x, n / 2);
        }

        private static long ExpToDecimal(char c)
        {
            if(c >= '0' && c <= '9')
            {
                return (long)(c - '0');
            }
            if(c == 'A')
                return 10;
            
            throw new ArgumentException($"Invalid char {c}");
        }

        private static char DecimalToExp(long d)
        {
            if(d >= 0 && d <= 9)
                return (char)(d + '0');
            if(d == 10)
                return 'A';

            throw new ArgumentException($"Invalid d {d}");
        }

        private static string ToExp(long value, long exp)
        {
            var sb = new StringBuilder();
            while(value > 0)
            {
                long digit = value % exp;
                sb.Append(DecimalToExp(digit));
                value /= exp;
            }

            var result = sb.ToString().ToCharArray();
            Array.Reverse(result);


            return new string(result);
        }

        private static long Parse(string value, long exp)
        {
            long decimalValue = 0;
            int n = value.Length;
            for(int i = n - 1; i >= 0; i--)
            {
                long power = n - 1 - i;
                long digitValue = ExpToDecimal(value[i]);
                decimalValue += digitValue * Power(exp, power);
            }

            return decimalValue;
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
                    return Parse(expression[0], 11) + Parse(expression[2], 11);
                case "-":
                    return Parse(expression[0], 11) - Parse(expression[2], 11);
                default:
                    return 0;
            }
        }

        private static List<char> PossibleFix(char a)
        {
            var results = new List<char>();
            if(ReArrangeOne(a).Length > 0)
            {
                foreach(char newA in ReArrangeOne(a))
                {
                    results.Add(newA);
                }
            }

            return results;
        }

        private static List<(char, char)> PossibleFix(char a, char b)
        {
            var results = new List<(char, char)>();
            if(AddOne(a).Length > 0 && SubtractOne(b).Length > 0)
            {
                foreach(char newA in AddOne(a))
                {
                    foreach(char newB in SubtractOne(b))
                    {
                        results.Add((newA, newB));
                    }
                }
            }
            
            if(AddOne(b).Length > 0 && SubtractOne(a).Length > 0)
            {
                foreach(char newB in AddOne(b))
                {
                    foreach(char newA in SubtractOne(a))
                    {
                        results.Add((newA, newB));
                    }
                }
            }

            return results;
        }

        private static List<string> PossibleFix(string a)
        {
            var results = new List<string>();
            int n = a.Length;
            for(int i = 0; i < n; i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    List<(char, char)> newAij = PossibleFix(a[i], a[j]);
                    foreach((char newAi, char newAj) in newAij)
                    {
                        var sb = new StringBuilder(a);
                        sb[i] = newAi;
                        sb[j] = newAj;
                        results.Add(sb.ToString());
                    }

                }
            }

            for(int i = 0; i < n; i++)
            {
                List<char> newAi = PossibleFix(a[i]);
                foreach(char newA in newAi)
                {
                    var sb = new StringBuilder(a);
                    sb[i] = newA;
                    results.Add(sb.ToString());
                }
            }

            return results;
        }

        private static List<(string, string)> PossibleFix(string a, string b)
        {
            var results = new List<(string, string)>();
            int n = a.Length;
            int m = b.Length;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    List<(char, char)> newAiBj = PossibleFix(a[i], b[j]);
                    foreach((char newAi, char newBj) in newAiBj)
                    {
                        var sbA = new StringBuilder(a);
                        var sbB = new StringBuilder(b);
                        sbA[i] = newAi;
                        sbB[j] = newBj;
                        results.Add((sbA.ToString(), sbB.ToString()));
                    }

                }
            }

            return results;
        }

        private static bool TryFixAndEvaluate(string[] lhs, string[] rhs)
        {
            int n = lhs.Length, m = rhs.Length;

            // cross lhs, rhs
            for (int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    foreach((string newLhsI, string newRhsJ) in PossibleFix(lhs[i], rhs[j]))
                    {
                        string[] newLhs = lhs.ToArray();
                        string[] newRhs = rhs.ToArray();

                        newLhs[i] = newLhsI;
                        newRhs[j] = newRhsJ;

                        if(Evaluate(newLhs) == Evaluate(newRhs))
                        {
                            return true;
                        }
                    }
                    
                }
            }

            // accross lhs
            for (int i = 0; i < n; i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    foreach((string newLhsI, string newLhsJ) in PossibleFix(lhs[i], lhs[j]))
                    {
                        string[] newLhs = lhs.ToArray();
                        newLhs[i] = newLhsI;
                        newLhs[j] = newLhsJ;

                        if(Evaluate(newLhs) == Evaluate(rhs))
                        {
                            return true;
                        }
                    }
                }
            }
            for(int i = 0; i < n; i++)
            {
                foreach(string newLhsI in PossibleFix(lhs[i]))
                {
                        string[] newLhs = lhs.ToArray();
                        newLhs[i] = newLhsI;

                        if(Evaluate(newLhs) == Evaluate(rhs))
                        {
                            return true;
                        }
                }
            }

            // across rhs
            for (int i = 0; i < m; i++)
            {
                for(int j = i + 1; j < m; j++)
                {
                    foreach((string newRhsI, string newRhsJ) in PossibleFix(rhs[i], rhs[j]))
                    {
                        string[] newRhs = rhs.ToArray();
                        newRhs[i] = newRhsI;
                        newRhs[j] = newRhsJ;

                        if(Evaluate(lhs) == Evaluate(newRhs))
                        {
                            return true;
                        }
                    }
                }
            }
            for(int i = 0; i < m; i++)
            {
                foreach(string newRhsI in PossibleFix(rhs[i]))
                {
                        string[] newRhs = rhs.ToArray();
                        newRhs[i] = newRhsI;

                        if(Evaluate(lhs) == Evaluate(newRhs))
                        {
                            return true;
                        }
                }
            }
            return false;
        }

        private static char[] AddOne(char c)
        {
            switch(c)
            {
                case '0':
                    return ['8'];
                case '1':
                    return ['7'];
                case '2':
                    return [];
                case '3':
                    return ['9'];
                case '4':
                    return [];
                case '5':
                    return ['6', '9'];
                case '6':
                    return ['8'];
                case '7':
                    return [];
                case '8':
                    return [];
                case '9':
                    return ['8'];
                case 'A':
                    return ['8'];
                case '-':
                    return ['+'];
                case '+':
                    return [];
                case '=':
                    return [];
                default:
                    return [];
            }
        }

        private static char[] SubtractOne(char c)
        {
            switch(c)
            {
                case '0':
                    return [];
                case '1':
                    return [];
                case '2':
                    return [];
                case '3':
                    return [];
                case '4':
                    return [];
                case '5':
                    return [];
                case '6':
                    return ['5'];
                case '7':
                    return ['1'];
                case '8':
                    return ['0', '6', '9', 'A'];
                case '9':
                    return ['3', '5'];
                case 'A':
                    return [];
                case '-':
                    return [];
                case '+':
                    return ['-'];
                case '=':
                    return [];
                default:
                    return [];
            }
        }

        private static char[] ReArrangeOne(char c)
        {
            switch(c)
            {
                case '0':
                    return ['6', '9', 'A'];
                case '1':
                    return [];
                case '2':
                    return ['3'];
                case '3':
                    return ['2', '5'];
                case '4':
                    return [];
                case '5':
                    return ['3'];
                case '6':
                    return ['0', '9', 'A'];
                case '7':
                    return [];
                case '8':
                    return [];
                case '9':
                    return ['0', '6', 'A'];
                case 'A':
                    return ['0', '6', '9'];
                case '-':
                    return [];
                case '+':
                    return [];
                case '=':
                    return [];
                default:
                    return [];
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
                case 'A':
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

        private static long CountSegments(string element)
        {
            long segments = 0;
            foreach(char c in element)
            {
                segments += CountSegments(c);
            }
            return segments;
        }

        private static long CountSegments(string[] expression)
        {
            long totalSegments = 0;
            foreach(string element in expression)
            {
                long segments = CountSegments(element);
                totalSegments += segments;
            }
            return totalSegments;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Matchmatics\Part 3\everybody_codes_e2026_q20_p3.txt");
            string[] lines = input.Split('\n');
            long ans = 0;
            foreach(string line in lines)
            {
                string[] lhs = (line.Split("=")[0].Trim()).Split(" ");
                string[] rhs = (line.Split("=")[1].Trim()).Split(" ");
                if(Evaluate(lhs) == Evaluate(rhs) || TryFixAndEvaluate(lhs, rhs))
                {
                    long segments = CountSegments(lhs) + CountSegments(rhs) + 2;
                    ans += segments;
                }
            }

            Console.WriteLine(ToExp(ans, 11));

        }
    }
}
