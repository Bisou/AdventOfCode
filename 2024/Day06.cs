public class Day06
{
    private const int day = 6;
    private static int width;
    private static int height;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        var total=0;
        height=map.Length;
        width = map[0].Length;
        var startRow=-1;
        var startCol=-1;
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (map[row][col]=='^') {
                    startRow=row;
                    startCol=col;
                }
            }
        }
        total = Move(map, startRow, startCol);
        Console.WriteLine($"Part1: total cells visited is {total}");
    }
    
    private static int Move(string[] map, int row, int col) {        
        var seen=new HashSet<(int Row, int Col)>();
        var shiftRow=new [] {-1,0,1,0};
        var shiftCol=new [] {0,1,0,-1};
        var way=0;//UP
        while(row>=0 &&col>=0 && row<height && col<width) {
            seen.Add((row,col));
            row+=shiftRow[way];
            col+=shiftCol[way];
            if (row>=0 &&col>=0 && row<height && col<width && map[row][col]=='#') {
                //must turn
                row-=shiftRow[way];
                col-=shiftCol[way];
                way = (way+1)%4;
            }
        }
        return seen.Count();
    }

    
    private static bool MoveInLoop(string[] map, int row, int col, int forbiddenRow, int forbiddenCol) {        
        var seen=new HashSet<(int Row, int Col, int way)>();
        var shiftRow=new [] {-1,0,1,0};
        var shiftCol=new [] {0,1,0,-1};
        var way=0;//UP
        while(row>=0 &&col>=0 && row<height && col<width) {
            if (seen.Contains((row,col,way))) return true;
            seen.Add((row,col,way));
            row+=shiftRow[way];
            col+=shiftCol[way];
            if (row>=0 && col>=0 && row<height && col<width && (map[row][col]=='#' || (row==forbiddenRow && col==forbiddenCol))) {
                //must turn
                row-=shiftRow[way];
                col-=shiftCol[way];
                way = (way+1)%4;
            }
        }
        return false;
    }



    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        var total=0;
        height = map.Length;
        width = map[0].Length;
        var startRow=-1;
        var startCol=-1;
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (map[row][col]=='^') {
                    startRow=row;
                    startCol=col;
                }
            }
        }
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (map[row][col]=='#') continue;
                if (map[row][col]=='^') continue;
                if (MoveInLoop(map, startRow, startCol, row, col)) total++;                
            }
        }
        Console.WriteLine($"Part2: total possible loops is {total}");
    }
}
