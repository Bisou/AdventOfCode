using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class TransparentOrigami    
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\13-TransparentOrigami\input1.txt");  
            var points = new List<(int X,int Y)>();
            string line;  
            var maxX=0;
            var maxY=0;
            while ((line = file.ReadLine()) != "")
            {
                var data = line.Split(',').Select(int.Parse).ToArray();
                points.Add((data[0],data[1]));
                maxX=Math.Max(maxX, data[0]);
                maxY=Math.Max(maxY, data[1]);
            }
            
            line = file.ReadLine();
            var values = line.Replace("fold along ","").Split('=');
            if (values[0]=="x")
            {
                var newWidth = int.Parse(values[1]);
                points = points
                    .Select(p => p.X<newWidth?p:(2*newWidth-p.X, p.Y))
                    .Distinct()
                    .ToList();
            }
            else if (values[0]=="y")
            {
                var newHeight = int.Parse(values[1]);
                points = points
                    .Select(p => p.Y<newHeight?p:(p.X, 2* newHeight-p.Y))
                    .Distinct()
                    .ToList();
            }
            
            Console.WriteLine($"Part1: after one fold, we have a total of {points.Count} points"); 
        }
        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\13-TransparentOrigami\input1.txt");  
            var points = new List<(int X,int Y)>();
            string line;  
            while ((line = file.ReadLine()) != "")
            {
                var data = line.Split(',').Select(int.Parse).ToArray();
                points.Add((data[0],data[1]));
            }
            
            while ((line = file.ReadLine()) != null)
            {
                var values = line.Replace("fold along ","").Split('=');
                if (values[0]=="x")
                {
                    var newWidth = int.Parse(values[1]);
                    points = points
                        .Select(p => p.X<newWidth?p:(2*newWidth-p.X, p.Y))
                        .Distinct()
                        .ToList();
                }
                else if (values[0]=="y")
                {
                    var newHeight = int.Parse(values[1]);
                    points = points
                        .Select(p => p.Y<newHeight?p:(p.X, 2* newHeight-p.Y))
                        .Distinct()
                        .ToList();
                }
            }

            var maxX=points.Max(p => p.X);
            var maxY=points.Max(p => p.Y);
            var map = Enumerable.Range(0,maxY+1).Select(_ => Enumerable.Range(0,maxX+1).Select(unused => ' ').ToArray()).ToArray();
            foreach(var point in points)
                map[point.Y][point.X] = '#';


            Console.WriteLine($"Part2: the code is"); 
            foreach(var row in map)
                Console.WriteLine(string.Join("",row));
        }   
    }
}
