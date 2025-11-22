using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class TuningTrouble
    {
        public static void SolvePart1()
        {   
            var test7="mjqjpqmgbljsphdztnvjfqwrcgsmlb";
            Console.WriteLine($"{test7} => {FindStart(test7)} (sould be 7)");
            var test5="bvwbjplbgvbhsrlpgdmjqwftvncz";
            Console.WriteLine($"{test5} => {FindStart(test5)} (sould be 5)");
            var test6="nppdvjthqldpwncqszvftbrmjlhg";
            Console.WriteLine($"{test6} => {FindStart(test6)} (sould be 6)");
            var test10="nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
            Console.WriteLine($"{test10} => {FindStart(test10)} (sould be 10)");
            var test11="zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
            Console.WriteLine($"{test11} => {FindStart(test11)} (sould be 11)");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\06-TuningTrouble\input.txt");  
            var input=new Stack<string>();
            string signal = file.ReadLine();
            var start = FindStart(signal);
            Console.WriteLine($"Part1: signal starts at {start}");
        }
        
        private static int FindStart(string signal) {
            for (var i=3;i<signal.Length;++i) {
                if (signal[i]!=signal[i-1] && signal[i]!=signal[i-2] && signal[i]!=signal[i-3]
                 && signal[i-1]!=signal[i-2] && signal[i-1]!=signal[i-3] && signal[i-2]!=signal[i-3])
                    return i+1;
            }
            return -1;
        }

        public static void SolvePart2()
        {            
            
            var test7="mjqjpqmgbljsphdztnvjfqwrcgsmlb";
            Console.WriteLine($"{test7} => {FindStartMessage(test7)} (sould be 19)");
            var test5="bvwbjplbgvbhsrlpgdmjqwftvncz";
            Console.WriteLine($"{test5} => {FindStartMessage(test5)} (sould be 23)");
            var test6="nppdvjthqldpwncqszvftbrmjlhg";
            Console.WriteLine($"{test6} => {FindStartMessage(test6)} (sould be 23)");
            var test10="nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
            Console.WriteLine($"{test10} => {FindStartMessage(test10)} (sould be 29)");
            var test11="zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";
            Console.WriteLine($"{test11} => {FindStartMessage(test11)} (sould be 26)");
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\06-TuningTrouble\input.txt");  
            var input=new Stack<string>();
            string signal = file.ReadLine();
            var start = FindStartMessage(signal);
            Console.WriteLine($"Part2: message starts at {start}");
        }
        
        private static int FindStartMessage(string signal) {
            for (var i=0;i<signal.Length-14;++i) {
                if (signal.Substring(i,14).GroupBy(x=>x).Count()==14)
                    return i+14;                
            }
            return -1;
        }
    }
}
