using System.Text;

namespace EverybodyCodes.Quests
{
    public static class Q20P2 
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
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Matchmatics\Part 2\everybody_codes_e2026_q20_p2.txt");
            string[] lines = input.Split('\n');
            long ans = 0;
            foreach(string line in lines)
            {
                string[] lhs = (line.Split("=")[0].Trim()).Split(" ");
                string[] rhs = (line.Split("=")[1].Trim()).Split(" ");
                if(Evaluate(lhs) == Evaluate(rhs))
                {
                    long segments = CountSegments(lhs) + CountSegments(rhs) + 2;
                    ans += segments;
                }
            }

            Console.WriteLine(ToExp(ans, 11));

        }
    }
}
