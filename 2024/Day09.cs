using System.Text;

public class Day09
{
    private const int day = 9;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        var total=0L;
        var spaces = new List<(int Start, int End)>();
        var id=0;
        var data=true;
        var position=0;
        var full = new List<(int Start, int End, int Id)>();
        foreach (var c in input[0]) {
            if (c!='0') {
                if (data) {
                    full.Add((position, position+c-'0', id++));
                } else {
                    spaces.Add((position, position+c-'0'));
                }
            }
            position+=c-'0';
            data=!data;
        }

        var log = new StringBuilder();
        while (full.Any()) {
            if (full[0].Start < spaces[0].Start) {
                //keep full
                for (var pos = full[0].Start; pos<full[0].End; ++pos) {
                    total+=pos*full[0].Id;
                    log.Append(full[0].Id);
                }
                full.RemoveAt(0);
            } else {
                //move the last one forward
                var space = spaces[0];
                var size=space.End - space.Start;
                var last = full[^1];
                var lastSize=last.End-last.Start;
                var minSize=Math.Min(size, lastSize);
                for (var pos = space.Start; pos<space.Start+minSize; ++pos) {
                    total+=pos*last.Id;
                    log.Append(last.Id);
                }
                if (minSize==lastSize) {
                    full.RemoveAt(full.Count-1);
                } else if (minSize<lastSize) {
                    full[full.Count-1] = (last.Start, last.End-minSize, last.Id);
                }
                if (minSize==size) {
                    spaces.RemoveAt(0);
                } else {
                    spaces[0] = (space.Start+minSize, space.End);
                }
            }
        }
        Console.WriteLine($"Part1: total checksum is {total}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);         
        var total=0L;
        var spaces = new List<(long Start, long End)>();
        var id=0;
        var data=true;
        var position=0;
        var full = new List<(long Start, long End, long Id)>();
        foreach (var c in input[0]) {
            if (c!='0') {
                if (data) {
                    full.Add((position, position+c-'0', id++));
                } else {
                    spaces.Add((position, position+c-'0'));
                }
            }
            position+=c-'0';
            data=!data;
        }
        
        //var sb = new StringBuilder(new String('.', Math.Max(spaces[^1].End, full[^1].End)));    
        //var log = new StringBuilder();
        while (full.Any()) {
            //move the last one forward
            var last = full[^1];
            var lastSize=last.End-last.Start;
            for (var i=0;i<spaces.Count;++i) {
                var space = spaces[i];
                if (space.Start >= last.Start) break;
                var size=space.End - space.Start;
                if (size>=lastSize) {
                    last=(space.Start, space.Start+lastSize, last.Id);
                    if (size==lastSize) {
                        spaces.RemoveAt(i);
                    } else {
                        spaces[i] = (space.Start+lastSize, space.End);
                    }
                    break;
                }
            }
            
            for (var pos = last.Start; pos<last.End; ++pos) {
                total+=pos*last.Id;
                //sb[pos]=(char)('0'+last.Id);
            }
            full.RemoveAt(full.Count-1);         
        }
        // Console.WriteLine(sb.ToString());
        Console.WriteLine($"Part2: total checksum is {total}");
    }

}
