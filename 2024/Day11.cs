using System.Text;

public class Day11
{
    private const int day = 11;
    
    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        var stones = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        
        for (int i = 0;i<25;++i) {
            if (i<10) Console.WriteLine(string.Join(" ", stones));
            var next = new List<long>();
            foreach(var stone in stones) {
                if (stone==0) {
                    next.Add(1);
                    continue;
                } 
                var s = stone.ToString();
                if ((s.Length % 2) == 0) {
                    next.Add(long.Parse(s.Substring(0,s.Length/2)));
                    next.Add(long.Parse(s.Substring(s.Length/2)));
                    continue;
                }
                next.Add(stone*2024);
            }
            stones = next;
        }

        Console.WriteLine($"Part1: total stones is {stones.Count}");
    }
    

    private static Dictionary<long, long?[]> memory = new Dictionary<long, long?[]>();

    private static long Blink(long stone, int turns) {
        if (turns==0) return 1;

        //check in memory
        if (memory.ContainsKey(stone)) {
            if (memory[stone][turns].HasValue) {
                return memory[stone][turns].Value;
            }
        } else {
            memory.Add(stone, new long?[80]);
        }

        //compute
        var res=0L;
        if (stone==0) {
            res = Blink(1, turns-1);
        } else {
            var digits=0;
            var tmp = stone;
            while(tmp>0) {
                tmp/=10;
                ++digits;
            }
            if ((digits % 2) == 0) {                
                var tens=1;
                for (var j=0;j<digits/2;j++) {
                    tens*=10;
                }
                res = Blink(stone%tens, turns-1) + Blink(stone/tens, turns-1);
            } else {
                res = Blink(stone*2024, turns-1);
            }
        }
        //save res in memory
        memory[stone][turns] = res;
        return res;
    }

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);
        var input = Tool.ReadAll(day, dataType);              
        var stones = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
        memory.Clear();
        var total = 0L;
        foreach(var stone in stones) {
            total += Blink(stone, 75);
        } 
        Console.WriteLine($"Part2: total stones is {total}");
    }

}
