using System.Text;

public class Day19
{
    private const int day = 19;
    private static HashSet<string> towels;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);  
        towels = new HashSet<string>();
        foreach(var towel in input[0].Split(new [] {' ',','}, StringSplitOptions.RemoveEmptyEntries))
            towels.Add(towel);
        var total=0;        
        
        for (var i=2;i<input.Length;++i) {
            var pattern = input[i];
            if (Possible(pattern)) total++;
        }
        Console.WriteLine($"Part1: we can do {total} patterns");
    }

    private static bool PossibleDfs(string pattern, int index) {
        //too slow
        if (index==pattern.Length) return true;
        var res=false;
        var sb = new StringBuilder();
        for (var i=index;i<pattern.Length;++i) {
            sb.Append(pattern[i]);
            if (towels.Contains(sb.ToString())) {
                if (PossibleDfs(pattern, i+1)) return true;
            }
        }
        return false;
    }

    private static bool Possible(string pattern) {
        //DP
        var possible = new bool[pattern.Length+1];
        possible[0]=true;
        for (var start=0;start<pattern.Length;++start) {
            if (!possible[start]) continue;
            var sb = new StringBuilder();
            for (var i=start;i<pattern.Length;++i) {
                sb.Append(pattern[i]);
                if (towels.Contains(sb.ToString())) {
                    possible[i+1]=true;
                }
            }
        }
        return possible[pattern.Length];
    }

    

    private static long Possible2(string pattern) {
        //DP
        var possible = new long[pattern.Length+1];
        possible[0]=1;
        for (var start=0;start<pattern.Length;++start) {
            if (possible[start]==0) continue;
            var sb = new StringBuilder();
            for (var i=start;i<pattern.Length;++i) {
                sb.Append(pattern[i]);
                if (towels.Contains(sb.ToString())) {
                    possible[i+1]+=possible[start];
                }
            }
        }
        return possible[pattern.Length];
    }

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);
        var input = Tool.ReadAll(day, dataType);  
        towels = new HashSet<string>();
        foreach(var towel in input[0].Split(new [] {' ',','}, StringSplitOptions.RemoveEmptyEntries))
            towels.Add(towel);
        var total=0L;        
        
        for (var i=2;i<input.Length;++i) {
            var pattern = input[i];
            total+=Possible2(pattern);
        }
        Console.WriteLine($"Part2: we can do {total} patterns");
    }
}
