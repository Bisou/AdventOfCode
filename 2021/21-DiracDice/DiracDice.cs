using System;
using System.Linq;

namespace AdventOfCode
{
    public class DiracDice    
    {
        public static void Example1()
        {
            SolvePart1(4,8);
        }

        public static void SolvePart1(int player1, int player2)
        {
            var dieRoll=0;
            var scores=new []{0,0};
            var positions=new[]{player1-1, player2-1};
            var activePlayer=1;
            do
            {
                activePlayer = 1-activePlayer;
                positions[activePlayer] = (positions[activePlayer] + (++dieRoll)) % 10;
                positions[activePlayer] = (positions[activePlayer] + (++dieRoll)) % 10;
                positions[activePlayer] = (positions[activePlayer] + (++dieRoll)) % 10;
                //no need for die modulo since we do a position modulo afterwards
                scores[activePlayer] += positions[activePlayer]+1;
                Console.WriteLine($"Player {activePlayer} is in cell {positions[activePlayer]+1} and his score is {scores[activePlayer]}");
            } while(scores[activePlayer]<1000);


            Console.WriteLine($"Part 1: we have rolled the die {dieRoll} times and loser has {scores[1- activePlayer]} points so the result is {scores[1- activePlayer]*dieRoll}"); 
        }

        public static void SolvePart1()
        {
            SolvePart1(10,1);            
        }

        public static void Example2()
        {
            SolvePart2(4,8);
        }

        public static void SolvePart2()
        {
            SolvePart2(10,1);  
        }   
        
        public static void SolvePart2(int player1, int player2)
        {
            var scores=new []{0,0};
            var win = new long[2];
            var activePlayer=1;
            var universes = new long[22,22,10,10];
            universes[0,0,player1-1,player2-1]=1;
            var finished=false;
                                        // 0 1 2 3 4 5 6 7 8 9
            var diracDieTotal = new long[]{0,0,0,1,3,6,7,6,3,1};
            /*111 3
            211 4
            121 4 
            112 4
            113 5
            131 5
            311 5
            221 5
            212 5
            122 5
            123 6
            213 6
            231 6
            312 6
            321 6
            222 6
            132 6
            133 7
            223 7
            313 7
            322 7
            331 7
            232 7
            233 8
            332 8
            323 8
            333 9*/
            do
            {
                finished=true;
                var nextUniverses = new long[22,22,10,10];
                activePlayer = 1-activePlayer;
                for (var score1=0;score1<22;score1++)
                    for (var score2=0;score2<22;score2++)
                        for (var pos1=0;pos1<10;pos1++)
                            for (var pos2=0;pos2<10;pos2++)
                            {
                                var count = universes[score1, score2, pos1, pos2];
                                if (count==0) continue;
                                for (var die=3;die<10;die++)
                                {
                                    if (activePlayer==0)
                                    {
                                        var newPos1=(pos1 + die)%10;
                                        var newScore1 = score1 + newPos1+1;
                                        if (newScore1>=21)
                                            win[activePlayer]+=count * diracDieTotal[die];
                                        else
                                        {
                                            finished=false;
                                            nextUniverses[newScore1, score2, newPos1, pos2] += count * diracDieTotal[die]; 
                                        }
                                    }
                                    else
                                    {
                                        var newPos2=(pos2 + die)%10;
                                        var newScore2 = score2 + newPos2+1;
                                        if (newScore2>=21)
                                            win[activePlayer]+=count * diracDieTotal[die];
                                        else
                                        {
                                            finished=false;
                                            nextUniverses[score1, newScore2, pos1, newPos2] += count * diracDieTotal[die]; 
                                        }
                                    }
                                }                                
                            }

                universes=nextUniverses;

            } while(!finished);


            Console.WriteLine($"Part 2: the winners wins in {win.Max()} universes"); 
        }
    }
}
