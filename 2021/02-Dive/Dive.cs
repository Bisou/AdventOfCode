using System;
using System.Linq;

namespace AdventOfCode
{
    public class Dive
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\02-Dive\input1.txt");  
            var depth = 0;
            var position = 0;
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var data = line.Split(' ');
                var val = int.Parse(data[1]);
                if (data[0]=="forward") position += val;
                if (data[0]=="down") depth += val;
                if (data[0]=="up") depth -= val;
            }
            Console.WriteLine($"Part1: position {position} and depth {depth} so answer is {position*depth}");
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\02-Dive\input1.txt");  
            var depth = 0;
            var position = 0;
            var aim = 0;
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var data = line.Split(' ');
                var val = int.Parse(data[1]);
                if (data[0]=="forward") 
                {
                    depth += aim * val;
                    position += val;
                }
                if (data[0]=="down") aim += val;
                if (data[0]=="up") aim -= val;
            }
            Console.WriteLine($"Part1: position {position} and depth {depth} so answer is {position*depth}");
        }
    }
}
