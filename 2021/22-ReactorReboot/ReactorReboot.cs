using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class ReactorReboot    
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\22-ReactorReboot\inputSample1.5.txt");  
            string line;
            var reactor = new bool[101,101,101];
            var count=0;
            while((line=file.ReadLine())!=null)
            {
                var data = line.Replace(" x=",",").Replace("y=","").Replace("z=","").Replace("..",",").Split(',');
                var xMin=int.Parse(data[1]);
                var xMax=int.Parse(data[2]);
                var yMin=int.Parse(data[3]);
                var yMax=int.Parse(data[4]);
                var zMin=int.Parse(data[5]);
                var zMax=int.Parse(data[6]);
                if (xMin<-50 || xMax>50 || yMin<-50 || yMax>50 ||zMin<-50 || zMax>50) continue;
                for (var x=xMin;x<=xMax;x++)
                    for (var y=yMin;y<=yMax;y++)
                        for (var z=zMin;z<=zMax;z++)
                        {
                            if (reactor[x+50,y+50,z+50] && data[0]=="off")
                            {
                                count--;
                                reactor[x+50,y+50,z+50]=false;
                            }
                            if (!reactor[x+50,y+50,z+50] && data[0]=="on")
                            {
                                count++;
                                reactor[x+50,y+50,z+50]=true;
                            }
                        }
                        
                Console.WriteLine($"{line} => {count} cubes are on");
            }

            Console.WriteLine($"Part 1: {count} cubes are on"); 
        }

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\22-ReactorReboot\input1.txt");  
            string line;
            var intervalsOn = new List<Interval>();
            while((line=file.ReadLine())!=null)
            {
                //Console.WriteLine(line);
                var nextIntervalsOn = new List<Interval>();
                var data = line.Replace(" x=",",").Replace("y=","").Replace("z=","").Replace("..",",").Split(',');
                var xMin=int.Parse(data[1]);
                var xMax=int.Parse(data[2]);
                var yMin=int.Parse(data[3]);
                var yMax=int.Parse(data[4]);
                var zMin=int.Parse(data[5]);
                var zMax=int.Parse(data[6]);
                if (data[0]=="on")
                {
                    var turnedOn = new Interval(xMin, xMax, yMin, yMax, zMin, zMax);
                    foreach(var interval in intervalsOn)
                        nextIntervalsOn.AddRange(interval.MergeOn(turnedOn));
                    nextIntervalsOn.Add(turnedOn);
                }
                else
                {
                    var turnedOff = new Interval(xMin, xMax, yMin, yMax, zMin, zMax);
                    foreach(var interval in intervalsOn)
                    {   
                        if (interval.Intersects(turnedOff)) 
                        {
                            var next = interval.MergeOff(turnedOff);
                            //Console.WriteLine($"For {interval}, we go from {interval.Count} to {next.Sum(i => i.Count)}");
                            nextIntervalsOn.AddRange(next);
                        }
                        else
                            nextIntervalsOn.Add(interval);
                    }
                }
                intervalsOn = nextIntervalsOn;
                //Console.WriteLine($"{intervalsOn.Sum(interval => interval.Count)} cubes are on");
            }

            Console.WriteLine($"Part 2: {intervalsOn.Sum(interval => interval.Count)} cubes are on"); 
        }

        public class Interval
        {
            public override string ToString()
            {
                return $"x=[{Xmin}..{Xmax}], y=[{Ymin}..{Ymax}], z=[{Zmin}..{Zmax}]";
            }
             
            public long Xmin;
            public long Xmax;
            public long Ymin;
            public long Ymax;
            public long Zmin;
            public long Zmax;

            public Interval(long xMin, long xMax, long yMin, long yMax, long zMin, long zMax)
            {
                Xmin=xMin;
                Xmax=xMax;
                Ymin=yMin;
                Ymax=yMax;
                Zmin=zMin;
                Zmax=zMax;
            }

            public Interval Clone => new Interval(Xmin, Xmax, Ymin, Ymax, Zmin, Zmax);

            public long Count => (Xmax-Xmin+1)*(Ymax-Ymin+1)*(Zmax-Zmin+1);
            
            public bool Intersects(Interval other)
            {
                if (Xmax < other.Xmin) return false;
                if (other.Xmax < Xmin) return false;
                if (Ymax < other.Ymin) return false;
                if (other.Ymax < Ymin) return false;
                if (Zmax < other.Zmin) return false;
                if (other.Zmax < Zmin) return false;
                return true;
            }

            public List<Interval> MergeOn(Interval turnedOn)
            {
                var result = new List<Interval>();

                if (Intersects(turnedOn))
                {
                    var intersection = new Interval(Math.Max(Xmin, turnedOn.Xmin),
                                                    Math.Min(Xmax, turnedOn.Xmax),
                                                    Math.Max(Ymin, turnedOn.Ymin),
                                                    Math.Min(Ymax, turnedOn.Ymax),
                                                    Math.Max(Zmin, turnedOn.Zmin),
                                                    Math.Min(Zmax, turnedOn.Zmax));
                    result.AddRange(MergeOff(intersection));
                }
                else
                    result.Add(this);
                return result;
            }

            public List<Interval> MergeOff(Interval turnedOff)
            {
                var start = new List<Interval>{this};
                //cut on X
                var cutX = new List<Interval>();
                foreach(var interval in start)
                {
                    if (turnedOff.Xmin <= interval.Xmin && interval.Xmin <= turnedOff.Xmax && turnedOff.Xmax <= interval.Xmax)
                    {
                        cutX.Add(new Interval(interval.Xmin, turnedOff.Xmax, interval.Ymin, interval.Ymax, interval.Zmin, interval.Zmax));
                        if (turnedOff.Xmax < interval.Xmax)
                            cutX.Add(new Interval(turnedOff.Xmax+1, interval.Xmax, interval.Ymin, interval.Ymax, interval.Zmin, interval.Zmax));
                    }
                    else if (interval.Xmin < turnedOff.Xmin && turnedOff.Xmax < interval.Xmax)
                    {
                        if (interval.Xmin < turnedOff.Xmin)
                            cutX.Add(new Interval(interval.Xmin, turnedOff.Xmin-1, interval.Ymin, interval.Ymax, interval.Zmin, interval.Zmax));
                        cutX.Add(new Interval(turnedOff.Xmin, turnedOff.Xmax, interval.Ymin, interval.Ymax, interval.Zmin, interval.Zmax));
                        if (turnedOff.Xmax < interval.Xmax)
                            cutX.Add(new Interval(turnedOff.Xmax+1, interval.Xmax, interval.Ymin, interval.Ymax, interval.Zmin, interval.Zmax));
                    }
                    else if (interval.Xmin <= turnedOff.Xmin && turnedOff.Xmin <= interval.Xmax && interval.Xmax <= turnedOff.Xmax)
                    {
                        if (interval.Xmin < turnedOff.Xmin)
                            cutX.Add(new Interval(interval.Xmin, turnedOff.Xmin-1, interval.Ymin, interval.Ymax, interval.Zmin, interval.Zmax));
                        cutX.Add(new Interval(turnedOff.Xmin, interval.Xmax, interval.Ymin, interval.Ymax, interval.Zmin, interval.Zmax));
                    }   
                    else  
                        cutX.Add(interval);               
                }

                //cut on Y
                var cutY = new List<Interval>();
                foreach(var interval in cutX)
                {
                    if (turnedOff.Ymin <= interval.Ymin && interval.Ymin <= turnedOff.Ymax && turnedOff.Ymax <= interval.Ymax)
                    {
                        cutY.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, turnedOff.Ymax, interval.Zmin, interval.Zmax));
                        if (turnedOff.Ymax < interval.Ymax)
                            cutY.Add(new Interval(interval.Xmin, interval.Xmax, turnedOff.Ymax+1, interval.Ymax, interval.Zmin, interval.Zmax));
                    }
                    else if (interval.Ymin < turnedOff.Ymin && turnedOff.Ymax < interval.Ymax)
                    {
                        if (interval.Ymin < turnedOff.Ymin)
                            cutY.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, turnedOff.Ymin-1, interval.Zmin, interval.Zmax));
                        cutY.Add(new Interval(interval.Xmin, interval.Xmax, turnedOff.Ymin, turnedOff.Ymax, interval.Zmin, interval.Zmax));
                        if (turnedOff.Ymax < interval.Ymax)
                            cutY.Add(new Interval(interval.Xmin, interval.Xmax, turnedOff.Ymax+1, interval.Ymax, interval.Zmin, interval.Zmax));
                    }
                    else if (interval.Ymin <= turnedOff.Ymin && turnedOff.Ymin <= interval.Ymax && interval.Ymax <= turnedOff.Ymax)
                    {
                        if (interval.Ymin < turnedOff.Ymin)
                            cutY.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, turnedOff.Ymin-1, interval.Zmin, interval.Zmax));
                        cutY.Add(new Interval(interval.Xmin, interval.Xmax, turnedOff.Ymin, interval.Ymax, interval.Zmin, interval.Zmax));
                    }   
                    else  
                        cutY.Add(interval);               
                }
                //cut on Z
                var cutZ = new List<Interval>();
                foreach(var interval in cutY)
                {
                    if (turnedOff.Zmin <= interval.Zmin && interval.Zmin <= turnedOff.Zmax && turnedOff.Zmax <= interval.Zmax)
                    {
                        cutZ.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, interval.Ymax, interval.Zmin, turnedOff.Zmax));
                        if (turnedOff.Zmax < interval.Zmax)
                            cutZ.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, interval.Ymax, turnedOff.Zmax+1, interval.Zmax));
                    }
                    else if (interval.Zmin < turnedOff.Zmin && turnedOff.Zmax < interval.Zmax)
                    {
                        if (interval.Zmin < turnedOff.Zmin)
                            cutZ.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, interval.Ymax, interval.Zmin, turnedOff.Zmin-1));
                        cutZ.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, interval.Ymax, turnedOff.Zmin, turnedOff.Zmax));
                        if (turnedOff.Zmax < interval.Zmax)
                            cutZ.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, interval.Ymax, turnedOff.Zmax+1, interval.Zmax));
                    }
                    else if (interval.Zmin <= turnedOff.Zmin && turnedOff.Zmin <= interval.Zmax && interval.Zmax <= turnedOff.Zmax)
                    {
                        if (interval.Zmin < turnedOff.Zmin)
                            cutZ.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, interval.Ymax, interval.Zmin, turnedOff.Zmin-1));
                        cutZ.Add(new Interval(interval.Xmin, interval.Xmax, interval.Ymin, interval.Ymax, turnedOff.Zmin, interval.Zmax));
                    }   
                    else  
                        cutZ.Add(interval);               
                }

                //Filter out the part turned off
                return cutZ.Where(interval => !interval.Intersects(turnedOff)).ToList();
            }
        }
    }
}
