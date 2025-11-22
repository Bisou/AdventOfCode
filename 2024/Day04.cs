public class Day04
{
    private const int day = 4;
    private static int width;
    private static int height;
    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        var total=0;
        var target = "XMAS";
        height=map.Length;
        width = map[0].Length;
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                total+=GetAll(map, row, col, target);
            }
        }
        Console.WriteLine($"Part1: total XMAS is {total}");
    }
    
    private static int GetAll(string[] map, int row, int col, string target) {        
        var total=0;
        if (map[row][col]!=target[0]) return 0;
        var shiftCol=new [] {1, 1, 1, 0, 0, -1, -1, -1};
        var shiftRow=new [] {1, 0, -1, -1, 1, -1, 0, 1};        
        for (var way=0;way<shiftCol.Length;++way) {
            for (var index = 1;index<target.Length;++index) {
                var newCol=col+index*shiftCol[way];
                var newRow=row+index*shiftRow[way];
                if (newCol<0 || newRow<0 ||newRow>=height || newCol >= width) break;
                if (map[newRow][newCol]!=target[index]) break;
                if (index==target.Length-1) total++;
            }
        }

        return total;
    }

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        var total=0;
        var target = "XMAS";
        height=map.Length;
        width = map[0].Length;
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                total+=GetAllCross(map, row, col);
            }
        }
        Console.WriteLine($"Part2: total XMAS is {total}");
    }

    private static int GetAllCross(string[] map, int row, int col) {        
        var total=0;
        if (map[row][col]!='A') return 0;
        if (col-1<0 || row-1<0 || row+1>=height || col+1 >= width) return 0;
        if ((map[row-1][col-1]=='M' && map[row+1][col+1]=='S')
            || (map[row-1][col-1]=='S' && map[row+1][col+1]=='M')) {
                if ((map[row-1][col+1]=='M' && map[row+1][col-1]=='S')
                    || (map[row-1][col+1]=='S' && map[row+1][col-1]=='M')) {
                        total++;
                    }
            }
            
        return total;
    }
}
