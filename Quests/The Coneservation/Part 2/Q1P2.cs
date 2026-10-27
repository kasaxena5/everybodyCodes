namespace EverybodyCodes.Quests
{
    public static class Q1P2 
    {
        public static long FindBorderUpperBound(long n)
        {
            long l = 3, r = Math.Max(n, 3);
            while(l < r)
            {
                long mid = (l + r) / 2;
                if(4 * mid - 4 >= n)
                {
                    r = mid;
                }
                else
                {
                    l = mid + 1;
                }
            }

            return 4 * l - 4;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Coneservation\Part 2\everybody_codes_e2026_q01_p2.txt");
            long[] outposts = input.Split(',').Select(long.Parse).ToArray();

            long answer = 0;
            foreach(long outpost in outposts)
            {
                long u = FindBorderUpperBound(outpost);
                answer += (u - outpost);
            }

            Console.WriteLine(answer);
        }
    }
}
