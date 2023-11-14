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
    public float DistanceMargin = 200f;
    public float CameraRadius = 200f;

    private Vector3 lastPosition, lastDirection;
    public override void OnUpdate()
    {
        Screen.CursorVisible = false;
        Screen.CursorLock = CursorLockMode.Locked;

        var mouseInput = new Vector2(Input.GetAxis(Values.InputMouseX), Input.GetAxis(Values.InputMouseY));
        var normalizedSpeed = Speed * Time.DeltaTime;
        var normalizedAproximationSpeed = AproximationSpeed * Time.DeltaTime;

        lastDirection = (lastPosition - Actor.Position).Normalized;
        lastPosition = Actor.Position;
        //Movimento livre
        Actor.RotateAround(Target.Position, Transform.Up, mouseInput.X * normalizedSpeed);
        Actor.RotateAround(Target.Position, Transform.Right, mouseInput.Y * normalizedSpeed);

        if(Physics.RayCast(Actor.Position, lastDirection,out var info, CameraRadius, ((uint)LayerEnum.World), false))
        {
            Debug.Log("Corrigido pela especulação");
            Actor.Position = info.Point * info.Normal + CameraRadius;
        }
        else
        {
            var outside = Physics.LineCast
                (
                    start: lastPosition,
                    end: Actor.Position,
                    hitInfo: out var hit,
                    layerMask: ((uint)LayerEnum.World),
                    hitTriggers: false
                );

            if (outside)
            {
                Debug.Log("Precisa corrigir " + hit.Collider.Name + " em " + hit.Point);
                Actor.Position = hit.Point * hit.Normal + CameraRadius;
                //Actor.AddMovement(info.Point);
            }
        }
        
        

        

        //var pointToTargetDirection = (Target.Position - collisionpoint).Normalized;
        //var angle = Vector3.Angle(surfaceNormal, pointToTargetDirection);
        //if(angle < 90f)
        //{
        //    Debug.Log("Precisa de correção");
        //}

        if (LookTo != null)
            Actor.LookAt(LookTo.Position, Vector3.Up);

        //Corrigindo distanciamento
        var distance = Vector3.Distance(Actor.Position, Target.Position);
        if (!Mathf.WithinEpsilon(distance, DistanceFromTarget, DistanceMargin))
        {
            if (distance < DistanceFromTarget)
                Actor.AddMovement(Actor.Transform.Backward * normalizedAproximationSpeed);

            else
                Actor.AddMovement(Actor.Transform.Forward * normalizedAproximationSpeed);
        }




    }

    //public override void OnFixedUpdate()
    //{
    //    var needFix = Physics.SphereCast(Actor.Position, CameraRadius, Vector3.Down, out var hitInfo, 10f, ((uint)LayerEnum.World), false);
    //    if (needFix)
    //    {

    //        //foreach (var collider in results)
    //        //{
    //        //    var rayDirection = collider.Position - Actor.Position;
    //        //    Physics.RayCast(Actor.Position, rayDirection.Normalized, out var info, layerMask: ((uint)LayerEnum.World), hitTriggers: false);
    //        //    surfaceNormal = info.Normal;
    //        //}
    //        Debug.Log("hit em " + hitInfo.Point);
    //        if (hitInfo.Point == Vector3.Zero)
    //            return;
    //        Actor.Position = hitInfo.Point + hitInfo.Normal * CameraRadius +11f;
    //    }
    //}
   



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
