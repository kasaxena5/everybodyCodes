namespace EverybodyCodes.Quests
{
    public static class Q1P1 
    {
        public static long FindPerfectSquareUpperBound(long n)
        {
            long l = 1, r = n;
            while(l < r)
            {
                long mid = (l + r) / 2;
                if(mid * mid >= n)
                {
                    r = mid;
                }
                else
                {
                    l = mid + 1;
                }
            }

            return l * l;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Coneservation\Part 1\everybody_codes_e2026_q01_p1.txt");
            long[] outposts = input.Split(',').Select(long.Parse).ToArray();

            long answer = 0;
            foreach(long outpost in outposts)
            {
                long u = FindPerfectSquareUpperBound(outpost);
                answer += (u - outpost);
            }

            Console.WriteLine(answer);
        }
    }
}
