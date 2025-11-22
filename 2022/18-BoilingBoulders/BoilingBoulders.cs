using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class BoilingBoulders
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\18-BoilingBoulders\input.txt");  
            string line;
            var set = new HashSet<(int,int,int)>();
            var sides=0;
            while((line=file.ReadLine())!=null) {
                var data = line.Split(',').Select(int.Parse).ToArray();
                sides+=6;
                set.Add((data[0],data[1],data[2]));
                if (set.Contains((data[0]-1,data[1],data[2]))) sides-=2;
                if (set.Contains((data[0]+1,data[1],data[2]))) sides-=2;
                if (set.Contains((data[0],data[1]-1,data[2]))) sides-=2;
                if (set.Contains((data[0],data[1]+1,data[2]))) sides-=2;
                if (set.Contains((data[0],data[1],data[2]-1))) sides-=2;
                if (set.Contains((data[0],data[1],data[2]+1))) sides-=2;
            }


            Console.WriteLine($"Part1: droplet has {sides} sides.");
        }

        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\18-BoilingBoulders\input.txt");  
            string line;
            var droplet = new HashSet<(int X,int Y,int Z)>();
            while((line=file.ReadLine())!=null) {
                var data = line.Split(',').Select(int.Parse).ToArray();
                droplet.Add((data[0],data[1],data[2]));
            }
            var minX=droplet.Min(d => d.X)-1;
            var maxX=droplet.Max(d => d.X)+1;
            var minY=droplet.Min(d => d.Y)-1;
            var maxY=droplet.Max(d => d.Y)+1;
            var minZ=droplet.Min(d => d.Z)-1;
            var maxZ=droplet.Max(d => d.Z)+1;
            
            var sides=0;
            var todo = new Queue<(int X, int Y, int Z)>();
            todo.Enqueue((minX,minY,minZ));
            var seen = new HashSet<(int X, int Y, int Z)>();
            seen.Add((minX,minY,minZ));
            var shiftX=new[] {-1,+1,0,0,0,0};
            var shiftY=new[] {0,0,-1,+1,0,0};
            var shiftZ=new[] {0,0,0,0,-1,+1};
            while(todo.Any()) {
                var cell = todo.Dequeue();
                for (var way=0;way<shiftX.Length;++way) {
                    var newX=cell.X+shiftX[way];
                    var newY=cell.Y+shiftY[way];
                    var newZ=cell.Z+shiftZ[way];
                    if (newX<minX || newX>maxX || newY<minY || newY>maxY || newZ<minZ || newZ>maxZ) continue; //off the sides
                    if (seen.Contains((newX,newY,newZ))) continue;                    
                    if (droplet.Contains((newX,newY,newZ))) {                        
                        sides++;
                    }
                    else {
                        todo.Enqueue((newX,newY,newZ));
                        seen.Add((newX,newY,newZ));
                    }
                }
            }

            Console.WriteLine($"Part2: droplet has {sides} external sides.");
        }
    }
}

