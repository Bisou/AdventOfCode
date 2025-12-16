public static class Day02 {
    private const int day = 2;
    public static void SolvePart1(string dataType) {
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var result = 0L;
        var intervals = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var interval in intervals)
        {
            var limits = interval.Split('-', StringSplitOptions.RemoveEmptyEntries);
            var start = long.Parse(limits[0]);
            var end = long.Parse(limits[1]);
            while(start<=end)
            {
                var number = start.ToString();
                var sequence = number.Substring(0, number.Length/2);
                if (sequence == number.Substring(number.Length/2))
                {
                    result += start;
                }
                ++start;
            }
        }
        Console.WriteLine($"Part1: total sum of IDs is {result}");    
    }
    
    public static void SolvePart2(string dataType) {
        Tool.LogStart(day,2,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var result = 0L;
        var intervals = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var interval in intervals)
        {
            var limits = interval.Split('-', StringSplitOptions.RemoveEmptyEntries);
            var start = long.Parse(limits[0]);
            var end = long.Parse(limits[1]);
            while(start<=end)
            {
                var number = start.ToString();
                var ok = false;
                for (var length = 1; length<=number.Length/2; ++length)
                {
                    if (number.Length % length !=0) continue;
                    ok = true;
                    var sequence = number[0..length];
                    var pos=length;
                    while (pos<number.Length)
                    {
                        if (sequence != number[pos..(pos+length)])
                        {
                            ok = false;
                            break;
                        }
                        pos += length;
                    }
                    if (ok) break;
                }
                if (ok) {
                    result += start;
                }         
                ++start;
            }
        }
        Console.WriteLine($"Part2: total sum of IDs is {result}");  
   }
}