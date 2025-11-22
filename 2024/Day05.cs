public class Day05
{
    private const int day = 5;
    private static Dictionary<int,List<int>> mustBeBefore;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        var sum=0;
        var phase1=true;
        mustBeBefore = new Dictionary<int,List<int>>();
        foreach(var line in input) {
            if (string.IsNullOrWhiteSpace(line)) {
                phase1=false;
            } else if (phase1) {
                var data = line.Split('|').Select(int.Parse).ToList();
                if (!mustBeBefore.ContainsKey(data[0])) {
                    mustBeBefore.Add(data[0], new List<int>());
                }
                mustBeBefore[data[0]].Add(data[1]);
            }else {
                var data = line.Split(',').Select(int.Parse).ToArray();
                if (IsOk(data)) {
                    sum+=data[data.Length/2];
                }
            }
        }

        Console.WriteLine($"Part1: total sum is {sum}");
    }
    
    private static bool IsOk(int[] data) {        
        var seen=new HashSet<int>();
        foreach(var num in data) {
            if (mustBeBefore.ContainsKey(num)) {
                foreach(var friend in mustBeBefore[num]) {
                    if (seen.Contains(friend)) {
                        return false;
                    }
                }
            }
            seen.Add(num);
        }
        return true;
    }

    private static void Reorder(int[] data) {
        var ok=false;
        while(!ok) {
            ok=true;
            var seen=new HashSet<int>();
            for(var i=0;i<data.Length;++i) {
                var num=data[i];
                if (mustBeBefore.ContainsKey(num)) {
                    foreach(var friend in mustBeBefore[num]) {
                        if (seen.Contains(friend)) {
                            ok=false;
                            //swap them
                            var j=0; 
                            while(data[j]!=friend) ++j;
                            data[j]=num;
                            data[i]=friend;
                            break;
                        }
                    }
                }
                if (!ok) break;
                seen.Add(num);
            }
        }
    }


    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        var sum=0;
        var phase1=true;
        mustBeBefore = new Dictionary<int,List<int>>();
        foreach(var line in input) {
            if (string.IsNullOrWhiteSpace(line)) {
                phase1=false;
            } else if (phase1) {
                var data = line.Split('|').Select(int.Parse).ToList();
                if (!mustBeBefore.ContainsKey(data[0])) {
                    mustBeBefore.Add(data[0], new List<int>());
                }
                mustBeBefore[data[0]].Add(data[1]);
            }else {
                var data = line.Split(',').Select(int.Parse).ToArray();
                if (!IsOk(data)) {
                    Reorder(data);
                    sum+=data[data.Length/2];
                }
            }
        }

        Console.WriteLine($"Part2: total sum is {sum}");
    }
}
