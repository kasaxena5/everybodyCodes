namespace EverybodyCodes.Quests
{
    public static class Q18P3 
    {
        private static int[] FirstLargerRight(List<(int, int)> heights)
        {
            int n = heights.Count;
            var right = new int[n];
            var st = new Stack<int>();
            for(int i = n - 1; i >= 0; i--)
            {
                while(st.Count > 0 && heights[st.Peek()].Item2 < heights[i].Item2)
                {
                    st.Pop();
                }

                right[i] = (st.Count > 0) ? st.Peek() : -1;
                st.Push(i);
            }

            return right;
        }

        private static int[] FirstLargerLeft(List<(int, int)> heights)
        {
            int n = heights.Count;
            var left= new int[n];
            var st = new Stack<int>();
            for(int i = 0; i < n; i++)
            {
                while(st.Count > 0 && heights[st.Peek()].Item2 < heights[i].Item2)
                {
                    st.Pop();
                }

                left[i] = (st.Count > 0) ? st.Peek() : -1;
                st.Push(i);
            }

            return left;
        }


        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\karti\everybodyCodes\Quests\The Keyometry\Part 3\everybody_codes_e2026_q18_p3.txt");
            string[] lines = input.Split('\n');
            List<((long, long), long)> heights = new List<((long, long), long)>();
            foreach(string line in lines)
            {
                long x = long.Parse(line.Split(' ')[0].Split('=')[1]);
                long h = long.Parse(line.Split(' ')[1].Split('=')[1]);
                long w = long.Parse(line.Split(' ')[2].Split('=')[1]);
                heights.Add(((x, x + w -1), h));
            }

            int n = heights.Count;
            // Console.WriteLine(string.Join(", ", heights.Select(h => h.Item2)));

            long[] preMax = new long[n];
            long[] sufMax = new long[n];
            for(int i = 0; i < n; i++)
            {
                if(i == 0)
                {
                    preMax[i] = heights[i].Item2;
                    continue;
                }

                preMax[i] = Math.Max(heights[i].Item2, preMax[i-1]);
            }

            for(int i = n - 1; i >= 0; i--)
            {
                if(i == n - 1)
                {
                    sufMax[i] = heights[i].Item2;
                    continue;
                }

                sufMax[i] = Math.Max(heights[i].Item2, sufMax[i+1]);
            }

            long ans = 0;
            for(int i = 0; i < n; i++)
            {
                (long startX, long endX) = heights[i].Item1;
                long w = endX - startX + 1;
                long h = heights[i].Item2;
                ans += w * h;
                ans += w * Math.Max(0, Math.Min(preMax[i], sufMax[i]) - h);
                if(i < n - 1)
                {
                    (long nStartX, long nEndX) = heights[i + 1].Item1;
                    long gap = nStartX - endX - 1;
                    ans += gap * Math.Min(preMax[i], sufMax[i+1]);
                }
            }
            Console.WriteLine(ans);
        }
    }
}

