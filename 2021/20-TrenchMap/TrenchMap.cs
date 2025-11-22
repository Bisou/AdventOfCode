using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class TrenchMap    
    {
        private static string decoder;

        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\20-TrenchMap\input1.txt");  
            string line;
            decoder = file.ReadLine();
            file.ReadLine();
            var input = new List<string>();
            while((line=file.ReadLine())!=null)
            {
                input.Add(line);
            }
            var map = input.ToArray();
            Show(map);
            char outside='.';
            for (var turn=0;turn<2;turn++)
            {
                map = Improve(map, outside);
                Console.WriteLine($"Turn {turn}:");
                Show(map);
                if (outside=='.')
                    outside=decoder[0];
                else
                    outside = decoder[511];
            }
            var count=0;
            foreach(var row in map)
                count+=(row.Count(x => x=='#'));
            Console.WriteLine($"Part1: we have {count} pixels lit after 2 turns"); 
        }

        private static void Show(string[] map)
        {
            foreach(var row in map)
                Console.WriteLine(row);
        }
        private static string[] Improve(string[] map, char outside)
        {
            var height = map.Length;
            var newHeight = height+4;
            var width = map[0].Length;
            var newWidth=width+4;
            var newMap = new string[newHeight];
            for (var row=0;row<newHeight;row++)
            {
                var sb = new StringBuilder();
                for (var col=0;col<newWidth;col++)
                {
                    var binary=0;
                    for (var oldRow=row-2;oldRow<=row;oldRow++)
                        for (var oldCol=col-2;oldCol<=col;oldCol++)
                        {
                            binary*=2;
                            if (oldRow>=0 && oldRow<height && oldCol>=0 && oldCol<width)
                            {    
                                if (map[oldRow][oldCol]=='#')
                                    binary++;
                            }
                            else if (outside=='#')
                                binary++;
                        }
                    sb.Append(decoder[binary]);
                }
                newMap[row]=sb.ToString();
            }
            return newMap;
        }

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\20-TrenchMap\input1.txt");  
            string line;
            decoder = file.ReadLine();
            file.ReadLine();
            var input = new List<string>();
            while((line=file.ReadLine())!=null)
            {
                input.Add(line);
            }
            var map = input.ToArray();
          //  Show(map);
            char outside='.';
            for (var turn=0;turn<50;turn++)
            {
                map = Improve(map, outside);
               // Console.WriteLine($"Turn {turn}:");
               // Show(map);
                if (outside=='.')
                    outside=decoder[0];
                else
                    outside = decoder[511];
            }
            var count=0;
            foreach(var row in map)
                count+=(row.Count(x => x=='#'));
            Console.WriteLine($"Part2: we have {count} pixels lit after 50 turns"); 
        }   
    }
}
