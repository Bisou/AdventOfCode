using System;
using System.Linq;

namespace AdventOfCode
{
    public class IWasToldThereWouldBeNoMath
    {
        public static void SolvePart1()
        {
            var test1 = GetPaperSize("2x3x4");
            var test2 = GetPaperSize("1x1x10");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\02.txt");  
            string line;
            var paperSize=0L;
            while((line=file.ReadLine()) != null)
                paperSize += GetPaperSize(line);
            Console.WriteLine($"Part 1: total paper needed is {paperSize}");
        }
        
        private static long GetPaperSize(string dimensions)
        {
            var dim = dimensions.Split('x').Select(int.Parse).OrderBy(x => x).ToArray();
            return 3*dim[0]*dim[1]+2*dim[0]*dim[2]+2*dim[1]*dim[2];
        }

        public static void SolvePart2()
        {
            var test1 = GetRibbonSize("2x3x4");
            var test2 = GetRibbonSize("1x1x10");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\02.txt");  
            string line;
            var ribbonSize=0L;
            while((line=file.ReadLine()) != null)
                ribbonSize += GetRibbonSize(line);
            Console.WriteLine($"Part 2: total ribbon needed is {ribbonSize}");
        }
        
        private static long GetRibbonSize(string dimensions)
        {
            var dim = dimensions.Split('x').Select(int.Parse).OrderBy(x => x).ToArray();
            return 2*(dim[0]+dim[1])+dim[0]*dim[1]*dim[2];
        }

    }
}
