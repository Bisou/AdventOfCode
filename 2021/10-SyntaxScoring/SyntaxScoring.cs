using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class SyntaxScoring
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\10-SyntaxScoring\input1.txt");  
            var score = 0;
            string line;            
            var openings = "<[({";
            while ((line = file.ReadLine()) != null)
            {
                var stack = new Stack<char>();
                foreach(var c in line)
                {
                    if (openings.Contains(c))
                        stack.Push(c);
                    else
                    {
                        var open = stack.Pop();
                        var cost = GetCost(open, c);
                        if (cost>0)
                        {
                            score += cost;
                            break;
                        }
                    }
                }
            }
            
            Console.WriteLine($"Part1: we have a syntax score of {score}"); 
        }
        
        private static int GetCost(char open, char end)
        {
            if (end==')' && open !='(')
                return 3;
            if (end==']' && open !='[')
                return 57;
            if (end=='}' && open !='{')
                return 1197;
            if (end=='>' && open !='<')
                return 25137;
            return 0;
        }

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\10-SyntaxScoring\input1.txt");  
            var scores = new List<long>();
            string line;            
            var openings = "<[({";
            var closingCost = new Dictionary<char,int>
            {
                {'(',1},
                {'[',2},
                {'{',3},
                {'<',4}
            };            
            while ((line = file.ReadLine()) != null)
            {
                var stack = new Stack<char>();
                var corrupted=false;
                foreach(var c in line)
                {
                    if (openings.Contains(c))
                        stack.Push(c);
                    else
                    {
                        var open = stack.Pop();
                        if (GetCost(open, c)>0)
                        {
                            corrupted=true;
                            break;
                        }
                    }
                }
                if (corrupted) continue;
                var cost=0L;
                while (stack.Any())
                {
                    cost = cost*5 + closingCost[stack.Pop()];
                }
                scores.Add(cost);
            }
            
            Console.WriteLine($"Part2: we have a medium score of {scores.OrderBy(x => x).ToArray()[scores.Count/2]}");            
        }        
    }
}
