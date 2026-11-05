namespace EverybodyCodes.Quests
{
    public static class Q8P2 
    {
        private static int CalculateShadow(int[,] grid, int i, int j, int mx)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            if(i >= 0 && i < n && j >= 0 && j < m)
            {
                if(grid[i, j] != 0) {
                    if(mx - 1 >= grid[i, j])
                    {
                        grid[i, j] = 0;
                        mx = mx - 1;
                    }
                    else
                    {
                        mx = grid[i, j];
                    }
                }
            }

            return mx;
        }

        private static int Count(int[,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int cnt = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(grid[i, j] != 0)
                        cnt++;
                }
            }

            return cnt;
        }

        private static void Print(int[,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(grid[i, j] == 0)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write(grid[i, j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Entgineering\Part 2\everybody_codes_e2026_q08_p2.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            int[,] grid = new int[n, m];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(lines[i][j] == '.')
                        grid[i, j] = 0;
                    else
                        grid[i, j] = (int)(lines[i][j] - '0');
                }
            }

            int ans = 0;

            // ↘
            int[,] copy = (int[,])grid.Clone();
            for (int d = n; d >= -m; d--)
            {
                int mx = 0;
                for(int i = 0; i < n; i++)
                {
                    int j = i - d;
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }
            ans += Count(copy);

            // ↖
            copy = (int[,])grid.Clone();
            for (int d = n; d >= -m; d--)
            {
                int mx = 0;
                for(int i = n - 1; i >= 0; i--)
                {
                    int j = i - d;
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }
            ans += Count(copy);

            // →
            copy = (int[,])grid.Clone();
            for(int i = 0; i < n; i++)
            {
                int mx = 0;
                for(int j = 0; j < m; j++)
                {
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }
            ans += Count(copy);

            // ←
            copy = (int[,])grid.Clone();
            for(int i = 0; i < n; i++)
            {
                int mx = 0;
                for(int j = m - 1; j >= 0; j--)
                {
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }
            ans += Count(copy);

            // ↗
            copy = (int[,])grid.Clone();
            for(int d = 0; d < n + m; d++)
            {
                int mx = 0;
                for(int i = n - 1; i >= 0; i--)
                {
                    int j = d - i;
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }
            ans += Count(copy);

            // ↙
            copy = (int[,])grid.Clone();
            for(int d = 0; d < n + m; d++)
            {
                int mx = 0;
                for(int i = 0; i < n; i++)
                {
                    int j = d - i;
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }
            ans += Count(copy);

            Console.WriteLine(ans);

        }
    }
}
