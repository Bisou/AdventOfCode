using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class HillCllimbingAlgorithm
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\12-HillCllimbingAlgorithm\input.txt");  
            string line;
            var map = new List<string>();
            while((line=file.ReadLine())!=null) {
               map.Add(line);                
            }
            (int Row, int Col) start = (0,0);
            (int Row, int Col) target = (0,0);
            var height = map.Count;
            var width = map[0].Length;
            for (var row=0;row<height;++row)                
                for (var col=0;col<width;++col) {
                    if (map[row][col]=='S') {
                        start = (row,col);
                        map[row] = map[row].Replace("S","a");
                    }
                    else if (map[row][col]=='E') {
                        target = (row,col);
                        map[row] = map[row].Replace("E","z");
                    }
                }

            var steps = GetShortestPath(map, start, target);
            Console.WriteLine($"Part1: We can reach the summit in {steps} steps");
        }

        private static int GetShortestPath(List<string> map, (int Row, int Col) start, (int Row, int Col) target) {            
            var height = map.Count;
            var width = map[0].Length;
            var seen = new bool[height,width];
            seen[start.Row,start.Col]=true;
            var todo = new List<(int Row, int Col)>{start};
            var step=0;
            var shiftRow = new[]{0,1,0,-1};
            var shiftCol = new[]{1,0,-1,0};
            while(todo.Any()) {
                ++step;
                var next = new List<(int Row, int Col)>();
                foreach(var current in todo) {
                    for (var way=0;way<shiftRow.Length;++way) {
                        var newRow=current.Row+shiftRow[way];
                        var newCol=current.Col+shiftCol[way];
                        if (newRow<0 || newCol<0 || newRow>=height || newCol>=width || seen[newRow,newCol]) continue;
                        if (map[newRow][newCol]<=1+map[current.Row][current.Col]) {
                            if (newRow==target.Row && newCol==target.Col) {
                                return step;
                            }
                            seen[newRow,newCol] = true;
                            next.Add((newRow,newCol));
                        }
                    }
                }
                todo = next;
            }
            return int.MaxValue;
        }
        
        public static void SolvePart2()        {        
            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\12-HillCllimbingAlgorithm\input.txt");  
            string line;
            var map = new List<string>();
            while((line=file.ReadLine())!=null) {
               map.Add(line);                
            }
            (int Row, int Col) start = (0,0);
            (int Row, int Col) target = (0,0);
            var height = map.Count;
            var width = map[0].Length;
            for (var row=0;row<height;++row)                
                for (var col=0;col<width;++col) {
                    if (map[row][col]=='S') {
                        map[row] = map[row].Replace("S","a");
                    }
                    else if (map[row][col]=='E') {
                        target = (row,col);
                        map[row] = map[row].Replace("E","z");
                    }
                }

            var steps=int.MaxValue;
            for (var row=0;row<height;++row)                
                for (var col=0;col<width;++col) {
                    if (map[row][col]=='a') {
                        start = (row,col);
                        var currentSteps = GetShortestPath(map, start, target);
                        steps = Math.Min(steps, currentSteps);
                    }
                }

            Console.WriteLine($"Part2: We can reach the summit in {steps} steps");
        }
        
    }
}

