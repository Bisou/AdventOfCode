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
            /*string line;
            var orders = new List<string>();
            while ((line=file.ReadLine())!=null)
            {
               orders.Add(line);
            }*/
            var code = file.ReadLine().Split(',').Select(long.Parse).ToArray();
            var intCode = new IntCodeRunner(code,0);
            //var phases = new []{5,6,7,8,9};
            var output = new List<long>();
            var input=1L;
            while(true)
            {
                input = intCode.Run(input);
                output.Add(input);
                if (intCode.Stopped) break;                    
            }
                
            Console.WriteLine($"{string.Join(",",output)}");
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
        private Dictionary<long,long> memory;
        private long relativeBase=0;
        private int phase;
        private long index=0;
        public long LastOutput;
        public bool Stopped = false;
        public bool initPhaseDone = true;//no phase

        public IntCodeRunner(long[] program, int phase)
        {
            memory = new Dictionary<long, long>();            
            for (var i=0;i<program.Length;i++) 
            {                
                SetMemory(i, program[i]);
            }
            this.phase = phase;
        }

        public long Run(long input)
        {
            var output=0L;
            while (true)
            {
                var instruction = ReadMemory(index);
                var operation = GetOperation(instruction);
                if (operation==99) 
                {
                    Stopped=true;    
                    break;
                }
                else if (operation==1)
                {
                    SetMemory(GetParameterValue(instruction,3,true), GetParameterValue(instruction,1) + GetParameterValue(instruction,2));
                    index+=4;
                }
                else if (operation==2)
                {
                    SetMemory(GetParameterValue(instruction,3,true), GetParameterValue(instruction,1) * GetParameterValue(instruction,2));
                    index+=4;
                }   
                else if (operation==3)
                {
                    if (initPhaseDone)
                        SetMemory(GetParameterValue(instruction,1,true),input);
                    else
                    {
                        SetMemory(GetParameterValue(instruction,1,true),phase);
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
                        SetMemory(GetParameterValue(instruction,3,true),1);
                    else
                        SetMemory(GetParameterValue(instruction,3,true),0);
                    index+=4;
                }
                else if (operation == 8)
                {
                    if (GetParameterValue(instruction,1) == GetParameterValue(instruction,2))
                        SetMemory(GetParameterValue(instruction,3,true),1);
                    else
                        SetMemory(GetParameterValue(instruction,3,true),0);
                    index+=4;
                }
                else if (operation == 9)
                {
                    relativeBase += GetParameterValue(instruction,1);
                    index+=2;
                }
            }
            return output;
        }

        private int GetOperation(long value)
        {
            return (int)(value % 100);
        }

        public const char PositionMode = '0';
        public const char ValueMode = '1';
        public const char RelativeMode = '2';
        
        private long GetParameterValue(long instruction, int shift, bool forWriting=false)
        {
            var value = ReadMemory(index+shift);
            var key = instruction.ToString();
            var modePosition = key.Length-2-shift;
            if (modePosition<0 || key[modePosition]==PositionMode)
                if (forWriting)
                    return value;
                else
                    return ReadMemory(value);
            else if (key[modePosition] == RelativeMode)
                if (forWriting)
                    return relativeBase + value;
                else
                    return ReadMemory(relativeBase + value);
            return value;
        }

        private void SetMemory(long address, long value)
        {
            memory[address] = value;
        }

        private long ReadMemory(long address)
        {
            if (!memory.ContainsKey(address))
                memory.Add(address,0);
            return memory[address];
        }
    }
}
