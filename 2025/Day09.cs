using System.Text;

public class Day09
{
    private const int day = 9;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var points = new List<long[]>();
        foreach (var line in input)
        {
            points.Add(line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray());
        }
        var res = 0L;
        for (var i=0;i<points.Count;++i)
        {
            for (var j=i+1;j<points.Count;++j)
            {
                var size = (Math.Abs(points[i][0]-points[j][0])+1) * (Math.Abs(points[i][1]-points[j][1])+1);
                res = Math.Max(res, size);
            }
        }
        Console.WriteLine($"Part1: max rectangle size is {res}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var points = new List<long[]>();
        foreach (var line in input)
        {
            points.Add(line.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray());
        }


        //do not do a distinct
        //a distinct will fail in this case since the top walls will be too close:
        /*
           ***  ***  ***  ***
           * *  * *  * *  * * 
           * **** **** **** *
           *                *
           ******************
        */
        //not having a distinct means we have all coordinates duplicated.
        //since we take the 1st one, the second one will always be empty and will be useful to separate our walls
        var allX = points.Select(p => p[0]).ToList(); 
        allX.Add(0);
        allX = allX.OrderBy(x => x).ToList();        
        var allY = points.Select(p => p[1]).ToList();
        allY.Add(0);
        allY = allY.OrderBy(y => y).ToList();
        var compressedPoints = new List<long[]>();
        foreach(var point in points)
        {
            var x1=0;
            while(allX[x1]<point[0]) ++x1;            
            var y1=0;
            while(allY[y1]<point[1]) ++y1;
            compressedPoints.Add(new long[] {x1,y1});
        }

        var maxX = compressedPoints.Max(p => p[0])+2; //+1 is not enough : we do not have the right border for our floodfill
        var maxY = compressedPoints.Max(p => p[1])+2;
        
        var coloredPoints = new int[maxX,maxY]; //0=not seen, 1=outside, 2=inside
        //color all borders
        for (var i=0;i<compressedPoints.Count;++i)
        {
            coloredPoints[compressedPoints[i][0], compressedPoints[i][1]] = 2;
            if (compressedPoints[i][0]==compressedPoints[(i+1)%compressedPoints.Count][0])
            {
                for (var j=Math.Min(compressedPoints[i][1], compressedPoints[(i+1)%compressedPoints.Count][1]);
                    j <= Math.Max(compressedPoints[i][1], compressedPoints[(i+1)%compressedPoints.Count][1]);
                    ++j)
                {
                    coloredPoints[compressedPoints[i][0], j] = 2;
                }
            } else
            {
                for (var j=Math.Min(compressedPoints[i][0], compressedPoints[(i+1)%compressedPoints.Count][0]);
                    j <= Math.Max(compressedPoints[i][0], compressedPoints[(i+1)%compressedPoints.Count][0]);
                    ++j)
                {
                    coloredPoints[j, compressedPoints[i][1]] = 2;
                }
            }
        }


        //mark the outside of the shape
        var todo = new Queue<(int X, int Y)>();
        var shiftX= new int [] {-1,0,1,0};
        var shiftY= new int [] {0,-1,0,1};
        todo.Enqueue((0,0));
        coloredPoints[0,0]=1;
        while (todo.Any()) {
            var current = todo.Dequeue();
            for (var way=0;way<shiftX.Length;way++)
            {
                var newX=current.X+shiftX[way];
                var newY=current.Y+shiftY[way];
                if (newX>=0 && newY>=0 && newX<maxX && newY<maxY && coloredPoints[newX,newY]==0)
                {
                    coloredPoints[newX,newY]=1;
                    todo.Enqueue((newX,newY));
                }
            }
        }
        
        using var sw = new StreamWriter("output.txt");
        //color the inside of the shape        
        for (var y=0; y<maxY; ++y)
        {
            for (var x=0; x<maxX; ++x)
            {
                if (coloredPoints[x,y]==0)
                {
                    coloredPoints[x,y]=3;
                }
                // Console.Write(coloredPoints[x,y]);
                sw.Write(coloredPoints[x,y]);
                
            }
            sw.WriteLine();
            // Console.WriteLine();
        }

        sw.Close();
        
        var res = 0L;
        for (var i=0;i<compressedPoints.Count;++i)
        {
            for (var j=i+1;j<compressedPoints.Count;++j)
            {
                var size = (Math.Abs(points[i][0]-points[j][0])+1) * (Math.Abs(points[i][1]-points[j][1])+1);
                if (size>res && AllAreColored(compressedPoints[i], compressedPoints[j], coloredPoints)) {
                    res = size;
                }
            }
        }

        Console.WriteLine($"Part2: result is {res}");
    }

    private static bool AllAreColored(long[] cornerA, long[] cornerB, int[,] coloredPoints)
    {
        for (var x=Math.Min(cornerA[0], cornerB[0]); x<=Math.Max(cornerA[0], cornerB[0]); ++x)
        {
            for (var y=Math.Min(cornerA[1], cornerB[1]); y<=Math.Max(cornerA[1], cornerB[1]); ++y)
            {
                if (coloredPoints[x,y]==1)
                {
                    return false;
                }
            }
        }
        return true;
    }
}