using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class LanternFish
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\06-LanternFish\input1.txt");  
            var fishes = file.ReadLine().Split(',');
            var currentFish = new int[9];
            foreach(var fish in fishes)
                currentFish[int.Parse(fish)]++;
            var maxDay = 80;
            while(maxDay-->0)
            {
                var tmp = currentFish[0];
                for (var i=0;i<currentFish.Length-1;i++)
                    currentFish[i] = currentFish[i+1];
                currentFish[6]+= tmp;
                currentFish[8] = tmp;
            }
            Console.WriteLine($"Part1: we have {currentFish.Sum()} fish");
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\06-LanternFish\input1.txt");  
            var fishes = file.ReadLine().Split(',');
            var currentFish = new long[9];
            foreach(var fish in fishes)
                currentFish[int.Parse(fish)]++;
            var maxDay = 256;
            while(maxDay-->0)
            {
                var tmp = currentFish[0];
                for (var i=0;i<currentFish.Length-1;i++)
                    currentFish[i] = currentFish[i+1];
                currentFish[6]+= tmp;
                currentFish[8] = tmp;
            }
            Console.WriteLine($"Part2: we have {currentFish.Sum()} fish");
        }
    }
}
