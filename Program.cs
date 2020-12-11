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
            var intCodes = new IntCodeRunner[5];
            var code = file.ReadLine().Split(',').Select(int.Parse).ToArray();
            var max=int.MinValue;
            var phases = new []{0,1,2,3,4};
            do
            {
                //Console.WriteLine($"Check {string.Join("",phases)}");
                for (var i=0;i<5;i++)
                {
                    intCodes[i] = new IntCodeRunner(code, phases[i]);
                }
                var input=0;
                for (var i=0;i<5;i++)
                {
                    input = intCodes[i].Run(input);
                }
                max = Math.Max(max, input);
            } while(!NextPermutation(phases));
            
            Console.WriteLine($"answer is {max}");
        }

        public static bool NextPermutation(int[] nums)
        {
            var count = nums.Length;
            var done = true;
            for (var i = count - 1; i > 0; i--)
            {
                var curr = nums[i];
                if (curr <= nums[i - 1])
                    continue;
                done = false;
                var prev = nums[i - 1];
                var currIndex = i;

                for (int j = i + 1; j < count; j++)
                {
                    var tmp = nums[j];
                    if (tmp <= curr && tmp > prev)
                    {
                        curr = tmp;
                        currIndex = j;
                    }
                }
                nums[currIndex] = prev;
                nums[i - 1] = curr;
                // Reverse the end
                for (int j = count - 1; j > i; j--, i++)
                {
                    var tmp = nums[j];
                    nums[j] = nums[i];
                    nums[i] = tmp;
                }
                break;
            }
            return done;
        }

    }

    //Advent of Code 2019... keep it until it is over
    class IntCodeRunner
    {
        private int[] data;
        private int phase;
        public IntCodeRunner(int[] program, int phase)
        {
            data = new int[program.Length];
            for (var i=0;i<data.Length;i++) data[i]=program[i];
            this.phase = phase;
        }

        public int Run(int input)
        {
            var initPhaseDone=false;
            var output=0;
            var i=0;
            while (i<data.Length)
            {
                var instruction = data[i];
                var operation = GetOperation(instruction);
                if (operation==99) 
                    break;
                else if (operation==1)
                {
                    data[data[i+3]] = GetParameterValue(data[i+1], instruction,1) + GetParameterValue(data[i+2],instruction,2);
                    i+=4;
                }
                else if (operation==2)
                {
                    data[data[i+3]] = GetParameterValue(data[i+1], instruction,1) * GetParameterValue(data[i+2],instruction,2);
                    i+=4;
                }   
                else if (operation==3)
                {
                    if (initPhaseDone)
                        data[data[i+1]]=input;
                    else
                    {
                        data[data[i+1]]=phase;
                        initPhaseDone = true;
                    }
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
