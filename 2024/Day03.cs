public class Day03
{
    private const int day = 3;

    public static void SolvePart1(string dataType)
    {
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);             
        var sum=0L;
        foreach(var line in input) {
        /*for(var i=0;i<line.Length;++i)
        {
            if (line[i]=='m') {
                var subRes=GetMul(i+1, line);
                sum+=subRes.val;
                i=subRes.i;
            }   
        }*/
            var res=GetMul(0,line);
            sum+=res.val;
        }
        Console.WriteLine($"Part1: total sum of results is {sum}");
    }
    
    private static (int i, long val, bool enabled) GetMul(int i, string line, bool doEnabled=false, bool mulWorks=true) {        
        var status=0;
        var a=0L;
        var b=0L;
        var res=0L;
        var start=0;
        for(;i<line.Length;++i)
        {
            if (line[i]=='d' && doEnabled) {
                status=10;
                continue;
            }
            if (status==10 && line[i]=='o') {
                status++;
                continue;
            }          
            if (status==11 && line[i]=='(') {
                status++;
                continue;
            }          
            if (status==12 && line[i]==')') {
                status=0;
                mulWorks=true;
                continue;
            }          
            if (status==11 && line[i]=='n') {
                status=15;
                continue;
            }          
            if (status==15 && line[i]=="'"[0]) {
                status++;
                continue;
            }          
            if (status==16 && line[i]=='t') {
                status++;
                continue;
            }          
            if (status==17 && line[i]=='(') {
                status++;
                continue;
            }          
            if (status==18 && line[i]==')') {
                status=0;
                mulWorks=false;
                continue;
            }          


           if (mulWorks && line[i]=='m') {
           /*     var subRes=GetMul(i+1, line);
                res+=subRes.val;
                i=subRes.i;*/
                status=1;//start anew
                a=0;
                b=0;
                start=i;
                continue;
            }
            if (status==1 && line[i]=='u') {
                status++;
                continue;
            }            
            if (status==2 && line[i]=='l') {
                status++;
                continue;
            }
            if (status==3 && line[i]=='(') {
                status++;
                continue;
            }
            if (status==4 && '0'<=line[i] && line[i]<='9') {
                a=10*a+line[i]-'0';
                continue;
            }
            if (status==4 && line[i]==',') {
                status++;
                continue;
            }
            if (status==5 && '0'<=line[i] && line[i]<='9') {
                b=10*b+line[i]-'0';
                continue;
            }
            if (status==5 && line[i]==')') {
                //Console.WriteLine($"{line.Substring(start,i-start+1)} => {a*b}");
                res+=a*b;
                a=0;
                b=0;
                status=0;
                continue;
            }
            status=0;
            a=0;
            b=0;
        }
        return (i,res, mulWorks);
    }

    public static void SolvePart2(string dataType)
    {            
        Tool.LogStart(day,1,dataType);
        var input = Tool.ReadAll(day, dataType);                 
        var sum=0L;
        var mulWorks=true;
        foreach(var line in input) {
        /*for(var i=0;i<line.Length;++i)
        {
            if (line[i]=='m') {
                var subRes=GetMul(i+1, line);
                sum+=subRes.val;
                i=subRes.i;
            }   
        }*/
            var res=GetMul(0,line,true, mulWorks);
            sum+=res.val;
            mulWorks=res.enabled;
        }
        Console.WriteLine($"Part2: total sum of results is {sum}");
    }
}
