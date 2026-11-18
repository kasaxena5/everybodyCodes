namespace EverybodyCodes.Quests
{
    public static class Q17P1 
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

        private static long Bfs(char[,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            int[,] color = new int[n, m];

            var q = new Queue<(int, int)>();
            q.Enqueue((0, 0));
            color[0, 0] = 1;
            long dist = 0;

            while(q.Count > 0)
            {
                var nq = new Queue<(int, int)>();

                while(q.Count > 0)
                {
                    (int i, int j) = q.Dequeue();
                    if(i == n - 1 && j == m - 1)
                        return dist;

                    for (int di = -1; di <= 1; di++)
                    {
                        for (int dj = -1; dj <= 1; dj++)
                        {
                            if(Math.Abs(di) == Math.Abs(dj))
                                continue;
                            int ni = i + di, nj = j + dj;
                            if(ni >= 0 && ni < n && nj >= 0 && nj < m && grid[ni, nj] == '#' && color[ni, nj] == 0)
                            {
                                nq.Enqueue((ni, nj));
                                color[ni, nj] = 1;
                            }
                        }
                    }
                    color[i, j] = 2;
                }

                q = nq;
                dist++;
            }

            return -1;
        }


        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\karti\everybodyCodes\Quests\The Lavairynth\Part 1\everybody_codes_e2026_q17_p1.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            for(int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                    grid[i, j] = lines[i][j];
            

            Console.WriteLine(Bfs(grid));


        }
    }
}

