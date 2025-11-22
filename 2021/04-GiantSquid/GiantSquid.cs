using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class GiantSquid
    {
        public static void SolvePart1()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\04-GiantSquid\input1.txt");  
            var numbers = file.ReadLine().Split(',');
            var boards = new List<string[][]>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var board = new string[5][];
                for (var i=0;i<5;i++)
                    board[i] = file.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                boards.Add(board);
            }
            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    for (var row=0;row<5;row++)
                    {
                        for (var col=0;col<5;col++)
                        {
                            if (board[row][col]==number)
                            {
                                board[row][col] = null;
                                var countRow=0;
                                var countCol=0;
                                for(var i=0;i<5;i++)
                                {    
                                    if (board[row][i]==null)
                                        countRow++;
                                    if (board[i][col]==null)
                                        countCol++;
                                }
                                if (countRow==5 ||countCol==5)
                                {
                                    var score = 0;
                                    for (var i=0;i<5;i++)
                                        for (var j=0;j<5;j++)
                                            if (board[i][j]!=null)
                                                score += int.Parse(board[i][j]);
                                    Console.WriteLine($"Part1: number {number} and score {score} so answer is {int.Parse(number) * score}");
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        

        public static void SolvePart2()
        {
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\04-GiantSquid\input1.txt");  
            var numbers = file.ReadLine().Split(',');
            var boards = new List<string[][]>();
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var board = new string[5][];
                for (var i=0;i<5;i++)
                    board[i] = file.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                boards.Add(board);
            }
            var bestTurn=0;
            var bestScore = 0;
            foreach (var board in boards)
            {
                var turn=0;
                    var score=0;
                foreach (var number in numbers)
                {
                    turn++;
                    if (score > 0) break;
                    for (var row=0;row<5;row++)
                    {
                        if (score > 0) break;
                        for (var col=0;col<5;col++)
                        {
                            if (score > 0) break;
                            if (board[row][col]==number)
                            {
                                board[row][col] = null;
                                var countRow=0;
                                var countCol=0;
                                for(var i=0;i<5;i++)
                                {    
                                    if (board[row][i]==null)
                                        countRow++;
                                    if (board[i][col]==null)
                                        countCol++;
                                }
                                if (countRow==5 ||countCol==5)
                                {
                                    for (var i=0;i<5;i++)
                                        for (var j=0;j<5;j++)
                                            if (board[i][j]!=null)
                                                score += int.Parse(board[i][j]);
                                    score *= int.Parse(number);
                                    if (turn > bestTurn)
                                    {
                                        bestScore = score;
                                        bestTurn = turn;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Part2: bestTurn {bestTurn} and score {bestScore}");
        }
    }
}
