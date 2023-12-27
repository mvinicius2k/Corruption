using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using FlaxEngine;

namespace Game;


public interface ICameraSlot
{

}


/// <summary>
/// TrackedCamera Script.
/// </summary>
/// 

public class PathCamera : Script, ICameraSlot
{

    public Spline SplinePath;
    public Actor Target;
    [DefaultValue(20f)]
    public float Speed;
    [DefaultValue(0f)]
    public float CurrentTime;

    

    /// <inheritdoc/>
    public override void OnUpdate()
    {
        //Tempo para acessar o ponto mais próximo no spline
        var time = SplinePath.GetSplineTimeClosestToPoint(Target.Position);
        CurrentTime = Mathf.Lerp(CurrentTime, time, Speed * Time.DeltaTime);

        var position = SplinePath.GetSplinePoint(CurrentTime);
        Actor.Position = position;

        Actor.LookAt(Target.Position, Vector3.Up);
    }


}
