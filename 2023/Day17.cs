public class Day17
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day17-Part1-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day17-{dataType}.txt");   
        var height= map.Length;
        var width = map[0].Length;
        var todo = new PriorityQueue<(int Row, int Col, int Direction), int>();
        var shiftRow=new []{0,1,0,-1}; //right, down, left, up
        var shiftCol=new[]{1,0,-1,0};
        const int RIGHT=0;
        const int DOWN=1;

        var minHeatLoss=0;
        var row=0;
        var col=0;
        while(row<height-1 && col<width-1) {
            row++;
            minHeatLoss+=map[row][col]-'0';
            if (col<width-1) {
                col++;
                minHeatLoss+=map[row][col]-'0';
            }
        }

        var seen=new HashSet<(int Row, int Col, int Direction, int Step)>();
        todo.Enqueue((0,0,RIGHT),0);
        todo.Enqueue((0,0,DOWN),0);
        seen.Add((0,0,RIGHT,0));
        seen.Add((0,0,DOWN,0));
        while(todo.TryDequeue(out (int Row, int Col, int Direction) curr, out int heatLoss)){
            if (heatLoss>minHeatLoss) break;
            var ways=new []{(curr.Direction+1)%4, (curr.Direction+3)%4};
            foreach(var way in ways) {
                var newCol=curr.Col;
                var newRow=curr.Row;
                var newHeatLoss=heatLoss;
                for (var step=1;step<=3;++step) {
                    newCol+=shiftCol[way];
                    newRow+=shiftRow[way];
                    if (newCol<0 || newRow<0 || newCol>=width || newRow>=height || newHeatLoss>minHeatLoss) continue;
                    newHeatLoss+=map[newRow][newCol]-'0';
                    if (newRow==height-1 && newCol==width-1) {
                        minHeatLoss = Math.Min(minHeatLoss, newHeatLoss);
                    } else {
                        if (!seen.Contains((newRow, newCol,way, step))) {
                            todo.Enqueue((newRow, newCol, way), newHeatLoss);
                            seen.Add((newRow, newCol, way, step));
                        }
                    }                    
                }
            }

        }
        
        Console.WriteLine($"Part1: MinimumHeatLoss is {minHeatLoss}");
    }

    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day17-Part2-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day17-{dataType}.txt");   
        var height= map.Length;
        var width = map[0].Length;
        var todo = new PriorityQueue<(int Row, int Col, int Direction), int>();
        var shiftRow=new []{0,1,0,-1}; //right, down, left, up
        var shiftCol=new[]{1,0,-1,0};
        const int RIGHT=0;
        const int DOWN=1;

        var minHeatLoss=0;
        var row=0;
        var col=0;
        while(row<height-1 && col<width-1) {
            row++;
            minHeatLoss+=map[row][col]-'0';
            if (col<width-1) {
                col++;
                minHeatLoss+=map[row][col]-'0';
            }
        }

        var seen=new HashSet<(int Row, int Col, int Direction, int Step)>();
        todo.Enqueue((0,0,RIGHT),0);
        todo.Enqueue((0,0,DOWN),0);
        seen.Add((0,0,RIGHT,0));
        seen.Add((0,0,DOWN,0));
        while(todo.TryDequeue(out (int Row, int Col, int Direction) curr, out int heatLoss)){
            if (heatLoss>minHeatLoss) break;
            var ways=new []{(curr.Direction+1)%4, (curr.Direction+3)%4};
            foreach(var way in ways) {
                var newCol=curr.Col;
                var newRow=curr.Row;
                var newHeatLoss=heatLoss;
                for (var step=1;step<=10;++step) {
                    newCol+=shiftCol[way];
                    newRow+=shiftRow[way];
                    if (newCol<0 || newRow<0 || newCol>=width || newRow>=height || newHeatLoss>minHeatLoss) continue;
                    newHeatLoss+=map[newRow][newCol]-'0';
                    if (newRow==height-1 && newCol==width-1) {
                        minHeatLoss = Math.Min(minHeatLoss, newHeatLoss);
                    } else {
                        if (!seen.Contains((newRow, newCol,way, step))) {
                            if (step>=4) {
                                todo.Enqueue((newRow, newCol, way), newHeatLoss);
                            }
                            seen.Add((newRow, newCol, way, step));
                        }
                    }                    
                }
            }

        }
        Console.WriteLine($"Part2: MinimumHeatLoss is {minHeatLoss}");
    }
}
