using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class PassagePathing    
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\12-PassagePathing\input1.txt");  
            var neighbours = new Dictionary<string,List<string>>();
            string line;  
            while ((line = file.ReadLine()) != null)
            {
                var data = line.Split('-');
                if (!neighbours.ContainsKey(data[0]))
                    neighbours.Add(data[0], new List<string>());
                if (!neighbours.ContainsKey(data[1]))
                    neighbours.Add(data[1], new List<string>());
                neighbours[data[0]].Add(data[1]);
                neighbours[data[1]].Add(data[0]);
            }

            var paths=0;
            var todo = new Queue<List<string>>();
            todo.Enqueue(new List<string>{"start"});
            while(todo.Any())
            {
                var current = todo.Dequeue();
                foreach(var next in neighbours[current.Last()])
                {
                    if (next=="end")
                    {
                        paths++;
                        continue;
                    }
                    if (next.ToLowerInvariant()==next && current.Contains(next))
                            continue;
                    var nextPath = current.ToList();
                    nextPath.Add(next);
                    todo.Enqueue(nextPath);
                }
            }
            Console.WriteLine($"Part1: we have a total of {paths} paths"); 
        }
        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\12-PassagePathing\input1.txt");  
            var neighbours = new Dictionary<string,List<string>>();
            string line;  
            while ((line = file.ReadLine()) != null)
            {
                var data = line.Split('-');
                if (!neighbours.ContainsKey(data[0]))
                    neighbours.Add(data[0], new List<string>());
                if (!neighbours.ContainsKey(data[1]))
                    neighbours.Add(data[1], new List<string>());
                neighbours[data[0]].Add(data[1]);
                neighbours[data[1]].Add(data[0]);
            }

            var paths=0;
            var todo = new Queue<(List<string> Path, bool SmallCaveDoubled)>();
            todo.Enqueue((new List<string>{"start"}, false));
            while(todo.Any())
            {
                var current = todo.Dequeue();
                foreach(var next in neighbours[current.Path.Last()])
                {
                    var smallCaveDoubled = current.SmallCaveDoubled;
                    if (next=="start") continue;
                    if (next=="end")
                    {
                        paths++;
                        //Console.WriteLine($"Path found: {string.Join("-", current)}-end");
                        continue;
                    }
                    if (next.ToLowerInvariant()==next && current.Path.Contains(next))
                    {
                        if (smallCaveDoubled)
                            continue;
                        smallCaveDoubled = true;
                    }  
                        
                    var nextPath = current.Path.ToList();
                    nextPath.Add(next);
                    todo.Enqueue((nextPath,smallCaveDoubled));
                }
            }
            Console.WriteLine($"Part2: we have a total of {paths} paths");
        }   
    }
}
