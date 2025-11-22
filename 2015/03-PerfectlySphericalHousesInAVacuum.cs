using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class PerfectlySphericalHousesInAVacuum
    {
        public static void SolvePart1()
        {
            var testShouldBe2 = GetNumberOfVisitedHouses(">");
            var testShouldBe4 = GetNumberOfVisitedHouses("^>v<");
            var testShouldAlsoBe2 = GetNumberOfVisitedHouses("^v^v^v^v^v");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\03.txt");  
            var visitedHouses=GetNumberOfVisitedHouses(file.ReadLine());
            Console.WriteLine($"Part 1: total visited houses is {visitedHouses}");
        }
        
        private static long GetNumberOfVisitedHouses(string path)
        {
            var visited = new HashSet<(int Row,int Col)>();
            var houses = 1;
            var row=0;
            var col=0;
            visited.Add((row,col));
            foreach(var step in path)
            {
                if (step == '<')
                    col--;
                else if (step=='>')
                    col++;
                else if (step=='v')
                    row++;
                else if (step=='^')
                    row--;
                if (!visited.Contains((row,col)))
                {       
                    houses++;
                    visited.Add((row,col));
                }
            }
            return houses;
        }

        public static void SolvePart2()
        {
            var testShouldBe3 = GetNumberOfVisitedHousesWithRoboSanta("^v");
            var testShouldAlsoBe3 = GetNumberOfVisitedHousesWithRoboSanta("^>v<");
            var testShouldBe11 = GetNumberOfVisitedHousesWithRoboSanta("^v^v^v^v^v");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\03.txt");  
            var visitedHouses=GetNumberOfVisitedHousesWithRoboSanta(file.ReadLine());
            Console.WriteLine($"Part 2: total visited houses is {visitedHouses}");
        }
        
        private static long GetNumberOfVisitedHousesWithRoboSanta(string path)
        {
            var visited = new HashSet<(int Row,int Col)>();
            var houses = 1;
            var rows=new[]{0,0};
            var cols=new[]{0,0};            
            visited.Add((0,0));
            var moving=0;
            foreach(var step in path)
            {
                if (step == '<')
                    cols[moving]--;
                else if (step=='>')
                    cols[moving]++;
                else if (step=='v')
                    rows[moving]++;
                else if (step=='^')
                    rows[moving]--;
                if (!visited.Contains((rows[moving],cols[moving])))
                {       
                    houses++;
                    visited.Add((rows[moving],cols[moving]));
                }
                moving = 1-moving;
            }
            return houses;
        }
        
    }
}
