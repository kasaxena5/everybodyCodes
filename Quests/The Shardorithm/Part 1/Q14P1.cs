namespace EverybodyCodes.Quests
{
    public static class Q14P1 
    {
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
            Console.WriteLine();
        }

        private static void UpdateEdges(int i, int j, int di, int dj, char[,] grid, HashSet<(int, int)> edges)
        {
            int m = grid.GetLength(1) + 1;
            int idx = m * i + j;

            if(di == 0 && dj == 1) // R
            {
                edges.Add((idx + 1, idx + m + 1));
            }
            else if(di == 0 && dj == -1) // L
            {
                edges.Add((idx, idx + m));
            }
            else if(di == 1 && dj == 0) // D
            {
                edges.Add((idx + m, idx + m + 1));
            }
            else if(di == -1 && dj == 0) // U
            {
                edges.Add((idx, idx + 1));
            }
        }

        private static void Dfs(int i, int j, char[,] grid, int[,] color, char symbol, HashSet<(int, int)> edges)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            color[i, j] = 1;
            for(int di = -1; di <= 1; di++)
            {
                for(int dj = -1; dj <= 1; dj++)
                {
                    if(Math.Abs(di) == Math.Abs(dj))
                        continue;
                    
                    int ni = i + di;
                    int nj = j + dj;

                    if(!(ni >= 0 && ni < n && nj >= 0 && nj < m) ||  grid[ni, nj] != symbol)
                    {
                        UpdateEdges(i, j, di, dj, grid, edges);
                    }

                    if(ni >= 0 && ni < n && nj >= 0 && nj < m && grid[ni, nj] == symbol && color[ni, nj] == 0)
                    {
                        Dfs(ni, nj, grid, color, symbol, edges);
                    }
                }
            }
            color[i, j] = 2;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Shardorithm\Part 1\everybody_codes_e2026_q14_p1.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            int[,] color = new int[n, m];
            for(int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                    grid[i, j] = lines[i][j];
            

            Print(grid);
        
            HashSet<(int, int)> edges = new HashSet<(int, int)>();
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(color[i, j] == 0)
                    {
                        char symbol = grid[i, j];
                        Dfs(i, j, grid, color, symbol, edges);
                    }
                }
            }


            Console.WriteLine(edges.Count);
            

        }
    }
}

