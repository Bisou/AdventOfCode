using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class TrickShot    
    {
        private static int xMin;
        private static int xMax;
        private static int yMin;
        private static int yMax;
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\17-TrickShot\input1.txt");  
            var data = file.ReadLine().Replace("..",",").Replace(", y=",",").Replace("target area: x=","").Split(',').Select(int.Parse).ToArray();
            xMin=data[0];
            xMax=data[1];
            yMin=data[2];
            yMax=data[3];
            var maxHeight=int.MinValue;
            for (var speedX=0;speedX<200;speedX++)
                for (var speedY=0;speedY<200;speedY++)
                    maxHeight = Math.Max(maxHeight, GetMaxHeight(speedX, speedY));

            Console.WriteLine($"Part1: highest point we can reach is {maxHeight}"); 
        }

        private static int GetMaxHeight(int speedX, int speedY)
        {
            var x=0;
            var y=0;
            var maxHeight=y;
            do
            {
                x+=speedX;
                y+=speedY;
                if (speedX>0) speedX--;
                speedY--;
                maxHeight = Math.Max(maxHeight, y);
                if (xMin<=x && x<=xMax && yMin<=y && y<=yMax)
                    return maxHeight;
            } while (x<=xMax && y>=yMin);
            return int.MinValue;
        }

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\17-TrickShot\input1.txt");  
            var data = file.ReadLine().Replace("..",",").Replace(", y=",",").Replace("target area: x=","").Split(',').Select(int.Parse).ToArray();
            xMin=data[0];
            xMax=data[1];
            yMin=data[2];
            yMax=data[3];
            var count=0;
            for (var speedX=0;speedX<200;speedX++)
                for (var speedY=-200;speedY<200;speedY++)
                    if (GetMaxHeight(speedX, speedY)>int.MinValue)
                        count++;

            Console.WriteLine($"Part2: there are {count} different initial velocity values that meet these criteria"); 
        }   
    }
}
