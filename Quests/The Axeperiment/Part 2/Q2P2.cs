namespace EverybodyCodes.Quests
{
    public static class Q2P2 
    {
        public class Graph
        {
            HashSet<long> nodes;
            Dictionary<long, int> colors;
            Dictionary<long, List<long>> adj;
            public string CheckSum = "";

            public Graph()
            {
                nodes = new HashSet<long>();
                colors = new Dictionary<long, int>();
                adj = new Dictionary<long, List<long>>();
            }

            public void AddEdge(long u, long v)
            {
                if(!adj.ContainsKey(u))
                    adj[u] = new List<long>();
                if(!adj.ContainsKey(v))
                    adj[v] = new List<long>();

                adj[u].Add(v);
                
                colors[u] = 0;
                colors[v] = 0;
                nodes.Add(u);
                nodes.Add(v);
            }

            public void Dfs()
            {
                foreach(long u in nodes) {
                    if(colors[u] == 0)
                        Dfs(u);
                }
            }

            void Dfs(long u)
            {
                colors[u] = 1;
                foreach(long v in adj[u])
                {
                    if(colors[v] == 0)
                    {
                        Dfs(v);
                    }
                }

                CheckSum += u.ToString();
                colors[u] = 2;
            }

        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Axeperiment\Part 2\everybody_codes_e2026_q02_p2.txt");
            string[] lines = input.Split('\n');
            long answer = 0;
            foreach(string line in lines)
            {
                Graph g = new Graph();
                string[] comparisions = line.Split(" ");
                foreach(var comparision in comparisions)
                {
                    long[] alloys;

                    if(comparision.Contains('<'))
                    {
                        alloys = comparision.Split('<').Select(long.Parse).ToArray();
                        g.AddEdge(alloys[0], alloys[1]);
                    }
                    else
                    {
                        alloys = comparision.Split('>').Select(long.Parse).ToArray();
                        g.AddEdge(alloys[1], alloys[0]);
                    }
                }
                g.Dfs();
                answer += long.Parse(g.CheckSum);
            }

            Console.WriteLine(answer);
        }
    }
}
