using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class RockPaperScissors
    {
        public static void SolvePart1()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\02-RockPaperScissors\input.txt");  
            var score=0;
            string line;
            while((line = file.ReadLine())!=null) {
                score += ScorePart1(line[0], line[2]);
            }
            Console.WriteLine($"Part1: my score is {score}");
        }
        
        private static int ScorePart1(char opponent, char me) {
            if (opponent=='A'){
                if (me=='X')
                    return 1+3;
                else if (me=='Y')
                    return 2+6;
                else if (me=='Z')
                    return 3+0;
            } else if (opponent=='B'){
                if (me=='X')
                    return 1+0;
                else if (me=='Y')
                    return 2+3;
                else if (me=='Z')
                    return 3+6;            
            } else if (opponent=='C'){
                if (me=='X')
                    return 1+6;
                else if (me=='Y')
                    return 2+0;
                else if (me=='Z')
                    return 3+3;
            }
            return -1;
        }
     
        public static void SolvePart2()
        {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\02-RockPaperScissors\input.txt");  
            var score=0;
            string line;
            while((line = file.ReadLine())!=null) {
                score += ScorePart2(line[0], line[2]);
            }
            Console.WriteLine($"Part2: my score is {score}");
        }
        
        private static int ScorePart2(char opponent, char me) {
            if (opponent=='A'){ //Rock
                if (me=='X')//lose
                    return 3+0;
                else if (me=='Y')//draw
                    return 1+3;
                else if (me=='Z')//win
                    return 2+6;
            } else if (opponent=='B'){//Paper
                if (me=='X')//lose
                    return 1+0;
                else if (me=='Y')//draw
                    return 2+3;
                else if (me=='Z')//win
                    return 3+6;            
            } else if (opponent=='C'){//Scissors
                if (me=='X')//lose
                    return 2+0;
                else if (me=='Y')//draw
                    return 3+3;
                else if (me=='Z')//win
                    return 1+6;
            }
            return -1;
        }
    }
}
