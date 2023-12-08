using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;
public class RelativeFront : Script
{
    [Tooltip("Em graus")]
    public float RangeAcceptable = 0.5f;
    public float PassLenght = 10f;
    private IMoveUpdater moveUpdater;

    public override void OnStart()
    {
        moveUpdater = Actor.GetScript<IMoveUpdater>();
        
        
    }

    public override void OnUpdate()
    {
        if (moveUpdater.PauseFront)
            return;
        float min = moveUpdater.TargetFront - RangeAcceptable;
        float max = moveUpdater.TargetFront + RangeAcceptable;
        
        if (Mathf.IsInRange(Actor.EulerAngles.Y, min, max))
            return;
        var yEuler = Mathf.LerpAngle(Actor.EulerAngles.Y, moveUpdater.TargetFront, PassLenght * Time.DeltaTime);
        Actor.EulerAngles = new Vector3
        {
            X = Actor.EulerAngles.X,
            Y = yEuler,
            Z = Actor.EulerAngles.Z
        };
        
    }

  
}

public interface IMoveUpdater
{
    public float TargetFront { get;  }
    public bool PauseFront { get; }

}


