using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Amphipod    
    {
        public static void SolvePart1()
        {
            const char EMPTY = '.';
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\23-Amphipod\input1.txt");  
            file.ReadLine();
            file.ReadLine();
            Map.Init();
            var posTop = file.ReadLine();
            var posBottom = file.ReadLine();
            var startState = Map.GetState(posTop, posBottom);
            var finalPosition = new Dictionary<char, int[]> {
                {'A', new []{11,12}},
                {'B', new []{13,14}},
                {'C', new []{15,16}},
                {'D', new []{17,18}}
            };
            var moveCost = new Dictionary<char, long> {
                {'A', 1},
                {'B', 10},
                {'C', 100},
                {'D', 1000}
            };
            const string target="...........AABBCCDD";
            var minCost = new Dictionary<string, long>();
            minCost.Add(startState, 0);
            var todo = new Queue<string>();
            todo.Enqueue(startState);
            while(todo.Any())
            {
                var state = todo.Dequeue();
                for (var cellId = 0;cellId < Map.Cells.Length; cellId++)
                {
                    if (state[cellId]==EMPTY) continue;
                    var amphipod = state[cellId];

                    //Déjà rangé : on skippe
                    if (finalPosition[amphipod][1] == cellId) continue;
                    if (finalPosition[amphipod][0] == cellId && state[finalPosition[amphipod][1]]==amphipod) continue;

                    //On range
                    if (Map.Cells[cellId].FinalPath != null)
                    {
                        var finalPath = Map.Cells[cellId].FinalPath[amphipod];
                        if (state[finalPath.Last()]==amphipod)
                            finalPath = finalPath.Take(finalPath.Count()-1).ToList();
                        if (finalPath.TrueForAll(m => state[m]==EMPTY))
                        {
                            var newCost = minCost[state] + moveCost[state[cellId]]*finalPath.Count;
                            if (minCost.ContainsKey(target) && minCost[target]< newCost) continue;
                            var sb = new StringBuilder(state);
                            sb[cellId] = EMPTY;
                            sb[finalPath.Last()] = state[cellId];
                            var nextState = sb.ToString();
                            if (minCost.ContainsKey(nextState))
                            {
                                if (minCost[nextState]> newCost)
                                {
                                    minCost[nextState]=newCost;
                                    todo.Enqueue(nextState);
                                }
                            }
                            else
                            {
                                minCost.Add(nextState, newCost);
                                todo.Enqueue(nextState);
                            }
                            continue;
                        }                       
                    }

                    //On bouge
                    foreach(var move in Map.Cells[cellId].Moves)
                    {
                        if (move.Any(m => state[m]!=EMPTY)) continue;
                        var newCost = minCost[state] + moveCost[state[cellId]]*move.Count;
                        if (minCost.ContainsKey(target) && minCost[target]< newCost) continue;
                        var sb = new StringBuilder(state);
                        sb[cellId] = EMPTY;
                        sb[move.Last()] = state[cellId];
                        var nextState = sb.ToString();
                        if (minCost.ContainsKey(nextState))
                        {
                            if (minCost[nextState]> newCost)
                            {
                                minCost[nextState]=newCost;
                                todo.Enqueue(nextState);
                            }
                        }
                        else
                        {
                            minCost.Add(nextState, newCost);
                            todo.Enqueue(nextState);
                        }
                    }
                }
            }
            Console.WriteLine($"Part 1: minimum cost to order is {minCost[target]}"); 
        }

        public static void SolvePart2()
        {
            const char EMPTY = '.';
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2021\23-Amphipod\input1.txt");  
            file.ReadLine();
            file.ReadLine();
            Map.Init2();
            var posTop = file.ReadLine();
            var posBottom = file.ReadLine();
            var startState = Map.GetState2(posTop, posBottom);
            var finalPosition = new Dictionary<char, int[]> {
                {'A', new []{11,12,13,14}},
                {'B', new []{15,16,17,18}},
                {'C', new []{19,20,21,22}},
                {'D', new []{23,24,25,26}}
            };
            var moveCost = new Dictionary<char, long> {
                {'A', 1},
                {'B', 10},
                {'C', 100},
                {'D', 1000}
            };
            const string target="...........AAAABBBBCCCCDDDD";
            var minCost = new Dictionary<string, long>();
            minCost.Add(startState, 0);
            var todo = new Queue<string>();
            todo.Enqueue(startState);
            while(todo.Any())
            {
                var state = todo.Dequeue();
                for (var cellId = 0;cellId < Map.Cells.Length; cellId++)
                {
                    if (state[cellId]==EMPTY) continue;
                    var amphipod = state[cellId];

                    //Déjà rangé : on skippe
                    if (finalPosition[amphipod][3] == cellId) continue;
                    if (finalPosition[amphipod][2] == cellId && state[finalPosition[amphipod][3]]==amphipod) continue;
                    if (finalPosition[amphipod][1] == cellId && state[finalPosition[amphipod][2]]==amphipod && state[finalPosition[amphipod][3]]==amphipod) continue;
                    if (finalPosition[amphipod][0] == cellId && state[finalPosition[amphipod][1]]==amphipod && state[finalPosition[amphipod][2]]==amphipod && state[finalPosition[amphipod][3]]==amphipod) continue;

                    //On range
                    if (Map.Cells[cellId].FinalPath != null)
                    {
                        var finalPath = Map.Cells[cellId].FinalPath[amphipod].ToList();
                        if (state[finalPath.Last()]==amphipod)
                            finalPath.RemoveAt(finalPath.Count()-1);
                        if (state[finalPath.Last()]==amphipod)
                            finalPath.RemoveAt(finalPath.Count()-1);
                        if (state[finalPath.Last()]==amphipod)
                            finalPath.RemoveAt(finalPath.Count()-1);
                        if (finalPath.TrueForAll(m => state[m]==EMPTY))
                        {
                            var newCost = minCost[state] + moveCost[state[cellId]]*finalPath.Count;
                            if (minCost.ContainsKey(target) && minCost[target]< newCost) continue;
                            var sb = new StringBuilder(state);
                            sb[cellId] = EMPTY;
                            sb[finalPath.Last()] = state[cellId];
                            var nextState = sb.ToString();
                            if (minCost.ContainsKey(nextState))
                            {
                                if (minCost[nextState]> newCost)
                                {
                                    minCost[nextState]=newCost;
                                    todo.Enqueue(nextState);
                                }
                            }
                            else
                            {
                                minCost.Add(nextState, newCost);
                                todo.Enqueue(nextState);
                            }
                            continue;
                        }                       
                    }

                    //On bouge
                    foreach(var move in Map.Cells[cellId].Moves)
                    {
                        if (move.Any(m => state[m]!=EMPTY)) continue;
                        var newCost = minCost[state] + moveCost[state[cellId]]*move.Count;
                        if (minCost.ContainsKey(target) && minCost[target]< newCost) continue;
                        var sb = new StringBuilder(state);
                        sb[cellId] = EMPTY;
                        sb[move.Last()] = state[cellId];
                        var nextState = sb.ToString();
                        if (minCost.ContainsKey(nextState))
                        {
                            if (minCost[nextState]> newCost)
                            {
                                minCost[nextState]=newCost;
                                todo.Enqueue(nextState);
                            }
                        }
                        else
                        {
                            minCost.Add(nextState, newCost);
                            todo.Enqueue(nextState);
                        }
                    }
                }
            }
            Console.WriteLine($"Part 2: minimum cost to order is {minCost[target]}"); 
        }

        static class Map
        {
            public static Cell[] Cells;
            public static void Init()
            {
                Cells = new Cell[19];
                for (var i=0; i<Cells.Length; i++)
                    Cells[i] = new Cell(i);
                Cells[0].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{1,2,11,12}},
                    {'B', new List<int>{1,2,3,4,13,14}},
                    {'C', new List<int>{1,2,3,4,5,6,15,16}},
                    {'D', new List<int>{1,2,3,4,5,6,7,8,17,18}}};
                Cells[1].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{2,11,12}},
                    {'B', new List<int>{2,3,4,13,14}},
                    {'C', new List<int>{2,3,4,5,6,15,16}},
                    {'D', new List<int>{2,3,4,5,6,7,8,17,18}}};
                Cells[3].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{2,11,12}},
                    {'B', new List<int>{4,13,14}},
                    {'C', new List<int>{4,5,6,15,16}},
                    {'D', new List<int>{4,5,6,7,8,17,18}}};
                Cells[5].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{4,3,2,11,12}},
                    {'B', new List<int>{4,13,14}},
                    {'C', new List<int>{6,15,16}},
                    {'D', new List<int>{6,7,8,17,18}}};
                Cells[7].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{6,5,4,3,2,11,12}},
                    {'B', new List<int>{6,5,4,13,14}},
                    {'C', new List<int>{6,15,16}},
                    {'D', new List<int>{8,17,18}}};
                Cells[9].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{8,7,6,5,4,3,2,11,12}},
                    {'B', new List<int>{8,7,6,5,4,13,14}},
                    {'C', new List<int>{8,7,6,15,16}},
                    {'D', new List<int>{8,17,18}}};
                Cells[10].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{9,8,7,6,5,4,3,2,11,12}},
                    {'B', new List<int>{9,8,7,6,5,4,13,14}},
                    {'C', new List<int>{9,8,7,6,15,16}},
                    {'D', new List<int>{9,8,17,18}}};

                Cells[11].Moves.Add(new List<int>{2,1,0});
                Cells[11].Moves.Add(new List<int>{2,1});
                Cells[11].Moves.Add(new List<int>{2,3});
                Cells[11].Moves.Add(new List<int>{2,3,4,5});
                Cells[11].Moves.Add(new List<int>{2,3,4,5,6,7});
                Cells[11].Moves.Add(new List<int>{2,3,4,5,6,7,8,9});
                Cells[11].Moves.Add(new List<int>{2,3,4,5,6,7,8,9,10});
                Cells[12].Moves.Add(new List<int>{11,2,1,0});
                Cells[12].Moves.Add(new List<int>{11,2,1});
                Cells[12].Moves.Add(new List<int>{11,2,3});
                Cells[12].Moves.Add(new List<int>{11,2,3,4,5});
                Cells[12].Moves.Add(new List<int>{11,2,3,4,5,6,7});
                Cells[12].Moves.Add(new List<int>{11,2,3,4,5,6,7,8,9});
                Cells[12].Moves.Add(new List<int>{11,2,3,4,5,6,7,8,9,10});
                
                Cells[13].Moves.Add(new List<int>{4,3,2,1,0});
                Cells[13].Moves.Add(new List<int>{4,3,2,1});
                Cells[13].Moves.Add(new List<int>{4,3});
                Cells[13].Moves.Add(new List<int>{4,5});
                Cells[13].Moves.Add(new List<int>{4,5,6,7});
                Cells[13].Moves.Add(new List<int>{4,5,6,7,8,9});
                Cells[13].Moves.Add(new List<int>{4,5,6,7,8,9,10});
                Cells[14].Moves.Add(new List<int>{13,4,3,2,1,0});
                Cells[14].Moves.Add(new List<int>{13,4,3,2,1});
                Cells[14].Moves.Add(new List<int>{13,4,3});
                Cells[14].Moves.Add(new List<int>{13,4,5});
                Cells[14].Moves.Add(new List<int>{13,4,5,6,7});
                Cells[14].Moves.Add(new List<int>{13,4,5,6,7,8,9});
                Cells[14].Moves.Add(new List<int>{13,4,5,6,7,8,9,10});

                Cells[15].Moves.Add(new List<int>{6,5,4,3,2,1,0});
                Cells[15].Moves.Add(new List<int>{6,5,4,3,2,1});
                Cells[15].Moves.Add(new List<int>{6,5,4,3});
                Cells[15].Moves.Add(new List<int>{6,5});
                Cells[15].Moves.Add(new List<int>{6,7});
                Cells[15].Moves.Add(new List<int>{6,7,8,9});
                Cells[15].Moves.Add(new List<int>{6,7,8,9,10});
                Cells[16].Moves.Add(new List<int>{15,6,5,4,3,2,1,0});
                Cells[16].Moves.Add(new List<int>{15,6,5,4,3,2,1});
                Cells[16].Moves.Add(new List<int>{15,6,5,4,3});
                Cells[16].Moves.Add(new List<int>{15,6,5});
                Cells[16].Moves.Add(new List<int>{15,6,7});
                Cells[16].Moves.Add(new List<int>{15,6,7,8,9});
                Cells[16].Moves.Add(new List<int>{15,6,7,8,9,10});

                Cells[17].Moves.Add(new List<int>{8,7,6,5,4,3,2,1,0});
                Cells[17].Moves.Add(new List<int>{8,7,6,5,4,3,2,1});
                Cells[17].Moves.Add(new List<int>{8,7,6,5,4,3});
                Cells[17].Moves.Add(new List<int>{8,7,6,5});
                Cells[17].Moves.Add(new List<int>{8,7});
                Cells[17].Moves.Add(new List<int>{8,9});
                Cells[17].Moves.Add(new List<int>{8,9,10});
                Cells[18].Moves.Add(new List<int>{17,8,7,6,5,4,3,2,1,0});
                Cells[18].Moves.Add(new List<int>{17,8,7,6,5,4,3,2,1});
                Cells[18].Moves.Add(new List<int>{17,8,7,6,5,4,3});
                Cells[18].Moves.Add(new List<int>{17,8,7,6,5});
                Cells[18].Moves.Add(new List<int>{17,8,7});
                Cells[18].Moves.Add(new List<int>{17,8,9});
                Cells[18].Moves.Add(new List<int>{17,8,9,10});
            }

            public static void Init2()
            {
                Cells = new Cell[27];
                for (var i=0; i<Cells.Length; i++)
                    Cells[i] = new Cell(i);
                Cells[0].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{1,2,11,12,13,14}},
                    {'B', new List<int>{1,2,3,4,15,16,17,18}},
                    {'C', new List<int>{1,2,3,4,5,6,19,20,21,22}},
                    {'D', new List<int>{1,2,3,4,5,6,7,8,23,24,25,26}}};
                Cells[1].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{2,11,12,13,14}},
                    {'B', new List<int>{2,3,4,15,16,17,18}},
                    {'C', new List<int>{2,3,4,5,6,19,20,21,22}},
                    {'D', new List<int>{2,3,4,5,6,7,8,23,24,25,26}}};
                Cells[3].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{2,11,12,13,14}},
                    {'B', new List<int>{4,15,16,17,18}},
                    {'C', new List<int>{4,5,6,19,20,21,22}},
                    {'D', new List<int>{4,5,6,7,8,23,24,25,26}}};
                Cells[5].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{4,3,2,11,12,13,14}},
                    {'B', new List<int>{4,15,16,17,18}},
                    {'C', new List<int>{6,19,20,21,22}},
                    {'D', new List<int>{6,7,8,23,24,25,26}}};
                Cells[7].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{6,5,4,3,2,11,12,13,14}},
                    {'B', new List<int>{6,5,4,15,16,17,18}},
                    {'C', new List<int>{6,19,20,21,22}},
                    {'D', new List<int>{8,23,24,25,26}}};
                Cells[9].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{8,7,6,5,4,3,2,11,12,13,14}},
                    {'B', new List<int>{8,7,6,5,4,15,16,17,18}},
                    {'C', new List<int>{8,7,6,19,20,21,22}},
                    {'D', new List<int>{8,23,24,25,26}}};
                Cells[10].FinalPath = new Dictionary<char, List<int>>{
                    {'A', new List<int>{9,8,7,6,5,4,3,2,11,12,13,14}},
                    {'B', new List<int>{9,8,7,6,5,4,15,16,17,18}},
                    {'C', new List<int>{9,8,7,6,19,20,21,22}},
                    {'D', new List<int>{9,8,23,24,25,26}}};

                Cells[11].Moves.Add(new List<int>{2,1,0});
                Cells[11].Moves.Add(new List<int>{2,1});
                Cells[11].Moves.Add(new List<int>{2,3});
                Cells[11].Moves.Add(new List<int>{2,3,4,5});
                Cells[11].Moves.Add(new List<int>{2,3,4,5,6,7});
                Cells[11].Moves.Add(new List<int>{2,3,4,5,6,7,8,9});
                Cells[11].Moves.Add(new List<int>{2,3,4,5,6,7,8,9,10});
                Cells[12].Moves.Add(new List<int>{11,2,1,0});
                Cells[12].Moves.Add(new List<int>{11,2,1});
                Cells[12].Moves.Add(new List<int>{11,2,3});
                Cells[12].Moves.Add(new List<int>{11,2,3,4,5});
                Cells[12].Moves.Add(new List<int>{11,2,3,4,5,6,7});
                Cells[12].Moves.Add(new List<int>{11,2,3,4,5,6,7,8,9});
                Cells[12].Moves.Add(new List<int>{11,2,3,4,5,6,7,8,9,10});
                Cells[13].Moves.Add(new List<int>{12,11,2,1,0});
                Cells[13].Moves.Add(new List<int>{12,11,2,1});
                Cells[13].Moves.Add(new List<int>{12,11,2,3});
                Cells[13].Moves.Add(new List<int>{12,11,2,3,4,5});
                Cells[13].Moves.Add(new List<int>{12,11,2,3,4,5,6,7});
                Cells[13].Moves.Add(new List<int>{12,11,2,3,4,5,6,7,8,9});
                Cells[13].Moves.Add(new List<int>{12,11,2,3,4,5,6,7,8,9,10});
                Cells[14].Moves.Add(new List<int>{13,12,11,2,1,0});
                Cells[14].Moves.Add(new List<int>{13,12,11,2,1});
                Cells[14].Moves.Add(new List<int>{13,12,11,2,3});
                Cells[14].Moves.Add(new List<int>{13,12,11,2,3,4,5});
                Cells[14].Moves.Add(new List<int>{13,12,11,2,3,4,5,6,7});
                Cells[14].Moves.Add(new List<int>{13,12,11,2,3,4,5,6,7,8,9});
                Cells[14].Moves.Add(new List<int>{13,12,11,2,3,4,5,6,7,8,9,10});
                
                Cells[15].Moves.Add(new List<int>{4,3,2,1,0});
                Cells[15].Moves.Add(new List<int>{4,3,2,1});
                Cells[15].Moves.Add(new List<int>{4,3});
                Cells[15].Moves.Add(new List<int>{4,5});
                Cells[15].Moves.Add(new List<int>{4,5,6,7});
                Cells[15].Moves.Add(new List<int>{4,5,6,7,8,9});
                Cells[15].Moves.Add(new List<int>{4,5,6,7,8,9,10});
                Cells[16].Moves.Add(new List<int>{15,4,3,2,1,0});
                Cells[16].Moves.Add(new List<int>{15,4,3,2,1});
                Cells[16].Moves.Add(new List<int>{15,4,3});
                Cells[16].Moves.Add(new List<int>{15,4,5});
                Cells[16].Moves.Add(new List<int>{15,4,5,6,7});
                Cells[16].Moves.Add(new List<int>{15,4,5,6,7,8,9});
                Cells[16].Moves.Add(new List<int>{15,4,5,6,7,8,9,10});
                Cells[17].Moves.Add(new List<int>{16,15,4,3,2,1,0});
                Cells[17].Moves.Add(new List<int>{16,15,4,3,2,1});
                Cells[17].Moves.Add(new List<int>{16,15,4,3});
                Cells[17].Moves.Add(new List<int>{16,15,4,5});
                Cells[17].Moves.Add(new List<int>{16,15,4,5,6,7});
                Cells[17].Moves.Add(new List<int>{16,15,4,5,6,7,8,9});
                Cells[17].Moves.Add(new List<int>{16,15,4,5,6,7,8,9,10});
                Cells[18].Moves.Add(new List<int>{17,16,15,4,3,2,1,0});
                Cells[18].Moves.Add(new List<int>{17,16,15,4,3,2,1});
                Cells[18].Moves.Add(new List<int>{17,16,15,4,3});
                Cells[18].Moves.Add(new List<int>{17,16,15,4,5});
                Cells[18].Moves.Add(new List<int>{17,16,15,4,5,6,7});
                Cells[18].Moves.Add(new List<int>{17,16,15,4,5,6,7,8,9});
                Cells[18].Moves.Add(new List<int>{17,16,15,4,5,6,7,8,9,10});

                Cells[19].Moves.Add(new List<int>{6,5,4,3,2,1,0});
                Cells[19].Moves.Add(new List<int>{6,5,4,3,2,1});
                Cells[19].Moves.Add(new List<int>{6,5,4,3});
                Cells[19].Moves.Add(new List<int>{6,5});
                Cells[19].Moves.Add(new List<int>{6,7});
                Cells[19].Moves.Add(new List<int>{6,7,8,9});
                Cells[19].Moves.Add(new List<int>{6,7,8,9,10});
                Cells[20].Moves.Add(new List<int>{19,6,5,4,3,2,1,0});
                Cells[20].Moves.Add(new List<int>{19,6,5,4,3,2,1});
                Cells[20].Moves.Add(new List<int>{19,6,5,4,3});
                Cells[20].Moves.Add(new List<int>{19,6,5});
                Cells[20].Moves.Add(new List<int>{19,6,7});
                Cells[20].Moves.Add(new List<int>{19,6,7,8,9});
                Cells[20].Moves.Add(new List<int>{19,6,7,8,9,10});
                Cells[21].Moves.Add(new List<int>{20,19,6,5,4,3,2,1,0});
                Cells[21].Moves.Add(new List<int>{20,19,6,5,4,3,2,1});
                Cells[21].Moves.Add(new List<int>{20,19,6,5,4,3});
                Cells[21].Moves.Add(new List<int>{20,19,6,5});
                Cells[21].Moves.Add(new List<int>{20,19,6,7});
                Cells[21].Moves.Add(new List<int>{20,19,6,7,8,9});
                Cells[21].Moves.Add(new List<int>{20,19,6,7,8,9,10});
                Cells[22].Moves.Add(new List<int>{21,20,19,6,5,4,3,2,1,0});
                Cells[22].Moves.Add(new List<int>{21,20,19,6,5,4,3,2,1});
                Cells[22].Moves.Add(new List<int>{21,20,19,6,5,4,3});
                Cells[22].Moves.Add(new List<int>{21,20,19,6,5});
                Cells[22].Moves.Add(new List<int>{21,20,19,6,7});
                Cells[22].Moves.Add(new List<int>{21,20,19,6,7,8,9});
                Cells[22].Moves.Add(new List<int>{21,20,19,6,7,8,9,10});

                Cells[23].Moves.Add(new List<int>{8,7,6,5,4,3,2,1,0});
                Cells[23].Moves.Add(new List<int>{8,7,6,5,4,3,2,1});
                Cells[23].Moves.Add(new List<int>{8,7,6,5,4,3});
                Cells[23].Moves.Add(new List<int>{8,7,6,5});
                Cells[23].Moves.Add(new List<int>{8,7});
                Cells[23].Moves.Add(new List<int>{8,9});
                Cells[23].Moves.Add(new List<int>{8,9,10});
                Cells[24].Moves.Add(new List<int>{23,8,7,6,5,4,3,2,1,0});
                Cells[24].Moves.Add(new List<int>{23,8,7,6,5,4,3,2,1});
                Cells[24].Moves.Add(new List<int>{23,8,7,6,5,4,3});
                Cells[24].Moves.Add(new List<int>{23,8,7,6,5});
                Cells[24].Moves.Add(new List<int>{23,8,7});
                Cells[24].Moves.Add(new List<int>{23,8,9});
                Cells[24].Moves.Add(new List<int>{23,8,9,10});
                Cells[25].Moves.Add(new List<int>{24,23,8,7,6,5,4,3,2,1,0});
                Cells[25].Moves.Add(new List<int>{24,23,8,7,6,5,4,3,2,1});
                Cells[25].Moves.Add(new List<int>{24,23,8,7,6,5,4,3});
                Cells[25].Moves.Add(new List<int>{24,23,8,7,6,5});
                Cells[25].Moves.Add(new List<int>{24,23,8,7});
                Cells[25].Moves.Add(new List<int>{24,23,8,9});
                Cells[25].Moves.Add(new List<int>{24,23,8,9,10});
                Cells[26].Moves.Add(new List<int>{25,24,23,8,7,6,5,4,3,2,1,0});
                Cells[26].Moves.Add(new List<int>{25,24,23,8,7,6,5,4,3,2,1});
                Cells[26].Moves.Add(new List<int>{25,24,23,8,7,6,5,4,3});
                Cells[26].Moves.Add(new List<int>{25,24,23,8,7,6,5});
                Cells[26].Moves.Add(new List<int>{25,24,23,8,7});
                Cells[26].Moves.Add(new List<int>{25,24,23,8,9});
                Cells[26].Moves.Add(new List<int>{25,24,23,8,9,10});
            }

            public static string GetState(string posTop, string posBottom)
            {
                var sb = new StringBuilder("...........");
                sb.Append(posTop[3]);
                sb.Append(posBottom[3]);
                sb.Append(posTop[5]);
                sb.Append(posBottom[5]);
                sb.Append(posTop[7]);
                sb.Append(posBottom[7]);
                sb.Append(posTop[9]);
                sb.Append(posBottom[9]);
                return sb.ToString();
            }

            public static string GetState2(string posTop, string posBottom)
            {
                var sb = new StringBuilder("...........");
                sb.Append(posTop[3]);
                sb.Append("DD");
                sb.Append(posBottom[3]);
                sb.Append(posTop[5]);
                sb.Append("CB");
                sb.Append(posBottom[5]);
                sb.Append(posTop[7]);
                sb.Append("BA");
                sb.Append(posBottom[7]);
                sb.Append(posTop[9]);
                sb.Append("AC");
                sb.Append(posBottom[9]);
                return sb.ToString();
            }
        }

        class Cell
        {
            public List<List<int>> Moves = new List<List<int>>();
            public Dictionary<char,List<int>> FinalPath;
            public int Id;
            public Cell(int id)            
            {
                Id = id;
            }
        }
    }
}
