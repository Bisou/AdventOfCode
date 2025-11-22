using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class UnstableDiffusion
    {
        public const char ELF='#';
        public const char EMPTY='.';
        public const int RIGHT = 0;
        public const int DOWN = 1;
        public const int LEFT = 2;
        public const int UP = 3;
        private static int[] RowShift={0, 1,0,-1};
        private static int[] ColShift={1,0,-1,0};

        public static void SolvePart1()
        {
            var elves = new List<Elf>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\23-UnstableDiffusion\input.txt");  
            string line;
            var row=0;
            var minRow=int.MaxValue;
            var maxRow=int.MinValue;
            var minCol=int.MaxValue;
            var maxCol=int.MinValue;
            var currentLocations = new HashSet<(int Row, int Col)>();
            while((line=file.ReadLine())!=null) {
                for (var col=0;col<line.Length;++col)
                    if (line[col]==ELF) {
                        elves.Add(new Elf(row,col));
                        currentLocations.Add((row,col));
                        minRow=Math.Min(minRow, row);
                        maxRow=Math.Max(maxRow, row);
                        minCol=Math.Min(minCol, col);
                        maxCol=Math.Max(maxCol, col);
                    }
                row++;
            }
            var n = elves.Count;
            var startWay=0;
            var lookRows = new[] {
                new []{-1,0,1},//east
                new []{-1,0,1},//west
                new []{1,1,1},//south
                new []{-1,-1,-1}//north
            };
            var lookCols = new[] {
                new []{1,1,1},//east
                new []{-1,-1,-1},//west
                new []{-1,0,1},//south
                new []{-1,0,1},//north
            };
            var shiftRow=new[]{0,0,1,-1};
            var shiftCol=new[]{1,-1,0,0};
            
            Log(currentLocations, 0, minRow, maxRow, minCol, maxCol);
            for (var turn=0;turn<10;++turn) {
                var nextLocations = new HashSet<(int Row, int Col)>();
                var forbiddenLocations = new HashSet<(int Row, int Col)>();
                for(var i=0;i<n;++i) {
                    var elf = elves[i];
                    var neighbours=false;
                    for (var way=0;way<4;++way) {
                        var direction=(way+startWay)%4;
                        (int Row, int Col) neighbour0 = (elf.Row+lookRows[direction][0],elf.Col+lookCols[direction][0]);
                        (int Row, int Col) neighbour1 = (elf.Row+lookRows[direction][1],elf.Col+lookCols[direction][1]);
                        (int Row, int Col) neighbour2 = (elf.Row+lookRows[direction][2],elf.Col+lookCols[direction][2]);
                        if (currentLocations.Contains(neighbour0)
                         || currentLocations.Contains(neighbour1)
                         || currentLocations.Contains(neighbour2)) {
                            //elf in this direction
                            neighbours=true;
                        } else {
                            elf.NextRow=elf.Row+shiftRow[direction];
                            elf.NextCol=elf.Col+shiftCol[direction];
                        }
                    }
                    if(!neighbours) {
                        elf.Stay();
                    }
                    if (nextLocations.Contains((elf.NextRow,elf.NextCol))) {
                        forbiddenLocations.Add((elf.NextRow,elf.NextCol));
                        elf.Stay();
                    } else {
                        nextLocations.Add((elf.NextRow,elf.NextCol));
                    }
                }

                //now move the elves                            
                nextLocations = new HashSet<(int Row, int Col)>();    
                for(var i=0;i<n;++i) {
                    var elf = elves[i];
                    if (forbiddenLocations.Contains((elf.NextRow,elf.NextCol)))
                        elf.Stay();
                    else {
                        elf.Move();
                        
                        minRow=Math.Min(minRow, elf.Row);
                        maxRow=Math.Max(maxRow, elf.Row);
                        minCol=Math.Min(minCol, elf.Col);
                        maxCol=Math.Max(maxCol, elf.Col);                 
                    }
                    //turn2 : 3,4 is not in nextLocations???
                    nextLocations.Add((elf.Row,elf.Col));   
                }
                currentLocations = nextLocations;
                Log(currentLocations, turn+1, minRow, maxRow, minCol, maxCol);
                startWay+=3;
            }
            var answer = (maxRow-minRow+1)*(maxCol-minCol+1)-n;

            Console.WriteLine($"Part1: area size is {answer}");//4165 is too low
        }

        
        public static void SolvePart2()
        {
            var elves = new List<Elf>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\23-UnstableDiffusion\input.txt");  
            string line;
            var row=0;
            var minRow=int.MaxValue;
            var maxRow=int.MinValue;
            var minCol=int.MaxValue;
            var maxCol=int.MinValue;
            var currentLocations = new HashSet<(int Row, int Col)>();
            while((line=file.ReadLine())!=null) {
                for (var col=0;col<line.Length;++col)
                    if (line[col]==ELF) {
                        elves.Add(new Elf(row,col));
                        currentLocations.Add((row,col));
                        minRow=Math.Min(minRow, row);
                        maxRow=Math.Max(maxRow, row);
                        minCol=Math.Min(minCol, col);
                        maxCol=Math.Max(maxCol, col);
                    }
                row++;
            }
            var n = elves.Count;
            var startWay=0;
            var lookRows = new[] {
                new []{-1,0,1},//east
                new []{-1,0,1},//west
                new []{1,1,1},//south
                new []{-1,-1,-1}//north
            };
            var lookCols = new[] {
                new []{1,1,1},//east
                new []{-1,-1,-1},//west
                new []{-1,0,1},//south
                new []{-1,0,1},//north
            };
            var shiftRow=new[]{0,0,1,-1};
            var shiftCol=new[]{1,-1,0,0};
            
            Log(currentLocations, 0, minRow, maxRow, minCol, maxCol);
            var turn=0;
            var hasMoved=true;
            while (hasMoved) {
                hasMoved=false;
                turn++;
                var nextLocations = new HashSet<(int Row, int Col)>();
                var forbiddenLocations = new HashSet<(int Row, int Col)>();
                for(var i=0;i<n;++i) {
                    var elf = elves[i];
                    var neighbours=false;
                    for (var way=0;way<4;++way) {
                        var direction=(way+startWay)%4;
                        (int Row, int Col) neighbour0 = (elf.Row+lookRows[direction][0],elf.Col+lookCols[direction][0]);
                        (int Row, int Col) neighbour1 = (elf.Row+lookRows[direction][1],elf.Col+lookCols[direction][1]);
                        (int Row, int Col) neighbour2 = (elf.Row+lookRows[direction][2],elf.Col+lookCols[direction][2]);
                        if (currentLocations.Contains(neighbour0)
                         || currentLocations.Contains(neighbour1)
                         || currentLocations.Contains(neighbour2)) {
                            //elf in this direction
                            neighbours=true;
                        } else {
                            elf.NextRow=elf.Row+shiftRow[direction];
                            elf.NextCol=elf.Col+shiftCol[direction];
                        }
                    }
                    if(!neighbours) {
                        elf.Stay();
                    }
                    if (nextLocations.Contains((elf.NextRow,elf.NextCol))) {
                        forbiddenLocations.Add((elf.NextRow,elf.NextCol));
                        elf.Stay();
                    } else {
                        nextLocations.Add((elf.NextRow,elf.NextCol));
                    }
                }

                //now move the elves                            
                nextLocations = new HashSet<(int Row, int Col)>();    
                for(var i=0;i<n;++i) {
                    var elf = elves[i];
                    if (forbiddenLocations.Contains((elf.NextRow,elf.NextCol)) || elf.IsNotMoving())
                        elf.Stay();
                    else {
                        elf.Move();
                        hasMoved=true;
                        minRow=Math.Min(minRow, elf.Row);
                        maxRow=Math.Max(maxRow, elf.Row);
                        minCol=Math.Min(minCol, elf.Col);
                        maxCol=Math.Max(maxCol, elf.Col);                 
                    }
                    //turn2 : 3,4 is not in nextLocations???
                    nextLocations.Add((elf.Row,elf.Col));   
                }
                currentLocations = nextLocations;
             //   Log(currentLocations, turn+1, minRow, maxRow, minCol, maxCol);
                startWay+=3;
            }

            Console.WriteLine($"Part2: stable after {turn} turns.");
        }

        private static void Log(HashSet<(int Row, int Col)> currentLocations, int turn, int minRow, int maxRow, int minCol, int maxCol) {
            Console.WriteLine();
            Console.WriteLine($"Turn {turn}");
            for (var row=minRow;row<=maxRow;++row) {
                var sb = new StringBuilder();
                for (var col=minCol; col<=maxCol;++col)
                    if (currentLocations.Contains((row,col)))
                        sb.Append(ELF);
                    else
                        sb.Append(EMPTY);
                Console.WriteLine(sb);
            }

        }

        public class Elf {
            public int Row;
            public int Col;
            public int NextRow;
            public int NextCol;

            public Elf(int row, int col) {
                Row=row;
                Col=col;
                NextRow=Row;
                NextCol=Col;
            }

            public void Move() {
                Row=NextRow;
                Col=NextCol;
            }

            public void Stay() {
                NextRow=Row;
                NextCol=Col;
            }

            public bool IsNotMoving() {
                return Row==NextRow && Col==NextCol;
            }


            public override string ToString() {
                return $"R={Row},C={Col} => NR={NextRow},NC={NextCol}";
            }
        }

    }
}

