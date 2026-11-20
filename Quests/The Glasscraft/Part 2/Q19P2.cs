using System.Drawing;

namespace EverybodyCodes.Quests
{
    public static class Q19P2 
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

        private static long CalculateCrystals(char[,,] grid)
        {
            int n = grid.GetLength(0), m = grid.GetLength(1), l = grid.GetLength(2);
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

            return colorCounts[0] * colorCounts[1] * colorCounts[2];
        }

        private static bool NextPermutation(char[] colors)
        {
            int n = colors.Length;
            int i = n - 2;
            while(i >= 0 && colors[i] > colors[i+1])
            {
                i--;
            }
            if(i < 0)
                return false;
            Array.Reverse(colors, i + 1, n - i - 1);
            int j = i + 1;
            while (colors[j] <= colors[i])
                j++;

            (colors[i], colors[j]) = (colors[j], colors[i]);
            return true;
        }

        public static void Solve()
        {
            string input = File.ReadAllText(@"C:\Users\karti\everybodyCodes\Quests\The Glasscraft\Part 2\everybody_codes_e2026_q19_p2.txt");
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

            long maxCrystals = 0;
            char[] colors = ['C', 'K', 'M', 'Y'];

            while(NextPermutation(colors))
            {
                char[,,] copy = (char[,,])grid.Clone();
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < m; j++)
                    {
                        int idx = int.Parse(grid[i, j, 1].ToString()) - 1;
                        copy[i, j, 1] = colors[idx];
                    }
                }

                maxCrystals = Math.Max(maxCrystals, CalculateCrystals(copy));
            }

            Console.WriteLine(maxCrystals);

        }
    }
}