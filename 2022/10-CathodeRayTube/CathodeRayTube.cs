using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class CathodeRayTube
    {
        private static int x;
        private static int cycle;
        private static int strengthSum;
        private static StringBuilder crtDisplay;

        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\10-CathodeRayTube\input.txt");  
            x = 1;
            cycle = 0;
            strengthSum = 0;
            string line;
            while((line=file.ReadLine())!=null) {
                var data = line.Split(' ');
                switch (data[0]) {
                    case "addx":
                        AddCycle();
                        AddCycle();
                        x += int.Parse(data[1]);
                        break;
                    case "noop":
                        AddCycle();
                        break;
                    default:
                        break;
                }
            }
                
            Console.WriteLine($"Part1: The sum of signal strengths is {strengthSum}");
        }

        private static void AddCycle() {
            ++cycle;
            if ((cycle-20)%40==0)
                strengthSum += x*cycle;
        }
        
        public static void SolvePart2()
        {
            Console.WriteLine("Part2:");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\10-CathodeRayTube\input.txt");  
            x = 1;
            cycle = 0;
            crtDisplay = new StringBuilder();
            string line;
            while((line=file.ReadLine())!=null) {
                var data = line.Split(' ');
                switch (data[0]) {
                    case "addx":
                        AddCycle2();
                        AddCycle2();
                        x += int.Parse(data[1]);
                        break;
                    case "noop":
                        AddCycle2();
                        break;
                    default:
                        break;
                }
            }
                
            Console.WriteLine("End of Part2");
        }
        
        private static void AddCycle2() {
            if (x-1<=cycle && cycle<=x+1)
                crtDisplay.Append("#");
            else
                crtDisplay.Append(" ");
            ++cycle;
            if (cycle%40==0) {
                Console.WriteLine(crtDisplay);
                crtDisplay.Clear();
                cycle=0;
            }
        }
    }
}

