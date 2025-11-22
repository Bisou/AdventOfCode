﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
//using Microsoft.Z3;

public class Day24
{   
    public static void SolvePart1(string dataType, double min, double max)
    {
        Console.WriteLine($"Day24-Part1-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day24-{dataType}.txt");   
        var count=0L;
        var hails = input.Select(line => line.Split(new char[] {' ',',','@'}, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray()).ToArray();
        for (var i=0;i<hails.Length;++i) {
            for (var j=i+1;j<hails.Length;++j) {
                var collision = Collision2(hails[i], hails[j]);
                if (min<=collision.X && collision.X<=max && min<=collision.Y && collision.Y<=max) {
                    count++;
                } else {
                    //Console.WriteLine($"({collision.X}, {collision.Y}) is outside of the zone");
                }
            }
        }
        
        Console.WriteLine($"Part1: answer is {count}");
    }

    public static (double X, double Y) Collision2 (double[] hail1, double[] hail2) {
        //line 1 = hail1. We need 2 points (p1 & p2)
        (double X, double Y) p1 = (hail1[0], hail1[1]);
        (double X, double Y) p2 = (hail1[0]+hail1[3], hail1[1]+hail1[4]);
        //line 2 = hail1. We need 2 points (p3 & p4)
        (double X, double Y) p3 = (hail2[0], hail2[1]);
        (double X, double Y) p4 = (hail2[0]+hail2[3], hail2[1]+hail2[4]);

         var denom = (p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y);
        if (denom == 0) return (-1,-1);
        var ua = ((p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p3.Y) * (p1.X - p3.X)) / denom;
        var ub = ((p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X)) / denom;
        if (ua<0 || ub<0) return (-1,-1);
        return (p1.X + ua * (p2.X - p1.X), p1.Y + ua * (p2.Y - p1.Y));
    }

    public static (double X, double Y) Collision (double[] hail1, double[] hail2) {
        //does not work :(
        //https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection
        //line 1 = hail1. We need 2 points (p1 & p2)
        (double X, double Y) p1 = (hail1[0], hail1[1]);
        (double X, double Y) p2 = (hail1[0]+hail1[3], hail1[1]+hail1[4]);
        //line 2 = hail1. We need 2 points (p3 & p4)
        (double X, double Y) p3 = (hail2[0], hail2[1]);
        (double X, double Y) p4 = (hail2[0]+hail2[3], hail2[1]+hail2[4]);
        
        var dividor = ((p1.X-p2.X)*(p3.Y-p4.Y)) - ((p1.Y-p2.Y)*(p3.X-p4.X));
        if (dividor==0) {
            //parallel
            //Console.WriteLine($"parallel lines");
            return (-1,-1);
        }
        double px = ((p1.X*p2.Y - p1.Y*p2.X)*(p3.X-p4.X) - (p1.X-p2.X)*(p3.X*p4.Y-p3.Y*p4.X))/dividor;
        double py = ((p1.X*p2.Y - p1.Y*p2.X)*(p3.Y-p4.Y) - (p1.Y-p2.Y)*(p3.X*p4.Y-p3.Y*p4.X))/dividor;
        var time1 = (px-p1.X)/hail1[3];
        var time2 = (px-p3.X)/hail2[3];
        if (time1<0 || time2<0) {
           // Console.WriteLine($"Collision in the past");
            return (-1,-1);//collision in the past
        }
        return (px,py);
    }

    public static void SolvePart2(string dataType)
    {     /*
        Console.WriteLine($"Day24-Part2-{dataType}");
        var input = File.ReadAllLines(@$".\..\..\..\Inputs\Day24-{dataType}.txt");   
        long count = 0;
        int m = inputs.Length;
        var hailstones = new List<(long x, long y, long z, long dx, long dy, long dz)>();
        for (int i = 0; i < m; i++)
        {
            var line = inputs[i].Split(new char[] { ',', '@', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            var h = (line[0], line[1], line[2], line[3], line[4], line[5]);
            hailstones.Add(h);
        }
        var ctx = new Context();
        var solver = ctx.MkSolver();
        IntExpr x = ctx.MkIntConst("x"), y = ctx.MkIntConst("y"), z = ctx.MkIntConst("z"), dx = ctx.MkIntConst("dx"), dy = ctx.MkIntConst("dy"), dz = ctx.MkIntConst("dz");
        for (int i = 0; i < 3; i++)
        {
            var h = hailstones[i];
            IntExpr t = ctx.MkIntConst($"t{i}");
            IntNum hx = ctx.MkInt(h.x), hy = ctx.MkInt(h.y), hz = ctx.MkInt(h.z), hdx = ctx.MkInt(h.dx), hdy = ctx.MkInt(h.dy), hdz = ctx.MkInt(h.dz);
            solver.Add(t >= 0);
            solver.Add(ctx.MkEq(ctx.MkAdd(x, ctx.MkMul(t, dx)), ctx.MkAdd(hx, ctx.MkMul(t, hdx))));
            solver.Add(ctx.MkEq(ctx.MkAdd(y, ctx.MkMul(t, dy)), ctx.MkAdd(hy, ctx.MkMul(t, hdy))));
            solver.Add(ctx.MkEq(ctx.MkAdd(z, ctx.MkMul(t, dz)), ctx.MkAdd(hz, ctx.MkMul(t, hdz))));
        }
        solver.Check();
        var mdl = solver.Model;
        var X= mdl.Eval(x);
        var Y = mdl.Eval(y);
        var Z = mdl.Eval(z);
        var DX=mdl.Eval(dx);
        var DY = mdl.Eval(dy);
        var DZ = mdl.Eval(dz);
        
        Console.WriteLine($"2nd star answer is {long.Parse(X.ToString())}, {long.Parse(Y.ToString())}, {long.Parse(Z.ToString())} = {long.Parse(X.ToString())+long.Parse(Y.ToString())+long.Parse(Z.ToString())}");
        Console.WriteLine($"{DX} {DY} {DZ}");*/
    }

}
