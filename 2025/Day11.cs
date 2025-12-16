public class Day11
{
    private const int day = 11;
    private static Dictionary<string,List<string>> graph;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        graph = new Dictionary<string,List<string>>();
        foreach (var line in input)
        {
            var data = line.Split(new []{':', ' '}, StringSplitOptions.RemoveEmptyEntries);
            var key = data[0];
            var neighbours = data.Skip(1).ToList();
            graph.Add(key, neighbours);
        }
        var res=Path("you");
        

        Console.WriteLine($"Part1: total path count is {res}");
    }

    private static int Path(string current)
    {
        if (current == "out") return 1;
        var res=0;
        foreach (var next in graph[current])
        {
            res += Path(next);
        }
        return res;
    }

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        graph = new Dictionary<string,List<string>>();
        foreach (var line in input)
        {
            var data = line.Split(new []{':', ' '}, StringSplitOptions.RemoveEmptyEntries);
            var key = data[0];
            var neighbours = data.Skip(1).ToList();
            graph.Add(key, neighbours);
        }
        graph["out"] = new List<string>();
        var res=0L;
        cache = new Dictionary<(string start, string target), long>();
        var dacToFft = Path2("dac", "fft");        
        var fftToDac = Path2("fft", "dac");
        if (dacToFft > 0)
        {
            res = dacToFft * Path2("svr", "dac") * Path2("fft", "out");
        } else
        {
            res = fftToDac * Path2("svr", "fft") * Path2("dac", "out");
        }



        Console.WriteLine($"Part2: total path is {res}");
    }

    private static Dictionary<(string start, string target), long> cache;

    private static long Path2(string start, string target)
    {
        if (cache.TryGetValue((start, target), out long value))
        {
            return value;
        }
        if (start == target) return 1;
        var res=0L;
        foreach (var next in graph[start])
        {
            res += Path2(next, target);
        }
        return cache[(start, target)]=res;
    }

}

