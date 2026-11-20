namespace EverybodyCodes.Quests
{
    public static class Q19P1 
    {
        private static void Print(char[,,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1), l = grid.GetLength(2);
            for(int k = 0; k < l; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(grid[i, j, k]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        /**
            Black mixed with any colour produced black  K .
            C + M  produced blue, represented by  B .
            C + Y  produced green, represented by  G .
            M + Y  produced red, represented by  R .
            Two pieces of the same colour kept that colour.
        **/
        private static char MergeColor(char[] colors)
        {
            Array.Sort(colors);
            if(colors.Contains('K'))
                return 'K';
            if(colors.SequenceEqual(['C', 'M']))
                return 'B';
            if(colors.SequenceEqual(['C', 'Y']))
                return 'G';
            if(colors.SequenceEqual(['M', 'Y']))
                return 'R';
            
            return colors[0];
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\karti\everybodyCodes\Quests\The Glasscraft\Part 1\everybody_codes_e2026_q19_p1.txt");
            string[] lines = input.Split('\n');
            var tempString = new List<string>();
            var stringList = new List<List<string>>();
            foreach(string line in lines)
            {
                if(line.Trim() == "")
                {
                    stringList.Add(tempString);
                    tempString = new List<string>();
                    continue;
                }

                tempString.Add(line.Trim());
            }
            stringList.Add(tempString);

            int n = stringList[0].Count, m = stringList[0][0].Length, l = stringList.Count;
            Console.WriteLine($"n: {n}, m: {m}, l: {l}");
            char[,,] grid = new char[n, m, l];
            for(int k = 0; k < l; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        grid[i, j, k] = stringList[k][i][j];
                    }
                }
            }

            Print(grid);

            var f = new Dictionary<char, long>(); 
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    char[] colors = Enumerable.Range(0, l)
                        .Select(k => grid[i, j, k])
                        .ToArray();
                    char mergedColor = MergeColor(colors);

                    if(!f.ContainsKey(mergedColor))
                        f[mergedColor] = 0;
                    f[mergedColor]++;
                }
            }

            var colorCounts = f.Values.ToList();
            colorCounts.Sort();
            colorCounts.Reverse();
            Console.WriteLine($"{string.Join(", ", colorCounts)}");

            Console.WriteLine(colorCounts[0] * colorCounts[1] * colorCounts[2]);

        }
    }
}

