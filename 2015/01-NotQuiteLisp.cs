using System;
using System.Linq;

namespace AdventOfCode
{
    public class NotQuiteLisp
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\01.txt");  
            string line = file.ReadLine();
            var floor=0;
            foreach(var c in line)
                if (c=='(')
                    floor++;
                else 
                    floor--;
            Console.WriteLine($"Part 1: final floor is {floor}");
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\01.txt");  
            string line = file.ReadLine();
            var floor=0;
            var pos=0;
            while(floor>=0)
            {
                var c = line[pos];
                if (c=='(')
                    floor++;
                else 
                    floor--;
                pos++;
            }
            Console.WriteLine($"Part 2: we enter the basement at position {pos}");
        }
    }
}
