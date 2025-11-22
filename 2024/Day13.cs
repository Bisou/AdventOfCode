using System.Text;

public class Day13
{
    private const int day = 13;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        var total = 0L;
        for (var i=0;i<input.Length;++i) {
            var data = input[i].Split(new [] {'+',' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            var xa=int.Parse(data[3]);
            var ya=int.Parse(data[5]);
            ++i;
            data = input[i].Split(new [] {'+',' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            var xb=int.Parse(data[3]);
            var yb=int.Parse(data[5]);
            ++i;
            data = input[i].Split(new [] {'=',' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            var xp=int.Parse(data[2]);
            var yp=int.Parse(data[4]);
            ++i;
            /*
            var pq = new PriorityQueue<(int A, int B, int Price), int>();
            pq.Enqueue((0, 0, 0),0);
            while (pq.Count>0) {
                var curr=pq.Dequeue();
                if (curr.A*xa+curr.B*xb==xp && curr.A*ya+curr.B*yb==yp) {
                    total+=curr.Price;
                    break;
                }
                if (curr.A<100 && (curr.A+1)*xa+curr.B*xb<=xp && (curr.A+1)*ya+curr.B*yb<=yp) {
                    pq.Enqueue((curr.A+1, curr.B, curr.Price+3), curr.Price+3);
                }
                if (curr.B<100 && curr.A*xa+(curr.B+1)*xb<=xp && curr.A*ya+(curr.B+1)*yb<=yp) {
                    pq.Enqueue((curr.A, curr.B+1, curr.Price+1), curr.Price+1);
                }                
            }*/
            var min=500;
            var maxB=Math.Min(xp/xb +1, yp/yb +1);
            var maxA=Math.Min(xp/xa +1, yp/ya +1);
            for (var b=0;b<=maxB;++b) {
                var leftx = xp - b*xb;
                var lefty = yp - b*yb;
                if (leftx%xa==0 && lefty%ya==0) {
                    var ax = leftx/xa;
                    var ay = lefty/ya;
                    if (ax==ay && ax<=maxA) {
                        min = Math.Min(min, b+3*ax);
                    }
                }
            }
            if (min<500) total+=min;

        }
        
        Console.WriteLine($"Part1: total price is {total}");
    }
    


    public static void SolvePart2(string dataType)
    {            
        var more=10000000000000L;
        Tool.LogStart(day,2,dataType);
        var input = Tool.ReadAll(day, dataType);             
        var total = 0L;
        for (var i=0;i<input.Length;++i) {
            var data = input[i].Split(new [] {'+',' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            long xa=int.Parse(data[3]);
            long ya=int.Parse(data[5]);
            ++i;
            data = input[i].Split(new [] {'+',' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            long xb=int.Parse(data[3]);
            long yb=int.Parse(data[5]);
            ++i;
            data = input[i].Split(new [] {'=',' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            long xp=more+int.Parse(data[2]);
            long yp=more+int.Parse(data[4]);
            ++i;

/*
            a*xa + b*xb = xp
            a*ya + b*yb = yp
            a = (xp - b*xb)/xa;
            b = (yp -a*ya)/yb
            b = yp/yb -xp*ya/yb +b*xb*ya/yb
            b(1 - xb*ya/yb) = yp/yb - xp*ya/yb = (yp-xp*ya)/yb
            b = (yp-xp*ya)/yb / (1 - xb*ya/yb)

            (xp - b*xb)*ya/xa + b*yb = yp
            b*(yb - xb*ya/xa) = yp -xp*ya/xa;

            b = (yp -xp*ya/xa) / (yb - xb*ya/xa)
            a = (xp - b*xb)/xa;
*/
            var btop = (yp*xa -xp*ya);
            var bbottom =  (yb*xa - xb*ya);
            if (btop%bbottom!=0) continue;
            var b = btop/bbottom;
            var atop = xp - b*xb;
            if (atop%xa!=0) continue;
            var a = atop/xa;
            total += 3*a+b;


        }
        
        Console.WriteLine($"Part2: total price is {total}");
    }

}
