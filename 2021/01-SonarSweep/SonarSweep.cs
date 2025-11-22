using System;
using System.Linq;

namespace AdventOfCode
{
    public class SonarSweep
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\01-SonarSweep\input1.txt");  
            var prev = int.Parse(file.ReadLine());
            string line;
            var increase=0;
            while ((line = file.ReadLine()) != null)
            {
                var current = int.Parse(line);
                if (current>prev) increase++;
                prev=current;
            }
            Console.WriteLine($"Part1: total increase is {increase}");
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\01-SonarSweep\input1.txt");  
            var a = int.Parse(file.ReadLine());
            var b = int.Parse(file.ReadLine());
            var c = int.Parse(file.ReadLine());
            string line;
            var increase=0;
            while ((line = file.ReadLine()) != null)
            {
                var d = int.Parse(line);
                var prev=a+b+c;
                var current = b+c+d;
                if (b+c+d>a+b+c) increase++;
                a=b;
                b=c;
                c=d;
            }
            Console.WriteLine($"Part2: total increase is {increase}");
        }
    }
}
