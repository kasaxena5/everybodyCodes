namespace EverybodyCodes.Quests
{
    public static class Q7P3 
    {
        public class Node
        {
            public int Id;
            public long InitialEnergy;
            public long Energy;
        }

        public class Graph
        {
            Dictionary<int, Node> nodes;
            HashSet<int> burstNodes;
            Dictionary<int, List<int>> adj;
            long limit = 1000000;

            public Graph()
            {
                nodes = new Dictionary<int, Node>();
                adj = new Dictionary<int, List<int>>();
                burstNodes = new HashSet<int>();
            }

            public void AddNode(int id, long energy)
            {
                nodes[id] = new Node { Id=id, InitialEnergy=energy, Energy=energy };
            }

            public void AddEdge(int u, int v)
            {
                if(!adj.ContainsKey(u))
                    adj[u] = new List<int>();

                adj[u].Add(v);
            }

            public long CalculateMaxEnergy(long impulse)
            {
                long maxEnergy = 0;
                foreach(int u in nodes.Keys)
                {
                    Reset();
                    SendImpulse(u, impulse);
                    long totalEnergy = CalculateEnergy();
                    maxEnergy = Math.Max(maxEnergy, totalEnergy);
                }

                return maxEnergy;
            }

            public class Wave 
            {
                public long InitialImpulse;
                public long InitiatedTime;
                public long ImpulseCount;
            }

            private void SendImpulse(int src, long startImpulse)
            {
                var q = new Queue<(int, Wave)>();
                Wave srcW = new Wave
                {
                    InitialImpulse = startImpulse,
                    ImpulseCount = 1,
                    InitiatedTime = 0 
                };
                q.Enqueue((src, srcW));

                long currentTime = 0;


                while(q.Count > 0)
                {
                    var waves = new Dictionary<int, List<Wave>>();
                    var newBurstedNodes = new HashSet<int>();

                    while(q.Count > 0)
                    {
                        (int u, Wave wave) = q.Dequeue();
                        long impulse = Math.Max(wave.InitialImpulse - (currentTime - wave.InitiatedTime), 0);
                        if (impulse == 0)
                            continue;
                        if (burstNodes.Contains(u))
                            continue;

                        foreach (int v in adj[u])
                        {
                            if(burstNodes.Contains(v))
                                continue;

                            // Waves Logic Start
                            if(!waves.ContainsKey(v))
                                waves[v] = new List<Wave>();

                            bool found = false;
                            foreach(Wave existingWave in waves[v])
                            {
                                if(existingWave.InitiatedTime == wave.InitiatedTime)
                                {
                                    existingWave.ImpulseCount += wave.ImpulseCount;
                                    found = true;
                                }
                            }
                            if(!found)
                            {
                                waves[v].Add(new Wave
                                {
                                    InitialImpulse = wave.InitialImpulse,
                                    InitiatedTime = wave.InitiatedTime,
                                    ImpulseCount = wave.ImpulseCount
                                });
                            }
                            // Waves Logic End

                        }
                        nodes[u].Energy += impulse * wave.ImpulseCount;
                        if(nodes[u].Energy >= limit)
                            newBurstedNodes.Add(u);
                    }

                    foreach(int u in newBurstedNodes)
                    {
                        burstNodes.Add(u);
                        foreach(int v in adj[u])
                        {
                            Wave burstWave = new Wave
                            {
                                InitialImpulse = startImpulse,
                                ImpulseCount = 1,
                                InitiatedTime = currentTime + 1
                            };

                            // Waves Logic Start
                            if(!waves.ContainsKey(v))
                                waves[v] = new List<Wave>();

                            bool found = false;
                            foreach(Wave existingWave in waves[v])
                            {
                                if(existingWave.InitiatedTime == burstWave.InitiatedTime)
                                {
                                    existingWave.ImpulseCount += burstWave.ImpulseCount;
                                    found = true;
                                }
                            }
                            if(!found)
                            {
                                waves[v].Add(new Wave
                                {
                                    InitialImpulse = burstWave.InitialImpulse,
                                    InitiatedTime = burstWave.InitiatedTime,
                                    ImpulseCount = burstWave.ImpulseCount
                                });
                            }
                            // Waves Logic End
                        }
                    }

                    foreach(int v in waves.Keys)
                    {
                        foreach(Wave wave in waves[v])
                        {
                            q.Enqueue((v, wave));
                        }
                    }
                    currentTime++;

                }

            }

            public void Reset()
            {
                burstNodes = new HashSet<int>();
                foreach(int u in nodes.Keys)
                {
                    nodes[u].Energy = nodes[u].InitialEnergy;
                }
            }

            public long CalculateEnergy()
            {
                long total = 0;
                foreach(Node node in nodes.Values)
                {
                    if(burstNodes.Contains(node.Id))
                        continue;
                    total += node.Energy;
                }

                return total;
            }

        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Explouding\Part 3\everybody_codes_e2026_q07_p3.txt");
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

            Console.WriteLine(g.CalculateMaxEnergy(10));

        }
    }
}
