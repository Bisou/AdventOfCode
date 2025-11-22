using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class CorporatePolicy
    {
        public static void SolvePart1()
        {
            var ShouldBeabcdffaa = GetNextPassword("abcdefgh");
            var ShouldBeghjaabcc = GetNextPassword("ghijklmn");
            var answer = GetNextPassword("hepxcrrq");
            
            Console.WriteLine($"Part 1: The next password is {answer}");            
        }
    
        public static void SolvePart2()
        {
            var answer1 = GetNextPassword("hepxcrrq");
            var answer2 = GetNextPassword(answer1);
            
            Console.WriteLine($"Part 2: The next password is {answer2}");            
        }
    
        private static string GetNextPassword(string password)
        {
            var sb = new StringBuilder(password);
            do
            {
                Increment(sb);
            }while(IsNotValid(sb.ToString()));

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
