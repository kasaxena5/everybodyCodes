namespace EverybodyCodes.Quests
{
    public static class Q12P3 
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
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Castastrophe\Part 3\everybody_codes_e2026_q12_p3.txt");
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
                        if((char)magma == '#')
                            magma++;
                        Dfs(grid, i, j, ref topLeft, ref bottomRight, (char)magma);

                        int sumv = 0, sumh = 0;
                        List<List<int>> intervalsV = new List<List<int>>(bottomRight.Item1 - topLeft.Item1 + 1);
                        List<List<int>> intervalsH = new List<List<int>>(bottomRight.Item2 - topLeft.Item2 + 1);
                        for(int r = topLeft.Item1; r <= bottomRight.Item1; r++)
                        {
                            intervalsV.Add(new List<int>());

                            for(int c = topLeft.Item2; c <= bottomRight.Item2;)
                            {
                                int k = c;
                                while(k <= bottomRight.Item2 && grid[r, k] == (char)magma)
                                    k++;
                                int len = k - c;
                                if(len > 0)
                                {
                                    intervalsV[r - topLeft.Item1].Add(len);
                                }
                                if(len == 0)
                                    c++;
                                else
                                    c = k;
                            }
                        }
                        for(int r = topLeft.Item1; r <= bottomRight.Item1; )
                        {
                            int k = r;
                            while(k <= bottomRight.Item1 && intervalsV[k - topLeft.Item1].SequenceEqual(intervalsV[r - topLeft.Item1]))
                                k++;
                            int len = k - r;
                            
                            foreach(int interval in intervalsV[r - topLeft.Item1])
                                sumv += Math.Min(interval, len);
                            
                            if(len == 0)
                                r++;
                            else
                                r = k;
                        }

                        for(int c = topLeft.Item2; c <= bottomRight.Item2; c++)
                        {
                            intervalsH.Add(new List<int>());

                            for(int r = topLeft.Item1; r <= bottomRight.Item1;)
                            {
                                int k = r;
                                while(k <= bottomRight.Item1 && grid[k, c] == (char)magma)
                                    k++;
                                int len = k - r;
                                if(len > 0)
                                {
                                    intervalsH[c - topLeft.Item2].Add(len);
                                }
                                if(len == 0)
                                    r++;
                                else
                                    r = k;
                            }
                        }
                        for(int c = topLeft.Item2; c <= bottomRight.Item2; )
                        {
                            int k = c;
                            while(k <= bottomRight.Item2 && intervalsH[k - topLeft.Item2].SequenceEqual(intervalsH[c - topLeft.Item2]))
                                k++;
                            int len = k - c;
                            
                            foreach(int interval in intervalsH[c - topLeft.Item2])
                                sumh += Math.Min(interval, len);
                            
                            if(len == 0)
                                c++;
                            else
                                c = k;
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