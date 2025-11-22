using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class NotEnoughMinerals
    {
        public static void SolvePart1()
        {
            maxTurns=24;
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\19-NotEnoughMinerals\input.txt");  
            string line;
            var sum=0;
            while((line=file.ReadLine())!=null) {
                var onlyData = line.Replace("Blueprint ","")
                               .Replace(": Each ore robot costs "," ")
                               .Replace(" ore. Each clay robot costs "," ")
                               .Replace(" ore. Each obsidian robot costs "," ")
                               .Replace(" ore and "," ")
                               .Replace(" clay. Each geode robot costs "," ")
                               .Replace(" obsidian.","");
                var data = onlyData.Split(' ').Select(int.Parse).ToArray();
                var maxGeode=GetMaxGeode(data[1], data[2], data[3], data[4], data[5], data[6],0,new[]{0,0,0,0},new[]{1,0,0,0});
                Console.WriteLine($"{DateTime.Now}: Blueprint {data[0]} allows us to get {maxGeode} geodes");
                sum+=maxGeode*data[0];
            }

            Console.WriteLine($"Part1: sum of all quality is {sum}.");
        }
        
        public static void SolvePart2()
        {
            maxTurns=32;
            var file = new System.IO.StreamReader(@"C:\Users\Bisou\Documents\Seb\Code\AdventOfCode\2022\19-NotEnoughMinerals\input.txt");  
            string line;
            var product=1L;
            var id=0;
            while((line=file.ReadLine())!=null) {
                var onlyData = line.Replace("Blueprint ","")
                               .Replace(": Each ore robot costs "," ")
                               .Replace(" ore. Each clay robot costs "," ")
                               .Replace(" ore. Each obsidian robot costs "," ")
                               .Replace(" ore and "," ")
                               .Replace(" clay. Each geode robot costs "," ")
                               .Replace(" obsidian.","");
                var data = onlyData.Split(' ').Select(int.Parse).ToArray();
                if (id<3) {
                    long maxGeode=GetMaxGeodeBeamSearch(data[1], data[2], data[3], data[4], data[5], data[6]);
                    Console.WriteLine($"{DateTime.Now}: Blueprint {data[0]} allows us to get {maxGeode} geodes");
                    product *=maxGeode;
                }
                id++;
            }

            Console.WriteLine($"Part2: product of all geodes for 3 first blueprints is {product}.");
        }

        private static int maxTurns;

        public class State : IEquatable<State> {
            public int Ore;
            public int Clay;
            public int Obsidian;
            public int Geode;
            public int OreRobot;
            public int ClayRobot;
            public int ObsidianRobot;
            public int GeodeRobot;          

            public State() {
                OreRobot=1;
            }

            public State Clone() {
                var clone = new State();
                clone.Ore=Ore;
                clone.Clay=Clay;
                clone.Obsidian=Obsidian;
                clone.Geode=Geode;
                clone.OreRobot=OreRobot;
                clone.ClayRobot=ClayRobot;
                clone.ObsidianRobot=ObsidianRobot;
                clone.GeodeRobot=GeodeRobot;
                return clone;
            }

            public bool Equals(State other)
            {
                if (other == null)
                    return false;

                if (Ore == other.Ore && Clay==other.Clay && Obsidian==other.Obsidian && Geode==other.Geode && 
                    OreRobot == other.OreRobot && ClayRobot==other.ClayRobot && ObsidianRobot==other.ObsidianRobot && GeodeRobot==other.GeodeRobot)
                    return true;
                else
                    return false;
            }

            public override bool Equals(Object obj)
            {
                if (obj == null)
                    return false;

                State state = obj as State;
                if (state == null)
                    return false;
                else
                    return Equals(state);
            }

            public override int GetHashCode()
            {
                var hash = Ore;
                hash = (hash*397) ^ Clay;
                hash = (hash*397) ^ Obsidian;
                hash = (hash*397) ^ Geode;
                hash = (hash*397) ^ OreRobot;
                hash = (hash*397) ^ ClayRobot;
                hash = (hash*397) ^ ObsidianRobot;
                hash = (hash*397) ^ GeodeRobot;
                return hash;
            }
        }

        private static int GetMaxGeodeBeamSearch(int oreRobotCostInOre, int clayRobotCostInOre, 
                                int obsidianRobotCostInOre, int obsidianRobotCostInClay, 
                                int geodeRobotCostInOre, int geodeRobotCostInObsidian) {
            var maxGeode=0;            
            var maxObsidian = geodeRobotCostInObsidian;
            var maxClay = obsidianRobotCostInClay;
            var maxOre = Math.Max(geodeRobotCostInOre, Math.Max(obsidianRobotCostInOre, clayRobotCostInOre));
            
            //state is materials then robots
            var start = new State();
            var todo = new List<State>{start};
            for (var i=0;i<maxTurns;++i) {
                Console.WriteLine($"Turn {i}: {todo.Count} states");
                var next = new List<State>();
                var maxGeodeThisTurn=0;
                var seen = new HashSet<State>();
                foreach(var state in todo) {                    
                    //can build
                    var build=new bool[4];
                    if (state.Ore>=oreRobotCostInOre && state.OreRobot<maxOre) {
                        build[0]=true;
                    }
                    if (state.Ore>=clayRobotCostInOre && state.ClayRobot<maxClay) {
                        build[1]=true;
                    }
                    if (state.Ore>=obsidianRobotCostInOre && state.Clay>=obsidianRobotCostInClay && state.ObsidianRobot<maxObsidian) {
                        build[2]=true;
                    }
                    if (state.Ore>=geodeRobotCostInOre && state.Obsidian>=geodeRobotCostInObsidian) {
                        build[3]=true;
                    }
                    //collect
                    state.Ore+=state.OreRobot;
                    state.Clay+=state.ClayRobot;
                    state.Obsidian+=state.ObsidianRobot;
                    state.Geode+=state.GeodeRobot;
                    maxGeodeThisTurn = Math.Max(maxGeodeThisTurn, state.Geode);

                    //real build
                    if (build[0]) {
                        var nextState = state.Clone();
                        nextState.Ore-=oreRobotCostInOre;
                        nextState.OreRobot++;
                        if (!seen.Contains(nextState)) {
                            next.Add(nextState);
                            seen.Add(nextState);
                        }
                    }
                    if (build[1]) {
                        var nextState = state.Clone();
                        nextState.Ore-=clayRobotCostInOre;
                        nextState.ClayRobot++;
                        if (!seen.Contains(nextState)) {
                            next.Add(nextState);
                            seen.Add(nextState);
                        }
                    }
                    if (build[2]) {
                        var nextState = state.Clone();
                        nextState.Ore-=obsidianRobotCostInOre;
                        nextState.Clay-=obsidianRobotCostInClay;
                        nextState.ObsidianRobot++;
                        if (!seen.Contains(nextState)) {
                            next.Add(nextState);
                            seen.Add(nextState);
                        }
                    }
                    if (build[3]) {
                        var nextState = state.Clone();
                        nextState.Ore-=geodeRobotCostInOre;
                        nextState.Obsidian-=geodeRobotCostInObsidian;
                        nextState.GeodeRobot++;
                        if (!seen.Contains(nextState)) {
                            next.Add(nextState);
                            seen.Add(nextState);
                        }
                    }
                    if (build.Any(b => !b))
                    {
                        next.Add(state);
                        seen.Add(state);
                    }
                }
                todo = next.Where(s => s.Geode>=maxGeodeThisTurn-2).ToList();
            }            

            maxGeode=todo.Max(s => s.Geode);
            return maxGeode;
        }

        private static int GetMaxGeode(int oreRobotCostInOre, int clayRobotCostInOre, 
                                int obsidianRobotCostInOre, int obsidianRobotCostInClay, 
                                int geodeRobotCostInOre, int geodeRobotCostInObsidian, 
                                int turn, int[] materials, int[] robots) {
            var maxGeode=0;            
            var maxObsidian = geodeRobotCostInObsidian;
            var maxClay = obsidianRobotCostInClay;
            var maxOre = Math.Max(geodeRobotCostInOre, Math.Max(obsidianRobotCostInOre, clayRobotCostInOre));
                        
            for (var i=turn;i<maxTurns;++i) {
                //can build
                var build=new bool[4];
                if (materials[0]>=oreRobotCostInOre && robots[0]<maxOre) {
                    build[0]=true;
                }
                if (materials[0]>=clayRobotCostInOre && robots[1]<maxClay) {
                    build[1]=true;
                }
                if (materials[0]>=obsidianRobotCostInOre && materials[1]>=obsidianRobotCostInClay && robots[2]<maxObsidian) {
                    build[2]=true;
                }
                if (materials[0]>=geodeRobotCostInOre && materials[2]>=geodeRobotCostInObsidian) {
                    build[3]=true;
                }

                //collect
                materials[0]+=robots[0];
                materials[1]+=robots[1];
                materials[2]+=robots[2];
                materials[3]+=robots[3];

                //real build
                if (build[0]) {
                    maxGeode=Math.Max(maxGeode, GetMaxGeode(oreRobotCostInOre, clayRobotCostInOre, 
                                obsidianRobotCostInOre, obsidianRobotCostInClay, 
                                geodeRobotCostInOre, geodeRobotCostInObsidian, 
                                i+1, new[] {materials[0]-oreRobotCostInOre, materials[1], materials[2], materials[3]}, 
                                new[] {robots[0]+1, robots[1], robots[2], robots[3]}));
                }
                if (build[1]) {
                    maxGeode=Math.Max(maxGeode, GetMaxGeode(oreRobotCostInOre, clayRobotCostInOre, 
                                obsidianRobotCostInOre, obsidianRobotCostInClay, 
                                geodeRobotCostInOre, geodeRobotCostInObsidian, 
                                i+1, new[] {materials[0]-clayRobotCostInOre, materials[1], materials[2], materials[3]}, 
                                new[] {robots[0], robots[1]+1, robots[2], robots[3]}));
                }
                if (build[2]) {
                    maxGeode=Math.Max(maxGeode, GetMaxGeode(oreRobotCostInOre, clayRobotCostInOre, 
                                obsidianRobotCostInOre, obsidianRobotCostInClay, 
                                geodeRobotCostInOre, geodeRobotCostInObsidian, 
                                i+1, new[] {materials[0]-obsidianRobotCostInOre, materials[1]-obsidianRobotCostInClay, materials[2], materials[3]}, 
                                new[] {robots[0], robots[1], robots[2]+1, robots[3]}));
                }
                if (build[3]) {
                    maxGeode=Math.Max(maxGeode, GetMaxGeode(oreRobotCostInOre, clayRobotCostInOre, 
                                obsidianRobotCostInOre, obsidianRobotCostInClay, 
                                geodeRobotCostInOre, geodeRobotCostInObsidian, 
                                i+1, new[] {materials[0]-geodeRobotCostInOre, materials[1], materials[2]-geodeRobotCostInObsidian, materials[3]}, 
                                new[] {robots[0], robots[1], robots[2], robots[3]+1}));
                }
            }            
            maxGeode=Math.Max(maxGeode, materials[3]);
            return maxGeode;
        }
    }
}

