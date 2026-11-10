namespace EverybodyCodes.Quests
{
    public static class Q11P1 
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

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Portalignment\Part 1\everybody_codes_e2026_q11_p1.txt");
            string[] lines = input.Split('\n');

            int n = int.Parse(lines[0].Split(": ").Last().Trim());
            lines = lines.Skip(2).ToArray();

            Graph g = new Graph(n);
            for(int i = 0; i < n; i++)
            {
                g.AddEdge(i, (i + 1) % n);
            }

            long initialStrain = g.AllPairShortestPath();
            foreach(string line in lines)
            {
                string[] words = line.Split(" ");
                int u = int.Parse(words[0]);
                int v = int.Parse(words[1]);
                g.AddEdge(u - 1, v - 1);
            }

            g.AllPairShortestPath();

            long finalStrain = g.AllPairShortestPath();
            Console.WriteLine(initialStrain - finalStrain);
        }
    }
}

