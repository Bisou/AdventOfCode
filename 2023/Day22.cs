using System.Runtime.ExceptionServices;
using System.Text;

public class Day22
{
    private const int XMIN=0;
    private const int YMIN=1;
    private const int ZMIN=2;
    private const int XMAX=3;
    private const int YMAX=4;
    private const int ZMAX=5;
    
    private class Brick {
        public int Xmin;
        public int Xmax;
        public int Ymin;
        public int Ymax;
        public int Zmin;
        public int Zmax;
        public int Id;
        public HashSet<int> Above = new HashSet<int>();

        public Brick(int id, int[] data)
        {
            Id=id;
            Xmin=data[XMIN];
            Ymin=data[YMIN];
            Zmin=data[ZMIN];
            Xmax=data[XMAX];
            Ymax=data[YMAX];
            Zmax=data[ZMAX];            
        }

        public bool IsUnder(Brick other) {
            for(var x=Xmin;x<=Xmax;++x) {
                for (var y=Ymin;y<=Ymax;++y) {
                    if (other.Xmin<=x && x<=other.Xmax && other.Ymin<=y && y<=other.Ymax && Zmax<=other.Zmin) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day22-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day22-{dataType}.txt");   
        var bricks = new List<Brick>();
        var id=0;
        foreach(var line in input) {
            var brick = new Brick(id++,line.Split(new char[]{',','~'},StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray());
            bricks.Add(brick);  
        } 
        var maxX=bricks.Max(b => b.Xmax)+1;
        var maxY=bricks.Max(b => b.Ymax)+1;
        var top = new (int Height, int BrickId)[maxX,maxY];
        for (var x=0;x<maxX;++x) {
            for (var y=0;y<maxY;++y) {
                top[x,y]=(0,-1);
            }
        }
        var fallenBricks=new Dictionary<int, Brick>();
        var canDestroy=new HashSet<int>();
        var count=0;
        while(bricks.Any()) {
            var nextBrick = bricks.First(b => !bricks.Any(b2 => b2.Id!=b.Id && b2.IsUnder(b)));
            bricks.Remove(nextBrick);
            //make it fall
            var height=nextBrick.Zmax-nextBrick.Zmin+1;   
            var newBase=0;
            for (var x=nextBrick.Xmin;x<=nextBrick.Xmax;++x) {
                for (var y=nextBrick.Ymin;y<=nextBrick.Ymax;++y) {
                    newBase = Math.Max(newBase,top[x,y].Height);
                }
            }
            
            nextBrick.Zmin=newBase+1;
            nextBrick.Zmax=nextBrick.Zmin+height;
            for (var x=nextBrick.Xmin;x<=nextBrick.Xmax;++x) {
                for (var y=nextBrick.Ymin;y<=nextBrick.Ymax;++y) {
                    if (top[x,y].Height==newBase && newBase>0) {
                        nextBrick.Above.Add(top[x,y].BrickId);
                    }
                    top[x,y]=(nextBrick.Zmax, nextBrick.Id);
                }
            }
            fallenBricks.Add(nextBrick.Id, nextBrick);
            canDestroy.Add(nextBrick.Id);
        }
        
        foreach(var brick in fallenBricks.Values) {
            if (brick.Above.Count==1) {
                canDestroy.Remove(brick.Above.First());
            }
        }

        Console.WriteLine($"Part1: answer is {canDestroy.Count()}");
    }

    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day22-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day22-{dataType}.txt");   
        var result=0;    
        Console.WriteLine($"Part1: answer is {result}");
    }

}
