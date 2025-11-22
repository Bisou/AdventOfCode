using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class NoSpaceLeftOnDevice
    {
        public static long SmallSizes;

        public static void SolvePart1()
        {
            SmallSizes = 0;   
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\07-NoSpaceLeftOnDevice\input.txt");  
            var tree = BuildTree(file);
            tree.ComputeSize();

            Console.WriteLine($"Part1: small folders total size is {SmallSizes}");
        }
        
        private static Tree BuildTree(System.IO.StreamReader file) {
            string line=null;
            var head = new Tree("dir root");
            var current=head;
            while((line=file.ReadLine())!=null) {
                if (line[0]=='$') {
                    if (line=="$ cd /")
                        current = head;
                    else if (line=="$ ls") {
                        //do nothing
                    }
                    else {//change folder
                        var folder = line.Split(' ')[2];
                        if (folder=="..")
                            current = current.Parent;
                        else
                            current = current.Children.Single(c => c.Name==folder);
                    }
                } else {
                    var node = new Tree(line);
                    node.Parent = current;
                    current.Children.Add(node);
                }
            }
            return head;
        }

        private class Tree{
            public Tree Parent;
            public List<Tree> Children = new List<Tree>();
            public long Size;
            public string Name;
            public bool IsFile=false;

            public Tree(string input)
            {
                var data = input.Split(' ');
                if (data[0]!="dir") {
                    IsFile=true;
                    Size = long.Parse(data[0]);
                }
                Name=data[1];
            }

            public void ComputeSize()
            {
                if (IsFile) return;
                foreach(var child in Children)
                {
                    child.ComputeSize();
                    Size += child.Size;
                }
                if (Size<=100000)
                    NoSpaceLeftOnDevice.SmallSizes+=Size;
            }

            public void FindFolderToDelete(){
                if (!IsFile && Size>= NoSpaceLeftOnDevice.TotalSizeToFree && Size < NoSpaceLeftOnDevice.ToDelete)
                    NoSpaceLeftOnDevice.ToDelete = Size;
                foreach(var child in Children)
                {
                    child.FindFolderToDelete();
                }
            }
        }
        
        public static long ToDelete;
        public static long TotalSizeToFree;
        public static void SolvePart2()
        {
            ToDelete=9999999999999999;
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\07-NoSpaceLeftOnDevice\input.txt");  
            var tree = BuildTree(file);
            tree.ComputeSize();
            TotalSizeToFree = tree.Size - 40000000;
            tree.FindFolderToDelete();
            Console.WriteLine($"Part2: folder to delete size is {ToDelete}");
        }
    }
}
