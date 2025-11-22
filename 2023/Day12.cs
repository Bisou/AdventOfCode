using System.Text;

public class Day12
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day12-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day12-{dataType}.txt");   
        var sum=0L;
        foreach(var line in input) {
            sum+=PossibleArrangements(line);
        }
        Console.WriteLine($"Part1: total sum of arrangements is {sum}");
    }

    private static long PossibleArrangements(string line) {
        var data = line.Split(' ');
        var s = data[0];
        var springs = data[1].Split(',').Select(int.Parse).ToArray();
        return BackTracking(s,springs,0, 0, 0);
    }

    private static long BackTracking(string s, int[] springs, int stringIndex, int springIndex, int springLength) {                
        if (stringIndex==s.Length) {
            //if end of string
            if (springIndex<springs.Length-1) {
                //not enough springs
                return 0;
            } else if (springIndex==springs.Length) {
                //correct number of springs
                return 1;
            } else if (springIndex==springs.Length-1) {
                //missing last spring
                if (springLength==springs[springIndex]) {
                    //last spring ends at end of line
                    return 1;            
                } else {
                    return 0;
                }
            } else if (springIndex>springs.Length) {
                //too many springs... possible?
                return 0;
            } 
        }
        if (s[stringIndex]=='#') {
            if (springIndex<springs.Length && springLength<springs[springIndex]) {
                //extend current spring
                return BackTracking(s, springs,stringIndex+1, springIndex, springLength+1);
            } else {
                return 0;
            }
        }
        if (s[stringIndex]=='.') {
            if (springLength>0) {
                //end of spring
                if (springIndex>=springs.Length) {
                    //too many springs
                    return 0;
                } else if (springLength==springs[springIndex]) {
                    //correct end of current spring
                    return BackTracking(s, springs, stringIndex+1, springIndex+1,0);
                } else {
                    return 0;
                }
            } else {
                return BackTracking(s, springs,stringIndex+1, springIndex, springLength);
            }
        }
        if (s[stringIndex]=='?') {
            var res=0L;
            //'#'
            if (springIndex<springs.Length && springLength<springs[springIndex]) {
                res += BackTracking(s, springs,stringIndex+1, springIndex, springLength+1);
            }
            //'.'
            if (springLength>0) {
                //end of spring                
                if (springIndex>=springs.Length) {
                    res += 0;
                } if (springLength==springs[springIndex]) {
                    //correct end
                    res += BackTracking(s, springs, stringIndex+1, springIndex+1,0);
                } 
            } else {
                res += BackTracking(s, springs,stringIndex+1, springIndex, springLength);
            }
            return res;
        }
        return -1;
    }
    
    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day12-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day12-{dataType}.txt");   
        var sum=0L;
        foreach(var line in input) {
            sum+=PossibleArrangements2(line);
        }
        Console.WriteLine($"Part2: total sum of arrangements is {sum}");
    }
    
    private static long PossibleArrangements2(string line) {
        var data = line.Split(' ');
        var sb = new StringBuilder();
        sb.Append(data[0]);
        for (var i=0;i<4;++i) {
            sb.Append('?');
            sb.Append(data[0]);
        }
        var s = sb.ToString();
        var springsTmp = data[1].Split(',').Select(int.Parse).ToArray();
        var springs = new int[springsTmp.Length*5];
        for (var i=0;i<springs.Length;++i) {
            springs[i]=springsTmp[i%springsTmp.Length];
        }
        return BackTracking(s,springs,0, 0, 0);
    }
}
