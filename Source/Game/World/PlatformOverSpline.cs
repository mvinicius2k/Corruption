using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Game;

public class PlatformOverSpline : Script, IPlatformSlider
{
    [ShowInEditor, Serialize]
    private Spline spline;

    public float Speed;
    public bool lockSplineDirection;

    private float currentTime;
    private Vector3 oldPosition;
    [ShowInEditor, ReadOnly]
    private Vector3 displacement;
    public Vector3 Displacement => displacement;


    public override void OnAwake()
    {
        oldPosition = Actor.Position;
    }


    public override void OnUpdate()
    {

        currentTime += Time.DeltaTime * Speed;
        MoveToTime(currentTime);

    }

    private void MoveToTime(float time)
    {
        
        var direction = Actor.Direction;

        if (!lockSplineDirection)
        {
            direction = spline.GetSplineDirection(time);
        }

        var transform = spline.GetSplineTransform(time);
        transform.Orientation = Quaternion.LookRotation(direction, Float3.Up) * transform.Orientation;
        Actor.Position = transform.Translation;
        Actor.Direction = direction;
        displacement = Actor.Position - oldPosition;


        oldPosition = Actor.Position;
    }

    [EditorAction]
    public void MoveToStart()
        => MoveToTime(0f);




}


