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
            //var phases = new []{0,1,2,3,4};
            var phases = new []{5,6,7,8,9};
            do
            {
                for (var i=0;i<5;i++)
                {
                    intCodes[i] = new IntCodeRunner(code, phases[i]);
                }
                var input=0;
                var amplifierId=0;
                while(true)
                {
                    input = intCodes[amplifierId].Run(input);
                    if (intCodes[amplifierId].Stopped) break;
                    amplifierId = (amplifierId+1)%5;
                }
                max = Math.Max(max, intCodes[4].LastOutput);
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
        private int index=0;
        public int LastOutput;
        public bool Stopped = false;
        public bool initPhaseDone = false;

        public IntCodeRunner(int[] program, int phase)
        {
            data = new int[program.Length];
            for (var i=0;i<data.Length;i++) data[i]=program[i];
            this.phase = phase;
        }

        public int Run(int input)
        {
            var output=0;
            while (index<data.Length)
            {
                var instruction = data[index];
                var operation = GetOperation(instruction);
                if (operation==99) 
                {
                    Stopped=true;    
                    break;
                }
                else if (operation==1)
                {
                    data[data[index+3]] = GetParameterValue(instruction,1) + GetParameterValue(instruction,2);
                    index+=4;
                }
                else if (operation==2)
                {
                    data[data[index+3]] = GetParameterValue(instruction,1) * GetParameterValue(instruction,2);
                    index+=4;
                }   
                else if (operation==3)
                {
                    if (initPhaseDone)
                        data[data[index+1]]=input;
                    else
                    {
                        data[data[index+1]]=phase;
                        initPhaseDone = true;
                    }
                    index+=2;
                }   
                else if (operation==4)
                {
                    output = GetParameterValue(instruction,1);
                    LastOutput = output;
                    index+=2;
                    return output;
                }
                else if (operation == 5)
                {
                    if (GetParameterValue(instruction,1) != 0)
                        index = GetParameterValue(instruction,2);
                    else
                        index+=3;
                }
                else if (operation == 6)
                {
                    if (GetParameterValue(instruction,1) == 0)
                        index = GetParameterValue(instruction,2);
                    else
                        index+=3;
                }
                else if (operation == 7)
                {
                    if (GetParameterValue(instruction,1) < GetParameterValue(instruction,2))
                        data[data[index+3]] =1;
                    else
                        data[data[index+3]]=0;
                    index+=4;
                }
                else if (operation == 8)
                {
                    if (GetParameterValue(instruction,1) == GetParameterValue(instruction,2))
                        data[data[index+3]] =1;
                    else
                        data[data[index+3]]=0;
                    index+=4;
                }
            }
            return output;
        }

        private int GetOperation(int value)
        {
            return value % 100;
        }

        private int GetParameterValue(int instruction, int shift)
        {
            var value = data[index+shift];
            var key = instruction.ToString();
            var modePosition = key.Length-2-shift;
            if (modePosition<0 || key[modePosition]=='0')
                return data[value];
            return value;
        }
    }
}
