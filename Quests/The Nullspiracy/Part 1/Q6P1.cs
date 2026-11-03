namespace EverybodyCodes.Quests
{
    public static class Q6P1 
    {
        public class Graph
        {
            Dictionary<string, List<string>> adj;
            Dictionary<string, int> color;

            public Graph(Dictionary<string, List<string>> adj)
            {
                this.adj = adj;
                this.color = new Dictionary<string, int>();
            }

            public int Dfs(string u)
            {
                color[u] = 1;
                if(adj.ContainsKey(u)) {
                    foreach(string v in adj[u])
                    {
                        if(!color.ContainsKey(v))
                        {
                            Dfs(v);
                        }
                    }
                }

                color[u] = 2;
                return color.Count();
            }


        }
        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Nullspiracy\Part 1\everybody_codes_e2026_q06_p1.txt");
            string[] lines = input.Split('\n');
            var adj = new Dictionary<string, List<string>>();
            foreach(string line in lines)
            {
                string[] words = line.Split(":");
                string node = words[0].Trim();
                string[] neighbours = words[1].Split(",");
                adj[node] = new List<string>();
                foreach(string neighbour in neighbours)
                {
                    adj[node].Add(neighbour.Trim());
                }
            }

            Graph g = new Graph(adj);
            Console.WriteLine(g.Dfs("NULL") - 1);
        }
    }
}
