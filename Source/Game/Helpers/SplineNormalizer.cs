using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;

public class SplineNormalizer : Script
{
    public int Iterations = 40;
    private Spline spline;
    
    public override void OnStart()
    {
        spline = Actor.As<Spline>();
    }

    [EditorAction]
    public void Normalize()
    {
        
        var spline = this.spline ?? Actor.As<Spline>();
        var k0 = spline.GetSplineKeyframe(0);
        var k1 = spline.GetSplineKeyframe(1);
        //var step = 1f / (float)Iterations;
        //var distance = 0f;
        //var oldPoint = k0.Value.Translation;
        //var length = Mathf.Abs( k0.Time - k1.Time);
        //for (int i = 0; i < Iterations; i++)
        //{

        //    var t = i / step;
        //    var tempPoint = BezierUtils.BezierLength(k0.Value.Translation, k0.TangentOut.Translation, k1.TangentIn.Translation, k1.Value.Translation, t, length);
        //    distance += Vector3.DistanceSquared(ref oldPoint, ref tempPoint);
        //    oldPoint = tempPoint;
        //    Debug.Log(distance);
        //}
        var controlPoints = new Vector3[]
        {
            k0.Value.Translation,
            k0.TangentOut.Translation,
            k1.TangentIn.Translation,
            k1.Value.Translation,

        };

        var length = CalcBezierLength(controlPoints, Iterations);
        Debug.Log(length);
    }

    public static float CalcBezierLength(Vector3[] controlPoints, int numSegments)
    {
        float length = 0;

        for (int i = 0; i < numSegments; i++)
        {
            float t1 = (float)i / numSegments;
            float t2 = (float)(i + 1) / numSegments;
            var point1 = DeCasteljau(controlPoints, t1);
            var point2 = DeCasteljau(controlPoints, t2);
            length += Vector3.Distance(point1, point2);
        }

        return length;
    }

    public static Vector3 DeCasteljau(Vector3[] controlPoints, float t)
    {
        while (controlPoints.Length > 1)
        {
            Vector3[] newPoints = new Vector3[controlPoints.Length - 1];
            for (int i = 0; i < controlPoints.Length - 1; i++)
            {
                newPoints[i] = (1 - t) * controlPoints[i] + t * controlPoints[i + 1];
            }
            controlPoints = newPoints;
        }
        return controlPoints[0];
    }
}
