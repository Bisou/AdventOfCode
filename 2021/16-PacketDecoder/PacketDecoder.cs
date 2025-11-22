using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class PacketDecoder    
    {
        public static void SolvePart1()
        {
           // var shouldBe6=GetSumOfVersionIds("D2FE28");
           // var shouldBe9=GetSumOfVersionIds("38006F45291200");
          //  var shouldBe16=GetSumOfVersionIds("8A004A801A8002F478");
            //  var shouldBe12=GetSumOfVersionIds("620080001611562C8802118E34");
           // var shouldBe23=GetSumOfVersionIds("C0015000016115A2E0802F182340");
           // var shouldBe31=GetSumOfVersionIds("A0016C880162017C3686B18A3D4780");
                        
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\16-PacketDecoder\input1.txt");  
            var versionId = GetSumOfVersionIds(file.ReadLine());

            Console.WriteLine($"Part1: total sum of versionIds is {versionId}"); 
        }
        private static int versionIdSum;
        private static int index;
        private static string binary;
        private const char SubPacketNumberType = '1';
        private static Dictionary<char,string> hexaMapping = new Dictionary<char, string>{
            {'0',"0000"},
            {'1',"0001"},
            {'2',"0010"},
            {'3',"0011"},
            {'4',"0100"},
            {'5',"0101"},
            {'6',"0110"},
            {'7',"0111"},
            {'8',"1000"},
            {'9',"1001"},
            {'A',"1010"},
            {'B',"1011"},
            {'C',"1100"},
            {'D',"1101"},
            {'E',"1110"},
            {'F',"1111"}
        };

        private static int GetSumOfVersionIds(string hexa)
        {
            ConvertHexaToBinary(hexa);
            index=0;
            versionIdSum=0;
            ReadPacket();


            return versionIdSum;
        }

        private static void ConvertHexaToBinary(string hexa)
        {
            binary = string.Join("", hexa.Select(h => hexaMapping[h]));
        }

        private static long ReadPacket()
        { 
            var spaceLeft = binary.Length - index;
            if (spaceLeft < 10)
                return 0;
            var versionId=0;
            for (var i=0;i<3;i++)
            {
                versionId*=2;
                versionId += binary[index++]-'0';
            }
            versionIdSum+=versionId;

            var packetType=0;
            for (var i=0;i<3;i++)
            {
                packetType*=2;
                packetType += binary[index++]-'0';
            }

            if (packetType==4)
            {
                //read litteral
                var value=ReadLitteral();
                return value;
            }
            else
            {
                //operation
                var subValues = new List<long>();
                var lengthType = binary[index++];
                if (lengthType==SubPacketNumberType)
                {
                    var numberOfPackets=0;
                    for (var i=0;i<11;i++)
                    {
                        numberOfPackets*=2;
                        numberOfPackets += binary[index++]-'0';
                    }
                    for (var p = 0;p<numberOfPackets;p++)
                    {
                        subValues.Add(ReadPacket());
                    }
                }
                else
                {
                    var bitLength=0;
                    for (var i=0;i<15;i++)
                    {
                        bitLength*=2;
                        bitLength += binary[index++]-'0';
                    }
                    var endOfPacket = Math.Min(index + bitLength, binary.Length);
                    while(index<endOfPacket)
                    {
                        subValues.Add(ReadPacket());
                    }
                }
                switch (packetType)
                {
                    case 0:
                        return subValues.Sum();
                    case 1:
                        return subValues.Aggregate(1L,(acc,v)=>acc*v);
                    case 2:
                        return subValues.Min();
                    case 3:
                        return subValues.Max();
                    case 5:
                        return subValues[0]>subValues[1]?1:0;
                    case 6:
                        return subValues[0]<subValues[1]?1:0;
                    case 7:
                        return subValues[0]==subValues[1]?1:0;
                    default:
                        return 0;
                }
            }
        }

        private static long ReadLitteral()
        {
            var value = 0L;
            bool keepReading;
            do  
            {
                keepReading = binary[index++]=='1';
                for (var i=0;i<4;i++)
                {
                    value*=2;
                    value += binary[index++]-'0';
                }
            }while(keepReading);
            return value;
        }

        public static void SolvePart2()
        {
            var shouldBe3=GetResult("C200B40A82");
            var shouldBe54=GetResult("04005AC33890");
            var shouldBe7=GetResult("880086C3E88112");
            var shouldBe9=GetResult("CE00C43D881120");
            var shouldBe1=GetResult("D8005AC2A8F0");
            var shouldBe0=GetResult("F600BC2D8F");
            var shouldAlsoBe0=GetResult("9C005AC2F8F0");
            var shouldAlsoBe1=GetResult("9C0141080250320F1802104A08");
                        
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\16-PacketDecoder\input1.txt");  
            var versionId = GetResult(file.ReadLine());

            Console.WriteLine($"Part2: result is {versionId}"); 
            //21788163907 is too low :(
        }   
        
        private static long GetResult(string hexa)
        {
            ConvertHexaToBinary(hexa);
            index=0;
            versionIdSum=0;
            return ReadPacket();
        }
    }
}
