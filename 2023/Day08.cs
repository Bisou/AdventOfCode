public class Day08
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day08-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day08-{dataType}.txt");   
        var ways = input[0];
        var graph = new Dictionary<string, string[]>();
        foreach (var line in input.Skip(2)) {
            var data = line.Split(new []{' ','=','(',',',')'}, StringSplitOptions.RemoveEmptyEntries);
            graph.Add(data[0], new []{data[1],data[2]});
        }
        var steps=0;
        var curr="AAA";
        while(curr!="ZZZ") {
            if (ways[steps%ways.Length]=='L') 
                curr=graph[curr][0];
            else
                curr=graph[curr][1];
            steps++;
        }
        Console.WriteLine($"Part1: we reach the end in {steps} steps");
    }
    public static void SolvePart2(string dataType)
    {      
        Console.WriteLine($"Day08-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day08-{dataType}.txt");   
        var ways = input[0];
        var currents=new List<string>();
        var graph = new Dictionary<string, string[]>();
        foreach (var line in input.Skip(2)) {
            var data = line.Split(new []{' ','=','(',',',')'}, StringSplitOptions.RemoveEmptyEntries);
            graph.Add(data[0], new []{data[1],data[2]});
            if (data[0][2]=='A') currents.Add(data[0]);
        }
        var steps=new List<long>();
        for(var i=0;i<currents.Count;++i) 
        {
            var step=0;
            var curr=currents[i];
            while(curr[2]!='Z') {
                if (ways[step%ways.Length]=='L') 
                    curr=graph[curr][0];
                else
                    curr=graph[curr][1];
                step++;
            }
            steps.Add(step);
        }

        var lcm=steps[0];
        for (var i=1;i<steps.Count;++i) {
            lcm = lcm*steps[i]/GCD(lcm, steps[i]);
        }
        Console.WriteLine($"Part2: we reach all the ends in {lcm} steps");
    }

    private static long GCD(long a, long b) {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }
}
