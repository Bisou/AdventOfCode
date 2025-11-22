using System.Text;

public class Day12
{
    private const int day = 12;
    private static int width;
    private static int height;
    private static HashSet<(int Row, int Col)> seen;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);             
        height = map.Length;
        width = map[0].Length;
        var total = 0L;
        seen = new HashSet<(int Row, int Col)>();
        
        for (int row=0;row<height;++row) {
            for (int col=0;col<width;++col) {
                if (seen.Contains((row,col))) continue;
                total += GetPrice(map, row, col);
            }
        }
        Console.WriteLine($"Part1: total price is {total}");
    }
    

    private static long GetPrice(string[] map, int row, int col) {
        var area=1L;
        var fence=0L;
        var shiftRow=new [] {-1,0,1,0};
        var shiftCol=new [] {0,1,0,-1};
        var flower = map[row][col];

        var todo = new Queue<(int Row, int Col)>();
        todo.Enqueue((row, col));
        seen.Add((row,col));
        while(todo.Any()) {
            var curr=todo.Dequeue();
            for (var way=0;way<4;++way) {
                var newCol=curr.Col+shiftCol[way];
                var newRow=curr.Row+shiftRow[way];
                if (newRow<0 || newCol<0 || newRow>=height || newCol>=width) {
                    fence++; //border with outside
                    continue;
                }
                if (map[newRow][newCol] == flower) {
                    if (seen.Contains((newRow, newCol))) continue;
                    area++;
                    seen.Add((newRow, newCol));
                    todo.Enqueue((newRow, newCol));
                } else {
                    fence++; //border with other flower
                    continue;
                }             
            }
        }
       // Console.WriteLine($"{area}*{fence}={area*fence}");
        return area*fence;
    }
    
    private static long GetPrice2(string[] map, int row, int col) {
        var area=1L;
        var sides=new List<List<(int Row, int Col, int Way)>>();
        var shiftRow=new [] {-1,0,1,0};
        var shiftCol=new [] {0,1,0,-1};
        var flower = map[row][col];

        var todo = new Queue<(int Row, int Col)>();
        todo.Enqueue((row, col));
        seen.Add((row,col));
        while(todo.Any()) {
            var curr=todo.Dequeue();
            for (var way=0;way<4;++way) {
                var newCol=curr.Col+shiftCol[way];
                var newRow=curr.Row+shiftRow[way];
                if (newRow<0 || newCol<0 || newRow>=height || newCol>=width) {
                    var sideFound=false;
                    (int Row, int Col, int Way) border = (newRow, newCol, way);
                    for (var i=0;i<sides.Count;++i) {
                        var side = sides[i];
                        foreach(var b in side) {
                            if (b.Way!=border.Way) break;
                            if (b.Way % 2==1 && b.Col==border.Col && (b.Row+1==border.Row || b.Row-1==border.Row)) {
                                sideFound=true;
                                break;
                            }
                            if (b.Way % 2==0 && b.Row==border.Row && (b.Col+1==border.Col || b.Col-1==border.Col)) {
                                sideFound=true;
                                break;
                            }
                        }
                        if (sideFound) {
                            side.Add(border);
                            var secondSideFound=false;
                            for (var j=i+1;j<sides.Count;++j) {
                                //check if 2 sides merge with this new cell                                
                                var side2 = sides[j];
                                foreach(var b in side2) {
                                    if (b.Way!=border.Way) break;
                                    if (b.Way % 2==1 && b.Col==border.Col && (b.Row+1==border.Row || b.Row-1==border.Row)) {
                                        secondSideFound=true;
                                        break;
                                    }
                                    if (b.Way % 2==0 && b.Row==border.Row && (b.Col+1==border.Col || b.Col-1==border.Col)) {
                                        secondSideFound=true;
                                        break;
                                    }
                                }
                                if (secondSideFound) {
                                    //merge them
                                    foreach(var b in side2) {
                                        side.Add(b);
                                    }
                                    sides.RemoveAt(j);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if (!sideFound) {
                        sides.Add(new List<(int Row, int Col, int Way)>{border});
                    }
                    
                    continue;
                }
                if (map[newRow][newCol] == flower) {
                    if (seen.Contains((newRow, newCol))) continue;
                    area++;
                    seen.Add((newRow, newCol));
                    todo.Enqueue((newRow, newCol));
                } else {
                    var sideFound=false;
                    (int Row, int Col, int Way) border = (newRow, newCol, way);
                    for (var i=0;i<sides.Count;++i) {
                        var side = sides[i];
                        foreach(var b in side) {
                            if (b.Way!=border.Way) break;
                            if (b.Way % 2==1 && b.Col==border.Col && (b.Row+1==border.Row || b.Row-1==border.Row)) {
                                sideFound=true;
                                break;
                            }
                            if (b.Way % 2==0 && b.Row==border.Row && (b.Col+1==border.Col || b.Col-1==border.Col)) {
                                sideFound=true;
                                break;
                            }
                        }
                        if (sideFound) {
                            side.Add(border);
                            var secondSideFound=false;
                            for (var j=i+1;j<sides.Count;++j) {
                                //check if 2 sides merge with this new cell                                
                                var side2 = sides[j];
                                foreach(var b in side2) {
                                    if (b.Way!=border.Way) break;
                                    if (b.Way % 2==1 && b.Col==border.Col && (b.Row+1==border.Row || b.Row-1==border.Row)) {
                                        secondSideFound=true;
                                        break;
                                    }
                                    if (b.Way % 2==0 && b.Row==border.Row && (b.Col+1==border.Col || b.Col-1==border.Col)) {
                                        secondSideFound=true;
                                        break;
                                    }
                                }
                                if (secondSideFound) {
                                    //merge them
                                    foreach(var b in side2) {
                                        side.Add(b);
                                    }
                                    sides.RemoveAt(j);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if (!sideFound) {
                        sides.Add(new List<(int Row, int Col, int Way)>{border});
                    }
                    continue;
                }             
            }
        }
        
        Console.WriteLine($"{flower} in ({row},{col} is {area}*{sides.Count}={area*sides.Count}");
        return area*sides.Count;
    }

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);
        var map = Tool.ReadAll(day, dataType);             
        height = map.Length;
        width = map[0].Length;
        var total = 0L;
        seen = new HashSet<(int Row, int Col)>();
        
        for (int row=0;row<height;++row) {
            for (int col=0;col<width;++col) {
                if (seen.Contains((row,col))) continue;
                total += GetPrice2(map, row, col);
            }
        }
        Console.WriteLine($"Part2: total price is {total}");
    }

}
