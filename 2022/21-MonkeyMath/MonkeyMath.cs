using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class MonkeyMath
    {
        public static void SolvePart1()
        {
            var monkeys = new Dictionary<string, Monkey>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\21-MonkeyMath\input.txt");  
            string line;
            while((line=file.ReadLine())!=null) {
                var data = line.Replace(":","").Split(' ');
                if (data.Length==2)
                    monkeys.Add(data[0], new Monkey(data[0], long.Parse(data[1])));
                else
                    monkeys.Add(data[0], new Monkey(data[0], data[1], data[2], data[3]));
            }

            var root = Dfs(monkeys, "root");
            Console.WriteLine($"Part1: monkey root yells {root}.");
        }

        private static long Dfs(Dictionary<string, Monkey> monkeys, string name) {
            var monkey = monkeys[name];
            if (monkey.Value.HasValue) return monkey.Value.Value;
            var monkey1 = Dfs(monkeys, monkey.Monkey1);
            var monkey2 = Dfs(monkeys, monkey.Monkey2);
            long res=monkey1;
            if (monkey.Operation=="+")
                res+=monkey2;
            else if (monkey.Operation=="-")
                res-=monkey2;
            else if (monkey.Operation=="*")
                res*=monkey2;
            else if (monkey.Operation=="/")
                res/=monkey2;
            monkey.Value=res;
            return res;
        }

        private static long? Dfs2(Dictionary<string, Monkey> monkeys, string name) {
            if (name=="humn") return null;
            var monkey = monkeys[name];
            if (monkey.Value.HasValue) return monkey.Value.Value;
            var monkey1 = Dfs2(monkeys, monkey.Monkey1);
            var monkey2 = Dfs2(monkeys, monkey.Monkey2);
            if (monkey1.HasValue && monkey2.HasValue) {
                long res=monkey1.Value;
                if (monkey.Operation=="+")
                    res+=monkey2.Value;
                else if (monkey.Operation=="-")
                    res-=monkey2.Value;
                else if (monkey.Operation=="*")
                    res*=monkey2.Value;
                else if (monkey.Operation=="/")
                    res/=monkey2.Value;
                monkey.Value=res;
                return res;
            }
            return null;
        }

        private static void Invert(Dictionary<string, Monkey> monkeys, Dictionary<string, Monkey> invertedMonkeys, string name) {
            if (name=="humn") return;
            var monkey = monkeys[name];
            if (monkey.Value.HasValue) {
                invertedMonkeys.Add(monkey.Name, monkey);
                return;
            }
            var monkey1 = monkeys[monkey.Monkey1];
            var monkey2 = monkeys[monkey.Monkey2];
            if (!monkey1.Value.HasValue) {                
                if (monkey.Operation=="+")
                    invertedMonkeys.Add(monkey1.Name,new Monkey(monkey1.Name,monkey.Name,"-",monkey2.Name));
                else if (monkey.Operation=="-")
                    invertedMonkeys.Add(monkey1.Name,new Monkey(monkey1.Name,monkey.Name,"+",monkey2.Name));
                else if (monkey.Operation=="*")
                    invertedMonkeys.Add(monkey1.Name,new Monkey(monkey1.Name,monkey.Name,"/",monkey2.Name));
                else if (monkey.Operation=="/")
                    invertedMonkeys.Add(monkey1.Name,new Monkey(monkey1.Name,monkey.Name,"*",monkey2.Name));
            } 
            if (!monkey2.Value.HasValue) {                
                if (monkey.Operation=="+")
                    invertedMonkeys.Add(monkey2.Name,new Monkey(monkey2.Name,monkey.Name,"-",monkey1.Name));
                else if (monkey.Operation=="-")
                    invertedMonkeys.Add(monkey2.Name,new Monkey(monkey2.Name,monkey1.Name,"-",monkey.Name));
                else if (monkey.Operation=="*")
                    invertedMonkeys.Add(monkey2.Name,new Monkey(monkey2.Name,monkey.Name,"/",monkey1.Name));
                else if (monkey.Operation=="/")
                    invertedMonkeys.Add(monkey2.Name,new Monkey(monkey2.Name,monkey1.Name,"/",monkey.Name));
            } 
            Invert(monkeys, invertedMonkeys, monkey1.Name);
            Invert(monkeys, invertedMonkeys, monkey2.Name);
        }
        
        public class Monkey {
            public string Name;
            public long? Value;
            public string Operation;            
            public string Monkey1;
            public string Monkey2;

            public override string ToString(){
                if (Value.HasValue)
                    return $"{Name}:{Value}";
                else
                    return $"{Name}:{Monkey1}{Operation}{Monkey2}";
            }

            public Monkey(string name, long val)
            {
               Name=name;
               Value=val;
            }

            public Monkey(string name, string m1, string op, string m2)
            {
               Name=name;
               Monkey1=m1;
               Operation=op;
               Monkey2=m2;
            }
        }
        
        public static void SolvePart2()
        {
            var monkeys = new Dictionary<string, Monkey>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\21-MonkeyMath\input.txt");  
            string line;
            while((line=file.ReadLine())!=null) {
                var data = line.Replace(":","").Split(' ');
                if (data.Length==2) //fixed value
                    monkeys.Add(data[0], new Monkey(data[0], long.Parse(data[1])));
                else {
                    monkeys.Add(data[0], new Monkey(data[0], data[1], data[2], data[3]));
                }
            }
            monkeys["root"].Operation="=";
            monkeys["humn"].Value=null;
            Dfs2(monkeys, monkeys["root"].Monkey1);
            var m2 = Dfs(monkeys, monkeys["root"].Monkey2);
            monkeys[monkeys["root"].Monkey2].Value=m2; //this value is fixed
            var invertedMonkeys = new Dictionary<string, Monkey>();
            Invert(monkeys, invertedMonkeys, monkeys["root"].Monkey1);
            invertedMonkeys.Add(monkeys["root"].Monkey1, new Monkey(monkeys["root"].Monkey1, m2)); //we want it to be equal
            var answer = Dfs(invertedMonkeys, "humn");

            Console.WriteLine($"Part2: human must yells {answer}.");
        }
    }
}

