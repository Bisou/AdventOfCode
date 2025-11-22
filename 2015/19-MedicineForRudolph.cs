using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class MedicineForRudolph
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\19.txt");
            var molecules = new HashSet<string>();
            var changes = new List<string[]>();
            string line=file.ReadLine();
            while (line.Length>1) {
                var data = line.Split(' ');
                changes.Add(new[] {data[0],data[2]});
                line=file.ReadLine();
            }
            var origin = file.ReadLine();
            var sb = new StringBuilder();
            foreach(var change in changes) {
                var parts = origin.Split(change[0]);

                for (var i=1;i<parts.Length;++i) {
                    sb.Clear();
                    sb.Append(parts[0]);
                    for (var j=1;j<parts.Length;++j) {
                        if (j==i)
                            sb.Append(change[1]);
                        else
                            sb.Append(change[0]);
                        sb.Append(parts[j]);
                    }
                    molecules.Add(sb.ToString());
                }
            }            
            
            Console.WriteLine($"Part 1: after 1 change, we have {molecules.Count()} different molecules");            
        }

        
        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\19.txt");
            var changes = new List<string[]>();
            string line=file.ReadLine();
            while (line.Length>1) {
                var data = line.Split(' ');
                changes.Add(new[]{ data[2], data[0] });
                line=file.ReadLine();
            }
            changes = changes.OrderByDescending(ch => ch[0].Length).ToList();
            var target = "e";
            var current = file.ReadLine();
            var steps = 0;
            while(true) {
                ++steps;
                foreach(var change in changes) {
                    var idx=current.IndexOf(change[0]);
                    if(idx>=0) {      
                        var sb = new StringBuilder(current);
                        sb.Remove(idx, change[0].Length);
                        sb.Insert(idx, change[1]);
                        current = sb.ToString();
                        if (current==target) {
                            Console.WriteLine($"Part 2: We can reach the target after {steps} steps");   
                            return;
                        }
                        Console.WriteLine($"Step {steps}: Length = {current.Length}: {current}");   
                        break;
                    }     
                }                                
            }
            Console.WriteLine($"Part 2: We cannot reach the target T_T");                                                    
        }      
    }
}
