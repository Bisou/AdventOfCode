using System.Text;

public class Day20
{
    private static long[] allPulses;    

    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day20-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day20-{dataType}.txt");   
        var modules = new Dictionary<string,Module>();
        foreach (var line in input)
        {
            var module = new Module(line);
            modules.Add(module.Name, module);
        }     
        var missing = new HashSet<string>();
        foreach(var module in modules.Values) {
            foreach(var next in module.Destinations) {
                if (!modules.ContainsKey(next)) {
                    missing.Add(next);
                }
            }
        }
        foreach(var absent in missing) {
            modules.Add(absent, new Module($"{absent} -> "));
        }
        foreach(var module in modules.Values) {
            foreach(var next in module.Destinations) {
                if (modules[next].Type=='&') {
                    modules[next].Incoming.Add(module.Name, 0);
                }
            }
        }
        allPulses = new long[2];
        var turn=0;
        while(turn<1000) {
            turn++;
            var localPulse = Action(modules);
            allPulses[0]+=localPulse[0];
            allPulses[1]+=localPulse[1];
        }

        


        Console.WriteLine($"Part1: answer is {allPulses[0]*allPulses[1]}");
    }

    private static long[] Action(Dictionary<string,Module> modules) {
        var pulses = new long[2]; //low, high
        ++pulses[0]; //button
        var todo = new Queue<(string Name, int Pulse, string Previous)>();
        todo.Enqueue(("broadcaster",0,"button"));
        while(todo.Any()) {
            var curr = todo.Dequeue();
            var module = modules[curr.Name];
            var output = module.Process(curr.Pulse, curr.Previous);
            foreach(var signal in output) {
                //Console.WriteLine($"{curr.Name} sends {signal.Pulse} to {signal.Name}");
                todo.Enqueue((signal.Name,signal.Pulse, curr.Name));
                pulses[signal.Pulse]++;
                if (signal.Name=="rx") {
                    ReceivedByRx[signal.Pulse]++;
                }
            }
        }
        return pulses;
    }

    class Module {
        public string Name;
        public int Pulse;
        public char Type;
        public bool FlipFlopOn=false;
        public string[] Destinations;        
        public Dictionary<string, int> Incoming = new Dictionary<string, int>();

        public List<(string Name, int Pulse)> Process(int pulse, string previous) {
            //Console.WriteLine($"{Name} receives {pulse} from {previous}");
            var res = new List<(string Name, int Pulse)>();
            Pulse=pulse;
            switch (Type) {
                case 'b': //broadcast                    
                    foreach(var next in Destinations) {
                        res.Add((next, pulse));
                    }
                    break;
                case '%':
                    if (pulse==0) { //low
                        if (FlipFlopOn) {
                            foreach(var next in Destinations) {
                                res.Add((next, 0));
                            }
                        } else {
                            foreach(var next in Destinations) {
                                res.Add((next, 1));
                            }
                        }                        
                        FlipFlopOn=!FlipFlopOn;
                    }                    
                    break;
                case '&':
                    Incoming[previous]=pulse;
                    Pulse = (Incoming.Values.Sum()==Incoming.Keys.Count())?0:1;                    
                    foreach(var next in Destinations) {
                        res.Add((next, Pulse));
                    }
                    break;
            }


            return res;
        }

        public Module(string line)
        {
            string[] data = line.Split(new [] {' ', '-', '>', ','}, StringSplitOptions.RemoveEmptyEntries);
            Type = data[0][0];
            if (data[0]=="broadcaster") {
                Name = data[0];
            } else {
                Name = data[0].Substring(1);
            }
            Destinations = data.Skip(1).ToArray();
        }
    }

    private static int[] ReceivedByRx = new int[2];

    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day20-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day20-{dataType}.txt"); 
        var modules = new Dictionary<string,Module>();
        ReceivedByRx[0]=0;
        ReceivedByRx[1]=0;
        foreach (var line in input)
        {
            var module = new Module(line);
            modules.Add(module.Name, module);
        }     
        var missing = new HashSet<string>();
        foreach(var module in modules.Values) {
            foreach(var next in module.Destinations) {
                if (!modules.ContainsKey(next)) {
                    missing.Add(next);
                }
            }
        }
        foreach(var absent in missing) {
            modules.Add(absent, new Module($"{absent} -> "));
        }
        foreach(var module in modules.Values) {
            foreach(var next in module.Destinations) {
                if (modules[next].Type=='&') {
                    modules[next].Incoming.Add(module.Name, 0);
                }
            }
        }
        allPulses = new long[2];
        var turn=0L;
        while(ReceivedByRx[0]==0) {
            ReceivedByRx[0]=0;
            ReceivedByRx[1]=0;
            turn++;
            Action(modules);
           // Console.WriteLine($"Turn {turn}: rx received {ReceivedByRx[0]} low pulses and {ReceivedByRx[1]} high pulses");
        }

        Console.WriteLine($"Part2: answer is {turn}");
    }
}
