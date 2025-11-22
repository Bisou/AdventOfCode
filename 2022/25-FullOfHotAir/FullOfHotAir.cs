using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class FullOfHotAir
    {
        public static void SolvePart1()
        {
            var sum = new int[50];            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\25-FullOfHotAir\input.txt");  
            string line;
            while((line=file.ReadLine())!=null) {
                Add(sum, line);
            }
            var answer = Display(sum);
            Console.WriteLine($"Part1: {answer}");
        }

        public static string Display(int[] sum) {
            var sb = new StringBuilder();
            var start=false;
            for (var i=sum.Length-1;i>=0;--i) {
                if (start || sum[i]!=0) {
                    start=true;
                    if (sum[i] >=0) 
                        sb.Append(sum[i]);
                    else if (sum[i]==-1)
                        sb.Append("-");
                    else if (sum[i]==-2) 
                        sb.Append("=");
                } 
            }
            return sb.ToString();
        }

        public static void Add(int[] sum, string number) {
            for (var i=0;i<sum.Length;++i) {
                var digit=0;
                if (i<number.Length) {
                    switch (number[number.Length-1-i]) {
                        case '2':
                            digit=2;
                            break;
                        case '1':
                            digit=1;
                            break;
                        case '0':
                            digit=0;
                            break;
                        case '-':
                            digit=-1;
                            break;
                        case '=':
                            digit=-2;
                            break;
                        default:
                            break;
                    }                  
                }
                sum[i]+=digit;
                if (sum[i]>2) {
                    sum[i]-=5;
                    sum[i+1]++;
                } else if (sum[i]<-2) {
                    sum[i]+=5;
                    sum[i+1]--;
                }
            }
        }

    }
}

