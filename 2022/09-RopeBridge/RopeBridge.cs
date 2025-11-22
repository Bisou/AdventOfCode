using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class RopeBridge
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\09-RopeBridge\input.txt");  
            string line;
            var seen = new HashSet<(int,int)>();
            seen.Add((0,0));
            var tail = new []{0,0};
            var head = new []{0,0};
            while((line=file.ReadLine())!=null) {
                var data = line.Split(' ');
                var way=data[0];
                var steps = int.Parse(data[1]);
                var shiftCol=0;
                var shiftRow=0;
                switch (way) {
                    case "R":
                        shiftCol=1;
                        break;
                    case "L":
                        shiftCol=-1;
                        break;
                    case "U":
                        shiftRow=-1;
                        break;
                    case "D":
                        shiftRow=1;
                        break;
                    default:
                        break;
                }
                for (var i=0;i<steps;++i) {
                    head[0]+=shiftRow;
                    head[1]+=shiftCol;
                    var distRow=Math.Abs(head[0]-tail[0]);                        
                    var distCol=Math.Abs(head[1]-tail[1]);
                    if (distRow+distCol>2) {
                        tail[0] += (head[0]>tail[0])?1:-1;
                        tail[1] += (head[1]>tail[1])?1:-1;
                    }
                    else if (distRow>1)
                        tail[0] += (head[0]>tail[0])?1:-1;
                    else if (distCol>1)
                        tail[1] += (head[1]>tail[1])?1:-1;
                    seen.Add((tail[0],tail[1]));


                }
            }
                
            Console.WriteLine($"Part1: Tail visited {seen.Count()} locations");
        }

        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\09-RopeBridge\input.txt");  
            string line;
            var seen = new HashSet<(int,int)>();
            seen.Add((0,0));
            var rope = Enumerable.Range(0,10).Select(_ => new[]{0,0}).ToArray();
            while((line=file.ReadLine())!=null) {
                var data = line.Split(' ');
                var way=data[0];
                var steps = int.Parse(data[1]);
                var shiftCol=0;
                var shiftRow=0;
                switch (way) {
                    case "R":
                        shiftCol=1;
                        break;
                    case "L":
                        shiftCol=-1;
                        break;
                    case "U":
                        shiftRow=-1;
                        break;
                    case "D":
                        shiftRow=1;
                        break;
                    default:
                        break;
                }
                for (var i=0;i<steps;++i) {
                    rope[0][0]+=shiftRow;
                    rope[0][1]+=shiftCol;
                    for (var j=1;j<10;++j) {
                        var distRow=Math.Abs(rope[j-1][0]-rope[j][0]);                        
                        var distCol=Math.Abs(rope[j-1][1]-rope[j][1]);
                        if (distRow+distCol>2) {
                            rope[j][0] += (rope[j-1][0]>rope[j][0])?1:-1;
                            rope[j][1] += (rope[j-1][1]>rope[j][1])?1:-1;
                        }
                        else if (distRow>1)
                            rope[j][0] += (rope[j-1][0]>rope[j][0])?1:-1;
                        else if (distCol>1)
                            rope[j][1] += (rope[j-1][1]>rope[j][1])?1:-1;
                    }
                    seen.Add((rope[9][0],rope[9][1]));
                }
            }
                
            Console.WriteLine($"Part2: Tail visited {seen.Count()} locations");
        }
    }
}
