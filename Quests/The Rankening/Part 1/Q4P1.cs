namespace EverybodyCodes.Quests
{
    public static class Q4P1 
    {
        public class Submission
        {
            public long Id;
            public long Quest;
            public long Part;
            public long Time;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Rankening\Part 1\everybody_codes_e2026_q04_p1.txt");
            string[] lines = input.Split('\n');
            List<Submission> submissions = new List<Submission>();
            Dictionary<long, long> IdTimeMapping = new Dictionary<long, long>();

            foreach(string line in lines)
            {
                string[] words = line.Split(" ");
                long id = long.Parse(words[0].Split("=")[1]);
                long quest = long.Parse(words[1].Split("=")[1]);
                long part = long.Parse(words[2].Split("=")[1]);
                long time = long.Parse(words[3].Split("=")[1]);
                Submission submission = new Submission
                {
                    Id = id,
                    Quest = quest,
                    Part = part,
                    Time = time
                };

                if(!IdTimeMapping.ContainsKey(id))
                    IdTimeMapping[id] = 0;
                IdTimeMapping[id] += time;
                submissions.Add(submission);
            }

            long[] times = IdTimeMapping.Values.ToArray();
            Array.Sort(times);

            long answer = 0;
            for(int i = 0; i < times.Length; i++)
            {
                answer += times[i] * (i + 1);
            }

            Console.WriteLine(answer);

        }
    }
}
