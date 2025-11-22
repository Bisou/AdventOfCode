using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class BlizzardBasin
    {
        public const char WALL='#';
        public const int RIGHT=0;
        public const int LEFT=2;
        public const int UP=3;
        public const int DOWN=1;
        private static int[] RowShift={0, 1,0,-1,0};
        private static int[] ColShift={1,0,-1,0,0};
        public static int Height;
        public static int Width;

        public static void SolvePart1()
        {
            var map = new List<string>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\24-BlizzardBasin\input.txt");  
            string line;
            var winds = new List<Wind>();
            while((line=file.ReadLine())!=null) {
                map.Add(line);
            }
            Height = map.Count;
            Width = map[0].Length;
            for (var row=0;row<Height;++row){
                for (var col=0;col<Width;++col) {
                    switch (map[row][col])
                    {
                        case '>':
                            winds.Add(new Wind(row,col,RIGHT));
                            break;
                        case '<':
                            winds.Add(new Wind(row,col,LEFT));
                            break;
                        case '^':
                            winds.Add(new Wind(row,col,UP));
                            break;
                        case 'v':
                            winds.Add(new Wind(row,col,DOWN));
                            break;                        
                        default:
                            break;
                    }
                }
            }
            var turn=0;
            var todo = new List<(int Row, int Col)>{(0,1)};
            while(todo.Any()) {
                turn++;
                var next = new List<(int Row, int Col)>();
                var blizzard = new HashSet<(int Row, int Col)>();
                var seen = new HashSet<(int Row, int Col)>();
                foreach (var wind in winds)
                {
                    wind.Move();
                    blizzard.Add(wind.Position());
                }
                foreach(var cell in todo) {
                    for (var way=0;way<5;++way) {
                        (int Row, int Col) neighbor = (cell.Row+RowShift[way], cell.Col + ColShift[way]);
                        if (neighbor.Row<0) continue;
                        if (map[neighbor.Row][neighbor.Col]==WALL) continue;
                        if (seen.Contains(neighbor)) continue;
                        if (blizzard.Contains(neighbor)) continue;
                        if (neighbor.Row==Height-1) {
                            Console.WriteLine($"Part1: we cross the basin in {turn} turns");
                            return;
                        }
                        seen.Add(neighbor);
                        next.Add(neighbor);
                    }
                }
                todo=next;
            }
            Console.WriteLine($"Part1: no path found T_T");
        }


        public class Wind {
            public int Row;
            public int Col;
            public int Direction;

            public Wind(int row, int col, int direction) {
                Row=row;
                Col=col;
                Direction=direction;
            }

            public void Move() {
                Row+=BlizzardBasin.RowShift[Direction];
                Col+=BlizzardBasin.ColShift[Direction];
                if (Row==0) 
                    Row = BlizzardBasin.Height-2;
                if (Row==BlizzardBasin.Height-1) 
                    Row = 1;
                if (Col==0) 
                    Col = BlizzardBasin.Width-2;
                if (Col==BlizzardBasin.Width-1) 
                    Col = 1;
            }

            public (int Row, int Col) Position() {
                return (Row, Col);
            }

            public override string ToString() {
                return $"R={Row},C={Col} => Direction={Direction}";
            }
        }

        public static void SolvePart2()
        {
            var map = new List<string>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\24-BlizzardBasin\input.txt");  
            string line;
            var winds = new List<Wind>();
            while((line=file.ReadLine())!=null) {
                map.Add(line);
            }
            Height = map.Count;
            Width = map[0].Length;
            for (var row=0;row<Height;++row){
                for (var col=0;col<Width;++col) {
                    switch (map[row][col])
                    {
                        case '>':
                            winds.Add(new Wind(row,col,RIGHT));
                            break;
                        case '<':
                            winds.Add(new Wind(row,col,LEFT));
                            break;
                        case '^':
                            winds.Add(new Wind(row,col,UP));
                            break;
                        case 'v':
                            winds.Add(new Wind(row,col,DOWN));
                            break;                        
                        default:
                            break;
                    }
                }
            }
            var turns=0;
            var turn1 = TurnsToReach(map, winds, (0,1), Height-1);
            var turn2 = TurnsToReach(map, winds, (Height-1, Width-2), 0);
            var turn3 = TurnsToReach(map, winds, (0,1), Height-1);
            Console.WriteLine($"Part2: paths are {turn1}+{turn2}+{turn3}={turn1+turn2+turn3}");
        }

        private static int TurnsToReach(List<string> map, List<Wind> winds, (int Row, int Col) start, int targetRow) {            
            var turn=0;
            var todo = new List<(int Row, int Col)>{start};
            while(todo.Any()) {
                turn++;
                var next = new List<(int Row, int Col)>();
                var blizzard = new HashSet<(int Row, int Col)>();
                var seen = new HashSet<(int Row, int Col)>();
                foreach (var wind in winds)
                {
                    wind.Move();
                    blizzard.Add(wind.Position());
                }
                foreach(var cell in todo) {
                    for (var way=0;way<5;++way) {
                        (int Row, int Col) neighbor = (cell.Row+RowShift[way], cell.Col + ColShift[way]);
                        if (neighbor.Row<0 || neighbor.Row==Height) continue;
                        if (map[neighbor.Row][neighbor.Col]==WALL) continue;
                        if (seen.Contains(neighbor)) continue;
                        if (blizzard.Contains(neighbor)) continue;
                        if (neighbor.Row==targetRow) {
                            return turn;
                        }
                        seen.Add(neighbor);
                        next.Add(neighbor);
                    }
                }
                todo=next;
            }
            return -1;
        }

    }
}

