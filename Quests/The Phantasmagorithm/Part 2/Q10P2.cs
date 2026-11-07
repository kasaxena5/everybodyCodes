namespace EverybodyCodes.Quests
{
    public static class Q10P2 
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

        private static void Print(char[,] grid, Dictionary<(int, int), List<Ghost>> ghostMap)
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
                            Console.Write(ghostMap[(i, j)][0].Value);
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

        public class Ghost
        {
            public long Id;
            public char Value;
            public long Collisions;
        }

        private static void Simulate(char[,] grid, Dictionary<(int, int), List<Ghost>> ghostMap, int limit)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            long totalCollisions = 0;
            long round = 0;
            while(ghostMap.Count > 0)
            {
                char[,] nextGrid = (char[,])grid.Clone();
                var nextGhostMap = new Dictionary<(int, int), List<Ghost>>() ;
                long collisions = 0;
                foreach((int i, int j) in ghostMap.Keys)
                {
                    foreach(Ghost g in ghostMap[(i, j)])
                    {
                        (int di, int dj) = d[g.Value];
                        int ni = i + di;
                        int nj = j + dj;
                        if(grid[ni, nj] == '#')
                        {
                            g.Collisions++;
                            g.Value = clockwise[g.Value];
                            if(g.Collisions < limit)
                            {
                                if(!nextGhostMap.ContainsKey((i, j)))
                                    nextGhostMap[(i, j)] = new List<Ghost>();
                                nextGhostMap[(i, j)].Add(g);
                            }
                            collisions++;
                        }
                        else
                        {
                            if(!nextGhostMap.ContainsKey((ni, nj)))
                                nextGhostMap[(ni, nj)] = new List<Ghost>();
                            nextGhostMap[(ni, nj)].Add(g);
                        }
                    }
                }

                List<(int ,int)> keysToRemove = new List<(int, int)>();
                foreach((int i, int j) in nextGhostMap.Keys)
                {
                    int cnt = nextGhostMap[(i, j)].Count;
                    if(cnt > 1)
                    {
                        for(int k = 0; k < cnt; k++)
                        {
                            nextGhostMap[(i, j)][k].Collisions++;
                            nextGhostMap[(i, j)][k].Value = reverse[nextGhostMap[(i, j)][k].Value];
                            collisions++;
                        }

                        nextGhostMap[(i, j)] = nextGhostMap[(i, j)].Where(ghost => ghost.Collisions < limit).ToList();
                        if(nextGhostMap[(i, j)].Count == 0)
                            keysToRemove.Add((i, j));
                    }
                }
                foreach((int, int) key in keysToRemove)
                    nextGhostMap.Remove(key);

                grid = nextGrid;
                ghostMap = nextGhostMap;
                totalCollisions += collisions;
                round++;
            }

            Console.WriteLine(round);
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Phantasmagorithm\Part 2\everybody_codes_e2026_q10_p2.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            var ghostMap = new Dictionary<(int, int), List<Ghost>>();
            long id = 0;

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
                            ghostMap[(i, j)] = new List<Ghost>();
                        ghostMap[(i, j)].Add(
                            new Ghost {
                                Id = id,
                                Value= lines[i][j],
                                Collisions = 0
                            } 
                        );
                        id++;
                    }
                }
            }
            
            Simulate(grid, ghostMap, 10000);

        }
    }
}

