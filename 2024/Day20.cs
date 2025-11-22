using System.Text;

public class Day20
{
    private const int day = 20;
    private static int width;
    private static int height;    

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var map = Tool.ReadAll(day, dataType);  
        height=map.Length;
        width=map[0].Length;
        var total=0;        
        (int Row, int Col) start=(-1,-1);
        (int Row, int Col) end=(-1,-1);
        var cells = 0;
        for(var r=0;r<height;++r) {
            for (var c=0;c<width;++c) {
                if (map[r][c]=='S') {
                    start=(r,c);
                }
                if (map[r][c]=='E') {
                    end=(r,c);
                }   
                if (map[r][c]=='.') {
                    cells++;
                }                
            }
        } 
        var shiftCol = new []{1,0,-1,0};
        var shiftRow = new []{0,1,0,-1};
        var seen = new Dictionary<(int Row, int Col), int>();
        seen.Add(start,0);
        var todo = new List<(int Row, int Col)>{start};
        var steps=1;
        while(todo.Any()) {
            var next = new List<(int Row, int Col)>();
            foreach (var curr in todo) {
                for (var way=0;way<4;++way) {
                    var newCol=curr.Col+shiftCol[way];
                    var newRow=curr.Row+shiftRow[way];
                    if (newCol<0 || newRow<0 || newCol>=width || newRow>=height || seen.ContainsKey((newRow, newCol)) || map[newRow][newCol]=='#') continue;
                    seen.Add((newRow, newCol), steps);
                    next.Add((newRow, newCol));
                }
            }
            todo=next;
            ++steps;
        }
        Console.WriteLine($"total of {steps} steps and {cells} empty cells");

        var cheats = new List<int>();
        for(var r=0;r<height;++r) {
            for (var c=0;c<width;++c) {
                if (map[r][c]=='#') continue;
                var current = seen[(r,c)];
                for (var way=0;way<4;++way) {
                    (int Row, int Col) wall = (r+shiftRow[way], c+shiftCol[way]);
                    if (map[wall.Row][wall.Col]!='#') continue;                    
                    (int Row, int Col) track = (r+2*shiftRow[way], c+2*shiftCol[way]);                    
                    if (track.Col<0 || track.Row<0 || track.Col>=width || track.Row>=height || !seen.ContainsKey(track) || map[track.Row][track.Col]=='#') continue;                    
                    var saving=seen[(track.Row,track.Col)]-current-2;
                    if (saving>0) {
                        cheats.Add(saving);
                        //Console.WriteLine($"Cheat for {saving}ps from ({r},{c}) to ({track.Row},{track.Col})");
                    }
                }
            }
        } 

        if (dataType=="Sample") {
            foreach(var g in cheats.GroupBy(t => t).OrderBy(g => g.Key)) {
                Console.WriteLine($"{g.Count()} cheats for {g.Key} picosedoncs");
            }
        } else 
            Console.WriteLine($"Part1: we can do {cheats.Count(t => t>=100)} shortcuts");
    }



    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);
        var map = Tool.ReadAll(day, dataType);  
        height=map.Length;
        width=map[0].Length;
        var total=0;        
        (int Row, int Col) start=(-1,-1);
        (int Row, int Col) end=(-1,-1);
        var cells = 0;
        for(var r=0;r<height;++r) {
            for (var c=0;c<width;++c) {
                if (map[r][c]=='S') {
                    start=(r,c);
                }
                if (map[r][c]=='E') {
                    end=(r,c);
                }   
                if (map[r][c]=='.') {
                    cells++;
                }                
            }
        } 
        var shiftCol = new []{1,0,-1,0};
        var shiftRow = new []{0,1,0,-1};
        var seen = new Dictionary<(int Row, int Col), int>();
        seen.Add(start,0);
        var todo = new List<(int Row, int Col)>{start};
        var steps=1;
        while(todo.Any()) {
            var next = new List<(int Row, int Col)>();
            foreach (var curr in todo) {
                for (var way=0;way<4;++way) {
                    var newCol=curr.Col+shiftCol[way];
                    var newRow=curr.Row+shiftRow[way];
                    if (newCol<0 || newRow<0 || newCol>=width || newRow>=height || seen.ContainsKey((newRow, newCol)) || map[newRow][newCol]=='#') continue;
                    seen.Add((newRow, newCol), steps);
                    next.Add((newRow, newCol));
                }
            }
            todo=next;
            ++steps;
        }
        Console.WriteLine($"total of {steps} steps and {cells} empty cells");

        var cheats = new List<int>();
        for(var r=0;r<height;++r) {
            for (var c=0;c<width;++c) {
                if (map[r][c]=='#') continue;
                var startTime = seen[(r,c)];
                var visited = new HashSet<(int Row, int Col)>();
                visited.Add((r,c));
                todo.Clear();
                steps=0;
                todo.Add((r,c));
                while(todo.Any() && steps<20) {      
                    steps++;              
                    var next = new List<(int Row, int Col)>();
                    foreach (var curr in todo) {
                        for (var way=0;way<4;++way) {
                            var newCol=curr.Col+shiftCol[way];
                            var newRow=curr.Row+shiftRow[way];
                            if (newCol<0 || newRow<0 || newCol>=width || newRow>=height || visited.Contains((newRow, newCol))) continue;                            
                            visited.Add((newRow, newCol));
                            if (map[newRow][newCol]=='#') {
                                next.Add((newRow, newCol));
                            } else {
                                var saving=seen[(newRow,newCol)]-startTime-steps;
                                if (saving>0) {
                                    cheats.Add(saving);
                                }
                                next.Add((newRow, newCol));
                            }
                            
                        }
                    }
                    todo=next;
                }
            }
        } 

        if (dataType=="Sample") {
            foreach(var g in cheats.GroupBy(t => t).OrderBy(g => g.Key)) {
                Console.WriteLine($"{g.Count()} cheats for {g.Key} picosedoncs");
            }
        } else 
            Console.WriteLine($"Part2: we can do {cheats.Count(t => t>=100)} shortcuts");
    }
}
