namespace EverybodyCodes.Quests
{
    public static class Q12P1 
    {
        private static void Print(List<long> list)
        {
            Console.WriteLine($"{string.Join(',', list)}");
        }

        public static void Dfs(char[,] grid, int i, int j, ref (int, int) topLeft, ref (int, int) bottomRight)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            grid[i, j] = '@';
            if(topLeft.CompareTo((i, j)) > 0)
            {
                topLeft = (i, j);
            }
            if(bottomRight.CompareTo((i, j)) < 0)
            {
                bottomRight = (i, j);
            }

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
                            Dfs(grid, ni, nj, ref topLeft, ref bottomRight);
                    }
                }
            }
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Castastrophe\Part 1\everybody_codes_e2026_q12_p1.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            for(int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                    grid[i, j] = lines[i][j];
            
            long ans = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(grid[i, j] == '#')
                    {
                        (int, int) topLeft = (i, j);
                        (int, int) bottomRight = (i, j);
                        Dfs(grid, i, j, ref topLeft, ref bottomRight);
                        int length = Math.Abs(topLeft.Item1 - bottomRight.Item1) + 1;
                        int width = Math.Abs(topLeft.Item2 - bottomRight.Item2) + 1;
                        int mn = Math.Min(length, width);
                        ans += mn;
                    }
                }
            }

            Console.WriteLine(ans);

        }
    }
}