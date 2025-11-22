using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class HydrothermalVenture
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\05-HydrothermalVenture\input1.txt");  
            var lines = new List<int[]>();
            string text;
            var maxX=0;
            var maxY=0;
            while ((text = file.ReadLine()) != null)
            {
                var data =text.Replace(" -> ",",").Split(',').Select(int.Parse).ToArray();
                lines.Add(data);
                if (data[0]>data[2])
                {
                    var tmp = data[0];
                    data[0] = data[2];
                    data[2] = tmp;
                }
                if (data[1]>data[3])
                {
                    var tmp = data[1];
                    data[1] = data[3];
                    data[3] = tmp;
                }
                maxX = Math.Max(maxX,data[2]);
                maxY = Math.Max(maxY,data[3]);
            }
            var map = new int[maxX+1, maxY+1];

            var overlaps=0;
            foreach (var line in lines)
            {
                if (line[0] == line[2])
                {
                    for (var y=line[1];y<=line[3];y++)
                    {
                        map[line[0],y]++;
                        if (map[line[0],y]==2)
                            overlaps++;
                    }
                }
                else if (line[1] == line[3])
                {
                    for (var x=line[0];x<=line[2];x++)
                    {
                        map[x, line[1]]++;
                        if (map[x, line[1]]==2)
                            overlaps++;
                    }
                }
            }
            Console.WriteLine($"Part1: we have {overlaps} overlaps");
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\05-HydrothermalVenture\input1.txt");  
            var lines = new List<int[]>();
            string text;
            var width=0;
            var height=0;
            while ((text = file.ReadLine()) != null)
            {
                var data =text.Replace(" -> ",",").Split(',').Select(int.Parse).ToArray();
                lines.Add(data);
                width = Math.Max(width,data[0]);
                height = Math.Max(height,data[1]);
                width = Math.Max(width,data[2]);
                height = Math.Max(height,data[3]);
            }
            var map = new int[height+1, width+1];

            var overlaps=0;
            foreach (var line in lines)
            {
                if (line[0] == line[2])
                {
                    var col = line[0];
                    var rowMin = Math.Min(line[1], line[3]);
                    var rowMax = Math.Max(line[1], line[3]);
                    for (var row=rowMin;row<=rowMax;row++)
                    {
                        map[row, col]++;
                        if (map[row, col]==2)
                            overlaps++;
                    }
                }
                else if (line[1] == line[3])
                {
                    var row = line[1];
                    var colMin = Math.Min(line[0], line[2]);
                    var colMax = Math.Max(line[0], line[2]);
                    for (var col=colMin;col<=colMax;col++)
                    {
                        map[row, col]++;
                        if (map[row, col]==2)
                            overlaps++;
                    }
                }
                else
                {
                    var rowSign = line[1]<line[3]?1:-1;
                    var colSign = line[0]<line[2]?1:-1;
                    
                    for (var row=line[1];row!=line[3]+rowSign;row+=rowSign)
                    {
                        var col = line[0] + colSign*rowSign*(row - line[1]);
                        map[row, col]++;
                        if (map[row, col]==2)
                            overlaps++;
                    }
                }
            }
            Console.WriteLine($"Part2: we have {overlaps} overlaps");
        }
    }
}
