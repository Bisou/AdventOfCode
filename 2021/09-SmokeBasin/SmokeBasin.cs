using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class SmokeBasin
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\09-SmokeBasin\input1.txt");  
            var risk = 0;
            var map = new List<string>();
            string line;            
            while ((line = file.ReadLine()) != null)
                map.Add(line);
            var height = map.Count;
            var width = map[0].Length;
            var rowShift=new []{0,1,0,-1};
            var colShift=new []{1,0,-1,0};
            for(var row=0;row<height;row++)
                for(var col=0;col<width;col++)
                {
                    var min=true;
                    for(var way=0;way<4;way++)
                    {
                        var newCol = col+colShift[way];
                        var newRow = row+rowShift[way];
                        if (newRow>=0 && newRow<height && newCol>=0 && newCol<width && map[row][col]>=map[newRow][newCol])
                        {
                            min=false;
                            break;
                        }
                    }
                    if (min)
                        {
                            risk += 1 + map[row][col] - '0';
                        }
                }

            Console.WriteLine($"Part1: we have a total risk of {risk}"); 
        }
        

        public static void SolvePart2()
        {
            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\09-SmokeBasin\input1.txt");  
            var map = new List<string>();
            string line;            
            while ((line = file.ReadLine()) != null)
                map.Add(line);
            var height = map.Count;
            var width = map[0].Length;
            var rowShift=new []{0,1,0,-1};
            var colShift=new []{1,0,-1,0};
            var basins = new List<long>();
            var seen = new bool[height,width];
            for(var row=0;row<height;row++)
                for(var col=0;col<width;col++)
                {
                    if (map[row][col]=='9')
                        seen[row,col]=true;
                    if (seen[row,col])
                        continue;
                    
                    var basin=1;
                    seen[row,col]=true;
                    var todo = new Queue<(int Row,int Col)>();
                    todo.Enqueue((row,col));
                    while(todo.Any())
                    {
                        var current = todo.Dequeue();
                        for(var way=0;way<4;way++)
                        {
                            var newCol = current.Col+colShift[way];
                            var newRow = current.Row+rowShift[way];
                            if (newRow>=0 && newRow<height && newCol>=0 && newCol<width && map[newRow][newCol]!='9' && !seen[newRow,newCol]) 
                            {
                                basin++;
                                todo.Enqueue((newRow,newCol));
                                seen[newRow,newCol]=true;
                            }
                        }
                    }
                    basins.Add(basin);
                }
            var biggestBasins = basins.OrderByDescending(x => x).Take(3).ToArray();

            Console.WriteLine($"Part2: our basins are {biggestBasins[0]}, {biggestBasins[1]} and {biggestBasins[2]} so the answer is {biggestBasins[0]*biggestBasins[1]*biggestBasins[2]}"); 
        }        
    }
}
