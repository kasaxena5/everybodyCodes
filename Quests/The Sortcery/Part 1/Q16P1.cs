using System.Text;

namespace EverybodyCodes.Quests
{
    public static class Q16P1 
    {
        private static void Flip(ref StringBuilder sb, int start, int end)
        {

            int i = start;
            int j = end;
            while(i < j)
            {
                char temp = sb[i];
                sb[i] = sb[j];
                sb[j] = temp;
                i++;
                j--;
            }
        }

        private static (int, int) GetCharGroup(ref StringBuilder sb, int i)
        {
            int n = sb.Length;
            char c = sb[i];
            int start = i, end = i;

            while(end < n && sb[end] == c)
                end++;

            while(start >= 0 && sb[start] == c)
                start--;


            return (start + 1, end - 1);
        }

        private static (int, int) FindLastCharGroup(ref StringBuilder sb, char c)
        {
            int n = sb.Length;
            int i = n - 1;
            while(i >= 0 && sb[i] != c)
                i--;
            return GetCharGroup(ref sb, i);
        }

        public static void Solve()
        {
            string s = File.ReadAllText(@"C:\Users\karti\everybodyCodes\Quests\The Sortcery\Part 1\everybody_codes_e2026_q16_p1.txt").Trim();
            StringBuilder sb = new StringBuilder(s);
            long flips = 0;
            int n = sb.Length;
            var f = new Dictionary<char, int>();
            foreach(char c in s)
            {
                if(!f.ContainsKey(c))
                    f[c] = 0;
                f[c]++;
            }

            int i = 0;
            while(i < n)
            {
                char c = sb[i];
                (int start, int end)= GetCharGroup(ref sb, i);
                int count = end - start + 1;
                if(count == f[c])
                {
                    i = end + 1;
                }
                else
                {
                    (int pairStart, int pairEnd) = FindLastCharGroup(ref sb, c);
                    Flip(ref sb, end + 1, pairEnd);
                    flips++;
                }
            }

            Console.WriteLine(flips);
        }
    }
}

