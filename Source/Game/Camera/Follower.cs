using FlaxEditor.Gizmo;
using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;


public class Follower : Script
{
    public Actor Target, LookTo;
    public float Speed = 500f;
    public RigidBody RigidBody;
    public Vector3 Angle;
    public float DistanceFromTarget;
    public float AproximationSpeed = 1000f;
    public float CameraRadius = 100f;
    public float DistanceMargin = 200f;
    public override void OnUpdate()
    {
        Screen.CursorVisible = false;
        Screen.CursorLock = CursorLockMode.Locked;

        var mouseInput = new Vector2(Input.GetAxis(Values.InputMouseX), Input.GetAxis(Values.InputMouseY));
        var normalizedSpeed = Speed * Time.DeltaTime;
        var normalizedAproximationSpeed = AproximationSpeed * Time.DeltaTime;

        bool needFix = false;
        Vector3 surfaceNormal;
        Vector3 collisionpoint;
        if (Physics.OverlapSphere(Actor.Position, CameraRadius, out Collider[] results, ((uint)LayerEnum.World), false))
        {

            foreach (var collider in results)
            {
                var rayDirection = collider.Position - Actor.Position;
                var raycastHit = Physics.RayCast(Actor.Position, rayDirection.Normalized, out var info, layerMask: ((uint)LayerEnum.World), hitTriggers: false);
                surfaceNormal = info.Normal;
                collisionpoint = info.Point;
            }

        }
        
        //Movimento livre
        Actor.RotateAround(Target.Position, Transform.Up, mouseInput.X * normalizedSpeed);
        Actor.RotateAround(Target.Position, Transform.Right, mouseInput.Y * normalizedSpeed);

        var angle = Vector3.Angle(surfaceNormal, 

        if (LookTo != null)
            Actor.LookAt(LookTo.Position, Vector3.Up);

        var distance = Vector3.Distance(Actor.Position, Target.Position);
        if (!Mathf.WithinEpsilon(distance, DistanceFromTarget, DistanceMargin))
        {
            if (distance < DistanceFromTarget)
                Actor.AddMovement(Actor.Transform.Backward * normalizedAproximationSpeed);

            else
                Actor.AddMovement(Actor.Transform.Forward * normalizedAproximationSpeed);
        }




    }

    public override void OnStart()
    {

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

        DebugDraw.DrawWireSphere(new BoundingSphere
        {
            Center = Target.Position,
            Radius = DistanceFromTarget
        }, Color.Red);

    }


}
