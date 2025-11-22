using System.Text;

public class Day22
{
    private const int day = 22;
    private const long modulo = 16777216;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);  
        
        //test
       /* var s = 123L;                
        Console.WriteLine(s);
        for (var i=0;i<10;++i) {
            s = OneTurn(s);
            Console.WriteLine(s);
        }*/

        var total=0L; 
        foreach(var line in input) {
            var secret = long.Parse(line);
            for (var i=0;i<2000;++i) {
                secret = OneTurn(secret);
            }
            total += secret;
        }
        
        Console.WriteLine($"Part1: result is {total}");
    }

    public static long OneTurn(long secret) {
        var s64 = secret*64;
        secret ^= s64;
        secret %= modulo;
        var s32 = secret/32; 
        secret ^= s32;
        secret %= modulo;
        var s2048=secret*2048;
        secret ^= s2048;
        secret %= modulo;
        return secret;
    }


    public static void SolvePart2(string dataType)
    {
        Tool.LogStart(day,2,dataType);
        
        var input = Tool.ReadAll(day, dataType);  
        var bananas = new Dictionary<(long A, long B, long C, long D),long>();
        for (var i=0;i<input.Length;++i) {       
            var monkey = new HashSet<(long A, long B, long C, long D)>();
            var priceVariations = new List<long>(); 
            long price;
            var secret = long.Parse(input[i]);
            var previousPrice = secret%10;
            for (var j=0;j<2000;++j) {
                secret = OneTurn(secret);
                price = secret%10;
                priceVariations.Add(price - previousPrice);
                if (priceVariations.Count>4) {
                    priceVariations.RemoveAt(0);
                }
                if (priceVariations.Count==4) {
                    var key = (priceVariations[0], priceVariations[1], priceVariations[2], priceVariations[3]);
                    if(!monkey.Contains(key)) {
                        monkey.Add(key);     
                        bananas.TryGetValue(key, out var count);
                        bananas[key] = count + price;   
                    }
                }   
                previousPrice = price;     
            }            
        }

        var total=bananas.Values.Max(b => b); 
        foreach(var key in bananas.Keys) {
            if (bananas[key]==total) {
                Console.WriteLine($"Key = {key.A}, {key.B}, {key.C}, {key.D}");
            }
        }
        Console.WriteLine($"Part2: result is {total}");
    }

}
