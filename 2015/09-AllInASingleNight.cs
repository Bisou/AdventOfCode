using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class AllInASingleNight
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\09.txt");  
            var graph = new Dictionary<string, List<(string City,int Distance)>>();
            string line;
            while((line=file.ReadLine())!=null)
            {
                var data = line.Replace(" to ", " ").Replace(" = ", " ").Split(' ');
                if (!graph.ContainsKey(data[0]))
                    graph.Add(data[0], new List<(string City, int Distance)>());
                if (!graph.ContainsKey(data[1]))
                    graph.Add(data[1], new List<(string City, int Distance)>());
                graph[data[0]].Add((data[1], int.Parse(data[2])));
                graph[data[1]].Add((data[0], int.Parse(data[2])));
            }
            var minPath = int.MaxValue;
            foreach (var start in graph.Keys)
            {
                var todo = new Queue<(List<string> Path, int Distance)>();
                todo.Enqueue((new List<string>{start}, 0));
                while(todo.Any())
                {
                    var current = todo.Dequeue();
                    foreach(var neighbor in graph[current.Path.Last()])
                    {
                        if (current.Path.Contains(neighbor.City)) continue;
                        if (current.Distance + neighbor.Distance >= minPath) continue;
                        var newPath = current.Path.ToList();
                        newPath.Add(neighbor.City);
                        if (newPath.Count == graph.Keys.Count)
                            minPath = current.Distance + neighbor.Distance;
                        else
                            todo.Enqueue((newPath, current.Distance+neighbor.Distance));
                    }
                }
            }

            Console.WriteLine($"Part 1: shortest path is {minPath}");            
        }
    
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\09.txt");  
            var graph = new Dictionary<string, List<(string City,int Distance)>>();
            string line;
            while((line=file.ReadLine())!=null)
            {
                var data = line.Replace(" to ", " ").Replace(" = ", " ").Split(' ');
                if (!graph.ContainsKey(data[0]))
                    graph.Add(data[0], new List<(string City, int Distance)>());
                if (!graph.ContainsKey(data[1]))
                    graph.Add(data[1], new List<(string City, int Distance)>());
                graph[data[0]].Add((data[1], int.Parse(data[2])));
                graph[data[1]].Add((data[0], int.Parse(data[2])));
            }
            var maxPath = 0;
            foreach (var start in graph.Keys)
            {
                var todo = new Queue<(List<string> Path, int Distance)>();
                todo.Enqueue((new List<string>{start}, 0));
                while(todo.Any())
                {
                    var current = todo.Dequeue();
                    foreach(var neighbor in graph[current.Path.Last()])
                    {
                        if (current.Path.Contains(neighbor.City)) continue;
                        var newPath = current.Path.ToList();
                        newPath.Add(neighbor.City);
                        if (newPath.Count == graph.Keys.Count)
                            maxPath = Math.Max(maxPath, current.Distance + neighbor.Distance);
                        else
                            todo.Enqueue((newPath, current.Distance+neighbor.Distance));
                    }
                }
            }

            Console.WriteLine($"Part 2: longest path is {maxPath}");            
        }
        

    }
}
