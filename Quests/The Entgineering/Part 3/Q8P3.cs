namespace EverybodyCodes.Quests
{
    public static class Q8P3 
    {
        private static int CalculateShadow(int[,] grid, int i, int j, int mx)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            if(i >= 0 && i < n && j >= 0 && j < m)
            {
                if(grid[i, j] > 0) {
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
                else if(grid[i, j] < 0)
                {
                    mx = mx - 1;
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
                    if(grid[i, j] > 0)
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
                    else if(grid[i, j] < 0)
                    {
                        Console.Write("X");
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

        //  ↘ ,  ↙ ,  ← ,  ↖ ,  ↗  and  → 
        public static int[,] SouthEastShadow(int[,] grid)
        {
            // ↘
            int n = grid.GetLength(0), m = grid.GetLength(1);
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

            return copy;
        }

        public static int[,] SouthWestShadow(int[,] grid)
        {
            // ↙
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int[,] copy = (int[,])grid.Clone();
            for(int d = 0; d < n + m; d++)
            {
                int mx = 0;
                for(int i = 0; i < n; i++)
                {
                    int j = d - i;
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }

            return copy;
        }

        public static int[,] WestShadow(int[,] grid)
        {
            // ←
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int[,] copy = (int[,])grid.Clone();
            for(int i = 0; i < n; i++)
            {
                int mx = 0;
                for(int j = m - 1; j >= 0; j--)
                {
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }

            return copy;
        }

        public static int[,] NorthWestShadow(int[,] grid)
        {
            // ↖
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int[,] copy = (int[,])grid.Clone();
            for (int d = n; d >= -m; d--)
            {
                int mx = 0;
                for(int i = n - 1; i >= 0; i--)
                {
                    int j = i - d;
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }

            return copy;
        }


        public static int[,] NorthEastShadow(int[,] grid)
        {
            // ↗
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int[,] copy = (int[,])grid.Clone();
            for(int d = 0; d < n + m; d++)
            {
                int mx = 0;
                for(int i = n - 1; i >= 0; i--)
                {
                    int j = d - i;
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }

            return copy;
        }

        public static int[,] EastShadow(int[,] grid)
        {
            // →
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int[,] copy = (int[,])grid.Clone();
            for(int i = 0; i < n; i++)
            {
                int mx = 0;
                for(int j = 0; j < m; j++)
                {
                    mx = CalculateShadow(copy, i, j, mx);
                }
            }

            return copy;
        }

        public static int[,] Shadow(int[,] grid, int idx)
        {
            switch(idx)
            {
                case 0:
                    return SouthEastShadow(grid);
                case 1:
                    return SouthWestShadow(grid);
                case 2:
                    return WestShadow(grid);
                case 3:
                    return NorthWestShadow(grid);
                case 4:
                    return NorthEastShadow(grid);
                case 5:
                    return EastShadow(grid);
                default:
                    return new int[0, 0];
            }
        }

        private static int GrowAndRecruit(int[,] grid, int[,] copy)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int cnt = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(copy[i, j] != 0)
                    {
                        grid[i, j]++;
                        if(grid[i, j] == 10)
                        {
                            cnt++;
                            grid[i, j] = -3;
                        }
                        if(grid[i, j] == 0)
                        {
                            grid[i, j] = 1;
                        }
                    }
                }
            }
            return cnt;
        }

        private static void Simulate(int[,] grid)
        {
            int armyLimit = 100000;
            int army = 0;
            int idx = 0;
            long cnt = 0;
            while(army < armyLimit)
            {
                int[,] copy = Shadow(grid, idx % 6);
                cnt += Count(copy);

                army += GrowAndRecruit(grid, copy);
                idx++;
            }

            Console.WriteLine(cnt);
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Entgineering\Part 3\everybody_codes_e2026_q08_p3.txt");
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

            Simulate(grid);

        }
    }
}
