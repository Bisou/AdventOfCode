using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class DistressSignal
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\13-DistressSignal\input.txt");  
            var index=1;
            var sum = 0;
            string line;
            string first=null;
            string second=null;
            while((line=file.ReadLine())!=null) {
                if (line.Length==0) {
                    if (IsInRightOrder(first,second)) {
                        sum+=index;
                    }
                    index++;
                    first=null;
                    second=null;
                }
                else if (first==null)
                    first=line;
                else
                    second=line;
            }
                
            Console.WriteLine($"Part1: The sum of pair indexes is {sum}");
        }

        private static bool IsInRightOrder(string a, string b) {
            var a2 = Node.Create(a);
            var b2 = Node.Create(b);
            return IsInRightOrder(a2,b2)>=0;
        }
        

        private static int IsInRightOrder(Node a, Node b) {
            if (a.Value.HasValue) {
                //take a.Value
                if (b.Value.HasValue) {
                    //take b.Value
                    if (a.Value.Value==b.Value.Value)
                        return 0;
                    if (a.Value.Value<b.Value.Value)
                        return 1;
                    return -1;
                }
                else {
                    //b is a list => make a into a list
                    var newA = new Node();
                    newA.Children.Add(a);
                    return IsInRightOrder(newA, b);
                }
            } 
            //a is a list
            if (b.Value.HasValue) {
                //make b into a list
                var newB = new Node();
                newB.Children.Add(b);
                return IsInRightOrder(a, newB);
            }

            //both lists
            var i=0;
            while(i<a.Children.Count && i<b.Children.Count) {
                if (IsInRightOrder(a.Children[i],b.Children[i])==0)
                    ++i;
                else return IsInRightOrder(a.Children[i],b.Children[i]);
            }
            if (a.Children.Count==b.Children.Count)
                return 0;
            if (a.Children.Count<b.Children.Count)
                return 1;
            return -1;
        }
        

        public static void SolvePart2() {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\13-DistressSignal\input.txt");  
            string line;
            var nodes = new List<Node>();
            while((line=file.ReadLine())!=null) {
                if (line.Length!=0) {
                    nodes.Add(Node.Create(line));
                }
            }
            nodes.Add(Node.Create("[[2]]"));
            nodes.Add(Node.Create("[[6]]"));
            var ordered = nodes.OrderByDescending(n => n).Select(n => n.ToString()).ToArray();
            var decoderKey=1;
            for (var i=0;i<ordered.Length;++i) {
                if (ordered[i]=="[[2]]" || ordered[i]=="[[6]]")
                    decoderKey *= i+1;
            }

            
            Console.WriteLine($"Part2: The decoder key is {decoderKey}");
        }

        class Node : IComparable<Node> {
            public List<Node> Children = new List<Node>();
            public int? Value;
            public Node Parent;

            public int CompareTo(object obj) {
                if (obj == null) return 1;

                Node other = obj as Node;
                if (other != null)
                    return CompareTo(other);
                else
                throw new ArgumentException("Object is not a Node");
            }

            public int CompareTo(Node other) {
                return DistressSignal.IsInRightOrder(this, other);                
            }

            public override string ToString(){
                if (Value.HasValue) {
                    return Value.Value.ToString();
                }
                return $"[{string.Join(",",Children)}]";
            }

            public static Node Create(string a) {
                var result = new Node();
                var current = result;
                var val=0;
                
                for(var i=0;i<a.Length;++i) {
                    if (a[i]==',') {
                        if (a[i-1]!=']') {                                                        
                            current.Value = val;
                            val=0;   
                        }                         
                        var next = new Node();
                        next.Parent=current.Parent;
                        current.Parent.Children.Add(next);
                        current=next;
                    }
                    else if (a[i]=='[') {
                        var next = new Node();
                        next.Parent=current;
                        current.Children.Add(next);
                        current=next;
                    }
                    else if (a[i]==']') {
                        if (a[i-1]==']' || a[i-1]=='[') {
                            //nothing to do
                        } else {
                            current.Value = val;
                            val=0;
                        }
                        current = current.Parent;
                    }
                    else {
                        val = 10*val + a[i] - '0';
                    }
                }
                return result;
            }    
        }    
    }
}

