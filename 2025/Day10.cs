using System.Drawing;
using Microsoft.Z3;

public class Day10
{
    private const int day = 10;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var powersOfTwo = new long[15];
        for (var i=0;i<powersOfTwo.Length;++i)
        {
            powersOfTwo[i]= 1<<i;
        }
        var res = 0L;
        foreach (var line in input)
        {
            var data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var pattern = data[0];
            var target = 0L;
            var power=0;
            foreach(var c in pattern[1..^1])
            { 
                if (c=='#') target+=powersOfTwo[power];
                power++;
            }
            var buttons = new List<long>();
            foreach(var buttonPresses in data.Skip(1).Take(data.Length-2))
            {
                var press = 0L;
                foreach (var p in buttonPresses.Split(new [] {',','(',')'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse))
                {
                    press += powersOfTwo[p];
                }
                buttons.Add(press);
            }
            var seen = new HashSet<long>();
            seen.Add(0);
            var step=0;
            var todo = new List<long>{0L};
            while(!seen.Contains(target))
            {
                var next = new List<long>();
                foreach (var state in todo)
                {
                    foreach (var button in buttons)
                    {
                        var nextState = state ^ button;
                        if (seen.Contains(nextState)) continue;
                        seen.Add(nextState);
                        next.Add(nextState);
                    }
                }
                ++step;
                todo=next;
            } 
            res += step;
        }
        Console.WriteLine($"Part1: total steps is {res}");
    }
    
    private static long SlowPart2FOrLessThan7Lights(string line)
    {
        var data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var target = data[^1].Split(new [] {',','}','{'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var size = target.Length;
        var compressedTarget = BitMaskForLessThan7Items.PackUlong(target);
        var buttons = new List<ulong>();
        foreach(var buttonPresses in data.Skip(1).Take(data.Length-2))
        {
            var presses = buttonPresses.Split(new [] {',','(',')'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var button = BitMaskForLessThan7Items.PackButtons(presses);
            buttons.Add(button);
        }

        var seen = new Dictionary<ulong,int>();
        seen.Add(0,0);
        var todo = new Stack<ulong>();
        todo.Push(0L);
        while(todo.Any())
        {
            var state = todo.Pop();
            var step = seen[state];
            foreach (var button in buttons)
            {
                var nextState = state + button;
                if (seen.TryGetValue(nextState, out int alreadySeenSteps))
                {
                    if (alreadySeenSteps <= step+1)
                    {
                        continue; //already seen with less steps
                    }
                } 
                //stop if too many presses on one light
                if (BitMaskForLessThan7Items.TooManyPresses(nextState, compressedTarget, size)) continue;
                seen[nextState] = step+1;
                if (nextState != compressedTarget) {
                    todo.Push(nextState);
                }
            }
        } 
        return seen[compressedTarget];
    }

    private static long SlowPart2(string line)
    {
        var data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var target = data[^1].Split(new [] {',','}','{'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var size = target.Length;
        var compressedTarget = BitMask.Pack(target);
        var buttons = new List<(ulong lo, ulong hi)>();
        foreach(var buttonPresses in data.Skip(1).Take(data.Length-2))
        {
            var presses = buttonPresses.Split(new [] {',','(',')'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var button = BitMask.PackButtons(presses);
            buttons.Add(button);
        }

        var seen = new Dictionary<(ulong lo, ulong hi),int>();
        seen.Add((0,0),0);
        var todo = new Stack<(ulong lo, ulong hi)>();
        todo.Push((0,0));
        while(todo.Any())
        {
            var state = todo.Pop();
            var step = seen[state];
            foreach (var button in buttons)
            {
                var nextState = (state.lo + button.lo, state.hi + button.hi);
                if (seen.TryGetValue(nextState, out int alreadySeenSteps))
                {
                    if (alreadySeenSteps <= step+1)
                    {
                        continue; //already seen with less steps
                    }
                } 
                //stop if too many presses on one light
                if (BitMask.TooManyPresses(nextState, compressedTarget, size)) continue;
                seen[nextState] = step+1;
                if (nextState != compressedTarget) {
                    todo.Push(nextState);
                }
            }
        } 
        return seen[compressedTarget];
    }

    private static long Part2(string line)
    {
        // Step 1: Create a Z3 context
        using (var ctx = new Context())
        {            
            var data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var target = data[^1].Split(new[] { ',', '}', '{' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();            
            var size = target.Length;
            var buttons = new List<List<int>>();
            foreach (var buttonPresses in data.Skip(1).Take(data.Length - 2))
            {
                var presses = buttonPresses.Split(new[] { ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                buttons.Add(presses);
            }
            // Step 2: Define variables
            var variables = new IntExpr[buttons.Count];
            for (var i = 0; i < variables.Length; ++i)
            {
                variables[i] = ctx.MkIntConst($"x{i}");
            }
            var solver = ctx.MkOptimize();
            // Step 3: Define constraints
            for (var b=0;b<buttons.Count;++b)
            {
                // Each variable must be non-negative
                solver.Add(ctx.MkGe(variables[b], ctx.MkInt(0)));
                // Each variable must be an integer
                //solver.Add(ctx.MkEq(ctx.MkMod(variables[b], ctx.MkInt(1)), ctx.MkInt(0)));
            }
            for (var light = 0; light < size; ++light)
            {
                var total = ctx.MkInt(target[light]);
                // The sum of button presses for this light must equal the target
                var sumExprs = new List<ArithExpr>();
                for (var buttonIndex = 0; buttonIndex < buttons.Count; ++buttonIndex)
                {
                    if (buttons[buttonIndex].Contains(light))
                    {
                        sumExprs.Add(variables[buttonIndex]);
                    }
                }
                var sum = ctx.MkAdd(sumExprs.ToArray());
                solver.Add(ctx.MkEq(sum, ctx.MkInt(target[light])));
            }
            // Step 4: Minimize the sum of all variables
            var totalPresses = ctx.MkAdd(variables);
            solver.MkMinimize(totalPresses);

            // Step 5: Solve the problem
            if (solver.Check() == Status.SATISFIABLE)
            {
                //Console.WriteLine("Solution found:");
                var model = solver.Model;
                var res = 0L;
                for (var i = 0; i < variables.Length; ++i)
                {
                    //Console.WriteLine($"x{i} = {model.Evaluate(variables[i])}");
                    res += ((IntNum)model.Evaluate(variables[i])).Int64;
                }
                return res;
            }
            else
            {
                Console.WriteLine("No solution exists.");
                return 0;
            }
        }
    }

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);      
        var input = Tool.ReadAll(day, dataType); 
        var res = 0L;
        foreach (var line in input)
        {        
            //Console.WriteLine(line);    
            var steps = Part2(line);
            res += steps;
            //Console.WriteLine($"Done in {steps} => result = {res}");
        }
        Console.WriteLine($"Part2: total steps is {res}");
    }
}


public static class BitMaskForLessThan7Items
{
    private const int BitsPerItem = 9;      // fits values 0..511 (covers 0..300)
    

    private const int MaxValue     = 300;
    private const int MaxItems     = 10;
    private const int ItemsPerUlong  = 64 / BitsPerItem; // 7
    private const ulong ItemMask = (1UL << BitsPerItem) - 1; // 0x1FF

    public static ulong PackButtons(List<int> buttons)
    {        
        ulong result = 0UL;
        int shift = 0;
        ulong buttonMask = 1;
        foreach(var button in buttons)
        {
            result |= (buttonMask << (BitsPerItem * button));
        }

        return result;
    }

    public static ulong PackUlong(int[] values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));
        if (values.Length == 0) throw new ArgumentException("Values must not be empty.", nameof(values));
        if (values.Length > ItemsPerUlong)
            throw new OverflowException($"At 9 bits per item, a single ulong fits at most {ItemsPerUlong} items.");
        
        ulong result = 0UL;
        int shift = 0;

        foreach (var v in values)
        {
            if (v < 0 || v > 300) throw new ArgumentOutOfRangeException(nameof(values), "Each value must be in 0..300.");
            result |= ((ulong)v & ItemMask) << shift;
            shift += BitsPerItem;
        }

        return result;
    }

    public static int[] UnpackUlong(ulong packed, int count)
    {
        if (count < 1 || count > ItemsPerUlong)
            throw new ArgumentOutOfRangeException(nameof(count), $"count must be 1..{ItemsPerUlong}.");

        var result = new int[count];
        int shift = 0;

        for (int i = 0; i < count; i++)
        {
            result[i] = (int)((packed >> shift) & ItemMask);
            shift += BitsPerItem;
        }

        return result;
    }

    internal static bool TooManyPresses(ulong state, ulong target, int size)
    {
        var shift=0;
        for (var i=0; i<size; ++i)
        {
            if ((state & (ItemMask<<shift)) > (target & (ItemMask<<shift))) return true;
            shift+=BitsPerItem;
        }

        return false;
    }
}


public static class BitMask
{
    private const int BitsPerItem = 9;               // 0..511 capacity; we validate to 0..300
    private const int MaxValue     = 300;
    private const int MaxItems     = 10;
    private const int ItemsPerUlong = 64 / BitsPerItem; // 7 items in lo (63 bits)

    private const ulong ItemMask = (1UL << BitsPerItem) - 1; // 0x1FF

    public static (ulong lo, ulong hi) PackButtons(List<int> buttons)
    {        
        ulong lo = 0, hi = 0;
        var res = (lo,hi);
        foreach(var button in buttons)
        {
            res = SetAt(res, button, 1);
        }

        return res;
    }

    /// <summary>
    /// Packs up to 10 items (each in 0..300) into two ulongs (lo, hi).
    /// LSB-first: values[0] at bit 0 of lo, values[1] at bit 9, etc.
    /// Up to 7 items go into lo; the remainder into hi starting at bit 0.
    /// </summary>
    public static (ulong lo, ulong hi) Pack(int[] values)
    {
        if (values is null) throw new ArgumentNullException(nameof(values));
        if (values.Length == 0 || values.Length > MaxItems)
            throw new ArgumentOutOfRangeException(nameof(values), $"Length must be 1..{MaxItems}.");

        ulong lo = 0, hi = 0;

        // Fill lo (first up to 7 items)
        int first = Math.Min(values.Length, ItemsPerUlong);
        int shift = 0;
        for (int i = 0; i < first; i++)
        {
            int v = values[i];
            if (v < 0 || v > MaxValue) throw new ArgumentOutOfRangeException(nameof(values), "Each value must be in 0..300.");
            lo |= ((ulong)v & ItemMask) << shift;
            shift += BitsPerItem;
        }

        // Fill hi (remaining items)
        shift = 0;
        for (int i = first; i < values.Length; i++)
        {
            int v = values[i];
            if (v < 0 || v > MaxValue) throw new ArgumentOutOfRangeException(nameof(values), "Each value must be in 0..300.");
            hi |= ((ulong)v & ItemMask) << shift;
            shift += BitsPerItem;
        }

        return (lo, hi);
    }

    /// <summary>
    /// Unpacks values from (lo, hi) given the explicit count (1..10).
    /// </summary>
    public static int[] Unpack((ulong lo, ulong hi) packed, int size)
    {
        if (size < 1 || size > MaxItems) throw new ArgumentOutOfRangeException(nameof(size));
        var result = new int[size];

        int first = Math.Min(size, ItemsPerUlong);

        // Read from lo
        int shift = 0;
        for (int i = 0; i < first; i++)
        {
            result[i] = (int)((packed.lo >> shift) & ItemMask);
            shift += BitsPerItem;
        }

        // Read remaining from hi
        shift = 0;
        for (int i = first; i < size; i++)
        {
            result[i] = (int)((packed.hi >> shift) & ItemMask);
            shift += BitsPerItem;
        }

        return result;
    }

    /// <summary>
    /// Gets the item at a specific index (0-based) from (lo, hi).
    /// </summary>
    public static int GetAt((ulong lo, ulong hi) packed, int index)
    {
        if (index < 0 || index >= MaxItems) throw new ArgumentOutOfRangeException(nameof(index));
        if (index < ItemsPerUlong)
        {
            int shift = index * BitsPerItem;
            return (int)((packed.lo >> shift) & ItemMask);
        }
        else
        {
            int shift = (index - ItemsPerUlong) * BitsPerItem;
            return (int)((packed.hi >> shift) & ItemMask);
        }
    }

    /// <summary>
    /// Sets the item at a specific index (0-based) within (lo, hi).
    /// Returns the updated pair. Validates value in 0..300.
    /// </summary>
    public static (ulong lo, ulong hi) SetAt((ulong lo, ulong hi) packed, int index, int value)
    {
        if (index < 0 || index >= MaxItems) throw new ArgumentOutOfRangeException(nameof(index));
        if (value < 0 || value > MaxValue) throw new ArgumentOutOfRangeException(nameof(value), "Value must be in 0..300.");

        int shift;
        if (index < ItemsPerUlong)
        {
            shift = index * BitsPerItem;
            ulong clearMask = ~(ItemMask << shift);
            ulong newVal = ((ulong)value & ItemMask) << shift;
            ulong lo = (packed.lo & clearMask) | newVal;
            return (lo, packed.hi);
        }
        else
        {
            shift = (index - ItemsPerUlong) * BitsPerItem;
            ulong clearMask = ~(ItemMask << shift);
            ulong newVal = ((ulong)value & ItemMask) << shift;
            ulong hi = (packed.hi & clearMask) | newVal;
            return (packed.lo, hi);
        }
    }    

    internal static bool TooManyPresses((ulong lo, ulong hi) state, (ulong lo, ulong hi) target, int size)
    {
        for (var i=0; i<size; ++i)
        {
            if (GetAt(state, i) > GetAt(target, i)) return true;
        }

        return false;
    }
}

