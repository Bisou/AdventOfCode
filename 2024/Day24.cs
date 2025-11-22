using System.Text;

public class Day24
{
    private const int day = 24;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);  
        var values = new Dictionary<string, int>();
        var relation=false;
        var wiring=new List<string[]>();
        foreach(var line in input) {
            if (string.IsNullOrEmpty(line)) {
                relation=true;
                continue;
            }
            if (relation) {
                wiring.Add(line.Split(new [] {'-', '>', ' '}, StringSplitOptions.RemoveEmptyEntries));
            } else {
                var data = line.Split(new [] {':', ' '}, StringSplitOptions.RemoveEmptyEntries);
                values.Add(data[0], int.Parse(data[1]));
            }
        }

        while(wiring.Any()) {
            foreach(var wire in wiring) {
                if(values.TryGetValue(wire[0], out var v1) && values.TryGetValue(wire[2], out var v2)) {
                    if (wire[1]=="AND") {
                        values.Add(wire[3], v1 & v2);
                    } else if (wire[1]=="OR") {
                        values.Add(wire[3], v1 | v2);
                    } else if (wire[1]=="XOR") {
                        values.Add(wire[3], v1 ^ v2);
                    } 
                    wiring.Remove(wire);
                    break;
                }
            }
        }


        var total=0L; 
        var z=0;
        var stack = new Stack<int>();
        while(values.TryGetValue($"z{z:00}", out var val)) {
            stack.Push(val);            
            ++z;
        }
        while(stack.Any()) {
            total = (total*2)+stack.Pop();
        }


        Console.WriteLine($"Part1: result is {total}");
    }

    public static void SolvePart2(string dataType)
    {
        Tool.LogStart(day,2,dataType);        
        var input = Tool.ReadAll(day, dataType);  
        var values = new Dictionary<string, int>();
        var gates = new List<Gate>();

        int i = 0;
        while (input[i].Contains(':'))
        {
            var data = input[i].Split(new [] {':', ' '}, StringSplitOptions.RemoveEmptyEntries);
            values.Add(data[0], int.Parse(data[1]));
            i++;
        }
        i++; // Sauter la ligne vide
        while (i < input.Length)
        {
            var data = input[i].Split(new [] {'-', '>', ' '}, StringSplitOptions.RemoveEmptyEntries);
            gates.Add(new Gate { Input1 = data[0], Type = data[1], Input2 = data[2], Output = data[3] });
            i++;
        }


        var toSwap=new List<string>();
        //reset values
        foreach(var key in values.Keys) values[key]=0;
        
        foreach(var key in values.Keys) {
            values[key]=1;
            if (!IsOk(values, gates)) {
                toSwap.Add(key);
            }

            values[key]=0;
        }

        var allOutputs = gates.Select(g => g.Output).ToList();
        var tests = GenerateTests(values.Keys.Where(k => k.StartsWith("x")).Count(), values.Keys.Where(k => k.StartsWith("y")).Count());
        int swapCount =dataType=="Full" ? 8 : 2; // 4 paires * 2 = 8
        var combinations = GetCombinations(allOutputs, swapCount).ToList();
        foreach (var swaps in combinations) 
        {
            PerformSwap(swaps, gates);
            var ok=true;
            foreach(var test in tests) {
                var testValues = SetValues(test, values);
                if (!IsOk(testValues, gates)) {
                    ok=false;
                    break;
                }                
            }
            if (ok) {
                var sortedSwaps = swaps.OrderBy(s => s).ToList();
                Console.WriteLine($"Part2: result is {string.Join(",", sortedSwaps)}"); 
                return;
            }
            PerformSwap(swaps, gates);
        }

        Console.WriteLine("No solution found.");
    }

    private static void PerformSwap(IEnumerable<string> swaps, List<Gate> gates) {
        string key1="";
        foreach (var key in swaps) {
            var gate1 = gates.FirstOrDefault(g => g.Output==key1);
            if (gate1==null) {
                key1=key;
            } else {
                var gate2 = gates.FirstOrDefault(g => g.Output == key);
                gate2.Output = key1;
                gate1.Output = key;
                key1="";
            }
        }
    }

    private static Dictionary<string, int> SetValues((int x, int y, int sum) test, Dictionary<string,int> initialValues) {
        var values = new Dictionary<string,int>();


        return values;
    }

    private static int GetValue(Dictionary<string, int> values, char letter) {
        var letterValues = values.Where(kvp => kvp.Key[0]==letter).OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value).Reverse().ToArray();
        var result = Convert.ToInt32(string.Join("", letterValues), 2);
        return result;
    }

    private static bool IsOk(Dictionary<string, int> initialValues, List<Gate> gates)
    {
        var values = initialValues.ToDictionary();

        var wiring = gates.ToList();
        while(wiring.Any()) {
            foreach(var wire in wiring) {
                if(values.TryGetValue(wire.Input1, out var v1) && values.TryGetValue(wire.Input2, out var v2)) {
                    if (wire.Type=="AND") {
                        values.Add(wire.Output, v1 & v2);
                    } else if (wire.Type=="OR") {
                        values.Add(wire.Output, v1 | v2);
                    } else if (wire.Type=="XOR") {
                        values.Add(wire.Output, v1 ^ v2);
                    } 
                    wiring.Remove(wire);
                    break;
                }
            }
        }

        var x = GetValue(values, 'x');
        var y = GetValue(values, 'y');
        var z = GetValue(values, 'z');

        return x + y == z;    
    }

    private static List<(int x, int y, int sum)> GenerateTests(int xBits, int yBits)
    {
        var tests = new List<(int x, int y, int sum)>();
        for (int x = 0; x < (1 << xBits); x++)
        {
            for (int y = 0; y < (1 << yBits); y++)
            {
                tests.Add((x, y, x + y));
            }
        }
        return tests;
    }

    public static IEnumerable<IEnumerable<T>> GetCombinations<T>(List<T> list, int length)
    {
        if (length == 0)
        {
            return new[] { Enumerable.Empty<T>() };
        }

        return list.SelectMany((item, i) =>
            GetCombinations(list.Skip(i + 1).ToList(), length - 1).Select(subCombination =>
                new[] { item }.Concat(subCombination)));
    }

    
    public class Gate
    {
        public string Type { get; set; }
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Output { get; set; }
    }

}
  