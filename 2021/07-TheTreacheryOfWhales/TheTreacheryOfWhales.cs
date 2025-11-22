using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class TheTreacheryOfWhales
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\07-TheTreacheryOfWhales\input1.txt");  
            var crabs = file.ReadLine().Split(',').Select(int.Parse).ToArray();
            var min = int.MaxValue;
            var max = int.MinValue;
            var groupedCrabs = new Dictionary<int,int>();
            foreach (var crab in crabs)
            {
                min = Math.Min(min, crab);
                max = Math.Max(max, crab);
                if (groupedCrabs.ContainsKey(crab))
                    groupedCrabs[crab]++;
                else
                    groupedCrabs.Add(crab,1);
            }
            var bestFuel = int.MaxValue;
            for(var axis=min;axis<=max;axis++)
            {
                var fuel=0;
                foreach(var crabGroup in groupedCrabs)
                    fuel += Math.Abs(axis-crabGroup.Key) * crabGroup.Value;
                bestFuel = Math.Min(fuel, bestFuel);
            }
            Console.WriteLine($"Part1: we need {bestFuel} fuel");
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\07-TheTreacheryOfWhales\input1.txt");  
            var crabs = file.ReadLine().Split(',').Select(int.Parse).ToArray();
            var min = long.MaxValue;
            var max = long.MinValue;
            var groupedCrabs = new Dictionary<long,long>();
            foreach (var crab in crabs)
            {
                min = Math.Min(min, crab);
                max = Math.Max(max, crab);
                if (groupedCrabs.ContainsKey(crab))
                    groupedCrabs[crab]++;
                else
                    groupedCrabs.Add(crab,1);
            }
            var bestFuel = long.MaxValue;
            for(var axis=min;axis<=max;axis++)
            {
                var fuel=0L;
                foreach(var crabGroup in groupedCrabs)
                {
                    var steps = Math.Abs(axis-crabGroup.Key);
                    fuel += crabGroup.Value * steps * (steps + 1) / 2;
                }
                bestFuel = Math.Min(fuel, bestFuel);
            }
            Console.WriteLine($"Part1: we need {bestFuel} fuel");
        }
    }
}
