public class Day07
{
    private const int day = 7;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);      
        var map = Tool.ReadAll(day, dataType); 
        var result=0;
        var height = map.Length;
        var width = map[0].Length;
        var beams = new HashSet<int>();
        for (var col=0;col<width;++col)
        {
            if (map[0][col]=='S')
            {
                beams.Add(col);
                break;
            }
        }
        foreach(var line in map.Skip(1))
        {
            var newBeams = new HashSet<int>();
            foreach(var beam in beams)
            {
                if (line[beam]=='^')
                {
                    newBeams.Add(beam-1);
                    newBeams.Add(beam+1);
                    result++;
                } else
                {
                    newBeams.Add(beam);
                }
            }


            beams = newBeams;
        }
        Console.WriteLine($"Part1: total split of beam is {result}");
    }

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);      
        var map = Tool.ReadAll(day, dataType); 
        var height = map.Length;
        var width = map[0].Length;
        var beams = new Dictionary<int,long>();
        for (var col=0;col<width;++col)
        {
            if (map[0][col]=='S')
            {
                beams.Add(col, 1);
                break;
            }
        }
        foreach(var line in map.Skip(1))
        {
            var newBeams = new Dictionary<int, long>();
            foreach(var beam in beams)
            {
                if (line[beam.Key]=='^')
                {
                    newBeams.TryGetValue(beam.Key-1, out long prev);
                    newBeams[beam.Key-1] = prev + beam.Value;
                    newBeams.TryGetValue(beam.Key+1, out prev);
                    newBeams[beam.Key+1] = prev + beam.Value;
                } else
                {
                    newBeams.TryGetValue(beam.Key, out long prev);
                    newBeams[beam.Key] = prev + beam.Value;
                }
            }


            beams = newBeams;
        }

        Console.WriteLine($"Part2: total split of beams is {beams.Values.Sum()}");
    }
}
