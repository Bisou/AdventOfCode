public class Day04
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day04-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day04-{dataType}.txt");     
        var sum=0;
        var points = new [] {0,1,2,4,8,16,32,64,128,256,512,1024};
        foreach (var line in input)
        {
            var data = line.Split(new [] {'|',':'});
            var winningNumbers = data[1].Split(' ',StringSplitOptions.RemoveEmptyEntries);
            var myNumbers = data[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var goodNumbers=myNumbers.Intersect(winningNumbers).Count();               

            sum += points[goodNumbers];
        }
        Console.WriteLine($"Part1: sum of points is {sum}");
    }
    

    public static void SolvePart2(string dataType)
    {      
        Console.WriteLine($"Day04-Part2-{dataType}");
        var lines = File.ReadAllLines(@$".\..\..\..\Inputs\Day04-{dataType}.txt");             
        var cards = new long[lines.Length];            
        for (var id=0;id<cards.Length;++id) {
            cards[id]++; //our original card
            var data = lines[id].Split(new [] {'|',':'});
            var winningNumbers = data[1].Split(' ',StringSplitOptions.RemoveEmptyEntries);
            var myNumbers = data[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var goodNumbers=myNumbers.Intersect(winningNumbers).Count();               
            for (var won=1;won<=goodNumbers;++won) {
                cards[id+won]+=cards[id];
            }                
        }
        Console.WriteLine($"Part2: total of cards is {cards.Sum()}");
    }
}
