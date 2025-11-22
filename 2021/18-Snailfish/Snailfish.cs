using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Snailfish    
    {
        public static void SolvePart1()
        {
            var sampleTree1 = ConvertToTree("[1,2]");
            var sampleTree2 = ConvertToTree("[[1,2],3]");
            var sampleTree3 = ConvertToTree("[9,[8,7]]");
            var sampleTree4 = ConvertToTree("[[1,9],[8,5]]");
            var sampleTree5 = ConvertToTree("[[[[1,2],[3,4]],[[5,6],[7,8]]],9]");
            var sampleTree6 = ConvertToTree("[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]");
            var sampleTree7 = ConvertToTree("[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]");
            var sampleTree8 = ConvertToTree("[[[[4,3],4],4],[7,[[8,4],9]]]");
            var sampleTree9 = ConvertToTree("[1,1]");
            var sum8And9 = Add(sampleTree8, sampleTree9);
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\18-Snailfish\input1.txt");  
            string line;
            var sum = ConvertToTree(file.ReadLine());
            while((line=file.ReadLine())!=null)
            {
                var y = ConvertToTree(line);
                sum = Add(sum, y);
            }
            Console.WriteLine($"Part1: final magnitude of the sum {sum} is {sum.Magnitude()}"); 
        }

        private static Node Add(Node left, Node right)
        {
            var sum = new Node();
            sum.Left=left;
            left.Parent=sum;
            sum.Right=right;
            right.Parent=sum;
            while(true)
            {
                if (TryToExplode(sum))
                    continue;
                if (TryToSplit(sum))
                    continue;
                break;
            }
            return sum;
        }

        private static bool TryToExplode(Node node, int depth=0)
        {
            if (node.Value.HasValue) return false;
            if (depth>=4)
            {
                var leftValue=node.Left.Value.Value;
                var rightValue=node.Right.Value.Value;
                node.Left=null;
                node.Right=null;
                node.Value=0;
                var left = node.Parent;
                var prev = node;
                while(left!=null)
                {
                    if (left.Left!=prev)
                    {
                        left = left.Left;
                        break;
                    }
                    left = left.Parent;
                    prev=prev.Parent;
                }
                if (left!=null)
                {
                    while(!left.Value.HasValue)
                        left = left.Right;
                    left.Value += leftValue;
                }
                var right = node.Parent;
                prev = node;
                //up until I can go right
                while(right!=null)
                {
                    if (right.Right!=prev)
                    {
                        right = right.Right;
                        break;
                    }
                    right = right.Parent;
                    prev=prev.Parent;
                }
                //now go left as far as I can
                if (right!=null)
                {
                    while(!right.Value.HasValue)
                        right = right.Left;
                    right.Value += rightValue;
                }
               
                return true;
            }
            if (TryToExplode(node.Left, depth+1)) return true;
            return TryToExplode(node.Right, depth+1);
        }

        private static bool TryToSplit(Node node)
        {
            if (node==null) return false;
            if (node.Value.HasValue && node.Value.Value>=10)
            {
                var leftValue = node.Value.Value/2;
                var rightValue = node.Value.Value-leftValue;
                node.Left = new Node(node, leftValue);
                node.Right = new Node(node, rightValue);
                node.Value=null;
                return true;
            }
            if (TryToSplit(node.Left)) return true;
            return TryToSplit(node.Right);
        }

        private static Node ConvertToTree(string number)
        {
            Node root = null;
            Node current=root;
            foreach(var c in number)
            {
                if (c=='[')
                {
                    if (current==null)
                    {
                        root = new Node();
                        current=root;
                    }
                    else if (current.Left==null)
                    {
                        current.Left=new Node(current);
                        current=current.Left;
                    }
                    else
                    {
                        current.Right=new Node(current);
                        current=current.Right;
                    }
                }
                else if (c==']')
                {
                    current = current.Parent;
                }
                else if (c==',')
                {

                }
                else
                {
                    var val = c-'0';
                    if (current.Left==null)
                        current.Left = new Node(current, val);
                    else
                        current.Right = new Node(current, val);
                }
            }
            return root;
        }

        private class Node
        {
            public int? Value;
            public Node Left;
            public Node Right;
            public Node Parent;

            public Node(Node parent = null, int? value = null)
            {
                Value = value;
                Parent = parent;                                
            }

            public override string ToString()
            {
                if (Value.HasValue) 
                    return Value.ToString();
                return $"[{Left},{Right}]";
            }

            public long Magnitude()
            {
                if (Value.HasValue) return Value.Value;
                return 3*Left.Magnitude() + 2*Right.Magnitude();
            }
        }

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\18-Snailfish\input1.txt");  
            string line;
            var lines = new List<string>();
            while((line=file.ReadLine())!=null)
            {
                lines.Add(line);
            }
            var bestMagnitude = 0L;
            for (var i=0;i<lines.Count;i++)
                for (var j=0;j<lines.Count;j++)
                    if (i!=j)
                    {
                        var a = ConvertToTree(lines[i]);
                        var b = ConvertToTree(lines[j]);
                        var sum = Add(a,b);
                        var magnitude = sum.Magnitude();
                        bestMagnitude = Math.Max(bestMagnitude, magnitude);
                    }

            Console.WriteLine($"Part2: largest magnitude {bestMagnitude}"); 
        }   
    }
}
