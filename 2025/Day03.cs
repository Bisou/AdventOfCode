public static class Day03 {
    private const int day = 3;
    public static void SolvePart1(string dataType) {
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var joltage = 0;
        foreach(var line in input)
        {
            var max=0;
            var digit1=0;
            var digit2=0;
            for(var i=0;i<line.Length;++i)
            {
                var digit=line[i]-'0';
                if (digit>digit1 && i<line.Length-1)
                {
                    digit1=digit;
                    digit2=0;
                    continue;
                } else if (digit>=digit2)
                {
                    digit2=digit;
                    max = Math.Max(max, digit1*10+digit2);
                }
            }
            joltage+=max;
        }
        Console.WriteLine($"Part1: max joltage is {joltage}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var joltage = 0L;
        foreach(var line in input)
        {
            var digits=Enumerable.Range(0,12).ToArray();//index of the various digits of my best joltage
            for(var i=0;i<line.Length;++i)
            {
                var digit=line[i];
                for (var j=0;j<12;++j)
                {
                    if (digits[j]<i && digit>line[digits[j]] && i<=line.Length-12+j)
                    {
                        for (var k=0; j+k<12;++k)
                        {
                            digits[j+k] = i+k;
                        }
                        break;
                    }
                }
            }
            
            var value = 0L;
            foreach(var d in digits)
            {
                value = value*10 + (line[d]-'0');
            }
            joltage+=value;
        }
        Console.WriteLine($"Part2: max joltage is {joltage}");
    }
}