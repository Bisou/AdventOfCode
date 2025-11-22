using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Matchsticks
    {
        public static void SolvePart1()
        {
            var shouldBe2=GetOnlyCodingLength("\"\"");
            var shouldAlsoBe2=GetOnlyCodingLength("\"abc\"");
            var shouldBe3=GetOnlyCodingLength("\"aaa\\\"aaa\"");
            var shouldBe5=GetOnlyCodingLength("\"\\x27\"");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\08.txt");  
            var size = 0;
            string line;
            while((line=file.ReadLine())!=null)
            {
                size+=GetOnlyCodingLength(line);

            }
            Console.WriteLine($"Part 1: total size is {size}");        
        }

        private static int GetOnlyCodingLength(string line)
        {
            var size=2;
            for (var i=1;i<line.Length-1;i++)                
            {
                if (line[i]=='\\' && line[i+1]=='"')
                {
                    size++;
                    continue;
                }    
                if (line[i]=='\\' && line[i+1]=='\\')
                {
                    size++;
                    i++;
                    continue;
                }    
                if (line[i]=='\\' && line[i+1]=='x')
                {
                    size+=3;
                    i+=3;
                    continue;
                }
            }

            return size;
        }

        private static int GetEncodingLength(string line)
        {
            var size=4; //for brackets
            for (var i=1;i<line.Length-1;i++)                
            {
                if (line[i]=='\\')
                {
                    size++;
                    continue;
                }    
                if (line[i]=='"')
                {
                    size++;
                    continue;
                }    
            }

            return size;
        }
        

        public static void SolvePart2()
        {
            var shouldBe4=GetEncodingLength("\"\"");
            var shouldAlsoBe4=GetEncodingLength("\"abc\"");
            var shouldBe6=GetEncodingLength("\"aaa\\\"aaa\"");
            var shouldBe5=GetEncodingLength("\"\\x27\"");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\08.txt");  
            var size = 0;
            string line;
            while((line=file.ReadLine())!=null)
            {
                size+=GetEncodingLength(line);

            }
            Console.WriteLine($"Part 2: total encoding size is {size}");       
        }
    }
}
