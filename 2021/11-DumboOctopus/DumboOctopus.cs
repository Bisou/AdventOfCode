using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class DumboOctopus
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\11-DumboOctopus\input1.txt");  
            var flash = 0;
            string line;  
            var map = new int[10][];
            var row=0;
            while ((line = file.ReadLine()) != null)
                map[row++] = line.Select(c => c-'0').ToArray();

            var shiftRow=new [] {-1,-1,-1, 0,0, 1,1,1};
            var shiftCol=new [] {-1, 0, 1,-1,1,-1,0,1};
            for (var turn=0;turn<100;turn++)
            {
                var toFlash = new Queue<(int Row,int Col)>();
                for (row=0;row<10;row++)
                    for(var col=0;col<10;col++)
                    {
                        map[row][col]++;
                        if (map[row][col]==10)
                            toFlash.Enqueue((row,col));
                    }
                while(toFlash.Any())
                {
                    var flashing = toFlash.Dequeue();
                    flash++;
                    map[flashing.Row][flashing.Col]=0;
                    for (var way=0;way<8;way++)
                    {
                        var newRow = flashing.Row+shiftRow[way];
                        var newCol = flashing.Col+shiftCol[way];
                        if (newRow>=0 && newRow<10 && newCol>=0 && newCol<10 && map[newRow][newCol]!=0)
                        {
                            map[newRow][newCol]++;
                            if (map[newRow][newCol]==10)
                                toFlash.Enqueue((newRow,newCol));
                        }
                    }
                }/*
                Console.WriteLine($"turn {turn+1}: {flash} flashes");
                for (row=0;row<10;row++)
                    Console.WriteLine(string.Join("", map[row]));                                */
            }

            Console.WriteLine($"Part1: we have a total of {flash} flashes"); 
        }
        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\11-DumboOctopus\input1.txt");  
            string line;  
            var map = new int[10][];
            var row=0;
            while ((line = file.ReadLine()) != null)
                map[row++] = line.Select(c => c-'0').ToArray();

            var shiftRow=new [] {-1,-1,-1, 0,0, 1,1,1};
            var shiftCol=new [] {-1, 0, 1,-1,1,-1,0,1};
            var turn=0;
            var flash=0;
            do
            {
                turn++;
                flash=0;
                var toFlash = new Queue<(int Row,int Col)>();
                for (row=0;row<10;row++)
                    for(var col=0;col<10;col++)
                    {
                        map[row][col]++;
                        if (map[row][col]==10)
                            toFlash.Enqueue((row,col));
                    }
                while(toFlash.Any())
                {
                    var flashing = toFlash.Dequeue();
                    flash++;
                    map[flashing.Row][flashing.Col]=0;
                    for (var way=0;way<8;way++)
                    {
                        var newRow = flashing.Row+shiftRow[way];
                        var newCol = flashing.Col+shiftCol[way];
                        if (newRow>=0 && newRow<10 && newCol>=0 && newCol<10 && map[newRow][newCol]!=0)
                        {
                            map[newRow][newCol]++;
                            if (map[newRow][newCol]==10)
                                toFlash.Enqueue((newRow,newCol));
                        }
                    }
                }/*
                Console.WriteLine($"turn {turn+1}: {flash} flashes");
                for (row=0;row<10;row++)
                    Console.WriteLine(string.Join("", map[row]));                                */
            }
            while(flash < 100);

            Console.WriteLine($"Part1: we have a total synchronization on turn {turn}"); 
        }   
    }
}
