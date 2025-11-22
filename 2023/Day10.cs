using System.Text;

public class Day10
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day10-Part1-{dataType}");
        var map = File.ReadAllLines(@$".\..\..\..\Inputs\Day10-{dataType}.txt");   
        (int Row, int Col) start=(-1,-1);
        height = map.Length;
        width = map[0].Length;
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (map[row][col]=='S') {
                    start=(row,col);
                    break;
                }
            }
        }
        var seen = new HashSet<(int Row, int Col)>();
        seen.Add(start);
        var TOP=0;
        var LEFT=1;
        var DOWN=2;
        var RIGHT=3;
        var shiftRow=new [] {-1,0,1,0};
        var shiftCol=new [] {0,-1,0,1};
        var cells = new List<(int Row, int Col, int Direction)>();
        //find the start of the pipe
        for( var way=0;way<4;++way) {
            var nextRow=start.Row+shiftRow[way];
            var nextCol=start.Col+shiftCol[way];
            if (nextRow<0 || nextCol<0 || nextRow>=height || nextCol>=width || map[nextRow][nextCol]=='.') continue;
            switch(map[nextRow][nextCol]) {
                case '|':
                    if (way==TOP || way==DOWN) {
                        cells.Add((nextRow,nextCol,way));                        
                        seen.Add((nextRow,nextCol));
                    }
                    break;
                case '-':
                    if (way==LEFT || way==RIGHT) {
                        cells.Add((nextRow,nextCol,way));                         
                        seen.Add((nextRow,nextCol));
                    }
                    break;
                case 'L':
                    if (way==DOWN || way==LEFT) {
                        cells.Add((nextRow,nextCol,way));                         
                        seen.Add((nextRow,nextCol));
                    }
                    break;
                case 'J':
                    if (way==DOWN || way==RIGHT) {
                        cells.Add((nextRow,nextCol,way));                          
                        seen.Add((nextRow,nextCol));
                    }
                    break;
                case '7':
                    if (way==TOP || way==RIGHT) {
                        cells.Add((nextRow,nextCol,way));  
                        seen.Add((nextRow,nextCol));                        
                    }
                    break;
                case 'F':
                    if (way==TOP || way==LEFT) {
                        cells.Add((nextRow,nextCol,way));  
                        seen.Add((nextRow,nextCol));
                    }
                    break;                    
            }
        }
        var steps=1;
        while (cells.Any()) {
            var nextCells = new List<(int Row, int Col, int Direction)>();
            foreach(var cell in cells) {
                switch(map[cell.Row][cell.Col]) {
                    case '|':
                        if (cell.Direction==TOP) {
                            nextCells.Add((cell.Row+shiftRow[TOP], cell.Col+shiftCol[TOP], TOP));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[DOWN], cell.Col+shiftCol[DOWN], DOWN));
                        }
                        break;
                    case '-':
                        if (cell.Direction==LEFT) {
                            nextCells.Add((cell.Row+shiftRow[LEFT], cell.Col+shiftCol[LEFT], LEFT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[RIGHT], cell.Col+shiftCol[RIGHT], RIGHT));
                        }
                        break;
                    case 'L': //TODO
                        if (cell.Direction==DOWN) {
                            nextCells.Add((cell.Row+shiftRow[RIGHT], cell.Col+shiftCol[RIGHT], RIGHT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[TOP], cell.Col+shiftCol[TOP], TOP));
                        }
                        break;
                    case 'J'://TODO
                        if (cell.Direction==DOWN) {
                            nextCells.Add((cell.Row+shiftRow[LEFT], cell.Col+shiftCol[LEFT], LEFT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[TOP], cell.Col+shiftCol[TOP], TOP));
                        }
                        break;
                    case '7'://TODO
                        if (cell.Direction==TOP) {
                            nextCells.Add((cell.Row+shiftRow[LEFT], cell.Col+shiftCol[LEFT], LEFT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[DOWN], cell.Col+shiftCol[DOWN], DOWN));
                        }
                        break;
                    case 'F'://TODO
                        if (cell.Direction==TOP) {
                            nextCells.Add((cell.Row+shiftRow[RIGHT], cell.Col+shiftCol[RIGHT], RIGHT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[DOWN], cell.Col+shiftCol[DOWN], DOWN));
                        }
                        break;                    
                }
            }
            
            if (nextCells.Any(nc => seen.Contains((nc.Row, nc.Col)))) {
                break;
            }     
            foreach(var nc in nextCells) seen.Add((nc.Row, nc.Col));
            cells=nextCells;
            steps++;
        }
        Console.WriteLine($"Part1: farthest part of the pipe is {steps} steps away");
    }

    private static int height;
    private static int width;
    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day10-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day10-{dataType}.txt");   
        (int Row, int Col) start=(-1,-1);
        height = input.Length;
        width = input[0].Length;
        var map = input.Select(line => new StringBuilder(line)).ToArray();
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (map[row][col]=='S') {
                    start=(row,col);
                    break;
                }
            }
        }
        var seen = new HashSet<(int Row, int Col)>();
        seen.Add(start);
        var TOP=0;
        var LEFT=1;
        var DOWN=2;
        var RIGHT=3;
        var shiftRow=new [] {-1,0,1,0};
        var shiftCol=new [] {0,-1,0,1};
        var cells = new List<(int Row, int Col, int Direction)>();
        //find the start of the pipe
        for( var way=0;way<4;++way) {
            var nextRow=start.Row+shiftRow[way];
            var nextCol=start.Col+shiftCol[way];
            if (nextRow<0 || nextCol<0 || nextRow>=height || nextCol>=width || map[nextRow][nextCol]=='.') continue;
            switch(map[nextRow][nextCol]) {
                case '|':
                    if (way==TOP || way==DOWN) {
                        cells.Add((nextRow,nextCol,way));                        
                        seen.Add((nextRow,nextCol));
                    }
                    break;
                case '-':
                    if (way==LEFT || way==RIGHT) {
                        cells.Add((nextRow,nextCol,way));                         
                        seen.Add((nextRow,nextCol));
                    }
                    break;
                case 'L':
                    if (way==DOWN || way==LEFT) {
                        cells.Add((nextRow,nextCol,way));                         
                        seen.Add((nextRow,nextCol));
                    }
                    break;
                case 'J':
                    if (way==DOWN || way==RIGHT) {
                        cells.Add((nextRow,nextCol,way));                          
                        seen.Add((nextRow,nextCol));
                    }
                    break;
                case '7':
                    if (way==TOP || way==RIGHT) {
                        cells.Add((nextRow,nextCol,way));  
                        seen.Add((nextRow,nextCol));                        
                    }
                    break;
                case 'F':
                    if (way==TOP || way==LEFT) {
                        cells.Add((nextRow,nextCol,way));  
                        seen.Add((nextRow,nextCol));
                    }
                    break;                    
            }
        }
        var steps=1;
        while (cells.Any()) {
            var nextCells = new List<(int Row, int Col, int Direction)>();
            foreach(var cell in cells) {
                switch(map[cell.Row][cell.Col]) {
                    case '|':
                        if (cell.Direction==TOP) {
                            nextCells.Add((cell.Row+shiftRow[TOP], cell.Col+shiftCol[TOP], TOP));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[DOWN], cell.Col+shiftCol[DOWN], DOWN));
                        }
                        break;
                    case '-':
                        if (cell.Direction==LEFT) {
                            nextCells.Add((cell.Row+shiftRow[LEFT], cell.Col+shiftCol[LEFT], LEFT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[RIGHT], cell.Col+shiftCol[RIGHT], RIGHT));
                        }
                        break;
                    case 'L': //TODO
                        if (cell.Direction==DOWN) {
                            nextCells.Add((cell.Row+shiftRow[RIGHT], cell.Col+shiftCol[RIGHT], RIGHT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[TOP], cell.Col+shiftCol[TOP], TOP));
                        }
                        break;
                    case 'J'://TODO
                        if (cell.Direction==DOWN) {
                            nextCells.Add((cell.Row+shiftRow[LEFT], cell.Col+shiftCol[LEFT], LEFT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[TOP], cell.Col+shiftCol[TOP], TOP));
                        }
                        break;
                    case '7'://TODO
                        if (cell.Direction==TOP) {
                            nextCells.Add((cell.Row+shiftRow[LEFT], cell.Col+shiftCol[LEFT], LEFT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[DOWN], cell.Col+shiftCol[DOWN], DOWN));
                        }
                        break;
                    case 'F'://TODO
                        if (cell.Direction==TOP) {
                            nextCells.Add((cell.Row+shiftRow[RIGHT], cell.Col+shiftCol[RIGHT], RIGHT));
                        } else {
                            nextCells.Add((cell.Row+shiftRow[DOWN], cell.Col+shiftCol[DOWN], DOWN));
                        }
                        break;                    
                }
            }
            
            if (nextCells.Any(nc => seen.Contains((nc.Row, nc.Col)))) {
                break;
            }     
            foreach(var nc in nextCells) seen.Add((nc.Row, nc.Col));
            cells=nextCells;
            steps++;
        }

        //remove the useless pipes
        for (var row=0;row<height;++row) {
            for (var col=0;col<width;++col) {
                if (!seen.Contains((row,col)) && map[row][col]!='.') {
                    map[row][col]='.';
                }
            }
        }

        //now, floodfill from the sides
        topLeftOfCrossingVisited = new HashSet<(int Row, int Col)>();
        var outside = new HashSet<(int Row, int Col)>();
        var toExpand = new List<(int Row, int Col)>();
        for (var row=0;row<height;++row) {
            if (map[row][0]=='.') {
                outside.Add((row,0));
                toExpand.Add((row,0));
            }
            if (map[row][width-1]=='.') {
                outside.Add((row,width-1));
                toExpand.Add((row,width-1));
            }
        }
        for (var col=0;col<width;++col) {
            if (map[0][col]=='.') {
                outside.Add((0,col));
                toExpand.Add((0,col));
            }
            if (map[height-1][col]=='.') {
                outside.Add((height-1,col));
                toExpand.Add((height-1,col));
            }
        }
        while(toExpand.Any()) {
            var toExpandNext = new List<(int Row, int Col)>();
            foreach(var cell in toExpand) {
                for (var way=0;way<4;++way) {
                    var nextRow=cell.Row+shiftRow[way];
                    var nextCol=cell.Col+shiftCol[way];
                    if (nextRow<0 || nextCol<0 || nextRow>=height || nextCol>=width || map[nextRow][nextCol]!='.' || outside.Contains((nextRow,nextCol))) continue;
                    outside.Add((nextRow,nextCol));
                    toExpandNext.Add((nextRow,nextCol));
                }
                //can squeeze to a diagonal?
                if (cell.Row>0 && cell.Col>0) {
                    //top left between 7 & L
                    //  ?L
                    //  7x
                    if (map[cell.Row][cell.Col-1]=='7' && map[cell.Row-1][cell.Col]=='L') {
                        if (map[cell.Row-1][cell.Col-1]=='.' && !outside.Contains((cell.Row-1,cell.Col-1))) {
                            outside.Add((cell.Row-1,cell.Col-1));
                            toExpandNext.Add((cell.Row-1,cell.Col-1));
                        }
                    }
                }
                if (cell.Row>0 && cell.Col<width-1) {
                    //top right between J & F
                    //  J?
                    //  xF
                    if (map[cell.Row][cell.Col+1]=='F' && map[cell.Row-1][cell.Col]=='J') {
                        if (map[cell.Row-1][cell.Col+1]=='.' && !outside.Contains((cell.Row-1,cell.Col+1))) {
                            outside.Add((cell.Row-1,cell.Col+1));
                            toExpandNext.Add((cell.Row-1,cell.Col+1));
                        }
                    }
                }
                if (cell.Row<height-1 && cell.Col<width-1) {
                    //bottom right between 7 & L
                    //  xL
                    //  7?
                    if (map[cell.Row][cell.Col+1]=='L' && map[cell.Row+1][cell.Col]=='7') {
                        if (map[cell.Row+1][cell.Col+1]=='.' && !outside.Contains((cell.Row+1,cell.Col+1))) {
                            outside.Add((cell.Row+1,cell.Col+1));
                            toExpandNext.Add((cell.Row+1,cell.Col+1));
                        }
                    }
                }
                if (cell.Row<height-1 && cell.Col>0) {
                    //bottom left between J & F
                    //  Jx
                    //  ?F
                    if (map[cell.Row][cell.Col-1]=='F' && map[cell.Row+1][cell.Col-1]=='J') {
                        if (map[cell.Row+1][cell.Col-1]=='.' && !outside.Contains((cell.Row+1,cell.Col-1))) {
                            outside.Add((cell.Row+1,cell.Col-1));
                            toExpandNext.Add((cell.Row+1,cell.Col-1));
                        }
                    }
                }
                //can squeeze through other pipes
                topLeftOfCrossingVisited.Add((cell.Row-1,cell.Col-1));
                topLeftOfCrossingVisited.Add((cell.Row,cell.Col-1));
                topLeftOfCrossingVisited.Add((cell.Row-1,cell.Col));
                topLeftOfCrossingVisited.Add((cell.Row,cell.Col));
                foreach(var newCell in Squeeze((cell.Row-1,cell.Col-1), map)) {
                    if (!outside.Contains(newCell)) {                        
                        outside.Add(newCell);
                        toExpandNext.Add(newCell);
                    }
                }
                foreach(var newCell in Squeeze((cell.Row,cell.Col-1), map)) {
                    if (!outside.Contains(newCell)) {                 
                        outside.Add(newCell);
                        toExpandNext.Add(newCell);
                    }
                }
                foreach(var newCell in Squeeze((cell.Row-1,cell.Col), map)) {
                    if (!outside.Contains(newCell)) {                 
                        outside.Add(newCell);
                        toExpandNext.Add(newCell);
                    }
                }
                foreach(var newCell in Squeeze((cell.Row,cell.Col), map)) {
                    if (!outside.Contains(newCell)) {                  
                        outside.Add(newCell);
                        toExpandNext.Add(newCell);
                    }
                }

            }
            toExpand=toExpandNext;
        }

        var res = width*height;
        res -= seen.Count();
        res-=outside.Count();
        Console.WriteLine($"Part2: there are {res} cells enclosed inside the loop"); 
    }

    private static HashSet<(int Row, int Col)> topLeftOfCrossingVisited;
    private static string verticalRightPart="|LF";
    private static string verticalLeftPart="|J7";
    private static string horizontalTopPart="-LJ";
    private static string horizontalDownPart="-7F";
    
    ///DFS
    private static List<(int Row, int Col)> Squeeze((int Row, int Col) topLeftCell, StringBuilder[] map) {
        var shiftRow=new []{-1,-1,-1,0,0,1,1,1};
        var shiftCol=new []{-1,0,1,-1,1,-1,0,1};
        var TOP_LEFT=0;
        var TOP=1;
        var TOP_RIGHT=2;
        var LEFT=3;
        var RIGHT=4;
        var DOWN_LEFT=5;
        var DOWN=6;
        var DOWN_RIGHT=7;
        var res = new List<(int Row, int Col)>();
        //we are between 4 cells
        if (topLeftCell.Row>=height-1 || topLeftCell.Col>=width-1 || topLeftCell.Row<0 || topLeftCell.Col<0) {
        //too on the side to go there
            return res;
        }
        var topLeft = map[topLeftCell.Row][topLeftCell.Col];
        var topRight = map[topLeftCell.Row][topLeftCell.Col+1];
        var downLeft = map[topLeftCell.Row+1][topLeftCell.Col];
        var downRight = map[topLeftCell.Row+1][topLeftCell.Col+1];
        //check end conditions
        if (topLeft=='.') res.Add((topLeftCell.Row,topLeftCell.Col));
        if (topRight=='.') res.Add((topLeftCell.Row,topLeftCell.Col+1));
        if (downLeft=='.') res.Add((topLeftCell.Row+1,topLeftCell.Col));
        if (downRight=='.') res.Add((topLeftCell.Row+1,topLeftCell.Col+1));
        //go up
        if (topLeftCell.Row>0 
                && verticalLeftPart.Contains(topLeft) && verticalRightPart.Contains(topRight) 
                && !topLeftOfCrossingVisited.Contains((topLeftCell.Row-1, topLeftCell.Col))) {
            topLeftOfCrossingVisited.Add((topLeftCell.Row-1, topLeftCell.Col));
            res.AddRange(Squeeze((topLeftCell.Row-1, topLeftCell.Col), map));
        }
        //go down
        if (topLeftCell.Row<height-1 
                && verticalLeftPart.Contains(downLeft) && verticalRightPart.Contains(downRight) 
                && !topLeftOfCrossingVisited.Contains((topLeftCell.Row+1, topLeftCell.Col))) {
            topLeftOfCrossingVisited.Add((topLeftCell.Row+1, topLeftCell.Col));
            res.AddRange(Squeeze((topLeftCell.Row+1, topLeftCell.Col), map));
        }        
        //go left
        if (topLeftCell.Col>0 
                && horizontalTopPart.Contains(topLeft) && horizontalDownPart.Contains(downLeft) 
                && !topLeftOfCrossingVisited.Contains((topLeftCell.Row, topLeftCell.Col-1))) {
            topLeftOfCrossingVisited.Add((topLeftCell.Row, topLeftCell.Col-1));
            res.AddRange(Squeeze((topLeftCell.Row, topLeftCell.Col-1), map));
        }
        //go right
        if (topLeftCell.Col<width-1 
                && horizontalTopPart.Contains(topRight) && horizontalDownPart.Contains(downRight) 
                && !topLeftOfCrossingVisited.Contains((topLeftCell.Row, topLeftCell.Col+1))) {
            topLeftOfCrossingVisited.Add((topLeftCell.Row, topLeftCell.Col+1));
            res.AddRange(Squeeze((topLeftCell.Row, topLeftCell.Col+1), map));
        }
        return res;
    }
}
