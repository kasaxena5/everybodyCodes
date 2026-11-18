namespace EverybodyCodes.Quests
{
    public static class Q17P3 
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

        private static long ShortPerimeter(char[,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1);

            Graph g = new Graph();
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(grid[i, j] != '#')
                        continue;

                    for (int di = -1; di <= 1; di++)
                    {
                        for (int dj = -1; dj <= 1; dj++)
                        {
                            if (Math.Abs(di) == Math.Abs(dj))
                                continue;
                            int ni = i + di, nj = j + dj;
                            if (!(ni >= 0 && ni < n && nj >= 0 && nj < m) || grid[ni, nj] == '.')
                            {
                                AddEdge(i, j, di, dj, grid, g);
                            }
                        }
                    }

                }
            }


            int x = grid.GetLength(1) + 1;
            int idx = x * (n - 1) + (m - 1);
            return g.Bfs(idx + x + 1, grid);
        }

        public class Graph
        {
            Dictionary<int, List<int>> adj;

            public Graph()
            {
                adj = new Dictionary<int, List<int>>();
            }
            
            public void AddEdge(int u, int v)
            {
                if(!adj.ContainsKey(u))
                    adj[u] = new List<int>();
                if(!adj.ContainsKey(v))
                    adj[v] = new List<int>();

                adj[u].Add(v);
                adj[v].Add(u);
            }

            private bool InSameSquare(int u, int v, char[,] grid)
            {
                int m = grid.GetLength(1) + 1;
                if(Math.Abs(u - v) == m - 1)
                {
                    int idx = Math.Min(u, v) - 1;
                    int i = idx / m;
                    int j = idx % m;

                    return grid[i, j] == '#';
                }
                else if(Math.Abs(u - v) != m + 1)
                {
                    int idx = Math.Min(u, v);
                    int i = idx / m;
                    int j = idx % m;

                    return grid[i, j] == '#';
                }
                
                return false;
            }

            public long Bfs(int dst, char[,] grid)
            {
                var q = new Queue<(int, int)>();
                q.Enqueue((0, 0));
                long dist = 0;

                while (q.Count > 0)
                {
                    var nq = new Queue<(int, int)>();

                    while (q.Count > 0)
                    {
                        (int u, int prev) = q.Dequeue();
                        if (u == dst)
                            return dist;

                        if(adj[u].Count == 2)
                        {
                            foreach (int v in adj[u])
                            {
                                if (v != prev)
                                {
                                    nq.Enqueue((v, u));
                                }
                            }
                        }
                        else
                        {
                            foreach (int v in adj[u])
                            {
                                if (v != prev && InSameSquare(v, prev, grid))
                                {
                                    nq.Enqueue((v, u));
                                }
                            }
                        }
                    }

                    q = nq;
                    dist++;
                }

                return -1;
            }
        }
        
        private static void AddEdge(int i, int j, int di, int dj, char[,] grid, Graph g)
        {
            int m = grid.GetLength(1) + 1;
            int idx = m * i + j;

            if(di == 0 && dj == 1) // R
            {
                g.AddEdge(idx + 1, idx + m + 1);
            }
            else if(di == 0 && dj == -1) // L
            {
                g.AddEdge(idx, idx + m);
            }
            else if(di == 1 && dj == 0) // D
            {
                g.AddEdge(idx + m, idx + m + 1);
            }
            else if(di == -1 && dj == 0) // U
            {
                g.AddEdge(idx, idx + 1);
            }
        }


        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\karti\everybodyCodes\Quests\The Lavairynth\Part 3\everybody_codes_e2026_q17_p3.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            for(int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                    grid[i, j] = lines[i][j];
            

            Console.WriteLine(ShortPerimeter(grid));


        }
    }
}

