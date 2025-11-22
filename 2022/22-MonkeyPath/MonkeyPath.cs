using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class MonkeyPath
    {
        public const char FLOOR='.';
        public const char WALL='#';
        public const char EMPTY=' ';
        public const int RIGHT = 0;
        public const int DOWN = 1;
        public const int LEFT = 2;
        public const int UP = 3;
        private static int[] RowShift={0, 1,0,-1};
        private static int[] ColShift={1,0,-1,0};

        public static void SolvePart1()
        {
            var map1 = new List<string>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\22-MonkeyPath\input.txt");  
            string line;
            while((line=file.ReadLine())!=null) {
                if (line.Length==0) break;
                map1.Add(line);
            }
            var width = map1.Max(l => l.Length);
            var map = map1.Select(l => l.PadRight(width)).ToArray();
            var path=file.ReadLine().Replace("R"," R ").Replace("L"," L ").Split(' ');
            var row=0;
            var col=-1;
            for (var i=0;i<map[row].Length;++i) {
                if (map[row][i]==FLOOR) {
                    col=i;
                    break;
                }
            }
            var way=0;
            foreach(var order in path) {                
                switch (order)
                {
                    case "R":
                        way = (way+1)%4;
                        break;
                    case "L":
                        way = (way+3)%4;
                        break;
                    default:
                        var steps = int.Parse(order);
                        for (var step=0;step<steps;++step) {
                            var newCol=col+ColShift[way];
                            var newRow=row+RowShift[way];
                            while(newRow<0 || newCol<0 || newRow>=map.Length || newCol >= map[newRow].Length || map[newRow][newCol]==EMPTY) {
                                if (newRow<0)
                                    newRow=map.Length-1;
                                else if (newRow>= map.Length)
                                    newRow=0;
                                else if (newCol<0)
                                    newCol=map[newRow].Length-1;
                                else if (newCol>=map[newRow].Length)
                                    newCol=0;
                                else {
                                    newCol+=ColShift[way];
                                    newRow+=RowShift[way];
                                }
                            }
                            if (map[newRow][newCol]==FLOOR) {
                                row=newRow;
                                col=newCol;
                            }
                        }
                        break;
                }
                Console.WriteLine($"Order={order}. Row={row}, col={col}, facing={way}");
            }
            var password = (row+1)*1000+4*(col+1)+way;

            Console.WriteLine($"Part1: password is  {password}.");
        }

        
        public static void SolvePart2()
        {
            var map1 = new List<string>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\22-MonkeyPath\input.txt");  
            //var side=4;
            var side=50;
            string line;
            while((line=file.ReadLine())!=null) {
                if (line.Length==0) break;
                map1.Add(line);
            }
            var width = map1.Max(l => l.Length);
            var map = map1.Select(l => l.PadRight(width)).ToArray();
            var path=file.ReadLine().Replace("R"," R ").Replace("L"," L ").Split(' ');
            var row=0;
            var col=-1;
            for (var i=0;i<map[row].Length;++i) {
                if (map[row][i]==FLOOR) {
                    col=i;
                    break;
                }
            }
            var way=0;
            foreach(var order in path) {                
                switch (order)
                {
                    case "R":
                        way = (way+1)%4;
                        break;
                    case "L":
                        way = (way+3)%4;
                        break;
                    default:
                        var steps = int.Parse(order);
                        if (steps==22 && row==98 && col==99)
                        {
                            var titi=0;
                        }
                        if (steps==34)// && row==100 && col==99)
                        {
                            var titi=0;
                        }
                        for (var step=0;step<steps;++step) {
                            var newCol=col+ColShift[way];
                            var newRow=row+RowShift[way];
                            var newWay=way;
                            while(newRow<0 || newCol<0 || newRow>=map.Length || newCol >= map[newRow].Length || map[newRow][newCol]==EMPTY) {
                                //change die side
                                if (row<50 && col<100) {
                                    //side A
                                    if (newRow<0) {
                                        //go up to F
                                        newRow=150+(newCol-50);
                                        newCol=0;
                                        newWay=RIGHT;//right
                                    }
                                    else if (newCol<50) {
                                        //move left to E
                                        newRow=149-newRow;
                                        newCol=0;
                                        newWay=RIGHT;//right
                                    }
                                } else if (row<50 && col<150) {
                                    //side B
                                    if (newRow<0) {
                                        //go up to F
                                        newRow=199;
                                        newCol=newCol-100;
                                        newWay=UP;
                                    } else if (newCol>=150) {
                                        //move right to D
                                        newRow=149-newRow;
                                        newCol=99;
                                        newWay=LEFT;//left
                                    } else if (newRow>=50) {
                                        //move down to C
                                        newRow=50+(newCol-100);
                                        newCol=99;
                                        newWay=LEFT;//left
                                    }                                
                                } else if (row<100) {
                                    //side C
                                    if (newCol>=100) {
                                        //move right to B
                                        newCol=100+(newRow-50);
                                        newRow=49;
                                        newWay=UP;//up
                                    } else if (newCol<50) {
                                        //move left to E
                                        newCol=0+(newRow-50);
                                        newRow=100;
                                        newWay=DOWN;//down
                                    }                   
                                } else if (row<150 && col<50) {
                                    //side E
                                    if (newCol<0) {
                                        //move left to A
                                        newRow=49-(newRow-100);
                                        newCol=50;
                                        newWay=RIGHT;//right
                                    } else if (newRow<100) {
                                        //move up to C
                                        newRow=50+ newCol;
                                        newCol=50;
                                        newWay=RIGHT;//right
                                    }       
                                } else if (row<150 && col<100) {
                                    //side D
                                    if (newRow>=150) {
                                        //move down to F
                                        newRow=150+(newCol-50);
                                        newCol=49;
                                        newWay=LEFT;//left
                                    } else if (newCol>=100) {
                                        //move right to B
                                        newRow=49-(newRow-100);
                                        newCol=149;
                                        newWay=LEFT;//left
                                    }       
                                } else if (row<200 && col<50) {
                                    //side F
                                     if (newCol>=50) {
                                        //move right to D
                                        newCol=50+(newRow-150);
                                        newRow=149;
                                        newWay=UP;//up
                                    } else if (newCol<0) {
                                        //move left to A
                                        newCol=50+ (newRow-150);
                                        newRow=0;
                                        newWay=DOWN;//down
                                    } else if (newRow>=200) {
                                        //move down to B
                                        newCol=100+newCol;
                                        newRow=0;
                                        newWay=DOWN;//down
                                    }
                                } else {
                                    var toto=0;
                                }
                                
                            }
                            if (map[newRow][newCol]==FLOOR) {
                                row=newRow;
                                col=newCol;
                                way=newWay;
                            }
                        }
                        break;
                }
                //Console.WriteLine($"Order={order}. Row={row}, col={col}, facing={way}");
            }
            var password = (row+1)*1000+4*(col+1)+way;

            Console.WriteLine($"Part2: password is {password}");
            // 57358 is too low
            //116363 is too high
        }
    }
}

