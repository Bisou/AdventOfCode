using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class DoesntHeHaveInternElvesForThis
    {
        public static void SolvePart1()
        {
            var testShouldBeNice = IsStringNice1("ugknbfddgicrmopn");
            var testShouldBeNice2 = IsStringNice1("aaa");
            var testShouldBeNaughty = IsStringNice1("jchzalrnumimnmhp");
            var testShouldBeNaughty2 = IsStringNice1("haegwjzuvuyypxyu");
            var testShouldBeNaughty3 = IsStringNice1("dvszwmarrgswjxmb");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\05.txt");  
            string line;
            var nice=0;
            while((line=file.ReadLine())!=null)
                if (IsStringNice1(line))
                    nice++;
            Console.WriteLine($"Part 1: total nice strings is {nice}");
        }
        
        private static bool IsStringNice1(string line)
        {
            var vowelCount=line.Count(c => "aeiou".Contains(c));
                if (vowelCount<3) return false;
            if (line.Contains("ab") 
                || line.Contains("cd") 
                || line.Contains("pq")
                || line.Contains("xy"))
                    return false;
            var doubled=false;
            for (var i=1;i<line.Length;i++)
                if (line[i]==line[i-1])
                {
                    doubled=true;
                    break;
                }
            return doubled;
        }

        public static void SolvePart2()
        {
           var testShouldBeNice = IsStringNice2("qjhvhtzxzqqjkmpb");
            var testShouldBeNice2 = IsStringNice2("xxyxx");
            var testShouldBeNaughty = IsStringNice2("uurcxstgmygtbstg");
            var testShouldBeNaughty2 = IsStringNice2("ieodomkazucvgmuy");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\05.txt");  
            string line;
            var nice=0;
            while((line=file.ReadLine())!=null)
                if (IsStringNice2(line))
                    nice++;
            Console.WriteLine($"Part 2: total nice strings is {nice}");
        }
        
        private static bool IsStringNice2(string line)
        {
            var doubled=false;
            for (var i=2;i<line.Length;i++)
                if (line[i]==line[i-2])
                {
                    doubled=true;
                    break;
                }
            if (!doubled) return false;
            var repeat=false;
            for (var i=1;i<line.Length;i++)
                for (var j=i+1;j<line.Length-1;j++)
                    if (line[i-1]==line[j] && line[i]==line[j+1])
                        repeat=true;

            return repeat;
        }
    }
}
