using System.Text;

public class Day10
{
    private const int day = 10;
    private static int width;
    private static int height;
    private static int[] shiftRow = new [] { 0, 1, 0, -1};
    private static int[] shiftCol = new [] { 1, 0, -1, 0};
    private static HashSet<(int Row, int Col)> seen = new HashSet<(int Row, int Col)>();

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        var total=0;
        width=map[0].Length;
        height=map.Length;
        
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (map[row][col]=='0') {                    
                    total += GetScore(map, row, col);
                    seen.Clear();
                }
            }
        }

        Console.WriteLine($"Part1: total score is {total}");
    }
    
    private static int GetScore(string[] map, int row, int col) {
        if (map[row][col]=='9') return 1;
        var res=0;
        for (var way=0;way<4;++way) {
            var newRow=row+shiftRow[way];
            var newCol=col+shiftCol[way];
            if (newRow<0 || newCol<0 || newRow>=height || newCol>=width || seen.Contains((newRow, newCol))) continue;
            if (map[newRow][newCol]==map[row][col]+1) {
                seen.Add((newRow,newCol));
                res+=GetScore(map, newRow, newCol);
            }
        }
        return res;
    }
    
    private static int GetScore2(string[] map, int row, int col) {
        if (map[row][col]=='9') return 1;
        var res=0;
        for (var way=0;way<4;++way) {
            var newRow=row+shiftRow[way];
            var newCol=col+shiftCol[way];
            if (newRow<0 || newCol<0 || newRow>=height || newCol>=width) continue;
            if (map[newRow][newCol]==map[row][col]+1) {
                res+=GetScore2(map, newRow, newCol);
            }
        }
        return res;
    }

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        var total=0;
        width=map[0].Length;
        height=map.Length;
        
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (map[row][col]=='0') {                    
                    total += GetScore2(map, row, col);
                }
            }
        }
        Console.WriteLine($"Part2: total score is {total}");
    }

}
