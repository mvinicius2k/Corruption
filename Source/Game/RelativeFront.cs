using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;
public class RelativeFront : Script
{
    public Actor TargetActor;
    [Tooltip("")]
    public float RangeAccuracy = 0.5f;
    public float PassLenght = 10f;
    private IMoveUpdater moveUpdater;

    public override void OnStart()
    {
        moveUpdater = Actor.GetScript<IMoveUpdater>();
        moveUpdater.OnMoveUpdate += new EventHandler(RotateFront);
        
        
    }

    private void RotateFront(object _, EventArgs e)
    {
        float min = TargetActor.EulerAngles.Y - RangeAccuracy;
        float max = TargetActor.EulerAngles.Y + RangeAccuracy;
        if (Mathf.IsInRange(Actor.EulerAngles.Y, min, max))
            return;
        var yEuler = Mathf.LerpAngle(Actor.EulerAngles.Y, TargetActor.EulerAngles.Y, PassLenght * Time.DeltaTime);
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
    public event EventHandler OnMoveUpdate;

}


