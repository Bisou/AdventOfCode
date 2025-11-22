public static class Day05
{
    private static int index;
    private static string[] input;

    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day05-Part1-{dataType}");
        input = File.ReadAllLines(@$".\..\..\..\Inputs\Day05-{dataType}.txt");              
        index=0;
        var seeds = input[index++].Split(' ').Skip(1).Select(long.Parse).ToList();
        var soil = ProcessMapping(seeds);
        var fertilizer = ProcessMapping(soil);
        var water = ProcessMapping(fertilizer);
        var light = ProcessMapping(water);
        var temperature = ProcessMapping(light);
        var humidity = ProcessMapping(temperature);
        var location = ProcessMapping(humidity);
        Console.WriteLine($"Part1: best location is {location.Min(x => x)}");
    }
    
    private static List<long> ProcessMapping(List<long> source) {
        index+=2;
        var destination = new List<long>();
        while(!string.IsNullOrEmpty(input[index])) {            
            var data = input[index].Split(' ').Select(long.Parse).ToArray();
            var startSource=data[1];
            var endSource=startSource+data[2]-1;
            var shift=data[0]-startSource;

            for(var i=source.Count-1;i>=0;--i) {
                if (source[i]>=startSource && source[i]<= endSource) {
                    destination.Add(source[i]+shift);
                    source.RemoveAt(i);
                }
            }
            index++;
            if (index==input.Length) break;
        }
        destination.AddRange(source);

        return destination;
    }

    public static void SolvePart2(string dataType)
    {      
        Console.WriteLine($"Day05-Part2-{dataType}");
        input = File.ReadAllLines(@$".\..\..\..\Inputs\Day05-{dataType}.txt");              
        index=0;
        var seedData = input[index++].Split(' ').Skip(1).Select(long.Parse).ToList();
        var seeds = new List<(long Start, long End)>();
        for (var i=0;i<seedData.Count; i+=2) {
            seeds.Add((seedData[i], seedData[i]+seedData[i+1]-1));
        }
        var soil = ProcessMapping2(seeds);
        var fertilizer = ProcessMapping2(soil);
        var water = ProcessMapping2(fertilizer);
        var light = ProcessMapping2(water);
        var temperature = ProcessMapping2(light);
        var humidity = ProcessMapping2(temperature);
        var location = ProcessMapping2(humidity);
        Console.WriteLine($"Part2: best location is {location.Min(x => x.Start)}");
    }
    
    private static List<(long Start, long End)> ProcessMapping2(List<(long Start, long End)> source) {
        index+=2;
        var destination = new List<(long Start, long End)>();
        while(!string.IsNullOrEmpty(input[index])) {            
            var data = input[index].Split(' ').Select(long.Parse).ToArray();
            var startSource=data[1];
            var endSource=startSource+data[2]-1;
            var shift=data[0]-startSource;
            var newSource = new List<(long Start, long End)>();
            for(var i=0;i<source.Count;i++) {
                if (source[i].End<startSource || source[i].Start>endSource) {
                    newSource.Add(source[i]);
                } else if (source[i].Start>=startSource && source[i].End<=endSource) {
                    destination.Add((source[i].Start+shift,source[i].End+shift));
                } else if (source[i].Start>=startSource && source[i].End>endSource) {
                    destination.Add((source[i].Start+shift,endSource+shift));
                    newSource.Add((endSource+1,source[i].End));
                } else if (source[i].Start<startSource && source[i].End<=endSource) {
                    destination.Add((startSource+shift,source[i].End+shift));
                    newSource.Add((source[i].Start,startSource-1));
                } else if (source[i].Start<startSource && source[i].End>endSource) {
                    destination.Add((startSource+shift,endSource+shift));
                    newSource.Add((source[i].Start,startSource-1));
                    newSource.Add((endSource+1,source[i].End));
                }
            }
            source=newSource;
            index++;
            if (index==input.Length) break;
        }
        destination.AddRange(source);
        return destination;
    }
}

