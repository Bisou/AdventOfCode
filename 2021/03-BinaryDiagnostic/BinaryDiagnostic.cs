using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class BinaryDiagnostic
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\03-BinaryDiagnostic\input1.txt");  
            string line = file.ReadLine();
            var count=0;
            var oneBits=new int[line.Length];
            do
            {
                count++;
                for(var i=0;i<line.Length;i++)
                    if (line[i]=='1')
                        oneBits[i]++;
            } while ((line = file.ReadLine()) != null);
            var gammaRate=0;
            var epsilonRate=0;
            count/=2;
            for (var i=0;i<oneBits.Length;i++)
            {
                gammaRate *= 2;
                epsilonRate *= 2;
                if (oneBits[i]>count)
                    gammaRate++;
                else
                    epsilonRate++;
            }
            Console.WriteLine($"Part1: gamma {gammaRate} and epsilon {epsilonRate} so answer is {gammaRate * epsilonRate}");
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\03-BinaryDiagnostic\input1.txt");  
            var numbers = new List<string>();
            string line;
            while ((line = file.ReadLine()) != null)
                numbers.Add(line);
            var size = numbers[0].Length;
            var oxygenGeneratorData = numbers.ToList();
            for (var i=0;i<size;i++)
            {
                var countOne=0; 
                foreach(var data in oxygenGeneratorData)
                {
                    if (data[i]=='1')
                        countOne++;
                }
                if (2 * countOne >= oxygenGeneratorData.Count)
                    oxygenGeneratorData = oxygenGeneratorData.Where(d => d[i]=='1').ToList();
                else
                    oxygenGeneratorData = oxygenGeneratorData.Where(d => d[i]=='0').ToList();
                if (oxygenGeneratorData.Count==1)
                    break;
            }
            var oxygenGeneratorRating=0;
            for (var i=0;i<size;i++)
            {
                oxygenGeneratorRating*=2;
                if (oxygenGeneratorData[0][i]=='1')
                    oxygenGeneratorRating++;
            }
                                   
            var co2ScrubbingData = numbers.ToList();
            for (var i=0;i<size;i++)
            {
                var countOne=0; 
                foreach(var data in co2ScrubbingData)
                {
                    if (data[i]=='1')
                        countOne++;
                }
                if (2 * countOne < co2ScrubbingData.Count)
                    co2ScrubbingData = co2ScrubbingData.Where(d => d[i]=='1').ToList();
                else
                    co2ScrubbingData = co2ScrubbingData.Where(d => d[i]=='0').ToList();
                if (co2ScrubbingData.Count==1)
                    break;
            }
            var co2ScrubbingRating = 0;

            for (var i=0;i<size;i++)
            {
                co2ScrubbingRating*=2;
                if (co2ScrubbingData[0][i]=='1')
                    co2ScrubbingRating++;
            }
            Console.WriteLine($"Part2: oxygenGeneratorRating {oxygenGeneratorRating} and co2ScrubbingRating {co2ScrubbingRating} so answer is {oxygenGeneratorRating * co2ScrubbingRating}");
        }
    }
}
