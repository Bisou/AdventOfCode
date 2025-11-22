using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class KnightsOfTheDinnerTable
    {
        private static int bestHappiness;
        private static int n;
        private static int[,] links;

        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\13.txt");
            string line;
            var people = new Dictionary<string,int>(); //name => index
            var nextIndex=0;
            links = new int[10,10]; //more than enough
            while((line=file.ReadLine())!=null)
            {
                var data = line.Replace(" would ", " ").Replace(" happiness units by sitting next to ", " ").Replace(".", "").Split(' ');
                var from = data[0];
                var to = data[3];
                if (!people.ContainsKey(from))
                    people.Add(from, nextIndex++);
                if (!people.ContainsKey(to))
                    people.Add(to, nextIndex++);
                var happiness = int.Parse(data[2]);
                if (data[1]=="lose") 
                    happiness *= -1;
                links[people[from],people[to]]+=happiness;
                links[people[to],people[from]]+=happiness;
            }

            n = people.Keys.Count();
            bestHappiness=0;
            var path = new List<int>{0};
            Dfs(path, 0);


            Console.WriteLine($"Part 1: The more happiness we can get is {bestHappiness}");            
        }

        private static void Dfs(List<int> path, int happiness) {
            if (path.Count==n) {
                //finished
                happiness+=links[path[0], path[n-1]];
                bestHappiness = Math.Max(bestHappiness, happiness);                
                return;
            }
            for (var i=1;i<n;++i) {
                if (path.Contains(i)) continue;
                happiness+=links[path.Last(),i];
                path.Add(i);
                Dfs(path, happiness);
                path.RemoveAt(path.Count-1);
                happiness-=links[path.Last(),i];
            }            
        }

        public static void SolvePart2()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\13Sample.txt");
            string line;
            var people = new Dictionary<string,int>(); //name => index
            var nextIndex=0;
            links = new int[10,10]; //more than enough
            people.Add("me", nextIndex++);
            while((line=file.ReadLine())!=null)
            {
                var data = line.Replace(" would ", " ").Replace(" happiness units by sitting next to ", " ").Replace(".", "").Split(' ');
                var from = data[0];
                var to = data[3];
                if (!people.ContainsKey(from))
                    people.Add(from, nextIndex++);
                if (!people.ContainsKey(to))
                    people.Add(to, nextIndex++);
                var happiness = int.Parse(data[2]);
                if (data[1]=="lose") 
                    happiness *= -1;
                links[people[from],people[to]]+=happiness;
                links[people[to],people[from]]+=happiness;
            }

            n = people.Keys.Count();
            bestHappiness=0;
            var path = new List<int>{0};
            Dfs(path, 0);


            Console.WriteLine($"Part 2: The more happiness we can get is {bestHappiness}");          
        }    
    }
}
