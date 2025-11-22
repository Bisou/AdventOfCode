public class Day03
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day03-Part1-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day03-{dataType}.txt");     
        var sum=0;
        var height = map.Length;
        var width=map[0].Length;
        var digits="0123456789";
        var notSymbol="0123456789.";
        var shiftRow=new [] {-1,-1,-1,0,0,1,1,1};
        var shiftCol=new [] {-1,0,1,-1,1,-1,0,1};
        for(var row=0;row<height;++row) {
            for(var col=0;col<width;++col) {
                if (digits.Contains(map[row][col])) {
                    //found number
                    var val=0;
                    var nextToSymbol=false;
                    
                    do {
                        val = 10*val + map[row][col]-'0';
                        for (var way=0;way<8 && !nextToSymbol;++way) {
                            var nextCol=col+shiftCol[way];
                            var nextRow=row+shiftRow[way];
                            if (nextCol<0 || nextRow<0 || nextCol>=width ||nextRow>=height || notSymbol.Contains(map[nextRow][nextCol])) continue;
                            nextToSymbol=true;
                            break;
                        }
                        ++col;
                    }
                    while (col<width && digits.Contains(map[row][col]));
                    if (nextToSymbol) {
                        sum += val;
                    }
                }
            }
        }
        Console.WriteLine($"Part1: sum of numbers in engine schematic is {sum}");
    }
    

    public static void SolvePart2(string dataType)
    {                        
        Console.WriteLine($"Day03-Part2-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day03-{dataType}.txt");     
        var sum=0L;
        var height = map.Length;
        var width=map[0].Length;
        var digits="0123456789";
        var notSymbol="0123456789.";
        var shiftRow=new [] {-1,-1,-1,0,0,1,1,1};
        var shiftCol=new [] {-1,0,1,-1,1,-1,0,1};
        var allGears=new Dictionary<(int Row, int Col),List<long>>();
        for(var row=0;row<height;++row) {
            for(var col=0;col<width;++col) {
                if (digits.Contains(map[row][col])) {
                    //found number
                    var val=0;
                    var closeGears = new HashSet<(int Row, int Col)>();
                    do {
                        val = 10*val + map[row][col]-'0';
                        for (var way=0;way<8;++way) {
                            var nextCol=col+shiftCol[way];
                            var nextRow=row+shiftRow[way];
                            if (nextCol<0 || nextRow<0 || nextCol>=width ||nextRow>=height || notSymbol.Contains(map[nextRow][nextCol])) continue;
                            closeGears.Add((nextRow,nextCol));
                        }
                        ++col;
                    }
                    while (col<width && digits.Contains(map[row][col]));
                    foreach(var gear in closeGears) {
                        if (!allGears.ContainsKey(gear)) {
                            allGears.Add(gear, new List<long>());
                        }
                        allGears[gear].Add(val);
                    }
                }
            }
        }
        foreach(var gear in allGears.Keys) {
            if (allGears[gear].Count==2) {
                sum += allGears[gear][0] * allGears[gear][1];
            }
        }
        Console.WriteLine($"Part2: sum of gears ratios in engine schematic is {sum}");
    }
}
