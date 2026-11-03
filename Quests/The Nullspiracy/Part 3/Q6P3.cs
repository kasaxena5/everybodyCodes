namespace EverybodyCodes.Quests
{
    public static class Q6P3 
    {
        public class Graph
        {
            Dictionary<string, List<string>> adj;
            HashSet<string> nodes;
            Dictionary<string, int>[] color;

            public Graph(Dictionary<string, List<string>> adj, HashSet<string> nodes)
            {
                this.adj = adj;
                this.nodes = nodes;
                this.color = new Dictionary<string, int>[3];
                for(int i = 0; i < 3; i++)
                {
                    this.color[i] = new Dictionary<string, int>();
                }
            }

            public void Dfs(string u, int i)
            {
                color[i][u] = 1;
                if(adj.ContainsKey(u)) {
                    foreach(string v in adj[u])
                    {
                        if(!color[i].ContainsKey(v))
                        {
                            Dfs(v, i);
                        }
                    }
                }

                color[i][u] = 2;
            }

            public void Calculate()
            {
                int ans = 0;
                foreach(string node in nodes)
                {
                    if(node == "NULLv1" || node == "NULLv2" || node == "NULLv3")
                        continue;

                    int cnt = 0;
                    for(int i = 0; i < 3; i++)
                    {
                        if(color[i].ContainsKey(node))
                            cnt++;
                    }

                    if(cnt > 1)
                        ans++;
                }

                Console.WriteLine(ans);
            }

            public void PrintAdj()
            {
                foreach(var node in adj.Keys)
                {
                    Console.WriteLine($"{node}: {string.Join(',', adj[node])}");
                }
            }


        }
        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Nullspiracy\Part 3\everybody_codes_e2026_q06_p3.txt");
            string[] lines = input.Split('\n');
            var adj = new Dictionary<string, List<string>>();
            var nodes = new HashSet<string>();
            foreach(string line in lines)
            {
                string[] words = line.Split(":");
                string node = words[0].Trim();
                string[] neighbours = words[1].Split(",");
                foreach(string neighbour in neighbours)
                {
                    if(neighbour.Length == 0)
                        continue;
                    if(!adj.ContainsKey(neighbour.Trim()))
                    {
                        adj[neighbour.Trim()] = new List<string>();
                    }
                    adj[neighbour.Trim()].Add(node.Trim());
                    nodes.Add(neighbour.Trim());
                    nodes.Add(node.Trim());
                }
            }

            Graph g = new Graph(adj, nodes);
            g.Dfs("NULLv1", 0);
            g.Dfs("NULLv2", 1);
            g.Dfs("NULLv3", 2);

            g.Calculate();
        }
    }
}
