public class Day01
{
    private const int day = 1;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);     
        var dist=0;
        var left = new List<int>();
        var right = new List<int>();
        foreach (var line in input)
        {
            var data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            left.Add(data[0]);
            right.Add(data[1]);
        }
        left = left.OrderBy(x => x).ToList();
        right = right.OrderBy(x => x).ToList();
        for(var i = 0; i < left.Count;++i) {
            dist += Math.Abs(left[i]-right[i]);
        }
        Console.WriteLine($"Part1: total distance is {dist}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType); 
        var similarity=0;
        
        var left = new List<int>();
        var right = new List<int>();
        foreach (var line in input)
        {
            var data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            left.Add(data[0]);
            right.Add(data[1]);
        }
        foreach(var location in left) {
            similarity += location * right.Count(x => x==location);
        }
                
        Console.WriteLine($"Part2: similarity is {similarity}");
    }
}
