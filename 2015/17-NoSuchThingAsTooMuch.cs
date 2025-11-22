using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class NoSuchThingAsTooMuch
    {
        private static int count;
        private static int[] containers;
        private static int total;
        private static int current;

        public static void SolvePart1()
        {
           // containers = new [] {20,15,10,5,5};
           // total=25;
            containers = new int[] {33,14,18,20,45,35,16,35,1,13,18,13,50,44,48,6,24,41,30,42};
            total=150;
            count = 0;
            current=0;
            Backtrack(0);

            Console.WriteLine($"Part 1: we have {count} combinations");            
        }

        private static void Backtrack(int index)
        {
            current +=containers[index];
            if (current==total)
                count++;
            else if (current<total && index<containers.Length-1)
                Backtrack(index+1);
            current-=containers[index];
            if (index<containers.Length-1)
                Backtrack(index+1);
        }
        
        public static void SolvePart2()
        {
           // containers = new [] {20,15,10,5,5};
           // total=25;
            containers = new int[] {33,14,18,20,45,35,16,35,1,13,18,13,50,44,48,6,24,41,30,42};
            total=150;
            combinations = new int[containers.Length+1];
            current=0;
            Backtrack2(0, 0);
            var containersUsed=0;
            while(combinations[containersUsed]==0)
                containersUsed++;
            Console.WriteLine($"Part 2: we have {combinations[containersUsed]} combinations for {containersUsed} containers");            
        }

        private static int[] combinations;

        private static void Backtrack2(int index, int containersUsed)
        {
            current +=containers[index];
            if (current==total)
                combinations[containersUsed+1]++;
            else if (current<total && index<containers.Length-1)
                Backtrack2(index+1, containersUsed+1);
            current-=containers[index];
            if (index<containers.Length-1)
                Backtrack2(index+1, containersUsed);
        }
    }
}
