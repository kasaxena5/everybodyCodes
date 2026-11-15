namespace EverybodyCodes.Quests
{
    public static class Q14P3 
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

        private static void UpdateEdges(int i, int j, int di, int dj, char[,] grid, HashSet<(int, int)> edges, Dictionary<(int, int), int> weights)
        {
            int l = grid.GetLength(1) + 1;
            int idx = l * i + j;

            (int, int) edge = (-1, -1);
            if(di == 0 && dj == 1) // R
            {
                edge = (idx + 1, idx + l + 1);
            }
            else if(di == 0 && dj == -1) // L
            {
                edge = (idx, idx + l);
            }
            else if(di == 1 && dj == 0) // D
            {
                edge = (idx + l, idx + l + 1);
            }
            else if(di == -1 && dj == 0) // U
            {
                edge = (idx, idx + 1);
            }
            
            if(!edges.Contains(edge))
            {
                int n = grid.GetLength(0), m = grid.GetLength(1);
                int symbol = grid[i, j];
                int otherSymbol = '.';
                int ni = i + di;
                int nj = j + dj;
                if(ni >= 0 && ni < n && nj >= 0 && nj < m)
                {
                    otherSymbol = grid[ni, nj];
                }
                if(!weights.ContainsKey((Math.Min(symbol, otherSymbol), Math.Max(symbol, otherSymbol))))
                    weights[(Math.Min(symbol, otherSymbol), Math.Max(symbol, otherSymbol))] = 0;
                weights[(Math.Min(symbol, otherSymbol), Math.Max(symbol, otherSymbol))]++;
            }

            if(edge != (-1, -1))
            {
                edges.Add(edge);
            }
        }

        private static void Dfs(int i, int j, char[,] grid, int[,] color, char symbol, HashSet<(int, int)> edges, Dictionary<(int, int), int> weights)
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
                        UpdateEdges(i, j, di, dj, grid, edges, weights);
                    }

                    if(ni >= 0 && ni < n && nj >= 0 && nj < m && grid[ni, nj] == symbol && color[ni, nj] == 0)
                    {
                        Dfs(ni, nj, grid, color, symbol, edges, weights);
                    }
                }
            }
            color[i, j] = 2;
        }

        public class Graph
        {
            Dictionary<int, List<(int, int)>> adj;

            public Graph()
            {
                this.adj = new Dictionary<int, List<(int, int)>>();
            }

            public void AddEdge(int u, int v, int w)
            {
                if(!adj.ContainsKey(u))
                    adj[u] = new List<(int, int)>();
                if(!adj.ContainsKey(v))
                    adj[v] = new List<(int, int)>();

                adj[u].Add((v, w));
                adj[v].Add((u, w));
            }

            private Dictionary<int, List<(int, int)>> CopyAdj(int src1, int src2)
            {
                var updatedAdj = new Dictionary<int, List<(int, int)>>();
                foreach(int u in adj.Keys)
                {
                    updatedAdj[u] = new List<(int, int)>();
                    foreach((int v, int w) in adj[u])
                    {
                        if(u == src1 || u == src2)
                            updatedAdj[u].Add((v, 0));
                        else if(v == src1 || v == src2)
                            updatedAdj[u].Add((v, 0));
                        else
                            updatedAdj[u].Add((v, w));
                    }
                }

                return updatedAdj;
            }

            public int MinMST()
            {
                int mn = int.MaxValue;
                foreach(int u in adj.Keys)
                {
                    if(u == (int)'.')
                        continue;

                    foreach(int v in adj.Keys)
                    {
                        if(v == (int)'.' || u == v)
                            continue;

                        var updatedAdj = CopyAdj(u, v);
                        mn = Math.Min(mn, MST(updatedAdj));
                    }
                }

                return mn;
            }

            public int MST(Dictionary<int, List<(int, int)>> updatedAdj)
            {
                int total = 0;
                var pq = new PriorityQueue<(int, int), int>();
                HashSet<int> selected = new HashSet<int>();
                int src = (int)'.';
                foreach((int v, int w) in updatedAdj[src])
                {
                    pq.Enqueue((v, w), w);
                }
                selected.Add(src);

                while(pq.Count > 0)
                {
                    (int u, int w) = pq.Dequeue();
                    if(selected.Contains(u))
                        continue;

                    selected.Add(u);
                    total += w;

                    foreach((int v, int wv) in updatedAdj[u])
                    {
                        pq.Enqueue((v, wv), wv);
                    }
                }

                return total;
            }
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Shardorithm\Part 3\everybody_codes_e2026_q14_p3.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length, m = lines[0].Trim().Length;
            char[,] grid = new char[n, m];
            int[,] color = new int[n, m];
            for(int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                    grid[i, j] = lines[i][j];
            
            HashSet<(int, int)> edges = new HashSet<(int, int)>();
            var weights = new Dictionary<(int, int), int>();
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(color[i, j] == 0)
                    {
                        char symbol = grid[i, j];
                        Dfs(i, j, grid, color, symbol, edges, weights);
                    }
                }
            }

            Graph g = new Graph();
            foreach((int u, int v) in weights.Keys)
            {
                g.AddEdge(u, v, weights[(u, v)]);
            }
            
            Console.WriteLine(g.MinMST());
            

        }
    }
}

