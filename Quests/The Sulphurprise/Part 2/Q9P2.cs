namespace EverybodyCodes.Quests
{
    public static class Q9P2 
    {
        private static void Print(List<long> list)
        {
            Console.WriteLine($"{string.Join(',', list)}");
        }

        public class Branch
        {
            public long Length;
            public (long, long) Junctions;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Sulphurprise\Part 2\everybody_codes_e2026_q09_p2.txt");
            string[] lines = input.Split('\n');
            long ans = 0;
            foreach(string line in lines)
            {
                string[] words = line.Trim().Split(", ");
                long id = long.Parse(words[0].Split('=').Last());
                long growth = long.Parse(words[1].Split('=').Last());
                long limit = long.Parse(words[2].Split('=').Last());

                Queue<Branch> plant = new Queue<Branch>();
                plant.Enqueue(new Branch { Junctions = (0, -1), Length = 0});
                long t = 0, leaves = 0, junctionIdx = 1;
                while(leaves < limit)
                {
                    Queue<Branch> newPlant = new Queue<Branch>();

                    while(plant.Count > 0)
                    {
                        Branch branch = plant.Dequeue();
                        branch.Length++;
                        if(branch.Length == growth)
                        {
                            (long, long) existingJunctions = branch.Junctions;
                            Branch branch0 = new Branch
                            {
                                Length = growth / 2,
                                Junctions = (existingJunctions.Item1, junctionIdx)

                            };
                            Branch branch1 = new Branch
                            {
                                Length = growth / 2,
                                Junctions = (existingJunctions.Item2, junctionIdx)
                            };
                            Branch branch2 = new Branch
                            {
                                Length = 1,
                                Junctions = (junctionIdx, -1)

                            };
                            newPlant.Enqueue(branch0);
                            newPlant.Enqueue(branch1);
                            newPlant.Enqueue(branch2);
                            junctionIdx++;
                        }
                        else
                        {
                            newPlant.Enqueue(branch);
                        }
                    }

                    plant = newPlant;
                    leaves = plant.ToList().Count(branch => branch.Junctions.Item1 == -1 || branch.Junctions.Item2 == -1);
                    t++;
                }

                ans += t;
                Console.WriteLine($"Id: {id}, Growth: {growth}, Limit: {limit}, Leaves: {leaves}, T = {t}");
            }

            Console.WriteLine(ans);

        }
    }
}

/*
state[0] = 0
state[1] = 1
..
state[g-1] = g - 1

state[g] = state[g / 2] + state[g / 2] + state[1]
state[g+1] = state[g / 2 + 1] + state[g / 2 + 1] + state[2]
*/
