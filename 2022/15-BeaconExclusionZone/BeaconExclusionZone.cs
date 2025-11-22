using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class BeaconExclusionZone
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\15-BeaconExclusionZone\input.txt");  
            var targetRow=2000000;
            var minCol=long.MaxValue;
            var maxCol = long.MinValue;
            string line;
            var sensors = new List<Sensor>();
            while((line=file.ReadLine())!=null) {
                if (line.Length!=0) {
                   var data = line.Replace("Sensor at x=","")
                                  .Replace(": closest beacon is at x="," ")
                                  .Replace(", y="," ")
                                  .Split(' ')
                                  .Select(long.Parse)
                                  .ToArray();
                    var sensor = new Sensor(data[1],data[0],data[3], data[2]);
                    sensors.Add(sensor);
                    minCol = Math.Min(minCol, sensor.GetMinCol());
                    maxCol = Math.Max(maxCol, sensor.GetMaxCol());
                }
            }
                
            long blocked=0;
            for (long col=minCol;col<=maxCol;++col) {
                if (sensors.Any(sensor => sensor.CloserThanBeacon(targetRow, col)))
                    blocked++;
            }



            Console.WriteLine($"Part1: in the row {targetRow}, the beacon cannot be in {blocked} positions");
        }

        class Sensor{
            public long Row;
            public long Col;
            public long BeaconRow;
            public long BeaconCol;
            public long Distance;

            public Sensor(long row, long col, long beaconRow, long beaconCol) {
                Row=row;
                Col=col;
                BeaconRow=beaconRow;
                BeaconCol=beaconCol;
                Distance =  Math.Abs(BeaconRow-Row)+Math.Abs(BeaconCol-Col);
            }

            public long GetMinCol() {
                var minCol = Math.Min(Col, BeaconCol);
                if (Col < BeaconCol) minCol = Math.Min(minCol, Col - (BeaconCol-Col));
                return minCol;
            }

            public long GetMinRow() {
                var minRow = Math.Min(Row, BeaconRow);
                if (Row < BeaconRow) minRow = Math.Min(minRow, Row - (BeaconRow-Row));
                return minRow;
            }

            public long GetMaxRow() {
                var maxRow = Math.Max(Row, BeaconRow);
                if (Row > BeaconRow) maxRow = Math.Max(maxRow, Row + (Row - BeaconRow));
                return maxRow;
            }

            public long GetMaxCol() {
                var maxCol = Math.Max(Col, BeaconCol);
                if (Col > BeaconCol) maxCol = Math.Max(maxCol, Col + (Col - BeaconCol));
                return maxCol;
            }

            public bool CloserThanBeacon(long row, long col) {
                return Math.Abs(row-Row)+Math.Abs(col-Col) <= Distance
                    && (row!=BeaconRow || col != BeaconCol);
            }

            public bool CloserThanBeacon2(long row, long col) {
                return Math.Abs(row-Row)+Math.Abs(col-Col) <= Distance;
            }
        }

        public static void SolvePart2() {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\15-BeaconExclusionZone\input.txt");              
            var minCol=0;
            var minRow=0;
            var maxCol = 4000000;
            var maxRow = 4000000;
            string line;
            var sensors = new List<Sensor>();
            while((line=file.ReadLine())!=null) {
                if (line.Length!=0) {
                   var data = line.Replace("Sensor at x=","")
                                  .Replace(": closest beacon is at x="," ")
                                  .Replace(", y="," ")
                                  .Split(' ')
                                  .Select(long.Parse)
                                  .ToArray();
                    var sensor = new Sensor(data[1],data[0],data[3], data[2]);
                    sensors.Add(sensor);
                }
            }
            
            for (long row=minRow;row<=maxRow;++row) {
                for (long col=minCol;col<=maxCol;++col) {
                    var ok=true;
                    for (var i=0;i<sensors.Count;++i) {
                        if (sensors[i].CloserThanBeacon2(row,col)) {
                            ok=false;
                            //if (sensors[i].Col>=col) {
                                col=sensors[i].Col+sensors[i].Distance-Math.Abs(row-sensors[i].Row);
                            //}
                            break;
                        }
                    }
                    if (ok) {
                        var distress = col*4000000+row;
                        
                        Console.WriteLine($"Part2: The distress signal is {distress}");
                        return;
                    }                    
                }
            }
        }
    }
}

