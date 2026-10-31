namespace EverybodyCodes.Quests
{
    public static class Q5P2 
    {
        static int[] di = new int[] {1, -1, 1, -1, 0, 0, 1, -1};
        static int[] dj = new int[] {1, -1, -1, 1, 1, -1, 0, 0};

        static bool CanBecomeLand(int i, int j, char[,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int cnt = 0;
            for (int k = 0; k < 8; k++)
            {
                int ni = i + di[k];
                int nj = j + dj[k];
                if (ni >= 0 && ni < n && nj >= 0 && nj < m)
                {
                    if (grid[ni, nj] == '#' || grid[ni, nj] == '1')
                        cnt++;
                }
            }

            return cnt >= 5;
        }

        static bool CanBecomeSand(int i, int j, char[,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int cnt = 0;
            for (int k = 0; k < 8; k++)
            {
                int ni = i + di[k];
                int nj = j + dj[k];
                if (ni >= 0 && ni < n && nj >= 0 && nj < m)
                {
                    if (grid[ni, nj] == '.')
                        return true;
                    if(grid[ni, nj] == '=')
                        cnt++;
                }
                else
                {
                    return true;
                }
            }

            return cnt >= 5;
        }

        private static int Count(char[,] grid, char c)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int cnt = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(grid[i, j] == c)
                        cnt++;
                }
            }

            return cnt;
        }

        private static void Print(char[,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Cartograffity\Part 2\everybody_codes_e2026_q05_p2.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            int[,] colors = new int[n, m];
            for(int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                    grid[i, j] = lines[i][j];
            
            var q = new Queue<(int, int)>();
            q.Enqueue((n / 2, m / 2));
            colors[n / 2, m / 2] = 1;

            while(q.Count > 0)
            {
                (int ci, int cj) = q.Dequeue();
                grid[ci, cj] = '#';
                colors[ci, cj] = 2;
                for(int k = 0; k < 8; k++)
                {
                    int ni = ci + di[k];
                    int nj = cj + dj[k];
                    if(ni >= 0 && ni < n && nj >= 0 && nj < m)
                    {
                        if(grid[ni, nj] != '#' && colors[ni, nj] == 0 && CanBecomeLand(ni, nj, grid))
                        {
                            colors[ni, nj] = 1;
                            q.Enqueue((ni, nj));
                        }
                    }
                }
            }

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    colors[i, j] = 0;
                    if(grid[i, j] != '#')
                        grid[i, j] = '.';
                }
            }

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(grid[i, j] == '#' && CanBecomeSand(i, j, grid))
                    {
                        colors[i, j] = 1;
                        q.Enqueue((i, j));
                    }
                }
            }

            while(q.Count > 0)
            {
                (int ci, int cj) = q.Dequeue();
                grid[ci, cj] = '=';
                colors[ci, cj] = 2;
                for(int k = 0; k < 8; k++)
                {
                    int ni = ci + di[k];
                    int nj = cj + dj[k];
                    if(ni >= 0 && ni < n && nj >= 0 && nj < m)
                    {
                        if(grid[ni, nj] == '#' && colors[ni, nj] == 0 && CanBecomeSand(ni, nj, grid))
                        {
                            colors[ni, nj] = 1;
                            q.Enqueue((ni, nj));
                        }
                    }
                }
            }

            Console.WriteLine(Count(grid, '='));

        }
    }
}

