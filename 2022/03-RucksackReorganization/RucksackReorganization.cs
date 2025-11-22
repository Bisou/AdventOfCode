using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class RucksackReorganization
    {
        public static void SolvePart1()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\03-RucksackReorganization\input.txt");  
            var sum=0;
            string line;
            while((line = file.ReadLine())!=null) {
                sum += Analyze(line);
            }
            Console.WriteLine($"Part1: the sum of priorities is {sum}");
        }
        
        private static int Analyze(string rucksack) {
            var item = rucksack.Take(rucksack.Length/2).Intersect(rucksack.Skip(rucksack.Length/2)).Single();
            if (item>='a')
                return item-'a'+1;
            return item-'A'+27;
        }
     
        public static void SolvePart2()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\03-RucksackReorganization\input.txt");  
            var sum=0;
            string line;
            while((line = file.ReadLine())!=null) {
                sum += Analyze(line, file.ReadLine(), file.ReadLine());
            }
            Console.WriteLine($"Part1: the sum of priorities is {sum}");
        }
                       
        private static int Analyze(string rucksack1, string rucksack2, string rucksack3) {
            var item = rucksack1.Intersect(rucksack2).Intersect(rucksack3).Single();
            if (item>='a')
                return item-'a'+1;
            return item-'A'+27;
        }
    }
}
