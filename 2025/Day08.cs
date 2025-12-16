using System.Text;

public class Day08
{
    private const int day = 8;
    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var pairs = dataType=="Sample"?10:1000;
        var points = new List<long[]>();
        foreach (var line in input)
        {
            points.Add(line.Split(',').Select(long.Parse).ToArray());
        }
        var segments = new List<(int A, int B, double Distance)>();
        for (var i=0;i<points.Count;++i)
        {
            for (var j=i+1;j<points.Count;++j)
            {
                double dist = (points[i][0] - points[j][0]) * (points[i][0] - points[j][0])
                            + (points[i][1] - points[j][1]) * (points[i][1] - points[j][1])
                            + (points[i][2] - points[j][2]) * (points[i][2] - points[j][2]);
                segments.Add((i,j,dist));
            }
        }

        var uf = new UnionFind(points.Count);
        foreach (var segment in segments.OrderBy(s => s.Distance).Take(pairs))
        {
            uf.Union(segment.A, segment.B);
        }

        var groups = Enumerable.Range(0,points.Count).GroupBy(x => uf.Find(x));

        
        Console.WriteLine($"Part1: result is {groups.OrderByDescending(g => g.Count()).Take(3).Aggregate(1, (acc, currg) => acc*currg.Count())}");
    }
    

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var points = new List<long[]>();
        foreach (var line in input)
        {
            points.Add(line.Split(',').Select(long.Parse).ToArray());
        }
        var segments = new List<(int A, int B, double Distance)>();
        for (var i=0;i<points.Count;++i)
        {
            for (var j=i+1;j<points.Count;++j)
            {
                double dist = (points[i][0] - points[j][0]) * (points[i][0] - points[j][0])
                            + (points[i][1] - points[j][1]) * (points[i][1] - points[j][1])
                            + (points[i][2] - points[j][2]) * (points[i][2] - points[j][2]);
                segments.Add((i,j,dist));
            }
        }

        var uf = new UnionFind(points.Count);
        var disjointCircuits = points.Count;
        foreach (var segment in segments.OrderBy(s => s.Distance))
        {
            if (uf.Connected(segment.A, segment.B)) continue;
            uf.Union(segment.A, segment.B);
            disjointCircuits--;
            if (disjointCircuits==1)
            {
                long result = points[segment.A][0];
                result *= points[segment.B][0];
                Console.WriteLine($"Part2: result is {result}");
                break;
            }
        }
    }
}



public sealed class UnionFind
{
    private readonly int[] _parent;
    private readonly int[] _size; // or use "rank"; size tends to perform very well

    public int Count { get; }

    public UnionFind(int n)
    {
        if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n));
        Count = n;
        _parent = new int[n];
        _size = new int[n];
        for (int i = 0; i < n; i++)
        {
            _parent[i] = i; // each node is its own parent initially
            _size[i] = 1;   // each set has size 1
        }
    }

    /// <summary>
    /// Finds the representative (root) of the set containing x.
    /// Path compression flattens the tree for near-constant amortized time.
    /// </summary>
    public int Find(int x)
    {
        if (x < 0 || x >= Count) throw new ArgumentOutOfRangeException(nameof(x));
        int root = x;
        // Find the root
        while (root != _parent[root])
            root = _parent[root];

        // Path compression
        while (x != root)
        {
            int next = _parent[x];
            _parent[x] = root;
            x = next;
        }

        return root;
    }

    /// <summary>
    /// Unions the sets containing a and b. Returns true if a merge happened.
    /// Uses union by size to keep trees shallow.
    /// </summary>
    public bool Union(int a, int b)
    {
        int rootA = Find(a);
        int rootB = Find(b);
        if (rootA == rootB) return false; // already in same set

        // attach smaller to larger
        if (_size[rootA] < _size[rootB])
        {
            _parent[rootA] = rootB;
            _size[rootB] += _size[rootA];
        }
        else
        {
            _parent[rootB] = rootA;
            _size[rootA] += _size[rootB];
        }

        return true;
    }

    /// <summary>
    /// Checks if two elements are in the same set.
    /// </summary>
    public bool Connected(int a, int b) => Find(a) == Find(b);

    /// <summary>
    /// Returns the size of the set containing x.
    /// </summary>
    public int SetSize(int x) => _size[Find(x)];
}