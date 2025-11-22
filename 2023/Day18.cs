using System.Text;

public class Day18
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day18-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day18-{dataType}.txt");   
        var minRow=0;
        var maxRow=0;
        var minCol=0;
        var maxCol=0;
        var shiftRow = new Dictionary<string, int>{
            {"R",0},
            {"D",1},
            {"L",0},
            {"U",-1}
        };
        var shiftCol = new Dictionary<string, int>{
            {"R",1},
            {"D",0},
            {"L",-1},
            {"U",0}
        };
        var walls=new HashSet<(int Row, int Col)>();
        walls.Add((0,0));
        var row=0;
        var col=0;
        foreach(var line in input) {
            var data = line.Split(' ');
            for (var i=0;i<int.Parse(data[1]);++i) {
                row+=shiftRow[data[0]];
                col+=shiftCol[data[0]];
                minRow=Math.Min(row, minRow);
                maxRow=Math.Max(row, maxRow);
                minCol=Math.Min(col, minCol);
                maxCol=Math.Max(col, maxCol);
                walls.Add((row,col));
            }
        }
        var height=maxRow+1-minRow;
        var width=maxCol+1-minCol;
        var map = new StringBuilder[height];
        for (var i=0;i<height;++i) {
            map[i]=new StringBuilder(new String('.', width));
        }
        foreach(var wall in walls) {
            map[wall.Row-minRow][wall.Col-minCol]='#';
        }
        var volume=width*height;

        //now, floodfill
        var todo=new Queue<(int Row, int Col)>();
        for (row=0;row<height;++row) {
            if (map[row][0]=='.') {
                map[row][0]=' ';
                volume--;
                todo.Enqueue((row,0));
            }
            if (map[row][width-1]=='.') {
                map[row][width-1]=' ';
                volume--;
                todo.Enqueue((row,width-1));
            }
        }
        for (col=0;col<width;++col) {
            if (map[0][col]=='.') {
                map[0][col]=' ';
                volume--;
                todo.Enqueue((0,col));
            }
            if (map[height-1][col]=='.') {
                map[height-1][col]=' ';
                volume--;
                todo.Enqueue((height-1,col));
            }
        }
        while(todo.Any()) {
            var curr = todo.Dequeue();
            foreach(var way in new [] {"R","D","L","U"}) {
                var newCol=curr.Col+shiftCol[way];
                var newRow=curr.Row+shiftRow[way];
                if (newRow<0 || newCol<0 || newRow>=height || newCol>=width || map[newRow][newCol]!='.') continue;
                map[newRow][newCol]=' ';
                volume--;
                todo.Enqueue((newRow,newCol));
            }
        }
        Console.WriteLine($"Part1: volume digged is {volume}");
    }

    
    public static void SolvePart15(string dataType)
    {
        Console.WriteLine($"Day18-Part1.5-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day18-{dataType}.txt");   
        
        var directions = "RDLU";
        var orders = new List<(int Direction, long Steps)>();
        foreach(var line in input) {
            var data = line.Split(' ');
            orders.Add((directions.IndexOf(line[0]), int.Parse(data[1])));
        }
        var volume = GetVolume(orders);
        Console.WriteLine($"Part1.5: volume digged is {volume}");
    }

    private static long GetVolume(List<(int Direction, long Steps)> orders) {

        var volume=0L;

        while(orders.Count>4) {
            var reduce=true;
            while(reduce) {
                reduce=false;
                for (var i=0;i<orders.Count;++i) {
                    if (orders[(i+1)%orders.Count].Direction==orders[i].Direction) {       
                        //merge 2 similar orders (R3 + R4 => R7)
                        orders[i] = (orders[i].Direction, orders[i].Steps+orders[(i+1)%orders.Count].Steps);
                        orders.RemoveAt(i+1);
                        reduce=true;
                    } else if (Math.Abs(orders[(i+1)%orders.Count].Direction-orders[i].Direction)==2) {
                        //opposite ways (R3+L5=>L2)                    
                        reduce=true;
                        volume += Math.Abs(orders[i].Steps-orders[(i+1)%orders.Count].Steps);
                        if (orders[i].Steps>orders[(i+1)%orders.Count].Steps) {                                    
                            orders[i] = (orders[i].Direction, orders[i].Steps-orders[(i+1)%orders.Count].Steps);
                            orders.RemoveAt((i+1)%orders.Count);
                        } else {
                            orders[(i+1)%orders.Count] = (orders[(i+1)%orders.Count].Direction, orders[(i+1)%orders.Count].Steps-orders[i].Steps);
                            orders.RemoveAt(i);
                        }
                    } else if (orders[i].Steps==0) {
                        orders.RemoveAt(i);                    
                        reduce=true;
                    }
                }
            }

            for (var i=0;i<orders.Count;++i) {
                if ((orders[i].Direction+1)%4 == orders[(i+1)%orders.Count].Direction
                    && (orders[(i+1)%orders.Count].Direction+1)%4 == orders[(i+2)%orders.Count].Direction) {
                    //can be reduced (ex: R8, D5, L6 ==> R2 D5)
                    reduce=true;
                    var width=orders[(i+1)%orders.Count].Steps;
                    var height=Math.Min(orders[i].Steps, orders[(i+2)%orders.Count].Steps);
                    volume+=(width+1)*height;
                    if (orders[i].Steps==height) {
                        orders[(i+2)%orders.Count] = (orders[(i+2)%orders.Count].Direction, orders[(i+2)%orders.Count].Steps-height);
                        orders.RemoveAt(i);
                    } else {
                        orders[i] = (orders[i].Direction, orders[i].Steps-height);
                        orders.RemoveAt((i+2)%orders.Count);
                    }
                } else if ((orders[i].Direction+3)%4 == orders[(i+1)%orders.Count].Direction
                    && (orders[(i+1)%orders.Count].Direction+3)%4 == orders[(i+2)%orders.Count].Direction) {
                    //can be reduced but in the other way
                    reduce=true;
                    var width=orders[(i+1)%orders.Count].Steps;
                    var height=Math.Min(orders[i].Steps, orders[(i+2)%orders.Count].Steps);
                    volume-=(width+1)*height;
                    if (orders[i].Steps==height) {
                        orders[(i+2)%orders.Count] = (orders[(i+2)%orders.Count].Direction, orders[(i+2)%orders.Count].Steps-height);
                        orders.RemoveAt(i);
                    } else {
                        orders[i] = (orders[i].Direction, orders[i].Steps-height);
                        orders.RemoveAt((i+2)%orders.Count);
                    }
                    }
                if (reduce) {
                    break;
                }
            }
            if (!reduce) {
                var stop=true;
            }
        }

        volume+=orders[0].Steps*orders[1].Steps;
        return volume;
    }

    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day18-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day18-{dataType}.txt"); 
        var directions = new [] {"R", "D", "L", "U"};
        var walls=new HashSet<(long Row, long Col)>();
        walls.Add((0,0));
        var row=0L;
        var col=0L;
        var orders = new List<(int Direction, long Steps)>();
        foreach(var line in input) {
            var data = line.Split(' ');
            var number = Convert.ToInt32(data[2].Substring(2,6),16);
            var steps = number/16;
            var direction=number%16;
            //Console.WriteLine($"{directions[direction]} {steps}");
            orders.Add((direction, steps));
        }

        var volume=0L;

        while(orders.Count>4) {
            var reduce=false;
            for (var i=0;i<orders.Count;++i) {
                if (orders[(i+2)%orders.Count].Direction-orders[(i+1)%orders.Count].Direction==orders[(i+1)%orders.Count].Direction-orders[i].Direction) {
                    //can be reduced (ex: R8, D5, L6 ==> R2 D5)
                    reduce=true;
                    var width=orders[(i+1)%orders.Count].Steps;
                    var height=Math.Min(orders[i].Steps, orders[(i+2)%orders.Count].Steps);
                    volume+=width*height;
                    if (orders[i].Steps==height) {
                        orders[(i+2)%orders.Count] = (orders[(i+2)%orders.Count].Direction, orders[(i+2)%orders.Count].Steps-height);
                        orders.RemoveAt(i);
                        //can we merge?
                        if (orders[i].Direction==orders[(i+1)%orders.Count].Direction) {
                            orders[(i+1)%orders.Count] = (orders[(i+1)%orders.Count].Direction, orders[(i+1)%orders.Count].Steps+orders[i].Steps);
                            orders.RemoveAt(i);
                        }
                    } else {
                        orders[i] = (orders[i].Direction, orders[i].Steps-height);
                        orders.RemoveAt((i+2)%orders.Count);
                        //can we merge?
                        if (i+2<orders.Count) {
                            if (orders[(i+2)%orders.Count].Direction==orders[i+1].Direction) {                                
                                orders[(i+1)%orders.Count] = (orders[(i+1)%orders.Count].Direction, orders[(i+1)%orders.Count].Steps+orders[(i+2)%orders.Count
                                ].Steps);
                                orders.RemoveAt(i+2);
                            }
                        } else {                            
                            if (orders[0].Direction==orders[(i+1)%orders.Count].Direction) {
                                orders[0] = (orders[0].Direction, orders[0].Steps+orders[(i+1)%orders.Count].Steps);
                                orders.RemoveAt((i+1)%orders.Count);
                            }
                        }
                    }
                }
                if (reduce) {
                    break;
                }
            }
        }

        volume+=orders[0].Steps*orders[1].Steps;
        Console.WriteLine($"Part2: volume is {volume}");
    }
}
