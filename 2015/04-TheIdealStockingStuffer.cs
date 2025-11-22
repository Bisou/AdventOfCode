using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode
{
    public class TheIdealStockingStuffer
    {
        public static void SolvePart1()
        {
            var testShouldBe609043 = GetMiningKey1("abcdef");
            var testShouldBe1048970 = GetMiningKey1("pqrstuv");
            var answer = GetMiningKey1("bgvyzdsv");

            Console.WriteLine($"Part 1: lowest number is {answer}");
        }
        
        private static long GetMiningKey1(string key)
        {
            long miningKey=1;
            var hashmd5 = new MD5CryptoServiceProvider();
            while(true) {var test = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key+miningKey.ToString()));
                
                var sb = new StringBuilder();
                if (test[0]==0 && test[1]==0) 
                {
                    //for (var i = 0; i < 5; i++) sb.Append(test[i].ToString("x2"));
                    //if (sb.ToString()=="00000") return miningKey;
                    if (test[2].ToString("x2")[0]=='0') return miningKey;
                }
                ++miningKey;
            }
            return 0;
        }

        public static void SolvePart2()
        {
            var answer = GetMiningKey2("bgvyzdsv");

            Console.WriteLine($"Part 2: lowest number is {answer}");
        }
        
        private static long GetMiningKey2(string key)
        {
            long miningKey=1;
            var hashmd5 = new MD5CryptoServiceProvider();
            while(true) {var test = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key+miningKey.ToString()));
                
                var sb = new StringBuilder();
                if (test[0]==0 && test[1]==0 && test[2]==0) 
                    return miningKey;                    
                ++miningKey;
            }
            return 0;
        }
    }
}
