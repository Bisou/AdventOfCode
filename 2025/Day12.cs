public class Day12
{
    private const int day = 12;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var res=0;
        const int pieceSize = 9;
        foreach (var line in input)
        {
            if (line.Contains('x'))
            {
                var data = line.Split(new [] {'x',':',' '}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var size =  data[0]*data[1];
                var pieces = data.Skip(2).Sum(x => x);
                if (size>=pieces*pieceSize) ++res;
            }
        }
        Console.WriteLine($"Part1: total correct configurations is {res}");
    }
    

}
