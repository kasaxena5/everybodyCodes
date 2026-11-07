namespace EverybodyCodes.Quests
{
    public static class Q10P1 
    {
        static Dictionary<char, (int, int)> d = new Dictionary<char, (int, int)>
        {
            ['>'] = (0, 1),
            ['<'] = (0, -1),
            ['^'] = (-1, 0),
            ['v'] = (1, 0),
        };

        static Dictionary<char, char> clockwise = new Dictionary<char, char>
        {
            ['>'] = 'v',
            ['<'] = '^',
            ['^'] = '>',
            ['v'] = '<',
        };

        static Dictionary<char, char> reverse = new Dictionary<char, char>
        {
            ['>'] = '<',
            ['<'] = '>',
            ['^'] = 'v',
            ['v'] = '^',
        };

        private static void Print(char[,] grid, Dictionary<(int, int), List<char>> ghostMap)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(ghostMap.ContainsKey((i, j)))
                    {
                        int cnt = ghostMap[(i, j)].Count;
                        if(cnt > 1)
                        {
                            Console.Write(cnt);
                        }
                        else
                        {
                            Console.Write(ghostMap[(i, j)][0]);
                        }
                    }
                    else
                    {
                        Console.Write(grid[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        private static void Simulate(char[,] grid, Dictionary<(int, int), List<char>> ghostMap, int rounds)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            long totalCollisions = 0;
            for(int round = 0; round < rounds; round++)
            {
                char[,] nextGrid = (char[,])grid.Clone();
                var nextGhostMap = new Dictionary<(int, int), List<char>>() ;
                long collisions = 0;
                foreach((int i, int j) in ghostMap.Keys)
                {
                    foreach(char c in ghostMap[(i, j)])
                    {
                        (int di, int dj) = d[c];
                        int ni = i + di;
                        int nj = j + dj;
                        if(grid[ni, nj] == '#')
                        {
                            if(!nextGhostMap.ContainsKey((i, j)))
                                nextGhostMap[(i, j)] = new List<char>();
                            nextGhostMap[(i, j)].Add(clockwise[c]);
                            collisions++;
                        }
                        else
                        {
                            if(!nextGhostMap.ContainsKey((ni, nj)))
                                nextGhostMap[(ni, nj)] = new List<char>();
                            nextGhostMap[(ni, nj)].Add(c);
                        }
                    }
                }
                foreach((int i, int j) in nextGhostMap.Keys)
                {
                    int cnt = nextGhostMap[(i, j)].Count;
                    if(cnt > 1)
                    {
                        for(int k = 0; k < cnt; k++)
                        {
                            nextGhostMap[(i, j)][k] = reverse[nextGhostMap[(i, j)][k]];
                            collisions++;
                        }
                    }
                }
                grid = nextGrid;
                ghostMap = nextGhostMap;
                totalCollisions += collisions;
            }

            Console.WriteLine(totalCollisions);
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Phantasmagorithm\Part 1\everybody_codes_e2026_q10_p1.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            var ghostMap = new Dictionary<(int, int), List<char>>();
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(lines[i][j] == '#' || lines[i][j] == '.')
                    {
                        grid[i, j] = lines[i][j];
                    }
                    else
                    {
                        grid[i, j] = '.';
                        if(!ghostMap.ContainsKey((i, j)))
                            ghostMap[(i, j)] = new List<char>();
                        ghostMap[(i, j)].Add(lines[i][j]);
                    }
                }
            }
            
            Simulate(grid, ghostMap, 10);

        }
    }
}

