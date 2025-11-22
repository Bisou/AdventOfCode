using System.Diagnostics.Contracts;

public class Day06
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day06-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day06-{dataType}.txt");     
        var times = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToArray();
        var distances = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(long.Parse).ToArray();
        var score=1L;
        
        for (var i=0;i<times.Length;++i) {            
            var win=0;
            var speed=0;
            for (var button=1;button<times[i];++button) {
                var distance = button*(times[i]-button);
                if (distance>distances[i]) win++;
            }
            score*=win;
        }
        Console.WriteLine($"Part1: result is {score}");
    }
    

    public static void SolvePart2(string dataType)
    {      
        Console.WriteLine($"Day06-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day06-{dataType}.txt");     
        var time=0L;
        foreach(var c in input[0]) {
            if (Char.IsDigit(c)) {
                time = 10*time + c - '0';
            }
        }
        var goal=0L;
        foreach(var c in input[1]) {
            if (Char.IsDigit(c)) {
                goal = 10*goal + c - '0';
            }
        }
                  
        var left=1L;
        var right=time/2;
        while(left<right) {
            var mid = left + (right-left)/2;
            var distance = mid*(time-mid);
            if (distance>goal) {
                right=mid;
            } else {
                left=mid+1;
            }
            if (left+1==right) {
                if (left*(time-left)>goal) right=left;
                else left=right;
            }
        }
        var firstWin=left;
        Console.WriteLine($"First win is with button {firstWin}");
        
        left=time/2;
        right=time;
        while(left<right) {
            var mid = left + (right-left)/2;
            var distance = mid*(time-mid);
            if (distance>goal) {
                left=mid;
            } else {
                right=mid-1;
            }
            if (left+1==right) {
                if (right*(time-right)>goal) left=right;
                else right=left;
            }
        }
        var lastWin=left;
        Console.WriteLine($"Last win is with button {lastWin}");
        Console.WriteLine($"Part2: we can win {lastWin-firstWin+1} times");
    }
}
