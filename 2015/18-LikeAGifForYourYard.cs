using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class LikeAGifForYourYard
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\18.txt");
            var map = new string[size];
            for (var row=0;row<size;row++)
                map[row]=file.ReadLine();
            for (var turn=0;turn<size;turn++)
                map = ProcessOneTurn(map);
            
            var count = map.Sum(line => line.Count(x => x==ON));
            Console.WriteLine($"Part 1: after 100 turns, we have {count} lights on");            
        }

        private static int[] rowShift = new [] {-1,-1,-1,0,0,1,1,1};
        private static int[] colShift = new [] {-1,0,1,-1,1,-1,0,1};
        private static char ON = '#';
        private static char OFF = '.';
        private static int size=100;
        private static string[] ProcessOneTurn(string[] map, bool cornersStuck = false)
        {
            var next = new string[size];
            for(var row=0;row<size;row++)
            {
                var sb = new StringBuilder();
                for (var col=0;col<size;col++)
                {
                    if (cornersStuck && (row==0 ||row==size-1) && (col==0 ||col==size-1))
                        sb.Append(ON);
                    else
                    {
                        var neighborsOn=0;
                        for (var way=0;way<rowShift.Length;way++)
                        {
                            var newRow=row+rowShift[way];
                            var newCol=col+colShift[way];
                            if (0<=newCol && 0<=newRow && newCol<size && newRow<size && map[newRow][newCol]==ON)
                                neighborsOn++;
                        }
                        if (map[row][col]==ON && (neighborsOn==2 ||neighborsOn==3))
                            sb.Append(ON);
                        else if (map[row][col]==OFF && neighborsOn==3)
                            sb.Append(ON);
                        else
                            sb.Append(OFF);
                    }
                }
                next[row] = sb.ToString();
            }
            return next;
        }  
        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\18.txt");
            var map = new string[size];
            for (var row=0;row<size;row++)
                map[row]=file.ReadLine();
            //set corners ON
            var sb = new StringBuilder(map[0]);
            sb[0]=ON;
            sb[size-1]=ON;
            map[0]=sb.ToString();
            sb = new StringBuilder(map[size-1]);
            sb[0]=ON;
            sb[size-1]=ON;
            map[size-1]=sb.ToString();
            //run 100 turns
            for (var turn=0;turn<size;turn++)
                map = ProcessOneTurn(map, true);
            
            var count = map.Sum(line => line.Count(x => x==ON));
            Console.WriteLine($"Part 2: after 100 turns with corners stuck on, we have {count} lights on");            
        }      
    }
}
