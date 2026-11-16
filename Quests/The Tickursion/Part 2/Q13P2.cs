namespace EverybodyCodes.Quests
{
    public static class Q13P2 
    {
        private static void Print(long[] num)
        {
            Console.WriteLine(string.Join(", ", num));
        }

        private static long Gcd(long a, long b)
        {
            if(b == 0)
                return a;

            return Gcd(b, a % b);
        }

        private static long Lcm(long a, long b)
        {
            return (a * b) / Gcd(a, b);
        }
        static long FloorDiv(long n, long d)
        {
            long q = n / d, r = n % d;
            return r < 0 ? q - 1 : q;
        }

        static long CeilDiv(long n, long d)
        {
            long q = n / d, r = n % d;
            return r > 0 ? q + 1 : q;
        }


        static long Count(long L, long U, long dirCross, long posDelta, long lcm)
        {
            return dirCross > 0
                ? FloorDiv(posDelta + U * dirCross, lcm)
                    - FloorDiv(posDelta + L * dirCross, lcm)
                : CeilDiv(posDelta + L * dirCross, lcm)
                    - CeilDiv(posDelta + U * dirCross, lcm);
        }


        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Tickursion\Part 2\everybody_codes_e2026_q13_p2.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length;
            long ans = 0;
            for(int i = 0; i < n; i += 5)
            {
                long[] phases = lines[i + 0].Split(" ").Last().Split(":").Select(long.Parse).ToArray();
                long[] dir = lines[i + 1].Split(" ").Last().Split(":").Select(c => (c.Trim() == "CW") ? 1L : -1L).ToArray();
                long[] initPhase = lines[i + 2].Split(" ").Last().Split(":").Select(long.Parse).ToArray();
                long[] T = lines[i + 3].Split(" ").Last().Split(":").Select(long.Parse).ToArray();

                long sol = Solve(phases, dir, initPhase, T);
                ans += sol;
            }

            Console.WriteLine(ans);
        }

        public static long Solve(long[] phases, long[] dir, long[] initPhase, long[] T)
        {
            long lcm = 1, gcd = 0; 
            foreach(var phase in phases)
            {
                lcm = Lcm(lcm, phase);
                gcd = Gcd(phase, gcd);
            }

            long[] v = phases.Select(phase => lcm / phase).ToArray();
            long[] pos = initPhase.ToArray();
            for(int i = 0; i < initPhase.Length; i++)
                pos[i] = pos[i] * v[i];
            
            int n = v.Length;
            long ans = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = i + 1; j < n; j++)
                {
                    long firstCount = Count(0, Math.Min(T[i], T[j]), (dir[i] * v[i] - dir[j] * v[j]), (pos[i] - pos[j]), lcm);
                    long secondCount = 0;
                    if(T[i] > T[j])
                        secondCount = Count(T[j], Math.Max(T[i], T[j]), (dir[i] * v[i]), (pos[i] - pos[j] - T[j] * dir[j] * v[j]), lcm);
                    else if(T[j] > T[i])
                        secondCount = Count(T[i], Math.Max(T[i], T[j]), (- dir[j] * v[j]), (pos[i] - pos[j] + T[i] * dir[i] * v[i]), lcm);

                    ans += (firstCount + secondCount);
                    // L < (k * lcm - posDelta) / dirCross <= U, k is Z, count the number of k
                }
            }

            return ans;
        }
    }
}