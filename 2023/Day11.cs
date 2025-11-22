using System.Text;

public class Day11
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day11-Part1-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day11-{dataType}.txt");   
        var height = map.Length;
        var width = map[0].Length;
        var doubleColumns = new HashSet<int>();
        var doubleRows = new HashSet<int>();
        var stars = new List<(int Row, int Col)>();
        for (var row=0;row<height;++row) {
            var rowEmpty=true;
            for (var col=0;col<width;++col) {
                if (map[row][col]=='#') {
                    stars.Add((row,col));
                    rowEmpty=false;
                }
            }
            if (rowEmpty) {
                doubleRows.Add(row);
            }
        }
        for (var col=0;col<width;++col) {
            var colEmpty=true;            
            for (var row=0;row<height;++row) {
                if (map[row][col]=='#') {
                    colEmpty=false;
                }
            }
            if (colEmpty) {
                doubleColumns.Add(col);
            }
        }
        var sum=0;
        for (var i=0;i<stars.Count;++i) {
            for (var j=i+1;j<stars.Count;++j) {
                var minRow = Math.Min(stars[i].Row,stars[j].Row);
                var minCol = Math.Min(stars[i].Col,stars[j].Col);
                var maxRow = Math.Max(stars[i].Row,stars[j].Row);
                var maxCol = Math.Max(stars[i].Col,stars[j].Col);
                sum += maxCol-minCol + maxRow-minRow; //Manhattan distance
                foreach(var row in doubleRows) {
                    if (minRow<row && row<maxRow) ++sum;
                }
                foreach(var col in doubleColumns) {
                    if (minCol<col && col<maxCol) ++sum;
                }
            }
        }
        Console.WriteLine($"Part1: total distance between galaxy pairs is {sum}");
    }
    
    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day11-Part2-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day11-{dataType}.txt");   
        var height = map.Length;
        var width = map[0].Length;
        var doubleColumns = new HashSet<int>();
        var doubleRows = new HashSet<int>();
        var stars = new List<(int Row, int Col)>();
        for (var row=0;row<height;++row) {
            var rowEmpty=true;
            for (var col=0;col<width;++col) {
                if (map[row][col]=='#') {
                    stars.Add((row,col));
                    rowEmpty=false;
                }
            }
            if (rowEmpty) {
                doubleRows.Add(row);
            }
        }
        for (var col=0;col<width;++col) {
            var colEmpty=true;            
            for (var row=0;row<height;++row) {
                if (map[row][col]=='#') {
                    colEmpty=false;
                }
            }
            if (colEmpty) {
                doubleColumns.Add(col);
            }
        }
        var sum=0L;
        for (var i=0;i<stars.Count;++i) {
            for (var j=i+1;j<stars.Count;++j) {
                var minRow = Math.Min(stars[i].Row,stars[j].Row);
                var minCol = Math.Min(stars[i].Col,stars[j].Col);
                var maxRow = Math.Max(stars[i].Row,stars[j].Row);
                var maxCol = Math.Max(stars[i].Col,stars[j].Col);
                sum += maxCol-minCol + maxRow-minRow; //Manhattan distance
                foreach(var row in doubleRows) {
                    if (minRow<row && row<maxRow) sum+=999999;
                }
                foreach(var col in doubleColumns) {
                    if (minCol<col && col<maxCol) sum+=999999;
                }
            }
        }
        Console.WriteLine($"Part2: total distance between galaxy pairs is {sum}");
    }
}
