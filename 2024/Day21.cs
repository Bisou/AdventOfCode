using System.Text;

public class Day21
{
    private const int day = 21;
          
          

    public static string GetMove(string[] keypad, Dictionary<(char Start, char End), string> cache, char start, char end, bool arrows=false) {
        if (cache.TryGetValue((start, end), out var val)) 
            return val; 
        if (start==end) return "A";
        var shiftCol = new []   {  -1,   0,   0,   1 };
        var shiftRow = new []   {   0,   1,  -1,   0 };
        var directions = new [] { '<', 'v', '^', '>' }; //move left, then down, then up, then right.
        var opposites =  new [] { '>', '^', 'v', '<' };
        var height=keypad.Length;
        var width = keypad[0].Length;

        (int Row, int Col) s=(-1,-1);
        for (var r=0;r<height;++r) {
            for (var c=0;c<width;++c) {
                if (keypad[r][c]==start) {
                    s=(r,c);
                }
            }
        }
        var paths = new List<string>();
        var seen=new HashSet<char>();
        seen.Add(start);
        seen.Add(' ');//to avoid going there
        var todo=new Queue<(int Row, int Col, string Path)>();
        todo.Enqueue((s.Row, s.Col, ""));
        while(todo.Any()) {
            var curr=todo.Dequeue();
            for (var way=0;way<4;++way) {
                var newCol=curr.Col+shiftCol[way];
                var newRow = curr.Row+shiftRow[way];
                if (newRow<0 || newRow>=height || newCol<0 || newCol>=width || seen.Contains(keypad[newRow][newCol])) continue;

                if (curr.Path.Contains(opposites[way])) continue; //no going in opposite directions
                var newPath=curr.Path+directions[way];                
                if (keypad[newRow][newCol]==end) {
                    paths.Add(newPath);  
                } else {
                    todo.Enqueue((newRow, newCol,newPath));
                }
            }
        }
        //I now got all paths
        var bestPaths=new List<string>();
        var minChanges=int.MaxValue;
        foreach(var path in paths) {
            var changes=0;
            for (var i=1;i<path.Length;++i) {
                if (path[i]!=path[i-1]) changes++;
            }
            if (changes<minChanges) {
                bestPaths=new List<string>{path};
                minChanges=changes;
            } else if (changes==minChanges) {
                bestPaths.Add(path);
            }
        }
        
        var bestPath = bestPaths[0];
        cache[(start,end)] = bestPath + "A";
        return cache[(start,end)];
    }

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);  
        var keypad1=new []{"789","456","123"," 0A"};
        var keypad2=new []{" ^A","<v>"};
        var full=true;
        
        var total=0L; 
        var moves1 = new Dictionary<(char Start, char End), string>();
        var moves2 = new Dictionary<(char Start, char End), string>();
        
        foreach(var line in input) {
            var prev='A';
            var path1 = new StringBuilder();
            var val=0L;
            foreach(var c in line) {
                if ('0'<=c && c<='9') {
                    val = val*10 + (c-'0');
                }
                path1.Append(GetMove(keypad1, moves1,prev,c));
                prev=c;
            }
            var path = path1.ToString();
            var pathLength = Dfs(keypad2, moves2, path, 2);
            Console.WriteLine($"{line} => {pathLength}");
            total += val * pathLength;
        }  
        Console.WriteLine($"Part1: result is {total}");
    }


    public static void SolvePart2(string dataType)
    {
        Tool.LogStart(day,2,dataType);
        var input = Tool.ReadAll(day, dataType);  
        globalCache.Clear();
        var keypad1=new []{"789","456","123"," 0A"};
        var keypad2=new []{" ^A","<v>"};        
        
        var total=0L; 
        var moves1 = new Dictionary<(char Start, char End), string>();
        var moves2 = new Dictionary<(char Start, char End), string>();
        
        foreach(var line in input) {
            var prev='A';
            var path1 = new StringBuilder();
            var val=0L;
            foreach(var c in line) {
                if ('0'<=c && c<='9') {
                    val = val*10 + (c-'0');
                }
                path1.Append(GetMove(keypad1, moves1,prev,c));
                prev=c;
            }
            var path = path1.ToString();
            var pathLength = Dfs(keypad2, moves2, path, 25);
            total += val * pathLength;
        }  
        Console.WriteLine($"Part2: result is {total}");
    }
    
    private static Dictionary<string, long?[]> globalCache = new Dictionary<string, long?[]>();

    private static long Dfs(string[] keypad, Dictionary<(char Start, char End), string> cache, string line, int level) {    
        if (globalCache.TryGetValue(line, out var answer)) {
            if (answer[level].HasValue) return answer[level].Value;
        } else {
            globalCache.Add(line, new long?[26]);
        }
        if (level==0) {
            //Console.Write(line);
            return line.Length;
        }
        var prev='A';
        var res=0L;
        foreach(var c in line) {
            var nextRobotMove = GetMove(keypad, cache, prev, c, true);
            res += Dfs(keypad, cache, nextRobotMove, level-1);
            prev=c;
        }
        globalCache[line][level] = res;
        return res;
    }
}
