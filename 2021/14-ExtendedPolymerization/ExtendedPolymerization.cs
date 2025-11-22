using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class ExtendedPolymerization    
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\14-ExtendedPolymerization\input1.txt");  
            var polymer = file.ReadLine();
            string line;  
            var mapping = new Dictionary<(char,char),char>();
            file.ReadLine();//empty line
            while ((line = file.ReadLine()) != null)
            {
                mapping.Add((line[0],line[1]), line[6]);
            }
            Console.WriteLine($"Template:     {polymer}");
            for(var turn=0;turn<10;turn++)
            {
                var sb = new StringBuilder();
                sb.Append(polymer[0]);
                for(var i=1;i<polymer.Length;i++)
                {
                    sb.Append(mapping[(polymer[i-1],polymer[i])]);
                    sb.Append(polymer[i]);
                }
                polymer = sb.ToString();
                Console.WriteLine($"After step {turn+1}: {polymer}");
            }
            
            var freq = polymer.GroupBy(c => c).OrderBy(g => g.Count()).ToArray();
            var min = freq[0].Count();
            var max = freq[freq.Length-1].Count();
            Console.WriteLine($"Part1: after 10 steps, we have max={max}, min={min} so a total of {max-min}"); 
        }
        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\14-ExtendedPolymerization\input1.txt");  
            var polymer = file.ReadLine();
            string line;  
            var mapping = new Dictionary<(char,char),char>();
            file.ReadLine();//empty line
            var frequencies = new Dictionary<(char,char),long>();
            while ((line = file.ReadLine()) != null)
            {
                mapping.Add((line[0],line[1]), line[6]);
                frequencies.Add((line[0],line[1]), 0);
            }
            Console.WriteLine($"Template:     {polymer}");
            for(var i=1;i<polymer.Length;i++)
                frequencies[(polymer[i-1],polymer[i])]++;
            var last = polymer[polymer.Length-1];
            for(var turn=0;turn<40;turn++)
            {
                var nextFrequencies = new Dictionary<(char,char),long>();
                foreach (var key in frequencies.Keys)
                    nextFrequencies.Add(key,0);
                foreach (var key in frequencies.Keys)
                {
                    nextFrequencies[(key.Item1, mapping[key])] += frequencies[key];
                    nextFrequencies[(mapping[key], key.Item2)] += frequencies[key];
                }
                frequencies = nextFrequencies;
            }
            
            var freq = new Dictionary<char,long>();
            foreach(var letter in mapping.Values.Distinct())
                freq.Add(letter, 0);
            foreach (var key in frequencies.Keys)
                freq[key.Item1] += frequencies[key];
            freq[last]++;
            var min = freq.Values.Min();
            var max = freq.Values.Max();
            Console.WriteLine($"Part2: after 40 steps, we have max={max}, min={min} so a total of {max-min}"); 
        }   
    }
}
