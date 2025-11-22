using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class TreetopTreeHouse
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\08-TreetopTreeHouse\input.txt");  
            string line;
            List<string> map = new List<string>();
            while((line=file.ReadLine())!=null)
                map.Add(line);
            var visibleTrees = GetVisibleTrees(map);

            Console.WriteLine($"Part1: we can see {visibleTrees} trees");
        }
        
        private static int GetVisibleTrees(List<string> map) {
            var visible = new HashSet<int>();
            var height = map.Count;
            var width = map[0].Length;
            //left
            for (var row=0;row<height;++row) {
                var maxHeight=-1;
                for (var col=0;col<width;++col) {
                    if (map[row][col]-'0'>maxHeight) {
                        visible.Add(row*width+col);
                        maxHeight = map[row][col]-'0';
                    }
                }
            }
            
            //right
            for (var row=0;row<height;++row) {
                var maxHeight=-1;
                for (var col=width-1;col>=0;--col) {
                    if (map[row][col]-'0'>maxHeight) {
                        visible.Add(row*width+col);
                        maxHeight = map[row][col]-'0';
                    }
                }
            }
            
            //top
            for (var col=0;col<width;++col) {
                var maxHeight=-1;
                for (var row=0;row<height;++row) {
                    if (map[row][col]-'0'>maxHeight) {
                        visible.Add(row*width+col);
                        maxHeight = map[row][col]-'0';
                    }
                }
            }
            
            //down
            for (var col=0;col<width;++col) {
                var maxHeight=-1;
                for (var row=height-1;row>=0;--row) {
                    if (map[row][col]-'0'>maxHeight) {
                        visible.Add(row*width+col);
                        maxHeight = map[row][col]-'0';
                    }
                }
            }         

            return visible.Count();
        }

        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\08-TreetopTreeHouse\input.txt");  
            string line;
            List<string> map = new List<string>();
            while((line=file.ReadLine())!=null)
                map.Add(line);
            var scenicScore = GetBestScenicScore(map);

            Console.WriteLine($"Part2: the best scenic score is {scenicScore}");
        }
        
        
        private static int GetBestScenicScore(List<string> map) {
            var bestScore = 0;
            var height = map.Count;
            var width = map[0].Length;

            for (var row=0;row<height;++row) {
                for (var col=0;col<width;++col) {
                    var score = 1;
                    //left
                    var vision=0;
                    for (var i=col-1;i>=0;--i) {
                        vision++;
                        if (map[row][i]>=map[row][col])
                            break;
                    }
                    score*=vision;
                    //right
                    vision=0;
                    for (var i=col+1;i<width;++i) {
                        vision++;
                        if (map[row][i]>=map[row][col])
                            break;
                    }
                    score*=vision;
                    //top
                    vision=0;
                    for (var i=row-1;i>=0;--i) {
                        vision++;
                        if (map[i][col]>=map[row][col])
                            break;
                    }
                    score*=vision;
                    //down
                    vision=0;
                    for (var i=row+1;i<height;++i) {
                        vision++;
                        if (map[i][col]>=map[row][col])
                            break;
                    }
                    score*=vision;
                    bestScore = Math.Max(bestScore, score);

                }
            }

            return bestScore;
        }
    }
}
