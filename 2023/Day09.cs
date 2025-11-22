public class Day09
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day09-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day09-{dataType}.txt");   
        var sum=0L;
        foreach (var line in input) {
            var tableau = new List<List<long>>();
            var data = line.Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            tableau.Add(data);
            var index=0;
            while(tableau[index].Any(x => x!=0)) {
                //fill new line
                var newLine=new List<long>();
                for (var i=1;i<tableau[index].Count;++i) {
                    newLine.Add(tableau[index][i]-tableau[index][i-1]);
                }
                tableau.Add(newLine);
                index++;
            }
            //now extrapolate
            tableau[index].Add(0);
            while(index>0) {
                tableau[index-1].Add(tableau[index-1][^1]+tableau[index][^1]);
                index--;
            }
            sum+=tableau[index][^1];
        }
        Console.WriteLine($"Part1: sum of extrapolated values is {sum}");
    }

    public static void SolvePart2(string dataType)
    {      
        Console.WriteLine($"Day09-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day09-{dataType}.txt");   
        var sum=0L;
        foreach (var line in input) {
            var tableau = new List<List<long>>();
            var data = line.Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            tableau.Add(data);
            var index=0;
            while(tableau[index].Any(x => x!=0)) {
                //fill new line
                var newLine=new List<long>();
                for (var i=1;i<tableau[index].Count;++i) {
                    newLine.Add(tableau[index][i]-tableau[index][i-1]);
                }
                tableau.Add(newLine);
                index++;
            }
            //now extrapolate
            tableau[index].Insert(0,0);
            while(index>0) {
                tableau[index-1].Insert(0,tableau[index-1][0]-tableau[index][0]);
                index--;
            }
            sum+=tableau[index][0];
        }
        Console.WriteLine($"Part2: sum of backwards extrapolated values is {sum}");
    }
}
