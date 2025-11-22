using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class PyroclasticFlow
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\17-PyroclasticFlow\input.txt");  
            string winds=file.ReadLine();
            var flatRock=new string[]{"####"};
            var crossRock=new string[]{".#.","###",".#."};
            var letterRock=new string[]{"..#","..#","###"};
            var tallRock=new string[]{"#","#","#","#"};
            var squareRock=new string[]{"##","##"};            
            var rocks = new []{flatRock, crossRock, letterRock, tallRock, squareRock};
            var towerTop=0;
            var width=7;
            var startLeft=2;
            var map = new StringBuilder[10000];
            map[0]=new StringBuilder("#######"); //bottom floor
            var wind=0;            
            for (var r=0;r<2022;++r) {
                var rock = rocks[r%rocks.Length];
                var rockWidth = rock[0].Length;
                var left=startLeft;
                var top = towerTop+3+rock.Length;
                for (var i=towerTop;i<=top;++i)
                    if (map[i]==null)
                        map[i]=new StringBuilder(".......");
                while (true) {
                    //wind pushes
                    if (winds[wind++]=='>') {
                        if (left+rockWidth<width) {
                            //TODO: check other pieces block it or not                            
                            var canMove=true;
                            for (var row=0;row<rock.Length;++row)
                                for (var col=0;col<rockWidth;++col)
                                    if (rock[row][col]=='#' && map[top-row][col+left+1]=='#')
                                        canMove=false;
                            if (canMove)
                                left++;
                        }

                            
                    }
                    else {
                        if (left>0){
                            //TODO: check other pieces block it or not                          
                            var canMove=true;
                            for (var row=0;row<rock.Length;++row)
                                for (var col=0;col<rockWidth;++col)
                                    if (rock[row][col]=='#' && map[top-row][col+left-1]=='#')
                                        canMove=false;
                            if (canMove)
                                left--;
                        }
                    }
                    if (wind==winds.Length) wind=0;
                    //rock is falling
                    //can if fall?
                    var canFall=true;
                    for (var row=0;row<rock.Length;++row)
                        for (var col=0;col<rockWidth;++col)
                            if (rock[row][col]=='#' && map[top-row-1][col+left]=='#')
                                canFall=false;
                    if (canFall)
                        top--;
                    else {
                        //rock is blocked
                        for (var row=0;row<rock.Length;++row)
                            for (var col=0;col<rockWidth;++col)
                                if (rock[row][col]=='#')
                                    map[top-row][col+left]='#';
                        towerTop = Math.Max(towerTop, top);
                        break;
                    }
                }
            }


            Console.WriteLine($"Part1: tower of rocks is {towerTop} tall");
        }

        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\17-PyroclasticFlow\input.txt");  
            string winds=file.ReadLine();
            var flatRock=new string[]{"####"};
            var crossRock=new string[]{".#.","###",".#."};
            var letterRock=new string[]{"..#","..#","###"};
            var tallRock=new string[]{"#","#","#","#"};
            var squareRock=new string[]{"##","##"};            
            var rocks = new []{flatRock, crossRock, letterRock, tallRock, squareRock};
            var towerTop=0L;
            var width=7;
            var startLeft=2;
            var map = new List<StringBuilder>();
            var mapShift=0L;
            map.Add(new StringBuilder("#######")); //bottom floor
            var wind=0;   
            //memory : r%rocks.Length   wind  towerTop
            var memory = new Dictionary<long,(long TowerTop, long Rock)>();
            var frequency = 0L;
            var lastDiff=0L;    
            var jump=false;     
            //var totalRocks=2022;   
            var totalRocks=1000000000000;
            //let it run n rocks for initialisation.
            //run it again until you find the same landscape => frequency
            //for (var r=0L;r<1000000000000;++r) {
            for (var r=0L;r<totalRocks;++r) {
                var rock = rocks[r%rocks.Length];
                var rockWidth = rock[0].Length;
                var left=startLeft;
                var top = towerTop+3+rock.Length;
                for (var i=map.Count+mapShift;i<=top;++i)
                    map.Add(new StringBuilder("......."));
                while (true) {
                    //wind pushes
                    if (winds[wind++]=='>') {
                        if (left+rockWidth<width) {
                            //TODO: check other pieces block it or not                            
                            var canMove=true;
                            for (var row=0;row<rock.Length;++row)
                                for (var col=0;col<rockWidth;++col)
                                    if (rock[row][col]=='#' && map[(int)(top-mapShift-row)][col+left+1]=='#')
                                        canMove=false;
                            if (canMove)
                                left++;
                        }

                            
                    }
                    else {
                        if (left>0){
                            //TODO: check other pieces block it or not                          
                            var canMove=true;
                            for (var row=0;row<rock.Length;++row)
                                for (var col=0;col<rockWidth;++col)
                                    if (rock[row][col]=='#' && map[(int)(top-mapShift-row)][col+left-1]=='#')
                                        canMove=false;
                            if (canMove)
                                left--;
                        }
                    }
                    if (wind==winds.Length) wind=0;
                    //rock is falling
                    //can if fall?
                    var canFall=true;
                    for (var row=0;row<rock.Length;++row)
                        for (var col=0;col<rockWidth;++col)
                            if (rock[row][col]=='#' && map[(int)(top-mapShift-row-1)][col+left]=='#')
                                canFall=false;
                    if (canFall)
                        top--;
                    else {
                        //rock is blocked
                        for (var row=0;row<rock.Length;++row)
                            for (var col=0;col<rockWidth;++col)
                                if (rock[row][col]=='#')
                                    map[(int)(top-mapShift-row)][col+left]='#';
                        towerTop = Math.Max(towerTop, top);
                        break;
                    }
                }
                
                if(!jump) {
                    var key = (r%rocks.Length) + wind*100;
                    if (memory.ContainsKey(key)) {
                        var lastTowerTop=memory[key].TowerTop;
                        var diff = towerTop-lastTowerTop;
                        var freq = r-memory[key].Rock;
                        Console.WriteLine($"Found similarity: diff = {diff}. Frequency = {freq} rocks");
                        if (freq!=frequency || diff!=lastDiff) {
                            lastDiff=diff;
                            frequency=freq;
                        } else {
                            Console.WriteLine("Frequency is good. Let's take a shortcut!");
                            long turnsToCut = (totalRocks-1-r)/frequency;
                            r+=turnsToCut*frequency;
                            towerTop+=turnsToCut*diff;
                            mapShift+=turnsToCut*diff;
                            jump=true;
                        }

                    } else {
                        memory.Add(key, (towerTop,r));
                    }
                }
                if (r%1000==0) {
                    //need to reduce map size
                   // foreach(var sb in map) Console.WriteLine(sb);
                    var cut=towerTop+1;
                    var current= new []{true,true,true,true,true,true,true};
                    var keepOn=true;
                    while(keepOn) {
                        --cut;
                        keepOn=false;
                        var next=new bool[7];
                        for (var col=0;col<7;++col) {
                            if (!current[col]) continue;
                            if (map[(int)(cut-mapShift)][col]=='.') {
                                next[col]=true;
                                keepOn=true;
                            }
                        }
                        for (var col=1;col<7;++col)
                            if (next[col-1] && map[(int)(cut-mapShift)][col]=='.') {
                                next[col]=true;
                                keepOn=true;
                            }
                        for (var col=5;col>=0;--col)
                            if (next[col+1] && map[(int)(cut-mapShift)][col]=='.') {
                                next[col]=true;
                                keepOn=true;
                            }
                        current=next;
                    }
                    --cut;
                    if (cut>mapShift) {
                        map.RemoveRange(0,(int)(cut-mapShift+1));
                        mapShift=cut+1;
                    }

                }
            }


            Console.WriteLine($"Part2: tower of rocks is {towerTop} tall");
        }
    }
}

