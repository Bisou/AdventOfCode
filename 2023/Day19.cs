using System.Text;

public class Day19
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day19-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day19-{dataType}.txt");   
        var workflows = new Dictionary<string, List<(char Property, char Condition, int Threshold, string Destination)>>();        
        var res=0L;
        foreach (var line in input) {
            if (line.Length==0) continue;
            if (line[0]=='{') {
                //process workflows
                var data = line.Split(new []{'{','}',',','='}, StringSplitOptions.RemoveEmptyEntries);
                var part = new Dictionary<char,int>();
                for (var i=0;i<4;++i) {
                    part.Add(data[2*i][0], int.Parse(data[2*i+1]));
                }
                var name = "in";
                while (true) {
                    if (name=="A")  {
                        res += part.Values.Sum();
                        break;
                    } else if (name=="R") {
                        break;
                    }
                    var wfl = workflows[name];
                    foreach(var step in wfl) {
                        if (step.Property=='_') {
                            name = step.Destination;
                            break;
                        } 
                        if (step.Condition=='<') {
                            if (part[step.Property]<step.Threshold) {
                                name = step.Destination;
                                break;
                            }
                        } else if (step.Condition=='>') {
                            if (part[step.Property]>step.Threshold) {
                                name = step.Destination;
                                break;
                            }
                        }
                    }
                }
            } else {
                var data = line.Split(new [] {'{','}',','}, StringSplitOptions.RemoveEmptyEntries);
                var name = data[0];
                workflows.Add(name, new List<(char Property, char Condition, int Threshold, string Destination)>());
                foreach(var condition in data.Skip(1)) {
                    var details = condition.Split(':');
                    if (details.Length==1) {
                        //simple goto
                        workflows[name].Add(('_','_',0,details[0]));
                    } else {
                        workflows[name].Add((details[0][0],details[0][1],int.Parse(details[0].Substring(2)),details[1]));
                    }
                }
            }
        }

        Console.WriteLine($"Part1: answer is {res}");
    }

    private static long combinations=0L;
    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day19-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day19-{dataType}.txt"); 
        var workflows = new Dictionary<string, List<(char Property, char Condition, int Threshold, string Destination)>>();                
        foreach (var line in input) {
            if (line.Length==0) break;
            if (line[0]!='{') {
                var data = line.Split(new [] {'{','}',','}, StringSplitOptions.RemoveEmptyEntries);
                var name = data[0];
                workflows.Add(name, new List<(char Property, char Condition, int Threshold, string Destination)>());
                foreach(var condition in data.Skip(1)) {
                    var details = condition.Split(':');
                    if (details.Length==1) {
                        //simple goto
                        workflows[name].Add(('_','_',0,details[0]));
                    } else {
                        workflows[name].Add((details[0][0],details[0][1],int.Parse(details[0].Substring(2)),details[1]));
                    }
                }
            }
        }

        combinations=0;
        var part = new Dictionary<char,(long Min, long Max)>();
        part['x']=(1,4000);
        part['m']=(1,4000);
        part['a']=(1,4000);
        part['s']=(1,4000);
        Dfs("in", part, workflows);

        Console.WriteLine($"Part2: answer is {combinations}");
    }

    private static void Dfs(string name, Dictionary<char,(long Min, long Max)> part, Dictionary<string, List<(char Property, char Condition, int Threshold, string Destination)>> workflows) {        
        if (name=="A")  {
            var res=1L;
            foreach(var range in part.Values) {
                res*=(range.Max-range.Min+1);
            }
            combinations+=res;
            return;
        }
        if (name=="R") {
            return;
        }
        var wfl = workflows[name];
        foreach(var step in wfl) {
            if (step.Property=='_') {
                Dfs(step.Destination, part, workflows);
                return;
            } 
            if (step.Condition=='<') {
                if (part[step.Property].Max<step.Threshold) {                    
                    Dfs(step.Destination, part, workflows);
                    return;
                }
                if (step.Threshold < part[step.Property].Min) {                    
                    continue;
                }
                var leftPart = new Dictionary<char,(long Min, long Max)>();
                foreach(var key in part.Keys) {
                    leftPart.Add(key, (part[key].Min, part[key].Max));
                }
                leftPart[step.Property] = (leftPart[step.Property].Min,step.Threshold-1);
                part[step.Property] = (step.Threshold, part[step.Property].Max);
                Dfs(step.Destination,leftPart,workflows);
                continue; //part continues the workflow
            } else if (step.Condition=='>') {
                if (step.Threshold < part[step.Property].Min) {                    
                    Dfs(step.Destination, part, workflows);
                    return;
                }
                if (part[step.Property].Max < step.Threshold) {                    
                    continue;
                }
                var rightPart = new Dictionary<char,(long Min, long Max)>();
                foreach(var key in part.Keys) {
                    rightPart.Add(key, (part[key].Min, part[key].Max));
                }
                part[step.Property] = (part[step.Property].Min,step.Threshold);
                rightPart[step.Property] = (step.Threshold+1, rightPart[step.Property].Max);
                Dfs(step.Destination,rightPart,workflows);
                continue; //part continues the workflow
            }
        }
    }
}
