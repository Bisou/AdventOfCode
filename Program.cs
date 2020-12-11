using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            var file = new System.IO.StreamReader(         @"C:\Users\Bisou\Documents\Seb\FacebookHackerCup\2020\RunningOnFumesChapter1\input");  
           // string line;
           // var adapters = new List<int>();
            //while ((line=file.ReadLine())!=null)
            //{
             //   adapters.Add(int.Parse(line));
            //}
            var intCode = new IntCodeRunner(file.ReadLine().Split(',').Select(int.Parse).ToArray());
            var output = intCode.Run();
            Console.WriteLine($"answer is {output}");
        }
    }

    //Advent of Code 2019... keep it until it is over
    class IntCodeRunner
    {
        private int[] data;
        public IntCodeRunner(int[] program)
        {
            data = new int[program.Length];
            for (var i=0;i<data.Length;i++) data[i]=program[i];
        }

        public int Run()
        {
            var input=5;
            var output=0;
            //data[1]=noun;
            //data[2]=verb;
            var i=0;
            while (i<data.Length)
            {
                var instruction = data[i];
                var operation = GetOperation(instruction);
                if (operation==99) 
                    break;
                else if (operation==1)
                {
                    Console.WriteLine($"{instruction},{data[i+1]},{data[i+2]},{data[i+3]}");
                    data[data[i+3]] = GetParameterValue(data[i+1], instruction,1) + GetParameterValue(data[i+2],instruction,2);
                    Console.WriteLine($"Adding {GetParameterValue(data[i+1], instruction,1)} and {GetParameterValue(data[i+2],instruction,2)} to store it in data[{data[i+3]}]");
                    i+=4;
                }
                else if (operation==2)
                {
                    Console.WriteLine($"{instruction},{data[i+1]},{data[i+2]},{data[i+3]}");
                    data[data[i+3]] = GetParameterValue(data[i+1], instruction,1) * GetParameterValue(data[i+2],instruction,2);
                    Console.WriteLine($"Multiply {GetParameterValue(data[i+1], instruction,1)} and {GetParameterValue(data[i+2],instruction,2)} to store it in data[{data[i+3]}]");
                    i+=4;
                }   
                else if (operation==3)
                {
                    data[data[i+1]]=input;
                    i+=2;
                }   
                else if (operation==4)
                {
                    output = GetParameterValue(data[i+1], instruction,1);
                    i+=2;
                }
                else if (operation == 5)
                {
                    if (GetParameterValue(data[i+1], instruction,1) != 0)
                        i = GetParameterValue(data[i+2],instruction,2);
                    else
                        i+=3;
                }
                else if (operation == 6)
                {
                    if (GetParameterValue(data[i+1], instruction,1) == 0)
                        i = GetParameterValue(data[i+2],instruction,2);
                    else
                        i+=3;
                }
                else if (operation == 7)
                {
                    if (GetParameterValue(data[i+1], instruction,1) < GetParameterValue(data[i+2],instruction,2))
                        data[data[i+3]] =1;
                    else
                        data[data[i+3]]=0;
                    i+=4;
                }
                else if (operation == 8)
                {
                    if (GetParameterValue(data[i+1], instruction,1) == GetParameterValue(data[i+2],instruction,2))
                        data[data[i+3]] =1;
                    else
                        data[data[i+3]]=0;
                    i+=4;
                }
            }
            return output;
        }

        private int GetOperation(int value)
        {
            return value % 100;
        }

        private int GetParameterValue(int value, int instruction, int index)
        {
            var key = instruction.ToString();
            var modePosition = key.Length-2-index;
            if (modePosition<0 || key[modePosition]=='0')
                return data[value];
            return value;
        }
    }
}
