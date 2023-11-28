

using System;
using System.Collections.Generic;
using System.Linq;
using FlaxEngine;

namespace Game;


public class CameraFollower : Script
{

    public Actor Target;
    public Actor LookTo;
    public float Speed = 500f;
    public float CameraRadius = 200f;
    public bool DisableInput;

    public float MinAngle = 10f;
    public float MaxAngle = 170f;

    private float distance;
    private Vector3 targetLastPosition;

   
    /// <inheritdoc/>
    public override void OnStart()
    {
        distance = Vector3.Distance(Actor.Position, Target.Position);
        targetLastPosition = Target.Position;
        
    }
  

    public override void OnUpdate()
    {
        Screen.CursorVisible = false;
        Screen.CursorLock = CursorLockMode.Locked;

        var mouse = new Vector2(Input.GetAxis(Values.InputMouseX), Input.GetAxis(Values.InputMouseY));

        var lastPosition = Actor.Position;

        Actor.RotateAround(Target.Position, Actor.Transform.Up, mouse.X * Speed);
        Actor.RotateAround(Target.Position, Actor.Transform.Right, mouse.Y * Speed);

        if(Mathf.IsNotInRange(Vector3.Angle(Vector3.Up, Actor.Transform.Forward),MinAngle, MaxAngle))
            Actor.Position = lastPosition;

        var rayDirection = (Actor.Position - Target.Position).Normalized;
        if (Physics.SphereCast(Target.Position,CameraRadius, rayDirection, out var hitInfo, distance, ((uint)LayerEnum.World)))
        {
            Actor.Position = hitInfo.Point + hitInfo.Normal * CameraRadius;
            
        }
        
        else if (Physics.SphereCast(Actor.Position, CameraRadius, rayDirection * -1f, out var hit, distance - (Vector3.Distance(Actor.Position, Target.Position)), ((uint)LayerEnum.World)))
        {
            Actor.Position = hitInfo.Point + hitInfo.Normal * CameraRadius;
        }


        


        Actor.AddMovement(Target.Position - targetLastPosition);

        if (LookTo != null)
            Actor.LookAt(LookTo.Position, Vector3.Up);
        targetLastPosition = Target.Position;
    }

    public override void OnDebugDrawSelected()
    {
        DebugDraw.DrawWireSphere(new BoundingSphere
        {
            Center = Actor.Position,
            Radius = CameraRadius
        }, Color.Orange);

        if (Target == null)
            return;


    }
}
