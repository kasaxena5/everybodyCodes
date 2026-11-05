namespace EverybodyCodes.Quests
{
    public static class Q8P1 
    {
        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Entgineering\Part 1\everybody_codes_e2026_q08_p1.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            Console.WriteLine($"n: {n}, m: {m}");
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
            for(int d = n; d >= -m; d--)
            {
                int mx = 0;
                for(int i = 0; i < n; i++)
                {
                    int j = i - d;
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

                            if(grid[i, j] != 0) {
                                ans++;
                            }
                        }
                    }
                }

            }
            Console.WriteLine(ans);


        }
    }
}
