using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class RegolithReservoir
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\14-RegolithReservoir\input.txt");  
            var lines = new List<List<int[]>>();
            string line;
            var minCol = int.MaxValue;
            var minRow = 0;
            var maxCol = int.MinValue;
            var maxRow = int.MinValue;
            while((line=file.ReadLine())!=null) {
                if (line.Length!=0) {
                    var data = line.Split(" -> ").Select(x => x.Split(',').Select(int.Parse).ToArray()).ToList();
                    lines.Add(data);
                    foreach (var point in data)
                    {
                        minCol = Math.Min(minCol,point[0]);
                        maxCol = Math.Max(maxCol,point[0]);
                        minRow = Math.Min(minRow,point[1]);
                        maxRow = Math.Max(maxRow,point[1]);
                    }
                }
            }
            var map = BuildMap(lines,minRow,maxRow, minCol,maxCol);
            var startCol = 500-minCol;
            var sand=0;
            var stop=false;
            //Console.WriteLine("Start");
            //Log(map);
            while(SandFlows(map, startCol)) {
                ++sand;                
            }
                
            Console.WriteLine($"Part1: {sand} units of sand will flow");
        }    

        private static bool SandFlows(bool[,] map, int startCol) {
            var width=map.GetLength(1);
            var height=map.GetLength(0);
            var col=startCol;
            var row=0;
            while(true) {
                if (row>=height-1) { //after bottom of screen
                    return false;
                }
                if (!map[row+1,col]) { //can go lower
                    ++row;
                    continue;
                }
                if(col==0) { //too far on left
                    return false;
                }
                if (!map[row+1,col-1]) { //can go to lower left
                    ++row;
                    --col;
                    continue;
                }
                if(col>=width-1) { //too far on right
                    return false;
                }
                if (!map[row+1,col+1]) { //can go to lower right
                    ++row;
                    ++col;
                    continue;
                }
                //cannot go anywhere
                map[row,col]=true;
                //   Console.WriteLine("New Sand");
                //   Log(map);
                return true;
            }
        }

        private static void Log(bool[,] map) {
            var width=map.GetLength(1);
            var height=map.GetLength(0);
            for(var row=0;row<height;++row) {
                var sb = new StringBuilder();
                for(var col=0;col<width;++col) {
                    sb.Append(map[row,col]?"#":".");
                }
                Console.WriteLine(sb.ToString());
            }
        }

        private static bool[,] BuildMap(List<List<int[]>> lines, int minRow, int maxRow, int minCol, int maxCol){            
            var width = maxCol-minCol+1;
            var height = maxRow-minRow+1;
            var map = new bool[height,width];
            foreach(var data in lines) {
                for (var i=1;i<data.Count;++i) {
                    var fromCol=data[i-1][0] - minCol;
                    var fromRow=data[i-1][1] - minRow;
                    var toCol=data[i][0] - minCol;
                    var toRow=data[i][1] - minRow;
                    var row1=Math.Min(fromRow,toRow);
                    var row2=Math.Max(fromRow,toRow);
                    var col1=Math.Min(fromCol,toCol);
                    var col2=Math.Max(fromCol,toCol);
                    for(var row=row1;row<=row2;++row)
                        for(var col=col1;col<=col2;++col)
                            map[row,col]=true;
                }
            }
            return map;
        }
        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\14-RegolithReservoir\input.txt");  
            var lines = new List<List<int[]>>();
            string line;
            var minCol = int.MaxValue;
            var minRow = 0;
            var maxCol = int.MinValue;
            var maxRow = int.MinValue;
            while((line=file.ReadLine())!=null) {
                if (line.Length!=0) {
                    var data = line.Split(" -> ").Select(x => x.Split(',').Select(int.Parse).ToArray()).ToList();
                    lines.Add(data);
                    foreach (var point in data)
                    {
                        minCol = Math.Min(minCol,point[0]);
                        maxCol = Math.Max(maxCol,point[0]);
                        minRow = Math.Min(minRow,point[1]);
                        maxRow = Math.Max(maxRow,point[1]);
                    }
                }
            }
            maxRow+=2;
            minCol = Math.Min(minCol, 490-maxRow);
            maxCol = Math.Max(maxCol, 510+maxRow);
            lines.Add(new List<int[]>{new[]{minCol,maxRow},new[]{maxCol,maxRow}});
            var map = BuildMap(lines,minRow,maxRow, minCol,maxCol);
            var sand=0;
            var startCol = 500-minCol;
            while(!map[0,startCol] && SandFlows(map,startCol)) {                ;
                sand++;
                
                //   Console.WriteLine("New Sand");
                //   Log(map);
            }
                
            Console.WriteLine($"Part2: {sand} units of sand will flow");
        }    
    }
}

