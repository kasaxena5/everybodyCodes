namespace EverybodyCodes.Quests
{
    public static class Q17P2 
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

        private static long Perimeter(char[,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            long dist = 0;


            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(grid[i, j] != '#')
                        continue;
                    int cnt = 0;

                    for (int di = -1; di <= 1; di++)
                    {
                        for (int dj = -1; dj <= 1; dj++)
                        {
                            if (Math.Abs(di) == Math.Abs(dj))
                                continue;
                            int ni = i + di, nj = j + dj;
                            if (!(ni >= 0 && ni < n && nj >= 0 && nj < m) || grid[ni, nj] == '.')
                            {
                                cnt++;
                            }
                        }
                    }

                    dist += cnt;
                }
            }


            return dist;
        }


        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\karti\everybodyCodes\Quests\The Lavairynth\Part 2\everybody_codes_e2026_q17_p2.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            for(int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                    grid[i, j] = lines[i][j];
            

            Console.WriteLine(Perimeter(grid));


        }
    }
}

