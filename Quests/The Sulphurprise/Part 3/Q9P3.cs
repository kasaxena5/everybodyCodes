namespace EverybodyCodes.Quests
{
    public static class Q9P3 
    {
        private static void Print(List<long> list)
        {
            Console.WriteLine($"{string.Join(',', list)}");
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Sulphurprise\Part 3\everybody_codes_e2026_q09_p3.txt");
            string[] lines = input.Split('\n');
            long ans = 0;
            foreach(string line in lines)
            {
                string[] words = line.Trim().Split(", ");
                long id = long.Parse(words[0].Split('=').Last());
                long growth = long.Parse(words[1].Split('=').Last());
                long limit = long.Parse(words[2].Split('=').Last());

                var mp = new SortedDictionary<long, long>();
                mp[growth] = 1;
                long t = 0, totalSplits = 0;
                while(totalSplits + 1 < limit)
                {
                    t = mp.First().Key;
                    long splits = mp[t];
                    mp.Remove(t);
                    totalSplits += splits;

                    if(!mp.ContainsKey(t + growth / 2 + 1))
                        mp[t + growth / 2 + 1] = 0;
                    if(!mp.ContainsKey(t + growth - 1))
                        mp[t + growth - 1] = 0;

                    mp[t + growth / 2 + 1] += splits;
                    mp[t + growth / 2 + 1] += splits;
                    mp[t + growth - 1] += splits;
                }

                if(t == 0)
                    ans += 1;
                else
                    ans += t;
            }

            Console.WriteLine(ans);

        }
    }
}

/*
leaf[0] = 1;
...
leaf[g - 1] = 1;

leaft[g] = 
state[0] = 0
state[1] = 1
..
state[g-1] = g - 1

state[g] = state[g / 2] + state[g / 2] + state[1] // every split increases a leaf?
leaf = number of split + 1
state[g+1] = state[g / 2 + 1] + state[g / 2 + 1] + state[2]


state[g + g/2] = state[g] + state[g] + state[1 + g / 2]

state[2g] = state[g] + state[g] + state[g+1]
state[2g] = 2 * state[g] + state[g+1]
state[2g] = 2 * (2 * state[g/2] + state[1]) + 2 * state[g/2 + 1] + 2* state[2]
         = 4 * state[g/2] + 2 * state[1] + 2 * state[2] + 2 * state[g / 2 + 1];

split[0] = 0
split[g] = 1 (g / 2, g / 2, 1)
g + g / 2 + 1

split[x] = 

split[g + ceil(g / 2)] = 4
0, g, g + g / 2 + 1, g + g  / 2  + 1
*/
