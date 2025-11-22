public class Day07
{
    private const int day = 7;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        var total=0L;
        foreach(var line in input) {
            var data = line.Split(new[]{':',' '},StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            if (Possible(data, data[1], 2)) total+=data[0];
        }
        Console.WriteLine($"Part1: total calibration result is {total}");
    }
    
    private static bool Possible(long[] data, long current, int index, bool concatAllowed = false) {        
        if (index>=data.Length) return current==data[0];
        //add
        current+=data[index];
        if (Possible(data, current, index+1, concatAllowed)) return true;
        current-=data[index];
        //mult
        current*=data[index];
        if (Possible(data, current, index+1, concatAllowed)) return true;
        current/=data[index];
        //concat
        if (concatAllowed) {
            var concatenation=current;
            foreach(var digit in data[index].ToString()) {
                concatenation*=10;
                concatenation+=digit-'0';
            }
            if (Possible(data, concatenation, index+1, concatAllowed)) return true;
        }

        return false;
    }

    


    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        var total=0L;
        foreach(var line in input) {
            var data = line.Split(new[]{':',' '},StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            if (Possible(data, data[1], 2, true)) {
                total+=data[0];
            }
        }
        Console.WriteLine($"Part2: total calibration result is {total}");
    }
}
