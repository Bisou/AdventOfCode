using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Chiton    
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\15-Chiton\input1.txt");  
            string line;  
            var map = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                map.Add(line);
            }
            var height = map.Count;
            var width = map[0].Length;
            var risk = new int[height,width];
            for (var row=0;row<height;row++)
                for (var col=0;col<width;col++)
                    risk[row,col]=int.MaxValue;
            risk[0,0]=0;
            var todo = new Queue<(int Row, int Col)>();
            todo.Enqueue((0,0));
            var shiftRow = new []{ 0, 1, 0,-1 };
            var shiftCol = new []{ 1, 0,-1, 0 };
            while(todo.Any())
            {
                var current = todo.Dequeue();
                for (var way=0;way<4;way++)
                {
                    var newCol = current.Col + shiftCol[way];
                    var newRow = current.Row + shiftRow[way];
                    if (newCol>=0 && newCol<width && newRow>=0 && newRow<height && risk[current.Row,current.Col] + map[newRow][newCol]-'0' < risk[newRow,newCol])
                    {
                        risk[newRow,newCol] = risk[current.Row,current.Col] + map[newRow][newCol]-'0';
                        todo.Enqueue((newRow,newCol));
                    }
                }
            }
            Console.WriteLine($"Part1: total risk is {risk[height-1,width-1]}"); 
        }
        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\15-Chiton\input1.txt");  
            string line;  
            var smallMap = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                smallMap.Add(line);
            }
            var smallHeight = smallMap.Count;
            var smallWidth = smallMap[0].Length;
            var height = 5*smallHeight;
            var width = 5*smallWidth;
            var map = new int[height][];
            for (var row=0;row<smallHeight;row++)
            {
                map[row] = new int[width];
                for (var col=0;col<smallWidth;col++)
                {
                    map[row][col] = smallMap[row][col]-'0';
                    map[row][col+smallWidth] = (map[row][col]+1)==10?1:(map[row][col]+1);
                    map[row][col+2*smallWidth] = (map[row][col+smallWidth]+1)==10?1:(map[row][col+smallWidth]+1);
                    map[row][col+3*smallWidth] = (map[row][col+2*smallWidth]+1)==10?1:(map[row][col+2*smallWidth]+1);
                    map[row][col+4*smallWidth] = (map[row][col+3*smallWidth]+1)==10?1:(map[row][col+3*smallWidth]+1);
                }
            }
            for (var row=0;row<smallHeight;row++)
            {
                map[row+smallHeight] = new int[width];
                map[row+2*smallHeight] = new int[width];
                map[row+3*smallHeight] = new int[width];
                map[row+4*smallHeight] = new int[width];
                for (var col=0;col<width;col++)
                {
                    map[row+smallHeight][col] = (map[row][col]+1)==10?1:(map[row][col]+1);
                    map[row+2*smallHeight][col] = (map[row+smallHeight][col]+1)==10?1:(map[row+smallHeight][col]+1);
                    map[row+3*smallHeight][col] = (map[row+2*smallHeight][col]+1)==10?1:(map[row+2*smallHeight][col]+1);
                    map[row+4*smallHeight][col] = (map[row+3*smallHeight][col]+1)==10?1:(map[row+3*smallHeight][col]+1);
                }
            }
            var risk = new int[height,width];
            for (var row=0;row<height;row++)
                for (var col=0;col<width;col++)
                    risk[row,col]=int.MaxValue;
            risk[0,0]=0;
            var todo = new Queue<(int Row, int Col)>();
            todo.Enqueue((0,0));
            var shiftRow = new []{ 0, 1, 0,-1 };
            var shiftCol = new []{ 1, 0,-1, 0 };
            while(todo.Any())
            {
                var current = todo.Dequeue();
                for (var way=0;way<4;way++)
                {
                    var newCol = current.Col + shiftCol[way];
                    var newRow = current.Row + shiftRow[way];
                    if (newCol>=0 && newCol<width && newRow>=0 && newRow<height && risk[current.Row,current.Col] + map[newRow][newCol] < risk[newRow,newCol])
                    {
                        risk[newRow,newCol] = risk[current.Row,current.Col] + map[newRow][newCol];
                        todo.Enqueue((newRow,newCol));
                    }
                }
            }
            Console.WriteLine($"Part2: total risk is {risk[height-1,width-1]}"); 
        }   
    }
}
