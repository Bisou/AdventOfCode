using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class CalorieCounting
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\01-CalorieCounting\input.txt");  
            var prev = int.Parse(file.ReadLine());
            string line;
            var max=0;
            var curr=0;
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length==0) {
                    max = Math.Max(max, curr);
                    curr=0;
                } else {
                    var current = int.Parse(line);
                    curr+=current;
                }
            }
            Console.WriteLine($"Part1: max calories is {max}");
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\01-CalorieCounting\input.txt");  
            var prev = int.Parse(file.ReadLine());
            string line;
            var elves = new List<int>();
            var curr=0;
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length==0) {
                    elves.Add(curr);
                    curr=0;
                } else {
                    var current = int.Parse(line);
                    curr+=current;
                }
            }
            elves.Add(curr);
            var total = elves.OrderByDescending(calorie => calorie).Take(3).Sum();
            Console.WriteLine($"Part1: max calories carried by 3 eleves is {total}");
        }
    }
}
