public class Day02
{
    private const int day = 2;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);     
        var safe=0;
        foreach (var line in input)
        {
            var report = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var ok=true;
            var sign=report[0]-report[1];
            for (var i=0;i<report.Count-1;++i) {
                if ((report[i]-report[i+1])*sign<0) {
                    ok=false;
                    break;
                }
                var diff = Math.Abs(report[i]-report[i+1]);
                if (diff<1 || 3<diff) {
                    ok=false;
                    break;
                }
            }
            if (ok) safe++;
        }
        Console.WriteLine($"Part1: total number of safe reports is {safe}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);     
        var safe=0;
        foreach (var line in input)
        {
            var report = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            for (var skip=-1;skip<report.Count;++skip) {
                var newReport = report.ToList();
                if (skip>=0) newReport.RemoveAt(skip);
                var ok=true;
                var sign=newReport[0]-newReport[1];
                for (var i=0;i<newReport.Count-1;++i) {
                    if ((newReport[i]-newReport[i+1])*sign<0) {
                        ok=false;
                        break;
                    }
                    var diff = Math.Abs(newReport[i]-newReport[i+1]);
                    if (diff<1 || 3<diff) {
                        ok=false;
                        break;
                    }
                }
                if (ok) {
                    safe++;
                    break;
                }
            }
        }
        Console.WriteLine($"Part2: total number of safe reports is {safe}");
    }

    private static int GetIndex(int i, int forbidden) {
        if (forbidden < 0) return i;
        if (i>=forbidden) return i+1;
        return i;
    }
}
