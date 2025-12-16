public class Day06
{
    private const int day = 6;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var result = 0L;
        var init = true;
        var operations = new List<List<long>>();
        foreach (var line in input)
        {
            var data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (init)
            {
                operations = Enumerable.Range(0,data.Length).Select(_ => new List<long>()).ToList();
                init=false;
            }
            if (data[0][0] == '+' || data[0][0] == '*')
            {
                for (var i=0;i<data.Length;++i) 
                {
                    var ops = operations[i];
                    if (data[i][0] == '+')
                    {
                        result += ops.Aggregate(0L,(acc, curr) => acc+curr);
                    } else if (data[i][0] == '*')
                    {
                        result += ops.Aggregate(1L,(acc, curr) => acc*curr);
                    }
                }
                break;
            }
            for(var i=0;i<data.Length;++i)
            {
                operations[i].Add(long.Parse(data[i]));
            }
        }

        Console.WriteLine($"Part1: total sum of answers is {result}");
    }
    


    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var result = 0L;
        var height = input.Length;
        var width = input[0].Length;
        var ops = new List<long>();
        for (var col = width-1;col>=0;--col)
        {
            var val=0L;
            for (var row=0;row<height-1;++row)
            {
                if (input[row][col]!=' ')
                {
                    val = val*10 + input[row][col]-'0';
                }
            }
            if (val==0) continue;
            ops.Add(val);
            if (input[height-1][col] != ' ')
            {
                if (input[height-1][col] == '+')
                {
                    result += ops.Aggregate(0L,(acc, curr) => acc+curr);
                } else if (input[height-1][col] == '*')
                {
                    result += ops.Aggregate(1L,(acc, curr) => acc*curr);
                }
                ops.Clear();
            }
        }   

        Console.WriteLine($"Part2: total sum of answers is {result}");
    }
}
