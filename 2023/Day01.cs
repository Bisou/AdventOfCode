public class Day01
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day01-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day01-{dataType}.txt");     
        var sum=0;
        var digits="0123456789";
        foreach (var line in input)
        {
            var first=-1;
            var last=-1;
            foreach(var c in line) {
                if (digits.Contains(c)) {
                    last=c-'0';
                    if (first<0) {
                        first=last;
                    }
                }
            }
            sum+=10*first+last;
        }
        Console.WriteLine($"Part1: sum of calibration values is {sum}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Console.WriteLine($"Day01-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day01-{dataType}.txt");     
        var sum=0;
        var digits= new Dictionary<string,int> {{"0",0},
                                                {"1",1},
                                                {"2",2},
                                                {"3",3},
                                                {"4",4},
                                                {"5",5},
                                                {"6",6},
                                                {"7",7},
                                                {"8",8},
                                                {"9",9},
                                                {"one",1},
                                                {"two",2},
                                                {"three",3},
                                                {"four",4},
                                                {"five",5},
                                                {"six",6},
                                                {"seven",7},
                                                {"eight",8},
                                                {"nine",9}};
        foreach (var line in input)
        {
            var firstIndex=line.Length;
            var lastIndex=-1;
            var first=0;
            var last=0;
            foreach(var digit in digits.Keys) {
                var firstPos = line.IndexOf(digit);
                if (firstPos<firstIndex && firstPos>=0) {
                    firstIndex=firstPos;
                    first=digits[digit];
                }
                var lastPos = line.LastIndexOf(digit);
                if (lastPos>lastIndex) {
                    lastIndex=lastPos;
                    last=digits[digit];
                }
            }
            sum+=10*first+last;
        }
        Console.WriteLine($"Part2: sum of calibration values is {sum}");
    }
}
