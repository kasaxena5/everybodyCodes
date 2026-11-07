using System.Text;

namespace EverybodyCodes.Quests
{
    public static class Q10P3 
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

        public class Ghost: IComparable<Ghost>
        {
            public long Id;
            public char Value;
            public long Collisions;

            public int CompareTo(Ghost other)
            {
                return Id.CompareTo(other.Id);
            }

            public override string ToString()
            {
                return $"{Id}:{Value}";
            }

            public Ghost Clone()
            {
                return new Ghost
                {
                    Id = this.Id,
                    Value = this.Value,
                    Collisions = this.Collisions
                };
            }
        }

        static List<Ghost> GetGhosts(Dictionary<(int, int), List<Ghost>> ghostMap)
        {
            var ghosts = new List<Ghost>();
            foreach(List<Ghost> ghostList in ghostMap.Values)
            {
                foreach(Ghost ghost in ghostList)
                {
                    ghosts.Add(ghost.Clone());
                }
            }
            ghosts.Sort();
            return ghosts;
        }

        static string ToString(char[,] grid, Dictionary<(int, int), List<Ghost>> ghostMap)
        {
            var sb = new StringBuilder();
            int n = grid.GetLength(0), m = grid.GetLength(1);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(ghostMap.ContainsKey((i, j)))
                    {
                        List<Ghost> ghosts = ghostMap[(i, j)];
                        ghosts.Sort();
                        sb.Append(string.Join(",", ghosts.Select(ghost => ghost.ToString())));
                    }
                    else
                    {
                        sb.Append(grid[i, j]);
                    }
                }
            }
            return sb.ToString();
        }

        private static void SimulateNextStep(ref char[,] grid, ref Dictionary<(int, int), List<Ghost>> ghostMap, long limit)
        {
            char[,] nextGrid = (char[,])grid.Clone();
            var nextGhostMap = new Dictionary<(int, int), List<Ghost>>();
            foreach ((int i, int j) in ghostMap.Keys)
            {
                foreach (Ghost g in ghostMap[(i, j)])
                {
                    (int di, int dj) = d[g.Value];
                    int ni = i + di;
                    int nj = j + dj;

                    Ghost ng = g.Clone();
                    if (grid[ni, nj] == '#')
                    {
                        ng.Collisions++;
                        ng.Value = clockwise[ng.Value];
                        if (ng.Collisions < limit)
                        {
                            if (!nextGhostMap.ContainsKey((i, j)))
                                nextGhostMap[(i, j)] = new List<Ghost>();
                            nextGhostMap[(i, j)].Add(ng);
                        }
                    }
                    else
                    {
                        if (!nextGhostMap.ContainsKey((ni, nj)))
                            nextGhostMap[(ni, nj)] = new List<Ghost>();
                        nextGhostMap[(ni, nj)].Add(ng);
                    }
                }
            }

            List<(int, int)> keysToRemove = new List<(int, int)>();
            foreach ((int i, int j) in nextGhostMap.Keys)
            {
                int cnt = nextGhostMap[(i, j)].Count;
                if (cnt > 1)
                {
                    for (int k = 0; k < cnt; k++)
                    {
                        nextGhostMap[(i, j)][k].Collisions++;
                        nextGhostMap[(i, j)][k].Value = reverse[nextGhostMap[(i, j)][k].Value];
                    }

                    nextGhostMap[(i, j)] = nextGhostMap[(i, j)].Where(ghost => ghost.Collisions < limit).ToList();
                    if (nextGhostMap[(i, j)].Count == 0)
                        keysToRemove.Add((i, j));
                }
            }
            foreach ((int, int) key in keysToRemove)
                nextGhostMap.Remove(key);

            grid = nextGrid;
            ghostMap = nextGhostMap;
        }

        private static void Simulate(char[,] grid, Dictionary<(int, int), List<Ghost>> ghostMap, long limit)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);
            long round = 0;
            var timeline = new Dictionary<string, (long, Dictionary<(int, int), List<Ghost>>)>();
            timeline[ToString(grid, ghostMap)] = (round, ghostMap);
            while(ghostMap.Count > 0)
            {
                SimulateNextStep(ref grid, ref ghostMap, limit);
                round++;
                if(timeline.ContainsKey(ToString(grid, ghostMap)))
                {
                    var oldGhosts = GetGhosts(timeline[ToString(grid, ghostMap)].Item2);
                    var ghosts = GetGhosts(ghostMap);

                    int count = ghosts.Count;
                    long mn = long.MaxValue;
                    for(int i = 0; i < count; i++)
                    {
                        long roundsToLimit = (limit - ghosts[i].Collisions - 1) / (ghosts[i].Collisions - oldGhosts[i].Collisions);
                        mn = Math.Min(mn, roundsToLimit);
                    }

                    foreach((int, int) key in ghostMap.Keys)
                    {
                        foreach(Ghost ghost in ghostMap[key])
                        {
                            Ghost oldGhost = oldGhosts.First(old => old.Id == ghost.Id);
                            ghost.Collisions += (ghost.Collisions - oldGhost.Collisions) * mn;
                        }
                    }

                    var oldRound = timeline[ToString(grid, ghostMap)].Item1;
                    round = round + mn * (round - oldRound);
                }
                timeline[ToString(grid, ghostMap)] = (round, ghostMap);
            }

            Console.WriteLine(round);
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Phantasmagorithm\Part 3\everybody_codes_e2026_q10_p3.txt");
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
            
            Simulate(grid, ghostMap, 1000000000000L);

        }
    }
}

