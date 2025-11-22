using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class SeaCucumber    
    {
        private static int Width;
        private static int Height;
        public static void SolvePart1()
        {
            const char EMPTY = '.';
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\25-SeaCucumber\input1.txt");  
            var input = new List<string>();
            string line;
            while((line=file.ReadLine())!=null)
            {
                input.Add(line);
            }
            Width = input[0].Length;
            Height = input.Count();

            var map = new StringBuilder[Height];
            for (var i=0;i<Height;i++)
                map[i] = new StringBuilder(input[i]);
            var turn=0;
            while(true)
            {
                Console.WriteLine($"Turn {turn}");
                Console.WriteLine(string.Join("\n", map.Select(l => l.ToString())));
                turn++;
                var moved=false;
                var nextMap = new StringBuilder[Height];
                for (var i=0;i<Height;i++)
                    nextMap[i] = new StringBuilder(map[i].ToString());
                for (var row=0;row<Height;row++)
                    for (var col=0;col<Width;col++)
                    {
                        if (map[row][col] == '>' && map[row][(col+1) % Width]==EMPTY) 
                        {
                            nextMap[row][col] = EMPTY;
                            nextMap[row][(col+1) % Width] = '>';
                            moved=true;
                        }
                    }
                map = nextMap;
                nextMap = new StringBuilder[Height];
                for (var i=0;i<Height;i++)
                    nextMap[i] = new StringBuilder(map[i].ToString());
                for (var row=0;row<Height;row++)
                    for (var col=0;col<Width;col++)
                    {
                        if (map[row][col] == 'v' && map[(row+1)%Height][col]==EMPTY) 
                        {
                            nextMap[row][col] = EMPTY;
                            nextMap[(row+1)%Height][col] = 'v';
                            moved=true;
                        }
                    }

                if (!moved) break;
                map = nextMap;
            }            
            Console.WriteLine($"Part 1: we must wait {turn} turns before landing"); 
        }
    }
}
