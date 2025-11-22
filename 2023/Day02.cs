public class Day02
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day02-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day02-{dataType}.txt");     
        var sum=0;
        var colors = new Dictionary<string, int>{{"red", 0},{"blue", 1},{"green",2}};
        var max = new []{12,14,13};
        
        foreach (var line in input)
        {
            var data = line.Replace("Game ","").Replace(": ",";").Replace(", ",",").Replace("; ",";").Split(';');
            var gameId=int.Parse(data[0]);
            var possible=true;
            foreach(var draw in data.Skip(1)) {
                var balls = draw.Split(',');
                foreach (var ball in balls) {
                    var details = ball.Split(' ');
                    if (int.Parse(details[0])>max[colors[details[1]]]) {
                        //impossible
                        possible=false;
                        break;
                    }                        
                }
            }
            if (possible) {
                sum += gameId;
            };
        }
        Console.WriteLine($"Part1: sum of possible games is {sum}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Console.WriteLine($"Day02-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day02-{dataType}.txt");     
        var sum=0;
        var colors = new Dictionary<string, int>{{"red", 0},{"blue", 1},{"green",2}};
        
        foreach (var line in input)
        {                
            var max = new []{0,0,0};
            var data = line.Replace("Game ","").Replace(": ",";").Replace(", ",",").Replace("; ",";").Split(';');
            var gameId=int.Parse(data[0]);                
            foreach(var draw in data.Skip(1)) {
                var balls = draw.Split(',');
                foreach (var ball in balls) {
                    var details = ball.Split(' ');
                    max[colors[details[1]]] = Math.Max(int.Parse(details[0]), max[colors[details[1]]]);     
                }
            }
            sum += max[0]*max[1]*max[2];                
        }
        Console.WriteLine($"Part2: sum of power of cube sets {sum}");
    }
}
