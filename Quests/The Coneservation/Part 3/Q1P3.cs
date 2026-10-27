namespace EverybodyCodes.Quests
{
    public static class Q1P3 
    {
        public static long FindOddBorderUpperBound(long n)
        {
            long l = 3, r = Math.Max(n, 3);
            while(l < r)
            {
                long mid = (l + r) / 2;
                if(mid * mid - 1 >= n)
                {
                    r = mid;
                }
                else
                {
                    l = mid + 1;
                }
            }

            if(l % 2 == 0)
                l++;
            return l * l - 1;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Coneservation\Part 3\everybody_codes_e2026_q01_p3.txt");
            long[] outposts = input.Split(',').Select(long.Parse).ToArray();

            long answer = 0;
            foreach(long outpost in outposts)
            {
                long u = FindOddBorderUpperBound(outpost);
                answer += (u - outpost);
            }

            Console.WriteLine(answer);
        }
    }
}
