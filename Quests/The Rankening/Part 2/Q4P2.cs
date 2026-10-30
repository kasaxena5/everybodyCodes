namespace EverybodyCodes.Quests
{
    public static class Q4P2 
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
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Rankening\Part 2\everybody_codes_e2026_q04_p2.txt");
            string[] lines = input.Split('\n');
            List<Submission> submissions = new List<Submission>();
            Dictionary<long, (long, long)> IdPartTimeMapping = new Dictionary<long, (long, long)>();

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

                if(!IdPartTimeMapping.ContainsKey(id))
                    IdPartTimeMapping[id] = (0, 0);

                long previousParts = IdPartTimeMapping[id].Item1;
                long previousTime = IdPartTimeMapping[id].Item2;
                IdPartTimeMapping[id] = (previousParts - 1, previousTime + time);
                submissions.Add(submission);
            }

            (long, long)[] times = IdPartTimeMapping.Values.ToArray();
            Array.Sort(times);

            long answer = 0;
            for(int i = 0; i < times.Length; i++)
            {
                answer += times[i].Item2 * (i + 1);
            }

            Console.WriteLine(answer);

        }
    }
}

