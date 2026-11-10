namespace EverybodyCodes.Quests
{
    public static class Q11P3 
    {
        public class Graph
        {
            int n;
            int[,] adj;

            public Graph(int n)
            {
                this.n = n;
                this.adj = new int[n, n];
                for(int i = 0; i < n; i++)
                    for(int j = 0; j < n; j++)
                        adj[i, j] = 0;
            }

            public void AddEdge(int u, int v)
            {
                adj[u, v] = 1;
                adj[v, u] = 1;
            }

            public void RemoveEdge(int u, int v)
            {
                adj[u, v] = 0;
                adj[v, u] = 0;
            }

            public bool HasEdge(int u, int v)
            {
                return adj[u, v] == 1;
            }

            public long AllPairShortestPath()
            {
                long[,] dist = new long[n, n];
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < n; j++)
                    {
                        if(i == j)
                            dist[i, j] = 0;
                        else if(adj[i, j] == 1)
                            dist[i, j] = 1;
                        else
                            dist[i, j] = long.MaxValue;
                    }
                }
                for (int k = 0; k < n; k++)
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (dist[i, k] != long.MaxValue 
                                && dist[k, j] != long.MaxValue)
                            {
                                dist[i, j] = Math.Min(dist[i, j], dist[i, k] + dist[k, j]);
                            }
                        }
                    }
                }

                long answer = 0;
                for(int i = 0; i < n; i++)
                    for(int j = 0; j < n; j++)
                        answer += dist[i, j];
                
                return answer;
            }
        }

        public static void AddPairs(int[] points, bool[] taken, int cnt, Graph g, ref long finalStrain)
        {
            if(cnt == 4)
            {
                long strain = g.AllPairShortestPath();
                finalStrain = Math.Min(finalStrain, strain);
                return;
            }
            for(int i = 0; i < 8; i++)
            {
                for(int j = i + 1; j < 8; j++)
                {
                    if(taken[i] || taken[j])
                        continue;
                    if(g.HasEdge(points[i], points[j]))
                        continue;

                    taken[i] = true;
                    taken[j] = true;
                    g.AddEdge(points[i], points[j]);

                    AddPairs(points, taken, cnt + 1, g, ref finalStrain);

                    g.RemoveEdge(points[i], points[j]);
                    taken[i] = false;
                    taken[j] = false;
                }
            }
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Portalignment\Part 3\everybody_codes_e2026_q11_p3.txt");
            string[] lines = input.Split('\n');

            int n = int.Parse(lines[0].Split(": ").Last().Trim());
            lines = lines.Skip(2).ToArray();

            Graph g = new Graph(n);
            for(int i = 0; i < n; i++)
            {
                g.AddEdge(i, (i + 1) % n);
                g.AddEdge(i, (i + 2) % n);
                g.AddEdge(i, (i + 3) % n);
            }

            long initialStrain = g.AllPairShortestPath();
            foreach(string line in lines)
            {
                string[] words = line.Split(" ");
                int u = int.Parse(words[0]);
                int v = int.Parse(words[1]);
                g.AddEdge(u - 1, v - 1);
            }

            long finalStrain = initialStrain;
            for(int i = 0; i < n / 8; i++)
            {
                int[] points = new int[8];
                bool[] taken = new bool[8];
                for(int j = 0; j < 8; j++)
                {
                    points[j] = i + j * (n / 8);
                }
                AddPairs(points, taken, 0, g, ref finalStrain);
            }

            Console.WriteLine(initialStrain - finalStrain);
        }
    }
}

