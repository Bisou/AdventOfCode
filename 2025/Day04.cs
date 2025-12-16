using System.Text;

public static class Day04 {
    private const int day = 4;
    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);      
        var map = Tool.ReadAll(day, dataType); 
        var height = map.Length;
        var width = map[0].Length;
        var rolls = 0;
        var shiftRow = new []{-1,-1,-1,0,0,1,1,1};
        var shiftCol = new []{-1,0,1,-1,1,-1,0,1};
        for (var row=0;row<height;++row)
        {
            for (var col=0;col<width;++col)
            {
                if (map[row][col] =='.') continue;

                var neighbours=0;
                for (var way=0;way<shiftRow.Length;++way)
                {
                    var newRow=row+shiftRow[way];
                    var newCol=col+shiftCol[way];
                    if (newRow<0 || newCol<0 || newRow>=height || newCol>=width) continue;
                    if (map[newRow][newCol]=='@') neighbours++;
                }
                if (neighbours<4) rolls++;
            }
        }
        Console.WriteLine($"Part1: total movable rools is {rolls}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);       
        var map = Tool.ReadAll(day, dataType).Select(x => new StringBuilder(x)).ToArray(); 
        var height = map.Length;
        var width = map[0].Length;
        var rolls = 0;
        var shiftRow = new []{-1,-1,-1,0,0,1,1,1};
        var shiftCol = new []{-1,0,1,-1,1,-1,0,1};
        var removed=0;
        do {
            removed=0;
            for (var row=0;row<height;++row)
            {
                for (var col=0;col<width;++col)
                {
                    if (map[row][col] =='.') continue;

                    var neighbours=0;
                    for (var way=0;way<shiftRow.Length;++way)
                    {
                        var newRow=row+shiftRow[way];
                        var newCol=col+shiftCol[way];
                        if (newRow<0 || newCol<0 || newRow>=height || newCol>=width) continue;
                        if (map[newRow][newCol]=='@') neighbours++;
                    }
                    if (neighbours<4) {
                        removed++;
                        map[row][col]='.';
                    }
                }
            }
            rolls += removed;
        } while (removed>0);
        Console.WriteLine($"Part2: total movable rools is {rolls}");
    }

}
