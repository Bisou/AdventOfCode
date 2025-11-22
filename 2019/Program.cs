using System;
using System.Text;
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
            var hull = new int[10000,10000];
            var row=5000;
            var col=5000;
            var facing=0;//UP
            var rowShift=new []{-1,0,1,0};//UP,XXXXX,DOWN,XXXX
            var colShift=new []{0,1,0,-1};//XX,RIGHT,XXXX,LEFT
            var seen = new bool[10000,10000];
            var count=0;
            hull[row,col]=1;
            var input=2L;
            var minRow=row;
            var maxRow=row;
            var minCol=col;
            var maxCol=col;
            while(true)
            {
                input = hull[row,col];
                var color= intCode.Run(input);
                if (!seen[row,col])
                {
                    count++;
                    seen[row,col]=true;
                }
                hull[row,col]=(int)color;
                var turn = intCode.Run(input);
                if (turn==1)//right
                    facing = (facing+1)%4;
                else if (turn==0)//left
                    facing=(facing+3)%4;
                else
                {
                    var test=0;
                }
                Console.Error.WriteLine($"We are in (row={row},col={col}) facing {facing}, color is {color} and we turn {turn}");
                row+=rowShift[facing];
                col+=colShift[facing];
                minRow=Math.Min(minRow,row);
                maxRow=Math.Max(maxRow,row);
                minCol=Math.Min(minCol,col);
                maxCol=Math.Max(maxCol,col);
                if (intCode.Stopped) break;                    
            }
            for (var r = minRow;r<=maxRow;r++)
            {
                var sb=new StringBuilder();
                for (var c=minCol;c<=maxCol;c++)
                    sb.Append(hull[r,c]==1?'#':' ');
                Console.WriteLine(sb.ToString());
                
            }
            //Console.WriteLine($"{count}");
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
                    Console.Error.WriteLine($"requesting color {input}");
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
