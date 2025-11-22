using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class BeaconScanner    
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\19-BeaconScanner\input1.txt");  
            string line;
            var scanners = new List<Scanner>();
            while((line=file.ReadLine())!=null)
            {
                if (line.StartsWith("---"))
                {
                    var scanner = new Scanner();
                    while((line=file.ReadLine())!="")
                    {
                        var data = line.Split(',').Select(int.Parse).ToArray();
                        scanner.Beacons.Add(new Beacon(data[0], data[1], data[2]));
                    }
                    scanners.Add(scanner);
                }
            }
            var correctScanners = new Scanner[scanners.Count];
            var nextCorrectScannerId=0;
            correctScanners[nextCorrectScannerId++] = scanners[0];
            scanners.RemoveAt(0);
            for (var i=0;i<correctScanners.Length;i++)
            {
                for (var j=scanners.Count-1;j>=0;j--)
                {
                    for (var direction=0;direction<24;direction++)
                    {
                        if (correctScanners[i].Matches(scanners[j], direction))
                        {
                            correctScanners[nextCorrectScannerId++] = scanners[j];
                            scanners.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            var beacons = correctScanners.SelectMany(sc => sc.Beacons.Select(b => (b.X,b.Y,b.Z))).Distinct().ToList();
            Console.WriteLine($"Part1: we have {beacons.Count} different beacons"); 
        }

        private class Scanner
        {
            public int X;
            public int Y;
            public int Z;

            public List<Beacon> Beacons = new List<Beacon>();

            public void TurnToward(int direction)
            {
                foreach (var beacon in Beacons)
                    beacon.TurnToward(direction);
            }

            public bool Matches(Scanner other, int direction)
            {
                for (var i=0;i<Beacons.Count;i++)
                {
                    for (var j=0;j<other.Beacons.Count;j++)
                    {
                        //i-j = first match
                        var jCoord = other.Beacons[j].Orient(direction);
                        var shift = (Beacons[i].X-jCoord.X,Beacons[i].Y-jCoord.Y,Beacons[i].Z-jCoord.Z);
                        var count=0;
                        for (var i2=0;i2<Beacons.Count;i2++)
                            for (var j2=0;j2<other.Beacons.Count;j2++)
                                if (Beacons[i2].Matches(other.Beacons[j2].Orient(direction), shift))
                                {
                                    count++;
                                    break;
                                }
                        if (count>=12)
                        {
                            other.TurnToward(direction);
                            other.Move(shift);
                            return true;
                        }
                    }
                }
                return false;
            }

            public void Move((int X,int Y,int Z) shift)
            {
                X = shift.X;
                Y = shift.Y;
                Z = shift.Z;
                foreach (var beacon in Beacons)
                    beacon.Move((X,Y,Z));
            }

            public int DistanceTo(Scanner other)
            {
                return Math.Abs(X-other.X) + Math.Abs(Y-other.Y) + Math.Abs(Z-other.Z);
            }
        }

        private class Beacon
        {
            public int X;
            public int Y;
            public int Z;
            
            public Beacon(int x, int y, int z)
            {
                X=x;
                Y=y;
                Z=z;
            }

            public void Move((int X, int Y, int Z) move)
            {
                X += move.X;
                Y += move.Y;
                Z += move.Z;
            }

            public override string ToString()
            {
                return $"({X},{Y},{Z})";
            }

            public void TurnToward(int direction)
            {
                var newData = Orient(direction);
                X=newData.Item1;
                Y=newData.Item2;
                Z=newData.Item3;
            }

            public bool Matches((int X,int Y,int Z) coord, (int X,int Y,int Z) shift)
            {
                return (X,Y,Z)==(coord.X+shift.X,coord.Y+shift.Y,coord.Z+shift.Z);
            }

            public (int X,int Y,int Z) Orient(int direction)
            {
                if (direction==0)
                    return (X,Y,Z);
                if (direction==1)
                    return (X,-Z,Y);
                if (direction==2)
                    return (X,-Y,-Z);
                if (direction==3)
                    return (X,Z,-Y);
                if (direction==4)
                    return (-X,-Y,Z);
                if (direction==5)
                    return (-X,-Z,-Y);
                if (direction==6)
                    return (-X,Y,-Z);
                if (direction==7)
                    return (-X,Z,Y);
                if (direction==8)
                    return (Y,-X,Z);
                if (direction==9)
                    return (Y,-Z,-X);
                if (direction==10)
                    return (Y,X,-Z);
                if (direction==11)
                    return (Y,Z,X);
                if (direction==12)
                    return (-Y,X,Z);
                if (direction==13)
                    return (-Y,-Z,X);
                if (direction==14)
                    return (-Y,-X,-Z);
                if (direction==15)
                    return (-Y,Z,-X);
                if (direction==16)
                    return (Z,-Y,X);
                if (direction==17)
                    return (Z,-X,-Y);
                if (direction==18)
                    return (Z,Y,-X);
                if (direction==19)
                    return (Z,X,Y);
                if (direction==20)
                    return (-Z,Y,X);
                if (direction==21)
                    return (-Z,-X,Y);
                if (direction==22)
                    return (-Z,-Y,-X);
                return (-Z,X,-Y);
            }
        }

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\19-BeaconScanner\input1.txt");  
            string line;
            var scanners = new List<Scanner>();
            while((line=file.ReadLine())!=null)
            {
                if (line.StartsWith("---"))
                {
                    var scanner = new Scanner();
                    while((line=file.ReadLine())!="")
                    {
                        var data = line.Split(',').Select(int.Parse).ToArray();
                        scanner.Beacons.Add(new Beacon(data[0], data[1], data[2]));
                    }
                    scanners.Add(scanner);
                }
            }
            var correctScanners = new Scanner[scanners.Count];
            var nextCorrectScannerId=0;
            correctScanners[nextCorrectScannerId++] = scanners[0];
            scanners.RemoveAt(0);
            for (var i=0;i<correctScanners.Length;i++)
            {
                for (var j=scanners.Count-1;j>=0;j--)
                {
                    for (var direction=0;direction<24;direction++)
                    {
                        if (correctScanners[i].Matches(scanners[j], direction))
                        {
                            correctScanners[nextCorrectScannerId++] = scanners[j];
                            scanners.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            var maxDist=0;
            for (var i=0;i<correctScanners.Length-1;i++)
                for (var j=i+1;j<correctScanners.Length;j++)
                    maxDist = Math.Max(maxDist, correctScanners[i].DistanceTo(correctScanners[j]));
            Console.WriteLine($"Part2: max Distance between 2 scanners is {maxDist}"); 
        }   
    }
}
