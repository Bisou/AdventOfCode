using System.Text;

public class Day18
{
    private const int day = 18;
    private static int width;
    private static int height;    

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);  
        var max= (dataType=="Full") ? 1024 : 12;           
        if (dataType=="Full") {
            width=71;
            height=71;
        } else {
            width=7;
            height=7;
        }
        var map = Enumerable.Range(0,height).Select(_ => new StringBuilder(new String('.', width))).ToArray();
        for (var i=0;i<max;++i) {
            var bit = input[i].Split(',');
            map[int.Parse(bit[1])][int.Parse(bit[0])]='#';
        }
        Console.WriteLine(string.Join("\n",map.Select(sb => sb.ToString())));
        (int Row, int Col) target=(height-1,width-1);     
        var shiftCol = new []{1,0,-1,0};
        var shiftRow = new []{0,1,0,-1};
        var seen = new HashSet<(int Row, int Col)>();
        seen.Add((0,0));
        var todo = new List<(int Row, int Col)>{(0,0)};
        var steps=0;
        while(!seen.Contains(target)) {
            var next = new List<(int Row, int Col)>();
            foreach (var curr in todo) {
                for (var way=0;way<4;++way) {
                    var newCol=curr.Col+shiftCol[way];
                    var newRow=curr.Row+shiftRow[way];
                    if (newCol<0 || newRow<0 || newCol>=width || newRow>=height || seen.Contains((newRow, newCol)) || map[newRow][newCol]=='#') continue;
                    seen.Add((newRow, newCol));
                    next.Add((newRow, newCol));
                }
            }


            todo=next;
            ++steps;
        }
        Console.WriteLine($"Part1: after {max} bits have fallen, path is {steps} long");
    }



    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);
        var input = Tool.ReadAll(day, dataType);          
        if (dataType=="Full") {
            width=71;
            height=71;
        } else {
            width=7;
            height=7;
        }
        (int Row, int Col) target=(height-1,width-1);     
        var shiftCol = new []{1,0,-1,0};
        var shiftRow = new []{0,1,0,-1};
        var seen = new HashSet<(int Row, int Col)>();
        var map = Enumerable.Range(0,height).Select(_ => new StringBuilder(new String('.', width))).ToArray();
        for (var i=0;i<input.Length;++i) {
            var bit = input[i].Split(',');
            map[int.Parse(bit[1])][int.Parse(bit[0])]='#';
        
            //Console.WriteLine(string.Join("\n",map.Select(sb => sb.ToString())));
            seen.Clear();
            seen.Add((0,0));
            var todo = new List<(int Row, int Col)>{(0,0)};
            var reachable=false;
            while(todo.Any()) {
                var next = new List<(int Row, int Col)>();
                foreach (var curr in todo) {
                    for (var way=0;way<4;++way) {
                        var newCol=curr.Col+shiftCol[way];
                        var newRow=curr.Row+shiftRow[way];
                        if (newCol<0 || newRow<0 || newCol>=width || newRow>=height || seen.Contains((newRow, newCol)) || map[newRow][newCol]=='#') continue;
                        if ((newRow,newCol)==target) {
                            reachable=true;
                            break;
                        }
                        seen.Add((newRow, newCol));
                        next.Add((newRow, newCol));
                    }
                    if (reachable) break;
                }
                todo=next;
                if (reachable) break;
            }
            if (!reachable) {
                Console.WriteLine($"Part2: path blocked after {i+1} bits have fallen, last block was {input[i]}");
                return;
            }
        }
        Console.WriteLine($"Part2: nothing found");
    }
}
