using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class JsAbacusFramework
    {
        public static void SolvePart1()
        {
            var shouldBe6 = GetSumOfNumbers("[1,2,3]");
            var shouldBeAlso6 = GetSumOfNumbers("{\"a\":2,\"b\":4}");
            var shouldBe3 = GetSumOfNumbers("[[[3]]]");
            var shouldBeAlso3 = GetSumOfNumbers("{\"a\":{\"b\":4},\"c\":-1}");
            var shouldBe0 = GetSumOfNumbers("{\"a\":[-1,1]}");
            var shouldBeAlso0 = GetSumOfNumbers("[-1,{\"a\":1}]");

            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\12.txt");
            var input = file.ReadLine();
            var answer = GetSumOfNumbers(input);

            Console.WriteLine($"Part 1: The sum of numbers is {answer}");            
        }
    
        private static long GetSumOfNumbers(string json)
        {
            var sum = 0L;
            var current=0L;
            var sign=1;
            for (var i=0;i<json.Length;i++)
            {
                if ('0'<=json[i] && json[i]<='9')
                {
                    current*=10;
                    current += json[i]-'0';
                }
                else
                {
                    sum += sign*current;
                    sign=1;
                    current=0;
                    if (json[i]=='-')
                        sign=-1;
                }
            }
            sum += sign*current;
            return sum;
        }
    
        public static void SolvePart2()
        {
            var shouldBe6 = GetSumOfNumbersWithoutRed("[1,2,3]");
            var shouldBe4 = GetSumOfNumbersWithoutRed("[1,{\"c\":\"red\",\"b\":2},3]");
            var shouldBe0 = GetSumOfNumbersWithoutRed("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}");
            var shouldAlsoBe6 = GetSumOfNumbersWithoutRed("[1,\"red\",5]");

            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2015\Data\12.txt");
            var input = file.ReadLine();
            var answer = GetSumOfNumbersWithoutRed(input);

            Console.WriteLine($"Part 2: The sum of non red numbers is {answer}");                       
        }

        private static int index;
    
        private static long GetSumOfNumbersWithoutRed(string json)
        {
            index=0;
            var sum = 0L;
            var current=0L;
            var sign=1;
            while (index<json.Length)
            {
                if ('0'<=json[index] && json[index]<='9')
                {
                    current*=10;
                    current += json[index]-'0';
                }
                else
                {
                    sum += sign*current;
                    sign=1;
                    current=0;
                    if (json[index]=='-')
                        sign=-1;
                    else if (json[index]=='[')
                    {
                        index++;
                        sum += GetSumOfNumbersWithoutRedForArray(json);
                    }
                    else if (json[index]=='{')
                    {
                        index++;
                        sum += GetSumOfNumbersWithoutRedForObject(json);
                    }
                }
                index++;
            }
            sum += sign*current;
            return sum;
        }
        
        private static long GetSumOfNumbersWithoutRedForObject(string json)
        {
            var isRed = false;
            var sum = 0L;
            var current=0L;
            var sign=1;
            var sb = new StringBuilder();
            while (index<json.Length)
            {
                if ('0'<=json[index] && json[index]<='9')
                {
                    current*=10;
                    current += json[index]-'0';
                }
                else
                {
                    sum += sign*current;
                    sign=1;
                    current=0;
                    if (json[index]=='-')
                        sign=-1;
                    else if (json[index]=='}')
                    {
                        break;
                    }
                    else if (json[index]=='[')
                    {
                        index++;
                        sum += GetSumOfNumbersWithoutRedForArray(json);
                    }
                    else if (json[index]=='{')
                    {
                        index++;
                        sum += GetSumOfNumbersWithoutRedForObject(json);
                    }
                    sb.Append(json[index]);
                }
                index++;
            }
            if (sb.ToString().Contains("\"red\""))
                isRed = true;
            if (isRed) return 0;
            return sum;        
        }
        
        private static long GetSumOfNumbersWithoutRedForArray(string json)
        {
            var sum = 0L;
            var current=0L;
            var sign=1;
            while (index<json.Length)
            {
                if ('0'<=json[index] && json[index]<='9')
                {
                    current*=10;
                    current += json[index]-'0';
                }
                else
                {
                    sum += sign*current;
                    sign=1;
                    current=0;
                    if (json[index]=='-')
                        sign=-1;
                    else if (json[index]==']')
                    {
                        break;
                    }
                    else if (json[index]=='[')
                    {
                        index++;
                        sum += GetSumOfNumbersWithoutRedForArray(json);
                    }
                    else if (json[index]=='{')
                    {
                        index++;
                        sum += GetSumOfNumbersWithoutRedForObject(json);
                    }
                }
                index++;
            }
            return sum;
        }
    }
}
