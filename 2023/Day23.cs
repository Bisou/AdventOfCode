using System.Text;

public class Day23
{   
    private static string[] map;
    private static int height;
    private static int width;
    private static int[] shiftRow = new [] {0,1,0,-1};
    private static int[] shiftCol = new [] {1,0,-1,0};
    private static Dictionary<char,int> slopeWay = new Dictionary<char,int>{
        {'>',0},
        {'v',1},
        {'<',2},
        {'^',3}
    };

    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day23-Part1-{dataType}");
        map = File.ReadAllLines(@$".\..\..\..\Inputs\Day23-{dataType}.txt");   
        height = map.Length;
        width = map[0].Length;
        var max=0L;
        var path = new HashSet<(int Row, int Col)>();
        path.Add((0,1));
        max = DepthFirstSearch(path, 0,1);

        Console.WriteLine($"Part1: answer is {max}");
    }

    private static int DepthFirstSearch(HashSet<(int Row, int Col)> path, int row, int col) {
        if (row==height-1) {
            return path.Count()-1; //start does not count
        }
        var res=0;
        if (map[row][col]=='.') {
            for (var way=0;way<4;++way) {
                var newCol=col+shiftCol[way];
                var newRow=row+shiftRow[way];
                if (newRow<0 || newCol<0 || newRow>=height || newCol>=width || map[newRow][newCol]=='#' || path.Contains((newRow,newCol))) continue;
                path.Add((newRow, newCol));
                res=Math.Max(res, DepthFirstSearch(path, newRow, newCol));
                path.Remove((newRow, newCol));
            }
        } else {
            var way = slopeWay[map[row][col]];
            var newCol=col+shiftCol[way];
            var newRow=row+shiftRow[way];
            if (newRow<0 || newCol<0 || newRow>=height || newCol>=width || map[newRow][newCol]=='#' || path.Contains((newRow,newCol))) {
            } else {
                path.Add((newRow, newCol));
                res=Math.Max(res, DepthFirstSearch(path, newRow, newCol));
                path.Remove((newRow, newCol));
            }
        }
        return res;
    }

    //Correct part2 is creating a graph (there are really few nodes)
    //but my DFS ended before I motivated myself to write it ;)
    private Dictionary<(int Row, int Col),List<((int Row, int Col),int length)>> graph = new Dictionary<(int Row, int Col),List<((int Row, int Col),int length)>>();


    private static int DepthFirstSearch2(HashSet<(int Row, int Col)> path, int row, int col) {
        if (row==height-1) {
            return path.Count()-1; //start does not count
        }
        var res=0;
        for (var way=0;way<4;++way) {
            var newCol=col+shiftCol[way];
            var newRow=row+shiftRow[way];
            if (newRow<0 || newCol<0 || newRow>=height || newCol>=width || map[newRow][newCol]=='#' || path.Contains((newRow,newCol))) continue;
            path.Add((newRow, newCol));
            res=Math.Max(res, DepthFirstSearch2(path, newRow, newCol));
            path.Remove((newRow, newCol));
        }
        return res;
    }

    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day23-Part2-{dataType}");
        map = File.ReadAllLines(@$".\..\..\..\Inputs\Day23-{dataType}.txt");   
        height = map.Length;
        width = map[0].Length;
        var max=0L;
        var path = new HashSet<(int Row, int Col)>();
        path.Add((0,1));
        max = DepthFirstSearch2(path, 0,1);

        Console.WriteLine($"Part1: answer is {max}");
    }

}
