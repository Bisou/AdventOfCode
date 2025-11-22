using System.Text;

public class Day13
{
    public static void SolvePart1(string dataType)
    {
        Console.WriteLine($"Day13-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day13-{dataType}.txt");   
        var sum=0L;
        var map=new List<string>();
        foreach(var line in input) {
            if (line.Length>1) {
                map.Add(line);
            } else {
                var height=map.Count;
                var width=map[0].Length;
                for(var row=1;row<height;++row) {
                    var sym=true;
                    for (var j=0;j<height;++j) {
                        if (row-j-1<0 || row+j>=height) break;
                        if (map[row-j-1]!=map[row+j]) {
                            sym=false;
                            break;
                        }
                    }
                    if(sym) {
                        sum += 100*row;
                    }
                }

                for(var col=1;col<width;++col) {
                    var sym=true;
                    for (var j=0;j<width;++j) {
                        if (col-j-1<0 || col+j>=width) break;
                        if (string.Join("",map.Select(r => r[col-j-1]).Select(c => c.ToString())) != string.Join("", map.Select(r => r[col+j]).Select(c => c.ToString()))) {
                            sym=false;
                            break;
                        }
                    }
                    if(sym) {
                        sum += col;
                    }
                }
                map.Clear();
            }
        }
        Console.WriteLine($"Part1: total sum of reflections is {sum}");
    }
    
    public static void SolvePart2(string dataType)
    {     
        Console.WriteLine($"Day13-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day13-{dataType}.txt");   
        var sum=0L;
        var map=new List<string>();
        foreach(var line in input) {
            if (line.Length>1) {
                map.Add(line);
            } else {
                var height=map.Count;
                var width=map[0].Length;
                for(var row=1;row<height;++row) {
                    var diff=0;
                    for (var j=0;j<height;++j) {
                        if (row-j-1<0 || row+j>=height) break;
                        if (diff>1) break;
                        for (var k=0;k<width;++k) {
                            if (map[row-j-1][k]!=map[row+j][k]) {
                                ++diff;
                                if (diff>1) break;
                            }
                        }
                    }
                    if(diff==1) {
                        sum += 100*row;
                    }
                }

                for(var col=1;col<width;++col) {
                    var diff=0;
                    for (var j=0;j<width;++j) {
                        if (col-j-1<0 || col+j>=width) break;
                        if (diff>1) break;
                        for (var k=0;k<height;++k) {
                            if (map[k][col-j-1]!=map[k][col+j]) {
                                ++diff;
                                if (diff>1) break;
                            }
                        }
                    }
                    if(diff==1) {
                        sum += col;
                    }
                }

                map.Clear();
            }
        }
        Console.WriteLine($"Part2: total sum of reflections is {sum}");
    }
}
