using System.Text;

public class Day14
{
    private static int width;
    private static int height;
    private static StringBuilder[] map;

    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day14-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day14-{dataType}.txt");   
        var load=0L;
        height = input.Length+2;
        width=input[0].Length+2;
        map = new StringBuilder[height];
        map[0]=new StringBuilder(new string('#',width));
        for (var i=0;i<input.Length;++i) {
            var sb=new StringBuilder();
            sb.Append('#');
            sb.Append(input[i]);
            sb.Append('#');
            map[i+1]=sb;
        }
        map[height-1]=new StringBuilder(new string('#',width));
        for (var col=1;col<width-1;++col) {
            for (var row=1;row<height;++row) {
                if (map[row][col]=='O') {
                    //move it
                    map[row][col]='.';
                    var newRow=row;
                    while(map[newRow-1][col]=='.') {
                        newRow--;
                    }
                    map[newRow][col]='O';
                    load += height-newRow-1;
                }
            }
        }
        Console.WriteLine($"Part1: total load of rocks is {load}");
    }

    private static void TiltNorth() {        
        for (var col=1;col<width-1;++col) {
            for (var row=1;row<height;++row) {
                if (map[row][col]=='O') {
                    //move it
                    map[row][col]='.';
                    var newRow=row;
                    while(map[newRow-1][col]=='.') {
                        newRow--;
                    }
                    map[newRow][col]='O';
                }
            }
        }
    }
    
    private static void TiltSouth() {        
        for (var col=1;col<width-1;++col) {
            for (var row=height-1;row>0;--row) {
                if (map[row][col]=='O') {
                    //move it
                    map[row][col]='.';
                    var newRow=row;
                    while(map[newRow+1][col]=='.') {
                        newRow++;
                    }
                    map[newRow][col]='O';
                }
            }
        }
    }
        
    private static void TiltEast() {        
        for (var row=1;row<height-1;++row) {
            for (var col=width-1;col>0;--col) {
                if (map[row][col]=='O') {
                    //move it
                    map[row][col]='.';
                    var newCol=col;
                    while(map[row][newCol+1]=='.') {
                        newCol++;
                    }
                    map[row][newCol]='O';
                }
            }
        }
    }
        
    private static void TiltWest() {        
        for (var row=1;row<height-1;++row) {
            for (var col=1;col<width-1;++col) {
                if (map[row][col]=='O') {
                    //move it
                    map[row][col]='.';
                    var newCol=col;
                    while(map[row][newCol-1]=='.') {
                        newCol--;
                    }
                    map[row][newCol]='O';
                }
            }
        }
    }
    
    private static void Cycle() {
        TiltNorth();
        TiltWest();
        TiltSouth();
        TiltEast();
    }

    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day14-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day14-{dataType}.txt");   
        height = input.Length+2;
        width=input[0].Length+2;
        map = new StringBuilder[height];
        map[0]=new StringBuilder(new string('#',width));
        for (var i=0;i<input.Length;++i) {
            var sb=new StringBuilder();
            sb.Append('#');
            sb.Append(input[i]);
            sb.Append('#');
            map[i+1]=sb;
        }
        map[height-1]=new StringBuilder(new string('#',width));

        var cycle=0;
        var loadsSeen = new Dictionary<long,List<int>>(); //gives cycle numbers
        var previousLoads=new List<HashSet<(int Row,int Col)>>();
        var prev = new HashSet<(int Row, int Col)>(); 
        var load = 0L;       
        for (var row=1;row<height-1;++row) {
            for (var col=1;col<width-1;++col) {
                if (map[row][col]=='O') {                    
                    prev.Add((row,col));                   
                    load += height-row-1;
                }
            }
        }
        loadsSeen.Add(load, new List<int>{0});
        previousLoads = new List<HashSet<(int Row,int Col)>>{prev};
        var loads = new List<long>{load};
        var cycled=false;

        while(cycle++<=1000000000) {
            Cycle();
           /* if (cycle%1000000==0) {
                Console.WriteLine($"Cycle {cycle}");
                Console.WriteLine(string.Join("\n", map.Select(sb => sb.ToString())));
            }*/
            var next = new HashSet<(int Row, int Col)>();  
            load=0;
            for (var row=1;row<height-1;++row) {
                for (var col=1;col<width-1;++col) {
                    if (map[row][col]=='O') {                    
                        next.Add((row,col));                                        
                        load += height-row-1;
                    }
                }
            }
            if (load==65) {
                var stop=true;
            }
            if (loadsSeen.ContainsKey(load)) {
                foreach(var prevCycle in loadsSeen[load]) {
                    if (next.Any(rock => !previousLoads[prevCycle].Contains(rock))) {
                        //different loads                    
                    } else {
                        //Cycle found:!!!
                        Console.WriteLine($"Found cycle from {prevCycle} to {cycle}");
                        cycled=true;
                        var cycleLength=cycle-prevCycle;
                        while(cycle+cycleLength<=1000000000) cycle+=cycleLength;
                        var answer = prevCycle+(1000000000-cycle);
                        Console.WriteLine($"Part2: total sum of load is {loads[answer]}");
                        return;
                    }
                }
                if (!cycled) {                                             
                    loadsSeen[load].Add(cycle);
                    previousLoads.Add(next);   
                    loads.Add(load);
                }
            } else {
                loadsSeen.Add(load, new List<int>{cycle});
                previousLoads.Add(next);
                loads.Add(load);
            }
        }
        Console.WriteLine($"Stopped after {cycle} cycles");


        load=0L;        
        for (var row=1;row<height-1;++row) {
            for (var col=1;col<width-1;++col) {
                if (map[row][col]=='O') {                    
                    load += height-row-1;
                }
            }
        }
        Console.WriteLine($"Part2: total sum of load is {load}");
    }
}
