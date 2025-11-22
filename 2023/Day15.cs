using System.Text;

public class Day15
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day15-Part1-{dataType}");
        Console.WriteLine($"Hash of 'HASH' is {GetHash("HASH")}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day15-{dataType}.txt");   
        var sum=0L;
        foreach(var step in input[0].Split(',')) {
            var hash = GetHash(step);
            sum+=hash;
        }
        Console.WriteLine($"Part1: total sum of hash is {sum}");
    }

    public static int GetHash(string s) {
        var val=0;
        foreach(var c in s) {
            val += (int)c;
            val *= 17;
            val = val % 256;
        }
        return val;
    }

    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day15-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day15-{dataType}.txt");   
        var boxes = Enumerable.Range(0,256).Select(_ => new List<(string Label, int Focal)>()).ToArray();

        foreach(var step in input[0].Split(',')) {
            var i = step.IndexOf("=");
            if (i<0) i=step.IndexOf("-");
            string name = step.Substring(0,i);
            var box = GetHash(name);
            var pos=0;
            var found=false;
            while(!found && pos<boxes[box].Count) {
                if (boxes[box][pos].Label==name) {
                    found=true;
                    break;
                } else {
                    ++pos;
                }
            }
            if (found) {
                if (step[i]=='=') {                    
                    var focal = int.Parse(step.Substring(i+1));
                    boxes[box][pos]=(name,focal);
                } else {
                    //-
                    boxes[box].RemoveAt(pos);
                }
            } else {
                if (step[i]=='=') {
                    var focal = int.Parse(step.Substring(i+1));
                    boxes[box].Add((name,focal));
                } else {
                    //-
                    //nothing to do
                }
            }
        }



        var power=0L;        
        for (var box=0;box<256;++box) {
            for (var slot=0;slot<boxes[box].Count;++slot) {
                power += (box+1)*(slot+1)*boxes[box][slot].Focal;
            }
        }
        Console.WriteLine($"Part2: total focusing power is {power}");
    }
}
