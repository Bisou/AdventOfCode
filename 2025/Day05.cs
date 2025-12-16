public class Day05
{
    private const int day = 5;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var fresh=0;
        var intervals = new List<long[]>();
        var step=1;
        foreach (var line in input)
        {
            if (line.Length==0) {
                ++step;
                continue;
            }
            if (step==1)
            {
                var interval = line.Split('-',StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                intervals.Add(interval);
            } else
            {
                var value = long.Parse(line);
                if (intervals.Any(x => x[0]<=value && value<=x[1])) ++fresh;
            }
        }

        Console.WriteLine($"Part1: total count of fresh ingredients is {fresh}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var fresh=0L;
        var intervals = new List<long[]>();
        foreach (var line in input)
        {
            if (line.Length==0) {
                break;;
            }
            var interval = line.Split('-',StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            intervals.Add(interval);
        }

        intervals = intervals.OrderBy(x => x[1]).ThenBy(x => x[0]).ToList();
        var i=1;
        while(i<intervals.Count)
        {
            if (intervals[i-1][1]<intervals[i][0])
            {
                //distinct
                ++i;
                continue;
            }
            intervals[i-1][0] = Math.Min(intervals[i-1][0], intervals[i][0]);
            intervals[i-1][1] = Math.Max(intervals[i-1][1], intervals[i][1]);
            intervals.RemoveAt(i);
        }
        
        foreach (var x in intervals)
        {
            fresh += x[1] - x[0] + 1;
        }

        Console.WriteLine($"Part2: total count of fresh ingredients is {fresh}");
    }
}