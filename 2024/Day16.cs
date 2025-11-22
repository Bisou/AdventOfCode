using System.Text;

public class Day16
{
    private const int day = 16;
    private static int width;
    private static int height;
    private static List<StringBuilder> map;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        width = map[0].Length;
        height=map.Length;
        (int Row, int Col, int Way, int Cost) start = (-1,-1, -1, -1);
        var shiftCol = new []{1,0,-1,0};
        var shiftRow = new []{0,1,0,-1};
        for(var r=0;r<height;++r) {
            for (var c=0;c<width;++c) {
                if (map[r][c]=='S') {
                    start = (r, c, 0, 0);
                }
            }
        }

        var todo = new PriorityQueue<(int Row, int Col, int Way, int Cost), int>();
        todo.Enqueue(start, 0);
        var seen = new Dictionary<(int Row, int Col, int Way), int>();
        seen.Add((start.Row, start.Col, start.Way), 0);

        var minCost=int.MaxValue;
        while (todo.Count > 0) {
            var curr = todo.Dequeue();
            if (seen[(curr.Row, curr.Col, curr.Way)]<curr.Cost) continue;
            
            //right
            var newLeftWay=(curr.Way+1)%4;
            var newCost=curr.Cost+1000;
            if (seen.TryGetValue((curr.Row, curr.Col, newLeftWay), out int cost)) {
                if (newCost<cost) {
                    seen[(curr.Row, curr.Col, newLeftWay)]=newCost;
                    todo.Enqueue((curr.Row, curr.Col, newLeftWay, newCost), newCost);
                }
            } else {
                seen.Add((curr.Row, curr.Col, newLeftWay),newCost);
                todo.Enqueue((curr.Row, curr.Col, newLeftWay, newCost), newCost);
            }

            //left
            var newRightWay=(curr.Way+3)%4;
            if (seen.TryGetValue((curr.Row, curr.Col, newRightWay), out cost)) {
                if (newCost<cost) {
                    seen[(curr.Row, curr.Col, newRightWay)]=newCost;
                    todo.Enqueue((curr.Row, curr.Col, newRightWay, newCost), newCost);
                }
            } else {
                seen.Add((curr.Row, curr.Col, newRightWay),newCost);
                todo.Enqueue((curr.Row, curr.Col, newRightWay, newCost), newCost);
            }

            //straight
            var col=curr.Col;
            var row=curr.Row;
            newCost = curr.Cost;
            while(true) {
                col+=shiftCol[curr.Way];
                row+=shiftRow[curr.Way];
                newCost++;
                if (map[row][col]=='E') {
                    minCost = Math.Min(minCost, newCost);
                    break;
                } else if (map[row][col]=='#') {
                    break;
                } else {
                    //can move here. Can we turn?
                    col+=shiftCol[newRightWay];
                    row+=shiftRow[newRightWay];
                    var canTurn=false;
                    if (map[row][col]=='.') canTurn=true;
                    col-=shiftCol[newRightWay];
                    row-=shiftRow[newRightWay];
                    col+=shiftCol[newLeftWay];
                    row+=shiftRow[newLeftWay];
                    if (map[row][col]=='.') canTurn=true;
                    col-=shiftCol[newLeftWay];
                    row-=shiftRow[newLeftWay];

                    if (canTurn) {
                        if (seen.TryGetValue((row, col, curr.Way), out cost)) {
                            if (newCost<cost) {
                                seen[(row, col, curr.Way)]=newCost;
                                todo.Enqueue((row, col, curr.Way, newCost), newCost);
                            }
                        } else {
                            seen.Add((row, col, curr.Way),newCost);
                            todo.Enqueue((row, col, curr.Way, newCost), newCost);
                        }                    
                        break;
                    }
                }
            }
        }

        Console.WriteLine($"Part1: best score is {minCost}");
    }
    


    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);
        var input = Tool.ReadAll(day, dataType);  
        var map = Tool.ReadAll(day, dataType);             
        width = map[0].Length;
        height=map.Length;
        (int Row, int Col, int Way, int Cost) start = (-1,-1, -1, -1);
        var shiftCol = new []{1,0,-1,0};
        var shiftRow = new []{0,1,0,-1};
        for(var r=0;r<height;++r) {
            for (var c=0;c<width;++c) {
                if (map[r][c]=='S') {
                    start = (r, c, 0, 0);
                }
            }
        }

        var todo = new PriorityQueue<(int Row, int Col, int Way, int Cost), int>();
        todo.Enqueue(start, 0);
        var seen = new Dictionary<(int Row, int Col, int Way), (int Cost, List<(int Row, int Col, int Way)> Parents)>();
        seen.Add((start.Row, start.Col, start.Way), (0, new List<(int Row, int Col, int Way)>()));
        (int Row, int Col) end = (-1,-1);
        var minCost=int.MaxValue;
        while (todo.Count > 0) {
            var curr = todo.Dequeue();
            if (seen[(curr.Row, curr.Col, curr.Way)].Cost<curr.Cost) continue;
            
            //right
            var newLeftWay=(curr.Way+1)%4;
            var newCost=curr.Cost+1000;
            if (seen.TryGetValue((curr.Row, curr.Col, newLeftWay), out var cost)) {
                if (newCost<cost.Cost) {
                    seen[(curr.Row, curr.Col, newLeftWay)]=(newCost, new List<(int Row, int Col, int Way)>{(curr.Row, curr.Col, curr.Way)});
                    todo.Enqueue((curr.Row, curr.Col, newLeftWay, newCost), newCost);
                } else if (newCost==cost.Cost) {
                    seen[(curr.Row, curr.Col, newLeftWay)].Parents.Add((curr.Row, curr.Col, curr.Way));
                }
            } else {
                seen.Add((curr.Row, curr.Col, newLeftWay),(newCost, new List<(int Row, int Col, int Way)>{(curr.Row, curr.Col, curr.Way)}));
                todo.Enqueue((curr.Row, curr.Col, newLeftWay, newCost), newCost);                
            }

            //left
            var newRightWay=(curr.Way+3)%4;
            if (seen.TryGetValue((curr.Row, curr.Col, newRightWay), out cost)) {
                if (newCost<cost.Cost) {
                    seen[(curr.Row, curr.Col, newRightWay)]=(newCost, new List<(int Row, int Col, int Way)>{(curr.Row, curr.Col, curr.Way)});
                    todo.Enqueue((curr.Row, curr.Col, newRightWay, newCost), newCost);
                } else if (newCost==cost.Cost) {
                    seen[(curr.Row, curr.Col, newRightWay)].Parents.Add((curr.Row, curr.Col, curr.Way));
                }
            } else {
                seen.Add((curr.Row, curr.Col, newRightWay),(newCost,new List<(int Row, int Col, int Way)>{(curr.Row, curr.Col, curr.Way)}));
                todo.Enqueue((curr.Row, curr.Col, newRightWay, newCost), newCost);
            }

            //straight
            var col=curr.Col;
            var row=curr.Row;
            newCost = curr.Cost+1;           
            col+=shiftCol[curr.Way];
            row+=shiftRow[curr.Way];
            if (map[row][col]=='E') {
                end = (row, col);
                if (newCost==minCost) {
                    seen[(row, col, curr.Way)].Parents.Add((curr.Row, curr.Col, curr.Way));
                } else if (newCost<minCost) {
                    minCost=newCost;
                    seen[(row, col, curr.Way)]=(newCost,new List<(int Row, int Col, int Way)>{(curr.Row, curr.Col, curr.Way)});
                }
            } else if (map[row][col]=='.') {
                if (seen.TryGetValue((row, col, curr.Way), out cost)) {
                    if (newCost<cost.Cost) {
                        seen[(row, col, curr.Way)]=(newCost,new List<(int Row, int Col, int Way)>{(curr.Row, curr.Col, curr.Way)});
                        todo.Enqueue((row, col, curr.Way, newCost), newCost);                   
                    } else if (newCost==cost.Cost) {                        
                        seen[(row, col, curr.Way)].Parents.Add((curr.Row, curr.Col, curr.Way));
                    }
                } else {
                    seen.Add((row, col, curr.Way),(newCost,new List<(int Row, int Col, int Way)>{(curr.Row, curr.Col, curr.Way)}));
                    todo.Enqueue((row, col, curr.Way, newCost), newCost);
                }                                    
            }
        }

        //backtrack
        var path = new HashSet<(int Row, int Col)>();
        path.Add(end);
        var queue = new Queue<(int Row, int Col, int Way)>();
        for(var way=0;way<4;++way) {
            if (seen.ContainsKey((end.Row, end.Col, way))) {
                queue.Enqueue((end.Row, end.Col, way));
            }
        }
        while(queue.Any()) {
            var curr = queue.Dequeue();
            foreach(var p in seen[curr].Parents) {
                path.Add((p.Row, p.Col));
                queue.Enqueue(p);
            }
        }


        Console.WriteLine($"Part2: total path cells is {path.Count()}");
    }
}
