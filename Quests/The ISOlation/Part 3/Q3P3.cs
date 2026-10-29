namespace EverybodyCodes.Quests
{
    public static class Q3P3 
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
            long originalExponents = exponents;

            int idx = 0;
            while(value > 0)
            {
                exponents = originalExponents;
                while (exponents > 0)
                {
                    long expoDigit = exponents % 10;
                    long valDigit = value % 10;

                    dec += Power(expoDigit, idx) * valDigit;

                    exponents /= 10;
                    value /= 10;
                    idx++;
                }
            }
            return dec;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The ISOlation\Part 3\everybody_codes_e2026_q03_p3.txt");
            string[] lines = input.Split('\n');
            long answer = 0;
            foreach(string line in lines)
            {
                string[] words = line.Split(" ");
                long exponents = long.Parse(words[0].Substring(5));
                long value = long.Parse(words[1]);

                long dec = CalculateDecimal(exponents, value);
                bool flag = true;
                for(long minValue = 0; minValue < value; minValue++)
                {
                    long minValueDec = CalculateDecimal(exponents, minValue);
                    if(minValueDec == dec)
                    {
                        flag = false;
                        break;
                    }
                }
                if(flag)
                    answer += dec;

            }

            Console.WriteLine(answer);

        }
    }
}

/*
5322 137
d1 + 2 * d2 + 3^2 * d3 + 5^3 * d4 = 137 -> min(d4.d3.d2.d1)

*/