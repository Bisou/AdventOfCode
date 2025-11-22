using System.Text;

public class Day21
{
    public static void SolvePart1(string dataType, int steps)
    {
        Console.WriteLine($"Day21-Part1-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day21-{dataType}.txt");   
        var height=map.Length;
        var width=map[0].Length;
        (int Row, int Col) start=(-1,-1);;
        for (var row=0;row<height;++row) {
            if (start.Row>=0) break;
            for (var col=0;col<width;++col) {
                if (map[row][col]=='S') {
                    start = (row,col);
                    break;
                }
            }
        }
        var reachable = new HashSet<(int Row, int Col)>();
        reachable.Add(start);
        for (var i=0;i<steps;++i) {
            var reachableNext = new HashSet<(int Row, int Col)>();
            foreach (var curr in reachable) {
                for(var way=0;way<shiftCol.Length;++way) {
                    var newCol=curr.Col+shiftCol[way];
                    var newRow=curr.Row+shiftRow[way];
                    if (newRow<0 || newCol<0 || newRow>=width || newCol>=height || map[newRow][newCol]=='#') continue;
                    reachableNext.Add((newRow, newCol));
                }
            }
            reachable = reachableNext;
        }


        Console.WriteLine($"Part1: answer is {reachable.Count()}");
    }


    private static int[] shiftRow=new [] {0,1,0,-1};
    private static int[] shiftCol=new [] {1,0,-1,0};

    public static void SolvePart2(string dataType, int steps)
    {     
        Console.WriteLine($"Day21-Part2-{dataType} with {steps} steps");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day21-{dataType}.txt");   
        var height=map.Length;
        var width=map[0].Length;
        (int Row, int Col) start=(-1,-1);;
        for (var row=0;row<height;++row) {
            if (start.Row>=0) break;
            for (var col=0;col<width;++col) {
                if (map[row][col]=='S') {
                    start = (row,col);
                    break;
                }
            }
        }
        
        var result = EvenOddMemory(map, height, width, steps, start);

        Console.WriteLine($"Part1: answer is {result}");
    }

    private static long EvenOddCardMemory(string[] map, int height, int width, int steps, (int Row, int Col) start) {
        //précalculer pour une carte pour chaque point d'entrée où on sort en combien de pas
        //garder en mémoire les coordonnées des cartes 
        var reachable = new HashSet<(int Row, int Col)>();
        reachable.Add(start);
        var reachableOdd = new HashSet<(int Row, int Col)>();
        var reachableEven = new HashSet<(int Row, int Col)>();
        reachableEven.Add(start);
        for (var i=1;i<=steps;++i) {
            var reachableNext = new HashSet<(int Row, int Col)>();
            foreach (var curr in reachable) {
                for(var way=0;way<shiftCol.Length;++way) {
                    var newCol=curr.Col+shiftCol[way];
                    var newRow=curr.Row+shiftRow[way];
                    if (map[((newRow%height)+height)%height][((newCol%width)+width)%width]=='#') continue;
                    HashSet<(int Row, int Col)> memory = (i%2==0) ? reachableEven : reachableOdd;
                    if (memory.Contains((newRow, newCol))) continue;
                    reachableNext.Add((newRow, newCol));
                    memory.Add((newRow, newCol));
                }
            }
            reachable = reachableNext;
        }

        HashSet<(int Row, int Col)> last = (steps%2==0) ? reachableEven : reachableOdd;
        return last.Count();
    }

    private static long EvenOddMemory(string[] map, int height, int width, int steps, (int Row, int Col) start) {

        var reachable = new HashSet<(int Row, int Col)>();
        reachable.Add(start);
        var reachableOdd = new HashSet<(int Row, int Col)>();
        var reachableEven = new HashSet<(int Row, int Col)>();
        reachableEven.Add(start);
        for (var i=1;i<=steps;++i) {
            var reachableNext = new HashSet<(int Row, int Col)>();
            foreach (var curr in reachable) {
                for(var way=0;way<shiftCol.Length;++way) {
                    var newCol=curr.Col+shiftCol[way];
                    var newRow=curr.Row+shiftRow[way];
                    if (map[((newRow%height)+height)%height][((newCol%width)+width)%width]=='#') continue;
                    HashSet<(int Row, int Col)> memory = (i%2==0) ? reachableEven : reachableOdd;
                    if (memory.Contains((newRow, newCol))) continue;
                    reachableNext.Add((newRow, newCol));
                    memory.Add((newRow, newCol));
                }
            }
            reachable = reachableNext;
        }

        HashSet<(int Row, int Col)> last = (steps%2==0) ? reachableEven : reachableOdd;
        return last.Count();
    }

}
