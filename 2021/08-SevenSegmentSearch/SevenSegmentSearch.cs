using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class SevenSegmentSearch
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\08-SevenSegmentSearch\input1.txt");  
            var count = 0;
            string line;            
            while ((line = file.ReadLine()) != null)
                count += FindEasyDigits(line);
            Console.WriteLine($"Part1: we have {count} easy numbers");
        }
        
        private static int FindEasyDigits(string line)
        {
            var data = line.Replace(" | ", " ").Split(' ');
            var easyDigits = new List<string>();
            for (var i=0;i<10;i++)
            {
                if (new []{2,4,3,7}.Contains(data[i].Length))
                    easyDigits.Add(string.Join("",data[i].OrderBy(x => x)));
            }
            var count=0;
            for (var i=10;i<data.Length;i++)
                if (easyDigits.Contains(string.Join("",data[i].OrderBy(x => x))))
                    count++;
            return count;
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\08-SevenSegmentSearch\input1.txt");  
            var sum = 0L;
            string line;            
            while ((line = file.ReadLine()) != null)
                sum += FindOutputValue(line);
            Console.WriteLine($"Part2: all output values sum up to {sum}");
        }
        
        private static long FindOutputValue(string line)
        {
            var data = line.Replace(" | ", " ").Split(' ').Select(num => string.Join("",num.OrderBy(x => x))).ToArray();
            var digits = new string[10];
            var value = new Dictionary<string,int>();
            var digitsLeftToDecode = data.Take(10).ToList();
            var one = digitsLeftToDecode.Single(x => x.Length == 2);
            digits[1]=one;
            value.Add(one, 1);
            digitsLeftToDecode.Remove(one);
            var four = digitsLeftToDecode.Single(x => x.Length == 4);
            digits[4]=four;
            value.Add(four, 4);
            digitsLeftToDecode.Remove(four);
            var seven = digitsLeftToDecode.Single(x => x.Length == 3);
            digits[7]=seven;
            value.Add(seven, 7);
            digitsLeftToDecode.Remove(seven);
            var eight = digitsLeftToDecode.Single(x => x.Length == 7);
            digits[8]=eight;
            value.Add(eight, 8);
            digitsLeftToDecode.Remove(eight);
            foreach(var isItASix in digitsLeftToDecode.Where(x => x.Length==6))
            {
                if(isItASix.Intersect(one).Count()==1)
                {
                    digits[6]=isItASix;
                    value.Add(isItASix, 6);
                    digitsLeftToDecode.Remove(isItASix);
                    break;
                }
            }
            var bd = four.Except(one).ToArray();
            foreach(var isItANine in digitsLeftToDecode.Where(x => x.Length==6))
            {
                if(isItANine.Intersect(bd).Count()==2)
                {
                    digits[9]=isItANine;
                    value.Add(isItANine, 9);
                    digitsLeftToDecode.Remove(isItANine);
                    break;
                }
            }
            var zero = digitsLeftToDecode.Single(x => x.Length == 6);
            digits[0]=zero;
            value.Add(zero, 0);
            digitsLeftToDecode.Remove(zero);
            var three = digitsLeftToDecode.Single(x => x.Intersect(one).Count()==2);
            digits[3]=three;
            value.Add(three, 3);
            digitsLeftToDecode.Remove(three);
            
            var five = digitsLeftToDecode.Single(x => x.Intersect(four).Count()==3);
            digits[5]=five;
            value.Add(five, 5);
            digitsLeftToDecode.Remove(five);

            var two = digitsLeftToDecode.Single();
            digits[2]=two;
            value.Add(two, 2);

            return 1000*value[data[10]] + 100*value[data[11]] + 10*value[data[12]] + value[data[13]];
        }
        
    }
}
