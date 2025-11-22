using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class SomeAssemblyRequired
    {
        public static Dictionary<string, int> Registry;

        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\07Part1.txt");  
            Registry = new Dictionary<string, int>();
            string line;
            var links = new List<Link>();
            while((line=file.ReadLine())!=null)
            {
                links.Add(new Link(line));
            }

            while(links.Count>0) {
                foreach (var link in links)
                {
                    if (link.CanBeDone()) {
                        link.Process();
                    }
                }
                links.RemoveAll(link => link.Done);
            }
                
            Console.WriteLine($"Part 1: wire a contains {Registry["a"]}");            
        }
        
        class Link {
            public string Order;
            public string From1;
            public string From2;
            public string To;
            public bool Done = false;

            public Link(string line){
                var order = line
                    .Replace(" -> ", " ")
                    .Split(' ');
                if (order.Length == 2)                    
                {
                    Order="ASSIGN";
                    To = order[1];
                    From1 = order[0];
                }    
                else if (order.Length==3)
                {
                    Order = "NOT";
                    From1 = order[1];
                    To = order[2];
                }
                else if (order[1] == "AND")
                {
                    Order = order[1];
                    From1 = order[0];
                    From2 = order[2];
                    To = order[3];
                }                    
                else if (order[1] == "OR")
                {
                    Order = order[1];
                    From1 = order[0];
                    From2 = order[2];
                    To = order[3];
                }                    
                else if (order[1] == "LSHIFT")
                {
                    Order = order[1];
                    From1 = order[0];
                    From2 = order[2];
                    To = order[3];
                }                    
                else if (order[1] == "RSHIFT")
                {
                    Order = order[1];
                    From1 = order[0];
                    From2 = order[2];
                    To = order[3];
                }                    
            }

            private bool ValueExists(string from) {
                if (int.TryParse(from, out var value))
                    return true;
                if (SomeAssemblyRequired.Registry.ContainsKey(from)) 
                    return true;
                return false;
            }

            private int GetValue(string from) {
                if (int.TryParse(from, out var value))
                    return value;
                return SomeAssemblyRequired.Registry[from]; 
            }

            public bool CanBeDone() {
                if (Order == "ASSIGN")                 
                    return ValueExists(From1);
                else if (Order == "NOT")
                    return ValueExists(From1);
                return ValueExists(From1) && ValueExists(From2);
            }

            public void Process() {
                if (Order == "ASSIGN")                    
                    SomeAssemblyRequired.Registry[To] = GetValue(From1);
                else if (Order == "NOT")
                    SomeAssemblyRequired.Registry[To] = (~GetValue(From1)) & 65535;
                else if (Order == "AND")
                    SomeAssemblyRequired.Registry[To] = (GetValue(From1) & GetValue(From2));
                else if (Order == "OR")
                    SomeAssemblyRequired.Registry[To] = (GetValue(From1) | GetValue(From2));
                else if (Order == "LSHIFT")
                    SomeAssemblyRequired.Registry[To] = (GetValue(From1) << GetValue(From2));
                else if (Order == "RSHIFT")
                    SomeAssemblyRequired.Registry[To] = (GetValue(From1) >> GetValue(From2));
                Done = true;
            }
        }

        public static void SolvePart2()
        {
           
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\07Part2.txt");  
            Registry = new Dictionary<string, int>();
            string line;
            var links = new List<Link>();
            while((line=file.ReadLine())!=null)
            {
                links.Add(new Link(line));
            }

            while(links.Count>0) {
                foreach (var link in links)
                {
                    if (link.CanBeDone()) {
                        link.Process();
                    }
                }
                links.RemoveAll(link => link.Done);
            }
                
            Console.WriteLine($"Part 2: wire a contains {Registry["a"]}");            
        }
    }
}
