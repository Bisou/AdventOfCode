using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class ProboscideaVolcanium
    {
        public static void SolvePart1()
        {
            var seen = new Dictionary<string,int>();
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\16-ProboscideaVolcanium\input.txt");  
            string line;
            var nodes = new Dictionary<string,Node>();
            var nodesToOpen=0;
            while((line=file.ReadLine())!=null) {
                if (line.Length!=0) {
                   var data = line.Replace("Valve ","")
                                  .Replace(" has flow rate="," ")
                                  .Replace("; tunnels lead to valves "," ")
                                  .Replace("; tunnel leads to valve "," ")
                                  .Replace(", ",",")
                                  .Split(' ');                                  
                    var node = new Node(data[0],int.Parse(data[1]),data[2].Split(','));
                    nodes.Add(node.Id, node);
                    if (node.Flow>0) nodesToOpen++;
                }
            }

            var maxFlow=0;
            var start = new Step("AA", 0);
            var todo = new List<Step>{start};
            seen[start.ToString()]=0;
                
            for(var i=0;i<30;++i) {
                var next = new List<Step>();
                foreach(var step in todo) {
                    var currentKey =step.ToString(); 
                    if (step.Open.Count()==nodesToOpen) {
                        /*step.TotalFlow+=step.Flow*(30-i);
                        maxFlow=Math.Max(maxFlow,step.TotalFlow);*/
                        step.TotalFlow+=step.Flow;
                        if (seen.ContainsKey(currentKey) && seen[currentKey]>step.TotalFlow) continue;
                        seen[currentKey]= step.TotalFlow;
                        next.Add(step);
                        continue;
                    }
                    
                    if (seen.ContainsKey(currentKey) && seen[currentKey]>step.TotalFlow) continue;

                    if (!step.Open.Contains(step.NodeId) && nodes[step.NodeId].Flow>0) {
                        //open valve
                        var openValve = step.Clone();
                        openValve.Open.Add(openValve.NodeId);
                        var key =openValve.ToString(); 
                        /*if (openValve.Open.Count()==nodesToOpen) {
                            //to be rethinked
                            openValve.TotalFlow+=step.Flow*(30-i);
                            maxFlow=Math.Max(maxFlow,openValve.TotalFlow);
                            if (seen.ContainsKey(key) && seen[key]>openValve.TotalFlow) continue;
                            seen[key]= openValve.TotalFlow;
                        }
                        else {*/
                            openValve.TotalFlow+=openValve.Flow;
                            openValve.Flow+=nodes[openValve.NodeId].Flow;
                            if (!seen.ContainsKey(key) || seen[key]<openValve.TotalFlow) {
                                seen[key]= openValve.TotalFlow;
                                next.Add(openValve);
                            }
                        //}
                        
                    }
                    foreach(var neighbour in nodes[step.NodeId].Next) {
                        //move there
                        var move = step.Clone();
                        move.NodeId=neighbour;
                        move.TotalFlow+=move.Flow;
                        var key =move.ToString(); 
                        if (seen.ContainsKey(key) && seen[key]>=move.TotalFlow) continue;
                            seen[key]= move.TotalFlow;
                        next.Add(move);
                    }
                }
                todo=next;
            }

            foreach(var step in todo)
                maxFlow=Math.Max(maxFlow,step.TotalFlow);


            Console.WriteLine($"Part1: maxFlow is {maxFlow}");
        }

        class Step{
            public string NodeId;
            public int Flow;
            public int TotalFlow;
            public HashSet<string> Open = new HashSet<string>();

            public Step(string nodeId, int flowPerTurn) {
                NodeId=nodeId;
                Flow=flowPerTurn;
            }

            public Step Clone() {
                var clone = new Step(NodeId, Flow);
                clone.TotalFlow=TotalFlow;
                foreach(var key in Open) clone.Open.Add(key);
                return clone;
            }

            public override string ToString(){
                return $"{NodeId}:{string.Join("-",Open.OrderBy(x => x))}";
            }
        }
        
        class Step2{
            public int Valve1;
            public int Valve2;
            public int AvailableInTurn1;
            public int AvailableInTurn2;
            public int Flow;
            public int TotalFlow;
            public bool[] Open;

            public Step2(int valve1, int valve2, int flowPerTurn, int n) {
                Valve1=valve1;
                Valve2=valve2;
                Flow=flowPerTurn;
                Open = new bool[n];
                Open[0]=true;
            }

            public bool Finished() {
                return !(Open.Any(x => !x)); 
            }

            public int NextTurn => Math.Min(AvailableInTurn1,AvailableInTurn2);

            public Step2 Clone() {
                var clone = new Step2(Valve1, Valve2, Flow, Open.Length);
                clone.TotalFlow=TotalFlow;
                clone.AvailableInTurn1=AvailableInTurn1;
                clone.AvailableInTurn2=AvailableInTurn2;
                for (var i=0;i<Open.Length;++i)
                    if (Open[i]) clone.Open[i]=true;
                return clone;
            }

            public override string ToString(){
                if (Valve1<=Valve2)
                    return $"{Valve1},{AvailableInTurn1}/{Valve2},{AvailableInTurn2}:{string.Join("",Open.Select(b => b?'O':'.'))}";
                return $"{Valve2},{AvailableInTurn2}/{Valve1},{AvailableInTurn1}:{string.Join("",Open.Select(b => b?'O':'.'))}";
            }
        }

        class Node{
            public string Id;
            public int Index;
            public int Flow;
            public bool Open = false;
            public string[] Next;

            public Node(string id, int flow, string[] next) {
                Id=id;
                Flow=flow;
                Next=next;
            }

            public Valve GetValve() {
                var valve = new Valve{Id=Index, Name=Id,Flow=Flow};
                return valve;
            }
        }

        class Valve{
            public int Id;
            public string Name;            
            public int Flow;
            public int[] Distances;
        }

        private static int Dfs2(Valve[] graph, Step2 step, int turn, int depth) {
            if (depth>520){
                var stop=true;
            }
            var maxFlow=0;
            while (turn!=step.AvailableInTurn1 && turn!=step.AvailableInTurn2 && turn<25) {
                step.TotalFlow+=step.Flow;
                ++turn;
            }
            if (turn>=25) return step.TotalFlow;

            
            if (step.AvailableInTurn1 == turn && !step.Finished()) {
                if (!step.Open[step.Valve1]) {
                    //open valve => mandatory!
                    step.Open[step.Valve1] = true;
                    step.Flow += graph[step.Valve1].Flow;
                    step.AvailableInTurn1++;
                    if (step.AvailableInTurn2!=turn) {
                        return Dfs2(graph, step, turn, depth+1);
                    }
                    //play 2
                    if (!step.Open[step.Valve2]) {
                        //open valve => mandatory!
                        step.Open[step.Valve2] = true;
                        step.Flow += graph[step.Valve2].Flow;
                        step.AvailableInTurn2++;
                        return Dfs2(graph, step, turn, depth+1);
                    }
                    //move 2
                    var move2Count=0;
                    for(var i=0;i<graph.Length;++i) {
                        if (step.Open[i]) continue;
                        var nextStep = step.Clone();
                        nextStep.AvailableInTurn2+=graph[step.Valve2].Distances[i];
                        nextStep.Valve2=i;
                        maxFlow = Math.Max(maxFlow, Dfs2(graph, nextStep, turn, depth+1));
                        move2Count++;
                    }                
                    if (move2Count==0) {
                        step.AvailableInTurn2=30;
                        return Dfs2(graph, step, turn, depth+1);
                    }     
                        
                    return maxFlow;     
                }
                //move 1
                var move1Count=0;
                for(var i=0;i<graph.Length;++i) {
                    if (step.Open[i] || step.Valve2==i) continue;
                    var nextStep = step.Clone();
                    nextStep.AvailableInTurn1+=graph[step.Valve1].Distances[i];
                    nextStep.Valve1=i;
                    move1Count++;
                    if (nextStep.AvailableInTurn2!=turn) {
                       // nextStep.TotalFlow += nextStep.Flow * (nextStep.NextTurn - turn - 1);
                        maxFlow = Math.Max(maxFlow, Dfs2(graph, nextStep, turn, depth+1));
                        continue;
                    }
                    //play 2
                    if (!nextStep.Open[nextStep.Valve2]) {
                        //open valve => mandatory!
                        nextStep.Open[nextStep.Valve2] = true;
                        nextStep.Flow += graph[nextStep.Valve2].Flow;
                        nextStep.AvailableInTurn2++;
                     //   nextStep.TotalFlow += nextStep.Flow * (nextStep.NextTurn - turn - 1);//useful?
                        maxFlow = Math.Max(maxFlow, Dfs2(graph, nextStep, turn, depth+1));
                        continue;
                    }
                    //move 2
                    var move2Count=0;
                    for(var j=0;j<graph.Length;++j) {
                        if (nextStep.Open[j] || nextStep.Valve1==j) continue;
                        var nextStep2 = nextStep.Clone();
                        nextStep2.AvailableInTurn2+=graph[nextStep2.Valve2].Distances[j];
                        nextStep2.Valve2 = j;
                        move2Count++;
                      //  nextStep2.TotalFlow += nextStep2.Flow * (nextStep2.NextTurn - turn - 1);
                        maxFlow = Math.Max(maxFlow, Dfs2(graph, nextStep2, turn, depth+1));
                    }
                    if (move2Count==0) {
                        nextStep.AvailableInTurn2=30;
                        maxFlow = Math.Max(maxFlow, Dfs2(graph, nextStep, turn, depth+1));
                    }     
                }
                if (move1Count==0) {
                    step.AvailableInTurn1=30;
                    maxFlow = Math.Max(maxFlow, Dfs2(graph, step, turn, depth+1));
                }    
            }
            else if (step.AvailableInTurn2==turn && !step.Finished()) {
                //play only 2
                if (!step.Open[step.Valve2]) {
                    //open valve => mandatory!
                    step.Open[step.Valve2] = true;
                    step.Flow += graph[step.Valve2].Flow;
                    step.AvailableInTurn2++;
                    //no need to add flow (only 1 turn)
                    return Dfs2(graph, step, turn, depth+1);
                }
                //move 2
                var move2Count=0;
                for(var i=0;i<graph.Length;++i) {
                    if (step.Open[i] || step.Valve1==i) continue;
                    var nextStep = step.Clone();
                    nextStep.AvailableInTurn2+=graph[step.Valve2].Distances[i];
                    nextStep.Valve2=i;
                    move2Count++;
                   // nextStep.TotalFlow += nextStep.Flow * (nextStep.NextTurn - turn - 1);
                    maxFlow = Math.Max(maxFlow, Dfs2(graph, nextStep, turn, depth+1));
                }
                if (move2Count==0) {
                    step.AvailableInTurn2=30;
                    maxFlow = Math.Max(maxFlow, Dfs2(graph, step, turn, depth+1));
                }     

            }
            else {
                //noone can play
                step.AvailableInTurn2=30;
                step.AvailableInTurn1=30;
                maxFlow = Math.Max(maxFlow, Dfs2(graph, step, turn, depth+1));
            }

            return maxFlow;


        }

        public static void SolvePart2() {            
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\16-ProboscideaVolcanium\input.txt");  
            string line;
            var nodes = new Dictionary<string,Node>();
            var valves = new List<Valve>();
            var nodesToOpen=0;
            var index=0;
            valves.Add(new Valve{Id=index++, Name="AA",Flow=0});
            while((line=file.ReadLine())!=null) {
                if (line.Length!=0) {
                   var data = line.Replace("Valve ","")
                                  .Replace(" has flow rate="," ")
                                  .Replace("; tunnels lead to valves "," ")
                                  .Replace("; tunnel leads to valve "," ")
                                  .Replace(", ",",")
                                  .Split(' ');                                  
                    var node = new Node(data[0],int.Parse(data[1]),data[2].Split(','));
                    if (node.Flow>0) {
                        node.Index=index++;
                        valves.Add(node.GetValve());
                    }
                    nodes.Add(node.Id, node);
                    if (node.Flow>0) nodesToOpen++;
                }
            }
            
            //Simplify graph & compute distances
            var n = valves.Count();
            var graph = valves.ToArray();
            for (var first=0;first<n;++first) {
                graph[first].Distances = new int[n];
                var step=0;
                var toExplore=new List<string>{graph[first].Name};
                var alreadySeen = new HashSet<string>{graph[first].Name};
                while(toExplore.Any()) {
                    step++;
                    var next=new List<string>();
                    foreach(var current in toExplore) {
                        foreach(var neighbour in nodes[current].Next) {
                            if (alreadySeen.Contains(neighbour)) continue;
                            next.Add(neighbour);
                            alreadySeen.Add(neighbour);
                            if (nodes[neighbour].Index>0) {
                                graph[first].Distances[nodes[neighbour].Index]=step;
                            }
                        }
                    }
                    toExplore=next;
                }
            }
            
            //graph is complete now
            var maxFlow=Dfs2(graph, new Step2(0,0,0,n), 0, 0);
/*















            var turns = new List<Step2>[27];            
            var seen = new Dictionary<string,int>[27];
            for (var i=0;i<27;++i) {
                turns[i] = new List<Step2>();
                seen[i] = new Dictionary<string,int>();
            }
            turns[0].Add(new Step2(0,0,0,n));
            for (var turn=0;turn<26;++turn) {
                foreach(var step in turns[turn]) {
                    step.TotalFlow+=step.Flow;
                    if (step.AvailableInTurn1 == turn && !step.Finished()) {
                        if (!step.Open[step.Valve1]) {
                            //open valve => mandatory!
                            step.Open[step.Valve1] = true;
                            step.Flow += graph[step.Valve1].Flow;
                            step.AvailableInTurn1++;
                            if (step.AvailableInTurn2!=turn) {
                                var key = step.ToString();
                                //no need to add flow (only 1 turn)
                                if (step.NextTurn>26 || seen[step.NextTurn].ContainsKey(key) && seen[step.NextTurn][key]>=step.TotalFlow) continue;
                                seen[step.NextTurn][key]=step.TotalFlow;                                
                                turns[step.NextTurn].Add(step);
                                continue;
                            }
                            //play 2
                            if (!step.Open[step.Valve2]) {
                                //open valve => mandatory!
                                step.Open[step.Valve2] = true;
                                step.Flow += graph[step.Valve2].Flow;
                                step.AvailableInTurn2++;
                                var key = step.ToString();
                                //no need to add flow (only 1 turn)
                                if (step.NextTurn>26 || seen[step.NextTurn].ContainsKey(key) && seen[step.NextTurn][key]>=step.TotalFlow) continue;
                                seen[step.NextTurn][key]=step.TotalFlow;
                                turns[step.NextTurn].Add(step);
                                continue;
                            }
                            //move 2
                            for(var i=0;i<n;++i) {
                                if (step.Open[i]) continue;
                                var nextStep = step.Clone();
                                nextStep.AvailableInTurn2+=graph[step.Valve2].Distances[i];
                                nextStep.Valve2=i;
                                var key = nextStep.ToString();
                                //no need to add flow (only 1 turn)
                                if (nextStep.NextTurn>26 || seen[nextStep.NextTurn].ContainsKey(key) && seen[nextStep.NextTurn][key]>=nextStep.TotalFlow) continue;
                                seen[nextStep.NextTurn][key]=nextStep.TotalFlow;
                                turns[nextStep.NextTurn].Add(nextStep);
                                continue;
                            }                     
                            continue;      
                        }
                        //move 1
                        for(var i=0;i<n;++i) {
                            if (step.Open[i]) continue;
                            var nextStep = step.Clone();
                            nextStep.AvailableInTurn1+=graph[step.Valve1].Distances[i];
                            nextStep.Valve1=i;
                            if (nextStep.AvailableInTurn2!=turn) {
                                var key = nextStep.ToString();
                                nextStep.TotalFlow += nextStep.Flow * (nextStep.NextTurn - turn - 1);
                                if (nextStep.NextTurn>26 || seen[nextStep.NextTurn].ContainsKey(key) && seen[nextStep.NextTurn][key]>=nextStep.TotalFlow) continue;
                                seen[nextStep.NextTurn][key]=nextStep.TotalFlow;
                                turns[nextStep.NextTurn].Add(nextStep);
                                continue;
                            }
                            //play 2
                            if (!nextStep.Open[nextStep.Valve2]) {
                                //open valve => mandatory!
                                nextStep.Open[nextStep.Valve2] = true;
                                nextStep.Flow += graph[nextStep.Valve2].Flow;
                                nextStep.AvailableInTurn2++;
                                var key = nextStep.ToString();
                                nextStep.TotalFlow += nextStep.Flow * (nextStep.NextTurn - turn - 1);
                                if (nextStep.NextTurn>26 || seen[nextStep.NextTurn].ContainsKey(key) && seen[nextStep.NextTurn][key]>=nextStep.TotalFlow) continue;
                                seen[nextStep.NextTurn][key]=nextStep.TotalFlow;
                                turns[nextStep.NextTurn].Add(nextStep);
                                continue;
                            }
                            //move 2
                            for(var j=0;j<n;++j) {
                                if (nextStep.Open[j]) continue;
                                var nextStep2 = nextStep.Clone();
                                nextStep2.AvailableInTurn2+=graph[nextStep2.Valve2].Distances[j];
                                nextStep2.Valve2 = j;
                                var key = nextStep2.ToString();
                                nextStep2.TotalFlow += nextStep2.Flow * (nextStep2.NextTurn - turn - 1);
                                if (nextStep2.NextTurn>26 || seen[nextStep2.NextTurn].ContainsKey(key) && seen[nextStep2.NextTurn][key]>=nextStep2.TotalFlow) continue;
                                seen[nextStep2.NextTurn][key]=nextStep2.TotalFlow;
                                turns[nextStep2.NextTurn].Add(nextStep2);
                                continue;
                            }
                        }
                    }
                    else if (step.AvailableInTurn2==turn && !step.Finished()) {
                        //play only 2
                        if (!step.Open[step.Valve2]) {
                            //open valve => mandatory!
                            step.Open[step.Valve2] = true;
                            step.Flow += graph[step.Valve2].Flow;
                            step.AvailableInTurn2++;
                            var key = step.ToString();
                            //no need to add flow (only 1 turn)
                            if (step.NextTurn>26 || seen[step.NextTurn].ContainsKey(key) && seen[step.NextTurn][key]>=step.TotalFlow) continue;
                            seen[step.NextTurn][key]=step.TotalFlow;
                            turns[step.NextTurn].Add(step);
                            continue;
                        }
                        //move 2
                        for(var i=0;i<n;++i) {
                            if (step.Open[i]) continue;
                            var nextStep = step.Clone();
                            nextStep.AvailableInTurn2+=graph[step.Valve2].Distances[i];
                            nextStep.Valve2=i;
                            var key = nextStep.ToString();
                            nextStep.TotalFlow += nextStep.Flow * (nextStep.NextTurn - turn - 1);
                            if (nextStep.NextTurn>26 || seen[nextStep.NextTurn].ContainsKey(key) && seen[nextStep.NextTurn][key]>=nextStep.TotalFlow) continue;
                            seen[nextStep.NextTurn][key]=nextStep.TotalFlow;
                            turns[nextStep.NextTurn].Add(nextStep);
                            continue;
                        }
                    }
                    else {
                        //noone can play
                        step.TotalFlow+=step.Flow * (26 - turn-1);
                        var key = step.ToString();
                        if (seen[26].ContainsKey(key) && seen[26][key]>=step.TotalFlow) continue;
                        seen[26][key]=step.TotalFlow;
                        turns[26].Add(step);
                    }
                }
                seen[turn]=null;
                turns[turn]=null;
            }

            foreach(var step in turns[26])
                maxFlow=Math.Max(maxFlow,step.TotalFlow);
*/

            Console.WriteLine($"Part2: maxFlow is {maxFlow}");   
        }
    }
}

