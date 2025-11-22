using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class ReindeerOlympics
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\14.txt");
            string line;
            var reindeers = new List<Reindeer>();
            var bestDistance = 0L;
            while((line=file.ReadLine())!=null)
            {
                var data = line.Replace(" can fly ", " ").Replace(" km/s for ", " ").Replace(" seconds, but then must rest for ", " ").Replace(" seconds.", "").Split(' ');
                var speed = int.Parse(data[1]);
                var speedingTime = int.Parse(data[2]);
                var rest = int.Parse(data[3]);
                var distance = 0L;
                var time=2503;
                while(time>0)
                {
                    distance += speed * Math.Min(time, speedingTime);
                    time -= speedingTime + rest;
                }
                bestDistance = Math.Max(bestDistance, distance);
            }
            Console.WriteLine($"Part 2: The distance that the winning reindeer traveled is {bestDistance}");            
        }

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\14.txt");
            string line;
            var reindeers = new List<Reindeer>();
            var bestDistance = 0L;
            while((line=file.ReadLine())!=null)
            {
                reindeers.Add(new Reindeer(line));                
            }
            for (var time=0;time<2503;time++)
            {
                foreach(var reindeer in reindeers)
                {
                    reindeer.Run();
                    bestDistance = Math.Max(bestDistance, reindeer.Distance);
                }
                foreach(var reindeer in reindeers.Where(r => r.Distance==bestDistance))
                    reindeer.Score++;
            }
            Console.WriteLine($"Part 2: The score that the winning reindeer has is {reindeers.Max(r => r.Score)}");            
        }


        public class Reindeer
        {
            public int[] Speed;
            public int Time;
            public int Distance;
            public int Score;
            public string Name;

            public Reindeer(string input)
            {
                Distance = 0;
                var data = input.Replace(" can fly ", " ").Replace(" km/s for ", " ").Replace(" seconds, but then must rest for ", " ").Replace(" seconds.", "").Split(' ');
                Name = data[0];
                var speed = int.Parse(data[1]);
                var speedingTime = int.Parse(data[2]);
                var rest = int.Parse(data[3]);
                Speed = new int[speedingTime+rest];
                for (var i=0;i<speedingTime;i++)
                    Speed[i]=speed;
            }

            public void Run()
            {
                Distance += Speed[(Time++)%Speed.Length];                
            }
        }
    
    }
}
