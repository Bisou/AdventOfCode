using System.Text;

public class Day17
{
    private const int day = 17;
    private static long A;
    private static long B;
    private static long C;
    private static List<int> output;
    private static int index;
    public static List<int> powersOfTwo;
    private static int[] program; 

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        A = int.Parse(input[0].Split(' ')[2]);
        B = int.Parse(input[1].Split(' ')[2]);
        C = int.Parse(input[2].Split(' ')[2]);
        program = input[4].Substring(9).Split(',').Select(int.Parse).ToArray();
        index=0;
        output = new List<int>();
        powersOfTwo = new List<int>();
        var power=1;

        while(power<Int32.MaxValue/2) {
            powersOfTwo.Add(power);
            power*=2;
        }

        while (index<program.Length) {
            var instruction = program[index++];
            var operand = program[index++];            
            PerformOperation(instruction, operand);
        }
        Console.WriteLine($"Part1: result is {string.Join(",", output)}");
    }

    private static void PerformOperation(int instruction, int operand) {  
        int combo;      
        switch (instruction) {
            case 0: //adv
                combo = GetComboOperand(operand);
                A /= powersOfTwo[combo];
                break; 
            case 1: //bxl
                B ^= operand;
                break;
            case 2: //bst
                combo = GetComboOperand(operand);
                B = combo % 8;
                break;
            case 3: //jnz
                if (A!=0)
                    index = operand;
                //do not increase index after it (should be ok since I increased it before)
                break;
            case 4: //bxc
                B ^= C;
                break;
            case 5: //out
                combo = GetComboOperand(operand);
                output.Add(combo % 8);
                break;
            case 6: //bdv
                combo = GetComboOperand(operand);
                B = A / powersOfTwo[combo];
                break;
            case 7: //cdv
                combo = GetComboOperand(operand);
                C = A / powersOfTwo[combo];
                break;

        }
    }

    
    private static bool PerformOperation2(int instruction, int operand) {  
        long combo;      
        switch (instruction) {
            case 0: //adv
                combo = GetComboOperand2(operand);
                A /= powersOfTwo[(int)combo];
                break; 
            case 1: //bxl
                B ^= operand;
                break;
            case 2: //bst
                combo = GetComboOperand2(operand);
                B = combo % 8;
                break;
            case 3: //jnz
                if (A!=0)
                    index = operand;
                //do not increase index after it (should be ok since I increased it before)
                break;
            case 4: //bxc
                B ^= C;
                break;
            case 5: //out
                combo = GetComboOperand2(operand);
                output.Add((int)combo % 8);
                if (output.Count>program.Length) return false;
                if (output[^1]!=program[output.Count-1]) return false;
                break;
            case 6: //bdv
                combo = GetComboOperand2(operand);
                B = A / powersOfTwo[(int)combo];
                break;
            case 7: //cdv
                combo = GetComboOperand2(operand);
                C = A / powersOfTwo[(int)combo];
                break;

        }
        return true;
    }


    private static int GetComboOperand(int operand) {
        switch (operand)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return operand;
                break;
            case 4:
                return (int)A;
                break;
            case 5:
                return (int)B;
                break;
            case 6:
                return (int)C;
                break;
            default:
                throw new Exception($"Operand {operand} is not allowed");
        }
    }
    private static long GetComboOperand2(int operand) {
        switch (operand)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return operand;
                break;
            case 4:
                return A;
                break;
            case 5:
                return B;
                break;
            case 6:
                return C;
                break;
            default:
                throw new Exception($"Operand {operand} is not allowed");
        }
    }
    


    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);
        var input = Tool.ReadAll(day, dataType);             
        long a = int.Parse(input[0].Split(' ')[2]);
        var b = int.Parse(input[1].Split(' ')[2]);
        var c = int.Parse(input[2].Split(' ')[2]);
        program = input[4].Substring(9).Split(',').Select(int.Parse).ToArray();
        
        powersOfTwo = new List<int>();
        var power=1;

        while(power<Int32.MaxValue/2) {
            powersOfTwo.Add(power);
            power*=2;
        }

        for(a=0;a<long.MaxValue;++a) {
            index=0;
            output = new List<int>();
            A=a;
            B=b;
            C=c;
            var ok=true;
            while (index<program.Length ) {
                var instruction = program[index++];
                var operand = program[index++];            
                if (!PerformOperation2(instruction, operand)) {
                    ok=false;
                    break;
                }
            }
            if (ok && output.Count==program.Length) {
                Console.WriteLine($"Part2: A should be {a}");
                return;
            }

        }
        Console.WriteLine($"Part1: result is {string.Join(",", output)}");
    }
}
