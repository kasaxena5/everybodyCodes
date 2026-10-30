namespace EverybodyCodes.Quests
{
    public static class Q4P3 
    {
        public class Submission
        {
            public long Id;
            public long Quest;
            public long Part;
            public long Time;
            public long Points;

            public void Print()
            {
                Console.WriteLine($"Quest: {this.Quest}, Part: {this.Part}, Id: {this.Id}, Time: {this.Time}, Points: {this.Points}");
            }
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\kasaxena\everybodyCodes\Quests\The Rankening\Part 3\everybody_codes_e2026_q04_p3.txt");
            string[] lines = input.Split('\n');
            List<Submission> submissions = new List<Submission>();
            Dictionary<(long, long, long), long> SubmissionMap = new Dictionary<(long, long, long), long>();

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
                    Time = time,
                    Points = 0
                };
                submissions.Add(submission);
                SubmissionMap[(quest, part, id)] = time;
            }

            List<Submission> processedSubmissions = new List<Submission>();
            Dictionary<(long, long, long), long> ProcessedSubmissionMap = new Dictionary<(long, long, long), long>();

            foreach(Submission submission in submissions)
            {
                long quest = submission.Quest;
                long part = submission.Part;
                long id = submission.Id;

                ProcessedSubmissionMap[(quest, part, id)] = 0;
                for(int i = 1; i <= part; i++)
                {
                    ProcessedSubmissionMap[(quest, part, id)] += SubmissionMap[(quest, i, id)];
                }

                processedSubmissions.Add(new Submission
                {
                    Id = submission.Id,
                    Quest = submission.Quest,
                    Part = submission.Part,
                    Time = ProcessedSubmissionMap[(quest, part, id)],
                    Points = 0
                });
            }

            processedSubmissions.Sort((a, b) =>
            {
                int result;
                result = a.Quest.CompareTo(b.Quest);
                if(result != 0) return result;

                result = a.Part.CompareTo(b.Part);
                if(result != 0) return result;

                result = a.Time.CompareTo(b.Time);
                return result;
            });

            long currentQuest = -1, currentPart = -1, points = -1, previousTime = -1, equalCount = 0;
            Dictionary<long, long> partPointsMap = new Dictionary<long, long>
            {
                [1] = 50,
                [2] = 100,
                [3] = 150
            };

            for(int i = 0; i < processedSubmissions.Count; i++)
            {
                long quest = processedSubmissions[i].Quest;
                long part = processedSubmissions[i].Part;
                if(quest != currentQuest || part != currentPart)
                {
                    currentQuest = quest;
                    currentPart = part;
                    points = partPointsMap[part];
                    equalCount = 0;
                    previousTime = -1;
                }

                if(previousTime != -1 && previousTime != processedSubmissions[i].Time)
                {
                    points -= equalCount;
                    equalCount = 0;
                }

                processedSubmissions[i].Points = Math.Max(points, 0);

                equalCount++;
                previousTime = processedSubmissions[i].Time;
            }

            Dictionary<long, long> IdPointsMap = new Dictionary<long, long>();
            foreach(Submission submission in processedSubmissions)
            {
                if(!IdPointsMap.ContainsKey(submission.Id))
                    IdPointsMap[submission.Id] = 0;
                IdPointsMap[submission.Id] += submission.Points;
            }

            long[] scores = IdPointsMap.Values.ToArray();
            Array.Sort(scores, (a, b) => b.CompareTo(a));
            long answer = 0;
            long idx = 1, previousScore = -1;
            equalCount = 0;
            for(int i = 0; i < scores.Length; i++)
            {
                if(previousScore != -1 && previousScore != scores[i])
                {
                    idx += equalCount;
                    equalCount = 0;
                }

                answer += scores[i] * idx;

                equalCount++;
                previousScore = scores[i];
            }

            Console.WriteLine(answer);

        }
    }
}

