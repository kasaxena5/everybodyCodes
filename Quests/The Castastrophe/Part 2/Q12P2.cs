namespace EverybodyCodes.Quests
{
    public static class Q12P2 
    {
        private static void Print(List<long> list)
        {
            Console.WriteLine($"{string.Join(',', list)}");
        }

        public static void Dfs(char[,] grid, int i, int j, ref (int, int) topLeft, ref (int, int) bottomRight, char marker)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            grid[i, j] = marker;
            topLeft = (Math.Min(topLeft.Item1, i), Math.Min(topLeft.Item2, j));
            bottomRight = (Math.Max(bottomRight.Item1, i), Math.Max(bottomRight.Item2, j));

            for(int di = -1; di <= 1; di++)
            {
                for(int dj = -1; dj <= 1; dj++)
                {
                    if(Math.Abs(di) == Math.Abs(dj))
                        continue;
                    
                    int ni = i + di;
                    int nj = j + dj;
                    if(ni >= 0 && ni < n && nj >= 0 && nj < m)
                    {
                        if(grid[ni, nj] == '#')
                            Dfs(grid, ni, nj, ref topLeft, ref bottomRight, marker);
                    }
                }
            }
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Castastrophe\Part 2\everybody_codes_e2026_q12_p2.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            for(int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                    grid[i, j] = lines[i][j];
            
            long ans = 0;
            int magma = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(grid[i, j] == '#')
                    {
                        (int, int) topLeft = (i, j);
                        (int, int) bottomRight = (i, j);
                        Dfs(grid, i, j, ref topLeft, ref bottomRight, (char)magma);

                        var fv = new Dictionary<int, int>();
                        var fh = new Dictionary<int, int>();
                        for(int r = topLeft.Item1; r <= bottomRight.Item1; r++)
                        {
                            int cnt = 0;
                            for(int c = topLeft.Item2; c <= bottomRight.Item2; c++)
                            {
                                if(grid[r, c] == (char)magma)
                                    cnt++;
                            }

                            if(!fv.ContainsKey(cnt))
                                fv[cnt] = 0;

                            fv[cnt]++;
                        }

                        for(int c = topLeft.Item2; c <= bottomRight.Item2; c++)
                        {
                            int cnt = 0;
                            for(int r = topLeft.Item1; r <= bottomRight.Item1; r++)
                            {
                                if(grid[r, c] == (char)magma)
                                    cnt++;
                            }

                            if(!fh.ContainsKey(cnt))
                                fh[cnt] = 0;

                            fh[cnt]++;
                        }

                        int sumv = 0, sumh = 0;
                        foreach(int key in fv.Keys)
                        {
                            int value = fv[key];
                            int mn = Math.Min(key, value);
                            sumv += mn;
                        }

                        foreach(int key in fh.Keys)
                        {
                            int value = fh[key];
                            int mn = Math.Min(key, value);
                            sumh += mn;
                        }

                        ans += Math.Min(sumh, sumv);
                        magma++;
                    }
                }
            }

            Console.WriteLine(ans);

        }
    }
}