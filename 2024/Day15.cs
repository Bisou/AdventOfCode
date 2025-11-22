using System.Text;

public class Day15
{
    private const int day = 15;
    private static int width;
    private static int height;
    private static List<StringBuilder> map;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        width = input[0].Length;
        height=0;
        (int Row, int Col) robot = (-1,-1);
        map = new List<StringBuilder>();
        var i=0;
        for (;i<input.Length;++i) {
            var line = input[i];
            if (String.IsNullOrEmpty(line)) {
                break;
            }
            map.Add(new StringBuilder(line));
            for (var c=0;c<width;++c) {
                if (line[c]=='@') {
                    robot=(height, c);
                }
            }
            height++;
        }        

        var moves = new Dictionary<char,(int shiftRow, int shiftCol)> {
            { '<', ( 0,-1) },
            { '>', ( 0, 1) },
            { '^', (-1, 0) },
            { 'v', ( 1, 0) }
        };
        for (;i<input.Length;++i) {
            var orders = input[i];
            foreach(var order in orders) {
                robot = Move(robot,moves[order]);
                //Console.WriteLine($"Moving in {moves[order]}");
                //Console.WriteLine(string.Join("\n", map.Select(sb => sb.ToString())));
            }
        }

        var total=0;
        for(var r=0;r<height;++r) {
            Console.WriteLine(map[r]);
            for (var c=0;c<width;++c) {
                if (map[r][c]=='O') {
                    total += c + 100*r;
                }
            }
        }
        Console.WriteLine($"Part1: total GPS is {total}");
    }
    
    private static (int Row, int Col) Move((int Row, int Col) robot, (int ShiftRow, int ShiftCol) move) {
        var newRow = robot.Row + move.ShiftRow;
        var newCol = robot.Col + move.ShiftCol;
        if (map[newRow][newCol]=='.') {
            //can move
            map[newRow][newCol] = map[robot.Row][robot.Col];
            map[robot.Row][robot.Col] = '.';
            return (newRow, newCol);
        } else if (map[newRow][newCol]=='#') {
            //cannot move
            return robot;
        } else {
            //push it
            var newCoordinates = Move((newRow, newCol), move);
            if (newCoordinates==(newRow, newCol)) {
                //cannot push
                return robot;
            } else {
                //can push (is already pushed by recursion) so I only have to move
                map[newRow][newCol] = map[robot.Row][robot.Col];
                map[robot.Row][robot.Col] = '.';
                return (newRow, newCol);
            }
        }
    }

    private static void Move2(List<(int Row, int Col, char Value)> cells, (int ShiftRow, int ShiftCol) move) {
        while(cells.Any()) {
            var next = new List<(int Row, int Col, char Value)>();
            foreach(var cell in cells) {                
                var newRow = cell.Row + move.ShiftRow;
                var newCol = cell.Col + move.ShiftCol;
                if (map[newRow][newCol]=='.') {
                    //can move
                    map[newRow][newCol] = cell.Value;
                } else {
                    //push it            
                    if (move.ShiftRow!=0) {
                        //move vertically
                        if (map[newRow][newCol]=='[') {
                            next.Add((newRow, newCol+1, map[newRow][newCol+1]));   
                            map[newRow][newCol+1] = '.';                
                        } else if (map[newRow][newCol]==']') {
                            next.Add((newRow, newCol-1, map[newRow][newCol-1]));                 
                            map[newRow][newCol-1] = '.';
                        } 
                        next.Add((newRow, newCol, map[newRow][newCol]));

                        map[newRow][newCol] = cell.Value;
                    } else {
                        next.Add((newRow, newCol, map[newRow][newCol]));
                        
                        map[newRow][newCol] = cell.Value;
                    }
                }
            }
            cells = next.Distinct().ToList();
        }
    }
    
    private static bool IsMovePossible2((int Row, int Col) robot, (int ShiftRow, int ShiftCol) move) {
        var newRow = robot.Row + move.ShiftRow;
        var newCol = robot.Col + move.ShiftCol;
        if (map[newRow][newCol]=='.') {
            //can move
            return true;
        } else if (map[newRow][newCol]=='#') {
            //cannot move
            return false;
        } else {
            //push it
            if (move.ShiftRow!=0) {
                //move vertically
                if (map[newRow][newCol]=='[') {
                    return IsMovePossible2((newRow, newCol), move) && IsMovePossible2((newRow, newCol+1), move);                    
                } else if (map[newRow][newCol]==']') {
                    return IsMovePossible2((newRow, newCol-1), move) && IsMovePossible2((newRow, newCol), move);                    
                } else {
                    return IsMovePossible2((newRow, newCol), move);
                }
            } else {                
                //push it horizontally 
                return IsMovePossible2((newRow, newCol), move);
            }
        }
    }


    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);
        var input = Tool.ReadAll(day, dataType);                     
        width = input[0].Length;
        height=0;
        (int Row, int Col) robot = (-1,-1);
        map = new List<StringBuilder>();
        var i=0;
        for (;i<input.Length;++i) {
            var line = input[i];
            if (String.IsNullOrEmpty(line)) {
                break;
            }
            var sb = new StringBuilder();            
            for (var c=0;c<width;++c) {
                switch(line[c]) {
                    case '.':
                    case '#':
                        sb.Append(line[c]);
                        sb.Append(line[c]);
                        break;
                    case 'O':
                        sb.Append('[');
                        sb.Append(']');
                        break;
                    case '@':
                        robot=(height, 2*c);
                        sb.Append('@');
                        sb.Append('.');
                        break;
                }
            }
            map.Add(sb);
            height++;
        }   
        width*=2;     

        var moves = new Dictionary<char,(int shiftRow, int shiftCol)> {
            { '<', ( 0,-1) },
            { '>', ( 0, 1) },
            { '^', (-1, 0) },
            { 'v', ( 1, 0) }
        };
        for (;i<input.Length;++i) {
            var orders = input[i];
            foreach(var order in orders) {
                if (IsMovePossible2(robot, moves[order])) {
                    Move2(new List<(int Row, int Col, char Value)>{(robot.Row, robot.Col, '@')},moves[order]);
                    map[robot.Row][robot.Col] = '.';
                    robot=(robot.Row+moves[order].shiftRow, robot.Col+moves[order].shiftCol);
                }
               // Console.WriteLine($"Moving in {moves[order]}");
               // Console.WriteLine(string.Join("\n", map.Select(sb => sb.ToString())));
            }
        }

        var total=0;
        for(var r=0;r<height;++r) {
            Console.WriteLine(map[r]);
            for (var c=0;c<width;++c) {
                if (map[r][c]=='[') {
                    total += c + 100*r;
                }
            }
        }
        Console.WriteLine($"Part2: total GPS is {total}");
    }
}
