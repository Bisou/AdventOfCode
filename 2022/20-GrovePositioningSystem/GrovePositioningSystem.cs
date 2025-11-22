using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class GrovePositioningSystem
    {
        public static void SolvePart1()
        {
            var list = new List<Number>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\20-GrovePositioningSystem\input.txt");  
            string line;
            while((line=file.ReadLine())!=null) {
                list.Add(new Number(int.Parse(line)));
            }

            for (var i=0;i<list.Count;) {
                if (list[i].Moved) {
                    ++i;
                } else {
                    var element = list[i];
                    var newPosition = i+element.Value;
                    while (newPosition<0) newPosition+= list.Count-1;
                    while (newPosition>=list.Count) newPosition-=list.Count-1;
                    //newPosition = (newPosition+list.Count)%list.Count;
                    list.RemoveAt(i);
                    element.Moved=true;
                    list.Insert(newPosition, element);
                }
            }
            var sum=0;
            var zeroPosition=-1;
            for(var i=0;i<list.Count;++i) 
                if (list[i].Value==0) {
                    zeroPosition=i;
                    break;
                }
            sum+=list[(zeroPosition+1000)%list.Count].Value;
            sum+=list[(zeroPosition+2000)%list.Count].Value;
            sum+=list[(zeroPosition+3000)%list.Count].Value;
            Console.WriteLine($"Part1: sum of all coordinates is {sum}.");
        }
        
        public class Number {
            public int Value;
            public bool Moved;            

            public override string ToString(){
                return $"{Value}-{Moved}";
            }

            public Number(int val)
             {
                Value=val;
             }
        }
        
        public class Number2 {
            public long Value;
            public int OriginalOrder;   
            public int CurrentOrder;         

            public override string ToString(){
                return $"{Value}-{OriginalOrder}-{CurrentOrder}";
            }

            public Number2(long val, int order)
             {
                Value=val;
                OriginalOrder = order;
                CurrentOrder = order;
             }
        }

        public static void SolvePart2()
        {
            var list = new List<Number2>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\20-GrovePositioningSystem\input.txt");  
            string line;
            long key = 811589153;
            var order=0;
            var currentOrder = new Dictionary<int, Number2>();
            var originalOrder = new Dictionary<int, Number2>();
            var zeroOriginalPosition=-1;
            while((line=file.ReadLine())!=null) {
                var number = new Number2(long.Parse(line)*key, order);
                list.Add(number);
                originalOrder.Add(order, number);
                currentOrder.Add(order, number);
                if (number.Value==0) zeroOriginalPosition=order;
                ++order;
            }
            var n = order;
            Console.WriteLine($"{DateTime.Now}: starting with {n} elements");

            for (var turn=0;turn<10;++turn) {
                for (order=0;order<n;++order) {
                    var element = originalOrder[order];
                    var origin=element.CurrentOrder;
                    var newPosition = (origin+element.Value)%(n-1);
                    while (newPosition<0) newPosition+= n-1;
                    while (newPosition>=n) newPosition-=n-1;
                    
                    list.RemoveAt(origin);
                    list.Insert((int)newPosition, element);                    
                    if (newPosition > origin) {
                        for (var i=origin;i<newPosition;++i) {
                            //set new orders
                            currentOrder[i]=currentOrder[i+1];
                            currentOrder[i].CurrentOrder=i;
                        }
                    } else {
                        for (var i=origin;i>newPosition;--i) {
                            //set new orders
                            currentOrder[i]=currentOrder[i-1];
                            currentOrder[i].CurrentOrder=i;
                        }
                    }
                    currentOrder[(int)newPosition]=element;
                    element.CurrentOrder=(int)newPosition;
                }
                Console.WriteLine($"{DateTime.Now}: completed turn {turn+1}");
            }
            var sum=0L;
            var zeroPosition=originalOrder[zeroOriginalPosition].CurrentOrder;
            sum+=currentOrder[(zeroPosition+1000)%n].Value;
            sum+=currentOrder[(zeroPosition+2000)%n].Value;
            sum+=currentOrder[(zeroPosition+3000)%n].Value;
            Console.WriteLine($"Part2: sum of all coordinates is {sum}.");
        }
    }
}

