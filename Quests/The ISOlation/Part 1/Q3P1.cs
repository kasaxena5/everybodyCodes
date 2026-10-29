namespace EverybodyCodes.Quests
{
    public static class Q3P1 
    {
        static long Power(long x, int n)
        {
            if(n == 0)
                return 1L;
            
            if(n % 2 == 0)
                return Power(x * x, n / 2);
            else
                return x * Power(x * x, n / 2);

        }

        static long CalculateDecimal(long exponents, long value)
        {
            long dec = 0;
            int idx = 0;
            while(exponents > 0)
            {
                long expoDigit = exponents % 10;
                long valDigit = value % 10;

                dec += Power(expoDigit, idx) * valDigit;

                exponents /= 10;
                value /= 10;
                idx++;
            }
            return dec;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The ISOlation\Part 1\everybody_codes_e2026_q03_p1.txt");
            string[] lines = input.Split('\n');
            long answer = 0;
            foreach(string line in lines)
            {
                string[] words = line.Split(" ");
                long exponents = long.Parse(words[0].Substring(4));
                long value = long.Parse(words[1]);

                long dec = CalculateDecimal(exponents, value);
                answer += dec;
            }

            Console.WriteLine(answer);

        }
    }
}
