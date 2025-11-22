using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class CampCleanup
    {
        public static void SolvePart1()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\04-CampCleanup\input.txt");  
            var overlaps=0;
            string line;
            while((line = file.ReadLine())!=null) {
                if (DoesFullyOverlap(line))
                ++overlaps;
            }
            Console.WriteLine($"Part1: {overlaps} pairs overlap");
        }
        
        private static bool DoesFullyOverlap(string pair) {
            var data = pair.Replace(",","-").Split('-').Select(int.Parse).ToArray();
            if (data[0]>=data[2] && data[1]<=data[3]) return true;
            if (data[0]<=data[2] && data[1]>=data[3]) return true;
            return false;
        }
     
        public static void SolvePart2()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\04-CampCleanup\input.txt");  
            var overlaps=0;
            string line;
            while((line = file.ReadLine())!=null) {
                if (DoesOverlap(line))
                ++overlaps;
            }
            Console.WriteLine($"Part2: {overlaps} pairs overlap");
        }
        
        private static bool DoesOverlap(string pair) {
            var data = pair.Replace(",","-").Split('-').Select(int.Parse).ToArray();
            if (data[0]>data[3]) return false;
            if (data[1]<data[2]) return false;
            return true;
        }
    }
}
