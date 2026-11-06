namespace EverybodyCodes.Quests
{
    public static class Q9P1 
    {
        private static void Print(List<long> list)
        {
            Console.WriteLine($"{string.Join(',', list)}");
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Sulphurprise\Part 1\everybody_codes_e2026_q09_p1.txt");
            string[] lines = input.Split('\n');
            long ans = 0;
            foreach(string line in lines)
            {
                string[] words = line.Trim().Split(", ");
                long id = long.Parse(words[0].Split('=').Last());
                long growth = long.Parse(words[1].Split('=').Last());
                long limit = long.Parse(words[2].Split('=').Last());

                Queue<long> plant = new Queue<long>();
                plant.Enqueue(0);
                long t = 0, size = 0, splits = 0;
                while(size < limit)
                {
                    Queue<long> newPlant = new Queue<long>();
                    long branches = 0;

                    while(plant.Count > 0)
                    {
                        long branch = plant.Dequeue();
                        branch++;
                        if(branch == growth)
                        {
                            newPlant.Enqueue(growth / 2);
                            newPlant.Enqueue(growth / 2);
                            newPlant.Enqueue(1);
                            splits++;
                        }
                        else
                        {
                            newPlant.Enqueue(branch);
                        }

                        branches += branch;
                    }

                    plant = newPlant;
                    size = splits + branches;
                    t++;
                }

                ans += t;
                Console.WriteLine($"Id: {id}, Growth: {growth}, Limit: {limit}, Size: {size}, T = {t}");
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
