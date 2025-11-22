using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class ElvesLookElvesSay
    {
        public static void SolvePart1()
        {
            var input = "1";
            var output = Process(input);
            Console.WriteLine($"Test: {input} => '11' and we got {output}");
            input = output;
            output = Process(input);
            Console.WriteLine($"Test: {input} => '21' and we got {output}");
            input = output;
            output = Process(input);
            Console.WriteLine($"Test: {input} => '1211' and we got {output}");
            input = output;
            output = Process(input);
            Console.WriteLine($"Test: {input} => '111221' and we got {output}");

            input = "1113122113";
            for (var i=0;i<40;++i) {
                output = Process(input);
                input = output;
            }
            
            Console.WriteLine($"Part 1: After 40 rounds, we have a length of {output.Length}");            
        }
    
        public static void SolvePart2()
        {
            var input = "1113122113";
            for (var i=0;i<50;++i) {
                input = Process(input);
            }
            
            Console.WriteLine($"Part 2: After 50 rounds, we have a length of {input.Length}");            
        }
    
        private static string Process(string input)
        {
            var sb = new StringBuilder();
            var count = 1;
            for (var i=1;i<input.Length;++i) {
                if (input[i]==input[i-1]) 
                    ++count;
                else {
                    sb.Append(count);
                    sb.Append(input[i-1]);
                    count=1;
                }
            }            
            sb.Append(count);
            sb.Append(input[input.Length-1]);

            return sb.ToString();
        }

        private static void Increment(StringBuilder sb)
        {
            var carry = true;
            for (var i=sb.Length-1; i>=0; i--)
            {
                if (carry)
                    sb[i]++;
                if (sb[i]>'z')
                {
                    carry=true;
                    sb[i]='a';
                }
                else
                    carry = false;
            }
        }
        
        private static bool IsNotValid(string password)
        {
            //Condition 1
            var straight = false;
            for (var i=2;i<password.Length;i++)
                if (password[i] == password[i-1]+1 && password[i-1]==password[i-2]+1)
                    straight = true;
            if (!straight) return true;

            //Condition 2
            if (password.Contains('i') || password.Contains('o') || password.Contains('l')) return true;
            
            //Condition 3
            var pairs=new HashSet<char>();
            for (var i=1;i<password.Length;i++)
            {
                if (password[i]==password[i-1])
                    pairs.Add(password[i]);
            }
            if (pairs.Count()<2) return true;
            
            //all valid
            return false;
        }
    }
}
