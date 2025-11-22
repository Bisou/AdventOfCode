using System.Text;

public class Day16
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day16-Part1-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day16-{dataType}.txt");   
        var height=map.Length;
        var width = map[0].Length;
        var seen=new HashSet<(int Row, int Col, int Direction)>();
        var energized = new HashSet<(int Row, int Col)>();
        var todo = new Queue<(int Row, int Col, int Direction)>();
        todo.Enqueue((0,-1,3));
        var shiftRow= new []{-1,0,1,0};
        var shiftCol= new []{0,-1,0,1};
        const int UP=0;
        const int LEFT=1;
        const int DOWN=2;
        const int RIGHT=3;
        while(todo.Any()) {
            var current = todo.Dequeue();
            var dir = current.Direction;
            var newRow = current.Row+shiftRow[dir];
            var newCol = current.Col+shiftCol[dir];
            if (newRow<0 || newCol<0 || newRow>=height || newCol>= width) continue;
            var next = new List<(int Row, int Col, int Direction)>();
            switch (map[newRow][newCol]) {
                case '/':
                    switch (dir) {
                        case UP:
                            dir=RIGHT;
                            break;
                        case RIGHT:
                            dir=UP;
                            break;
                        case DOWN:
                            dir=LEFT;
                            break;
                        case LEFT:
                            dir=DOWN;
                            break;
                    }
                    next.Add((newRow, newCol, dir));
                    break;
                case '\\':
                    switch (dir) {
                        case UP:
                            dir=LEFT;
                            break;
                        case LEFT:
                            dir=UP;
                            break;
                        case DOWN:
                            dir=RIGHT;
                            break;
                        case RIGHT:
                            dir=DOWN;
                            break;
                    }
                    next.Add((newRow, newCol, dir));
                    break;
                case '-':
                    if (dir==UP || dir==DOWN) {
                        next.Add((newRow, newCol, LEFT));
                        next.Add((newRow, newCol, RIGHT));
                    } else {                        
                        next.Add((newRow, newCol, dir));    
                    }
                    break;
                case '|':
                    if (dir==LEFT || dir==RIGHT) {
                        next.Add((newRow, newCol, UP));
                        next.Add((newRow, newCol, DOWN));
                    } else {                        
                        next.Add((newRow, newCol, dir));    
                    }
                    break;   
                default:
                    next.Add((newRow, newCol, dir));     
                    break;
            }
            foreach (var pos in next) {
                if (seen.Contains(pos)) continue;
                seen.Add(pos);
                energized.Add((pos.Row,pos.Col));
                todo.Enqueue(pos);
            }
        }
        
        Console.WriteLine($"Part1: {energized.Count()} are energized");
    }

    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day16-Part2-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day16-{dataType}.txt");   
        var height=map.Length;
        var width = map[0].Length;
        var maxEnergized=0;
            var shiftRow= new []{-1,0,1,0};
            var shiftCol= new []{0,-1,0,1};
            const int UP=0;
            const int LEFT=1;
            const int DOWN=2;
            const int RIGHT=3;
        var start = new List<(int Row, int Col, int Dir)>();
        for (var r=0;r<height;++r) {
            start.Add((r,-1,RIGHT));
            start.Add((r,width,LEFT));
        }
        for (var c=0;c<width;++c) {
            start.Add((-1,c,DOWN));
            start.Add((height,c,UP));            
        }
        foreach(var s in start) {
            var seen=new HashSet<(int Row, int Col, int Direction)>();
            var energized = new HashSet<(int Row, int Col)>();
            var todo = new Queue<(int Row, int Col, int Direction)>();
            todo.Enqueue(s);
            while(todo.Any()) {
                var current = todo.Dequeue();
                var dir = current.Direction;
                var newRow = current.Row+shiftRow[dir];
                var newCol = current.Col+shiftCol[dir];
                if (newRow<0 || newCol<0 || newRow>=height || newCol>= width) continue;
                var next = new List<(int Row, int Col, int Direction)>();
                switch (map[newRow][newCol]) {
                    case '/':
                        switch (dir) {
                            case UP:
                                dir=RIGHT;
                                break;
                            case RIGHT:
                                dir=UP;
                                break;
                            case DOWN:
                                dir=LEFT;
                                break;
                            case LEFT:
                                dir=DOWN;
                                break;
                        }
                        next.Add((newRow, newCol, dir));
                        break;
                    case '\\':
                        switch (dir) {
                            case UP:
                                dir=LEFT;
                                break;
                            case LEFT:
                                dir=UP;
                                break;
                            case DOWN:
                                dir=RIGHT;
                                break;
                            case RIGHT:
                                dir=DOWN;
                                break;
                        }
                        next.Add((newRow, newCol, dir));
                        break;
                    case '-':
                        if (dir==UP || dir==DOWN) {
                            next.Add((newRow, newCol, LEFT));
                            next.Add((newRow, newCol, RIGHT));
                        } else {                        
                            next.Add((newRow, newCol, dir));    
                        }
                        break;
                    case '|':
                        if (dir==LEFT || dir==RIGHT) {
                            next.Add((newRow, newCol, UP));
                            next.Add((newRow, newCol, DOWN));
                        } else {                        
                            next.Add((newRow, newCol, dir));    
                        }
                        break;   
                    default:
                        next.Add((newRow, newCol, dir));     
                        break;
                }
                foreach (var pos in next) {
                    if (seen.Contains(pos)) continue;
                    seen.Add(pos);
                    energized.Add((pos.Row,pos.Col));
                    todo.Enqueue(pos);
                }
            }
            maxEnergized = Math.Max(maxEnergized, energized.Count());
        }
        Console.WriteLine($"Part2: max energized cells are {maxEnergized}");
    }
}
