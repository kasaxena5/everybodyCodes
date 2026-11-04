using System.Data;
using System.Data.Common;

namespace EverybodyCodes.Quests
{
    public static class Q7P1 
    {
        public class Node
        {
            public int Id;
            public long Energy;
        }

        public class Graph
        {
            Dictionary<int, Node> nodes;
            Dictionary<int, List<int>> adj;

            public Graph()
            {
                nodes = new Dictionary<int, Node>();
                adj = new Dictionary<int, List<int>>();
            }

            public void AddNode(int id, long energy)
            {
                nodes[id] = new Node { Id=id, Energy=energy };
            }

            public void AddEdge(int u, int v)
            {
                if(!adj.ContainsKey(u))
                    adj[u] = new List<int>();

                adj[u].Add(v);
            }

            private int MaxConnectionNode()
            {
                int mx = -1, ans = -1;
                foreach(int u in adj.Keys)
                {
                    if(adj[u].Count > mx)
                    {
                        mx = adj[u].Count;
                        ans = u;
                    }
                }
                return ans;
            }

            public void SendImpulse(long impulse)
            {
                int u = MaxConnectionNode();
                SendImpulse(u, impulse);
            }

            private void SendImpulse(int u, long impulse)
            {
                if(impulse == 0)
                    return;

                nodes[u].Energy += impulse;
                foreach(int v in adj[u])
                {
                    SendImpulse(v, impulse - 1);
                }
            }

            public long CalculateEnergy()
            {
                long total = 0;
                foreach(Node node in nodes.Values)
                {
                    total += node.Energy;
                }

                return total;
            }

        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Explouding\Part 1\everybody_codes_e2026_q07_p1.txt");
            string[] lines = input.Split('\n');
            int n = lines.Length;
            Graph g = new Graph();
            foreach(string line in lines)
            {
                string[] words = line.Split(", ");

                int id = int.Parse(words[0].Split('=').Last());
                long energy = long.Parse(words[1].Split('=').Last());
                int[] neighbours = words[2].Split('=').Last().Split(',').Select(int.Parse).ToArray();

                g.AddNode(id, energy);
                foreach(int neighbour in neighbours) {
                    g.AddEdge(id, neighbour);
                }
            }

            g.SendImpulse(5);
            Console.WriteLine(g.CalculateEnergy());

        }
    }
}
