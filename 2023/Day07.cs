public class Day07
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day07-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day07-{dataType}.txt").Select(line => new Hand(line)).OrderBy(x => x).ToArray();     
        var score=0L;
        for (var i=0;i<input.Length;++i) {
            score += input[i].Bid * (i+1);
        }
        Console.WriteLine($"Part1: total winnings is {score}");
    }
    
    public class Hand : IComparable<Hand> {
        public string Cards;
        public long Bid;
        public Hand(string line)
        {
            var data = line.Split(' ');
            Cards = data[0];
            Bid = long.Parse(data[1]);
        }

        private static Dictionary<char, int> value = new Dictionary<char,int> {
            {'2',2},
            {'3',3},
            {'4',4},
            {'5',5},
            {'6',6},
            {'7',7},
            {'8',8},
            {'9',9},
            {'T',10},
            {'J',11},
            {'Q',12},
            {'K',13},
            {'A',14}
        };
        
        public int CompareTo(object obj) {
            if (obj == null) return 1;

            Hand other = obj as Hand;
            if (other==null) return 1;
            return CompareTo(other);
        }

        public int CompareTo(Hand other) {
            var otherStrength =other.GetStrength();
            var myStrength= GetStrength();
            if (myStrength>otherStrength) return 1;
            if (myStrength<otherStrength) return -1;
            for(var i=0;i<5;++i) {
                if (value[Cards[i]]>value[other.Cards[i]]) return 1;
                if (value[Cards[i]]<value[other.Cards[i]]) return -1;
            }
            return 0;
        }

        public int GetStrength() {
            var groups = Cards.GroupBy(c => c).OrderByDescending(g => g.Count()).ToArray();
            if (groups.Length==1) {
                return 6; //5 of a kind
            } else if (groups.Length==2) {
                if (groups[0].Count()==4) {
                    return 5; //4 of a kind
                } else {
                    return 4; //full house
                }
            } else if (groups.Length==3) {
                if (groups[0].Count()==3) {
                    return 3; //3 of a kind
                } else {
                    return 2; //2 pairs
                }
            } else if (groups.Length==4) {
                return 1; //1 pair
            }
            return 0; //High card
        }
    }
    
    public class HandWithJoker : IComparable<HandWithJoker> {
        public string Cards;
        public long Bid;
        public int Strength;

        public HandWithJoker(string line)
        {
            var data = line.Split(' ');
            Cards = data[0];
            Bid = long.Parse(data[1]);
            Strength = GetStrength();
        }

        private static Dictionary<char, int> value = new Dictionary<char,int> {
            {'J',1},
            {'2',2},
            {'3',3},
            {'4',4},
            {'5',5},
            {'6',6},
            {'7',7},
            {'8',8},
            {'9',9},
            {'T',10},
            {'Q',12},
            {'K',13},
            {'A',14}
        };
        
        public int CompareTo(object obj) {
            if (obj == null) return 1;

            HandWithJoker other = obj as HandWithJoker;
            if (other==null) return 1;
            return CompareTo(other);
        }

        public int CompareTo(HandWithJoker other) {
            if (Strength>other.Strength) return 1;
            if (Strength<other.Strength) return -1;
            for(var i=0;i<5;++i) {
                if (value[Cards[i]]>value[other.Cards[i]]) return 1;
                if (value[Cards[i]]<value[other.Cards[i]]) return -1;
            }
            return 0;
        }

        public int GetStrength() {
            var bestStrength=0;
            var groups = Cards.GroupBy(c => c).OrderByDescending(g => g.Count()).ToArray();
            var bestCard = groups[0].Key;
            if (bestCard=='J' && groups.Length>1) bestCard=groups[1].Key;
            var bestCombination = Cards.Replace('J',bestCard);
            groups = bestCombination.GroupBy(c => c).OrderByDescending(g => g.Count()).ToArray();
            if (groups.Length==1) {
                return 6; //5 of a kind
            } else if (groups.Length==2) {
                if (groups[0].Count()==4) {
                    return 5; //4 of a kind
                } else {
                    return 4; //full house
                }
            } else if (groups.Length==3) {
                if (groups[0].Count()==3) {
                    return 3; //3 of a kind
                } else {
                    return 2; //2 pairs
                }
            } else if (groups.Length==4) {
                return 1; //1 pair
            }
            return 0; //High card
        }
    }

    public static void SolvePart2(string dataType)
    {      
        Console.WriteLine($"Day07-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day07-{dataType}.txt").Select(line => new HandWithJoker(line)).OrderBy(x => x).ToArray();     
        var score=0L;
        for (var i=0;i<input.Length;++i) {
            score += input[i].Bid * (i+1);
        }
        Console.WriteLine($"Part2: total winnings is {score}");
    }
}
