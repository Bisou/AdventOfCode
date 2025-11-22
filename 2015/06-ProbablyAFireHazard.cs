using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class ProbablyAFireHazard
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\06.txt");  
            string line;
            var lightsOn = new HashSet<(int,int)>();
            while((line=file.ReadLine())!=null)
            {
                var order = line
                    .Replace(" through ", ",")
                    .Replace("turn ", "")
                    .Replace(" ",",")
                    .Split(',');
                if (order[0]=="on")
                    for(var i=int.Parse(order[1]);i<=int.Parse(order[3]);i++)
                        for(var j=int.Parse(order[2]);j<=int.Parse(order[4]);j++)
                            lightsOn.Add((i,j));
                else if (order[0]=="off")
                    for(var i=int.Parse(order[1]);i<=int.Parse(order[3]);i++)
                        for(var j=int.Parse(order[2]);j<=int.Parse(order[4]);j++)
                            lightsOn.Remove((i,j));
                else if (order[0]=="toggle")
                    for(var i=int.Parse(order[1]);i<=int.Parse(order[3]);i++)
                        for(var j=int.Parse(order[2]);j<=int.Parse(order[4]);j++)
                            if (lightsOn.Contains((i,j)))
                                lightsOn.Remove((i,j));
                            else
                                lightsOn.Add((i,j));
            }
            Console.WriteLine($"Part 1: total lights on is {lightsOn.Count}");            
        }
        

        public static void SolvePart2()
        {
           var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\06.txt");  
            string line;
            var lights = new int[1000,1000];
            var totalLights=0;
            while((line=file.ReadLine())!=null)
            {
                var order = line
                    .Replace(" through ", ",")
                    .Replace("turn ", "")
                    .Replace(" ",",")
                    .Split(',');
                if (order[0]=="on")
                    for(var i=int.Parse(order[1]);i<=int.Parse(order[3]);i++)
                        for(var j=int.Parse(order[2]);j<=int.Parse(order[4]);j++)
                            {
                                lights[i,j]++;
                                totalLights++;
                            }
                else if (order[0]=="off")
                    for(var i=int.Parse(order[1]);i<=int.Parse(order[3]);i++)
                        for(var j=int.Parse(order[2]);j<=int.Parse(order[4]);j++)
                            {
                                if (lights[i,j]>0)
                                {
                                    lights[i,j]--;
                                    totalLights--;
                                }
                            }
                else if (order[0]=="toggle")
                    for(var i=int.Parse(order[1]);i<=int.Parse(order[3]);i++)
                        for(var j=int.Parse(order[2]);j<=int.Parse(order[4]);j++)
                        {
                            lights[i,j]+=2;
                            totalLights+=2;
                        }
            }
            Console.WriteLine($"Part 2: total lights power is {totalLights}");
        }
    }
}
