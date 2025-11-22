using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class SupplyStacks
    {
        public static void SolvePart1()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\05-SupplyStacks\input.txt");  
            var input=new Stack<string>();
            string line;
            while((line = file.ReadLine())!=null) {
                if (line.Length==0) break;
                input.Push(line);
            }
            line = input.Pop();
            var size = line[line.Length-2] - '0';
            var crates = Enumerable.Range(0,size).Select(x => new Stack<char>()).ToArray();
            while(input.Any()) {
                var data = input.Pop();
                for (var i=0;i<size;++i)
                    if(data[1+4*i]!=' ')
                        crates[i].Push(data[1+4*i]);
            }
            
            while((line = file.ReadLine())!=null) {
                var order = line.Replace("move ","").Replace(" from "," ").Replace(" to "," ").Split(' ').Select(int.Parse).ToArray();
                for (var i=0;i<order[0];++i)
                    crates[order[2]-1].Push(crates[order[1]-1].Pop());
            }
            var sb = new StringBuilder();
            for (var i=0;i<size;++i)
                sb.Append(crates[i].Peek());
            Console.WriteLine($"Part1: top crates are {sb}");
        }
     
        public static void SolvePart2()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\05-SupplyStacks\input.txt");  
            var input=new Stack<string>();
            string line;
            while((line = file.ReadLine())!=null) {
                if (line.Length==0) break;
                input.Push(line);
            }
            line = input.Pop();
            var size = line[line.Length-2] - '0';
            var crates = Enumerable.Range(0,size).Select(x => new Stack<char>()).ToArray();
            while(input.Any()) {
                var data = input.Pop();
                for (var i=0;i<size;++i)
                    if(data[1+4*i]!=' ')
                        crates[i].Push(data[1+4*i]);
            }
            
            while((line = file.ReadLine())!=null) {
                var order = line.Replace("move ","").Replace(" from "," ").Replace(" to "," ").Split(' ').Select(int.Parse).ToArray();
                var temp = new Stack<char>();
                for (var i=0;i<order[0];++i)
                    temp.Push(crates[order[1]-1].Pop());
                for (var i=0;i<order[0];++i)
                    crates[order[2]-1].Push(temp.Pop());
            }
            var sb = new StringBuilder();
            for (var i=0;i<size;++i)
                sb.Append(crates[i].Peek());
            Console.WriteLine($"Part1: top crates are {sb}");
        }
    }
}
