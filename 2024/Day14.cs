using System.Text;

public class Day14
{
    private const int day = 14;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
       // var positions = new List<(int Row, int Col)>();
        var width = (dataType=="Full") ? 101 : 11;
        var height = (dataType=="Full") ? 103 : 7;
        var middleRow = height/2;
        var middleCol = width/2;
        var quadrants = new int[4];
        foreach (var robot in input) {
            var data = robot.Split(new [] {'=',' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            var col=int.Parse(data[1]);
            var row=int.Parse(data[2]);
            var shiftCol=int.Parse(data[4]);
            var shiftRow=int.Parse(data[5]);
            var targetRow = (row + 100 * shiftRow) % height;
            if (targetRow<0) targetRow+=height;
            var targetCol = (col + 100 * shiftCol) % width;
            if (targetCol<0) targetCol+=width;
         //   positions.Add((targetRow,targetCol));
            if (targetCol<middleCol && targetRow<middleRow) quadrants[0]++;
            if (targetCol>middleCol && targetRow<middleRow) quadrants[1]++;
            if (targetCol<middleCol && targetRow>middleRow) quadrants[2]++;
            if (targetCol>middleCol && targetRow>middleRow) quadrants[3]++;
        }
        
        Console.WriteLine($"Part1: total price is {quadrants[0]*quadrants[1]*quadrants[2]*quadrants[3]}");
    }
    


    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,2,dataType);
        var input = Tool.ReadAll(day, dataType);                
       // var positions = new List<(int Row, int Col)>();
        var width = (dataType=="Full") ? 101 : 11;
        var height = (dataType=="Full") ? 103 : 7;
        var middleRow = height/2;
        var middleCol = width/2;
        var robots = new List<Robot>();
        foreach (var robot in input) {
            var data = robot.Split(new [] {'=',' ', ','}, StringSplitOptions.RemoveEmptyEntries);
            var col=int.Parse(data[1]);
            var row=int.Parse(data[2]);
            var shiftCol=int.Parse(data[4]);
            var shiftRow=int.Parse(data[5]);robots.Add(new Robot(row, col, shiftRow, shiftCol));
        }

        var max=15000L;      

        for (var i = 1; i < max; i++)
        {            
            foreach (var robot in robots) robot.Move(height, width);
            var robotsClose=0;
            foreach(var robot in robots) {
                if (robots.Any(r => r.Col==robot.Col && Math.Abs(r.Row-robot.Row)==1)) robotsClose++;
                else if (robots.Any(r => r.Row==robot.Row && Math.Abs(r.Col-robot.Col)==1)) robotsClose++;
            }
            if (robotsClose>=200) {
                var map = Enumerable.Range(0,height).Select(_ => new StringBuilder(new String(' ', width))).ToArray();
                foreach (var robot in robots) {
                    map[(int)robot.Row][(int)robot.Col] = '#';
                }
                Console.WriteLine(string.Join("\n", map.Select(sb => sb.ToString())));
                Console.WriteLine($"turn {i}");
            }
        } 
        Console.WriteLine("Nothing found :(");
    }

    private class Robot {
        public long Row;
        public long Col;
        public int ShiftRow;
        public int ShiftCol;
        
        public Robot(int row, int col, int shiftRow, int shiftCol) {
            Row = row;
            Col = col;
            ShiftRow = shiftRow;
            ShiftCol = shiftCol;
        }

        public void Move(int height, int width) {
            Row = (Row + ShiftRow) % height;
            if (Row < 0) Row+=height;
            Col = (Col + ShiftCol) % width;
            if (Col<0) Col+=width;            
        }

        public void Move(int height, int width, long seconds) {
            Row = (Row + seconds * ShiftRow) % height;
            if (Row<0) Row+=height;
            Col = (Col + seconds * ShiftCol) % width;
            if (Col<0) Col+=width;
        }
    }

}
