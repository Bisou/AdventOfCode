public static class Day01 {
    private const int day = 1;
    public static void SolvePart1(string dataType) {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType); 
        var code = 0;
        var position = 50;
        foreach (var line in input)
        {
            var sign = 1;
            if (line[0]=='L') sign=-1;
            var step = int.Parse(line.Substring(1));
            position = (position + sign * step) % 100;
            if (position==0) code++;
        }
        Console.WriteLine($"Part1: Secret code is {code}");      
    }
    
    public static void SolvePart2(string dataType) {
        Tool.LogStart(day,2,dataType);                
        var input = Tool.ReadAll(day, dataType); 
        var code = 0;
        var position = 50;
        foreach (var line in input)
        {
            var sign = (line[0]=='L')? -1 : 1; 
            var step = int.Parse(line.Substring(1));
            code += step/100;
            var newPosition = position + sign * (step%100);
            if (position !=0 && (newPosition <0 || newPosition >100)) code++;
            position = (newPosition+100)%100;
            if (position==0) code++;
        }
        Console.WriteLine($"Part2: Secret code is {code}");   
   }
    
    public static void SolvePart2_slow(string dataType) {
        Tool.LogStart(day,2,dataType);                
        var input = Tool.ReadAll(day, dataType); 
        var code = 0;
        var position = 50;
        foreach (var line in input)
        {
            var sign = (line[0]=='L')? -1 : 1; 
            var step = int.Parse(line.Substring(1));
            for (var i=0; i<step; ++i)
            {
                position = (position+sign+100)%100;
                if (position==0) code++;
            }
        }
        Console.WriteLine($"Part2: Secret code is {code}");   
   }
}