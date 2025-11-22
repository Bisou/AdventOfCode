using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class MonkeyInTheMiddle
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\11-MonkeyInTheMiddle\input.txt");  
            string line;
            var tribe = new List<Monkey>();
            while((line=file.ReadLine())!=null) {
                if (line.StartsWith("Monkey")) {
                    var monkey = new Monkey();
                    monkey.Items.AddRange(file.ReadLine().Replace(",","").Replace("  Starting items: ","").Split(' ').Select(long.Parse));
                    var data = file.ReadLine().Replace("  Operation: new = old ","").Split(' ');
                    monkey.Operation = data[0];
                    monkey.Operand = data[1];
                    monkey.Modulo = long.Parse(file.ReadLine().Replace("  Test: divisible by ",""));
                    monkey.ThrowIfTrue = int.Parse(file.ReadLine().Replace("    If true: throw to monkey ",""));
                    monkey.ThrowIfFalse = int.Parse(file.ReadLine().Replace("    If false: throw to monkey ",""));
                    tribe.Add(monkey);
                }
            }

            for (var turn=0;turn<20;++turn){
                for (var i=0;i<tribe.Count;++i)
                    tribe[i].ProcessOneTurn1(tribe);
            }
                
            var activities = tribe.Select(m => m.Activity).OrderByDescending(x => x).Take(2).ToArray();
            Console.WriteLine($"Part1: The two most active monkeys' business is {activities[0]*activities[1]}");
        }

        
        public static void SolvePart2()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\11-MonkeyInTheMiddle\input.txt");  
            string line;
            var tribe = new List<Monkey>();
            long globalModulo=1;
            while((line=file.ReadLine())!=null) {
                if (line.StartsWith("Monkey")) {
                    var monkey = new Monkey();
                    monkey.Items.AddRange(file.ReadLine().Replace(",","").Replace("  Starting items: ","").Split(' ').Select(long.Parse));
                    var data = file.ReadLine().Replace("  Operation: new = old ","").Split(' ');
                    monkey.Operation = data[0];
                    monkey.Operand = data[1];
                    monkey.Modulo = long.Parse(file.ReadLine().Replace("  Test: divisible by ",""));
                    monkey.ThrowIfTrue = int.Parse(file.ReadLine().Replace("    If true: throw to monkey ",""));
                    monkey.ThrowIfFalse = int.Parse(file.ReadLine().Replace("    If false: throw to monkey ",""));
                    tribe.Add(monkey);
                    globalModulo *= monkey.Modulo;
                }
            }

            for (var turn=0;turn<10000;++turn){
                if (new[]{1,20,1000,2000,3000,4000,5000,6000,7000,8000,9000}.Contains(turn))
                    Console.WriteLine($"Turn{turn}: {string.Join(" ",tribe.Select(m => m.Activity))}");
                for (var i=0;i<tribe.Count;++i)
                    tribe[i].ProcessOneTurn2(tribe, globalModulo);
            }
                
            var activities = tribe.Select(m => m.Activity).OrderByDescending(x => x).Take(2).ToArray();
            Console.WriteLine($"Part2: The two most active monkeys' business is {activities[0]*activities[1]}");
        }
        
        class Monkey {
            public List<long> Items = new List<long>();
            public string Operation;
            public string Operand;
            public long Modulo;
            public int ThrowIfTrue;
            public int ThrowIfFalse;
            public long Activity;

            public override string ToString() {
                return string.Join(", ", Items);
            }

            public void ProcessOneTurn1(List<Monkey> tribe){
                foreach (var item in Items)
                {
                    ++Activity;
                    var newItem=item;
                    if (Operation=="*")
                        newItem *= GetValue(item);
                    else
                        newItem += GetValue(item);
                        newItem /=3;
                    if (newItem%Modulo==0)
                        tribe[ThrowIfTrue].Items.Add(newItem);
                    else
                        tribe[ThrowIfFalse].Items.Add(newItem);
                }
                Items.Clear();
            }
            
            public void ProcessOneTurn2(List<Monkey> tribe, long globalModulo){
                foreach (var item in Items)
                {
                    ++Activity;
                    var newItem=item;
                    if (Operation=="*")
                        newItem *= GetValue(item);
                    else
                        newItem += GetValue(item);
                    newItem = newItem % globalModulo;
                    if (newItem%Modulo==0)
                        tribe[ThrowIfTrue].Items.Add(newItem);
                    else
                        tribe[ThrowIfFalse].Items.Add(newItem);
                }
                Items.Clear();
            }

            private long GetValue(long item) {
                if (Operand=="old")
                    return item;
                return long.Parse(Operand);
            }
        }
    }
}

