using System.Text;

public class Day08
{
    private const int day = 8;
    private static int width;
    private static int height;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        var total=0;
        height=map.Length;
        width = map[0].Length;
        var takenLocations = new HashSet<(int Row, int Col)>();
        var antennas = new Dictionary<char, List<(int Row, int Col)>>();
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (map[row][col]!='.') {
                    if(!antennas.ContainsKey(map[row][col])) {
                        antennas.Add(map[row][col], new List<(int Row, int Col)>());
                    }
                    antennas[map[row][col]].Add((row,col));
                    takenLocations.Add((row, col));
                }
            }
        }
        var antinodes = new HashSet<(int Row, int Col)>();
        foreach (var frequency in antennas.Keys) {
            var cells = antennas[frequency];
            for (var i=0;i<cells.Count;++i) {
                for (var j=0;j<cells.Count;++j) {
                    if (i==j) continue;
                    var newRow= 2*cells[j].Row - cells[i].Row; 
                    var newCol= 2*cells[j].Col - cells[i].Col;
                    if (newRow>=0 && newCol>=0 && newRow<height && newCol<width /* && !takenLocations.Contains((newRow, newCol))*/) {
                        antinodes.Add((newRow, newCol));
                    }
                }
            }            
        }
        total = antinodes.Count();
        Console.WriteLine($"Part1: total antinodes is {total}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        var total=0;
        height=map.Length;
        width = map[0].Length;
        var antennas = new Dictionary<char, List<(int Row, int Col)>>();
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (map[row][col]!='.') {
                    if(!antennas.ContainsKey(map[row][col])) {
                        antennas.Add(map[row][col], new List<(int Row, int Col)>());
                    }
                    antennas[map[row][col]].Add((row,col));
                }
            }
        }
        var antinodes = new HashSet<(int Row, int Col)>();
        foreach (var frequency in antennas.Keys) {
            var cells = antennas[frequency];
            for (var i=0;i<cells.Count;++i) {
                for (var j=i+1;j<cells.Count;++j) {
                    if (i==j) continue;
                    var rowDiff= cells[j].Row - cells[i].Row; 
                    var colDiff= cells[j].Col - cells[i].Col;
                    var gcd = GCD(rowDiff, colDiff);
                    rowDiff/=gcd;
                    colDiff/=gcd;
                    var newCol=cells[i].Col;
                    var newRow=cells[i].Row;
                    while(newRow>=0 && newCol>=0 && newRow<height && newCol<width) {                        
                        antinodes.Add((newRow, newCol));
                        newRow+=rowDiff;
                        newCol+=colDiff;
                    }
                    newCol=cells[i].Col;
                    newRow=cells[i].Row;
                    while(newRow>=0 && newCol>=0 && newRow<height && newCol<width) {                        
                        antinodes.Add((newRow, newCol));
                        newRow-=rowDiff;
                        newCol-=colDiff;
                    }
                }
            }            
        }
        total = antinodes.Count();
        var log = map.Select(s => new StringBuilder(s)).ToArray();
        foreach(var antinode in antinodes) {
            if (map[antinode.Row][antinode.Col]=='.')
                log[antinode.Row][antinode.Col]='#';
        }
        Console.WriteLine(string.Join("\n",log.Select(sb => sb.ToString())));
        Console.WriteLine($"Part2: total antinodes is {total}");
    }

    private static int GCD(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }
}
