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
    public float Speed;

    public Vector3 Angle;
    public Vector3 eyeTranslation;
    public Vector3 moveToHere;

    public override void OnUpdate()
    {
        Screen.CursorVisible = false;
        Screen.CursorLock = CursorLockMode.Locked;

        var mouseInput = new Vector2(Input.GetAxis(Values.InputMouseX), Input.GetAxis(Values.InputMouseY));
        var normalizedSpeed = Speed * Time.DeltaTime;
        //Corrigindo distância
        //var sDistance = Vector3.DistanceSquared(Actor.Transform.Translation, Target.Transform.Translation);
        //(var minSDistance, var maxSDistance) = (Mathf.NextPowerOfTwo(MinMaxDistances.MinValue), Mathf.NextPowerOfTwo(MinMaxDistances.MaxValue));
        //var targetPosition = Actor.Position - Target.Position;
        //    Actor.Position = Vector3.Lerp(Actor.Position, targetPosition, normalizedSpeed);

        //argetTransform.Scale = Vector3.One;

        if (!Mathf.WithinEpsilon(Vector3.Distance(Actor.Position, Target.Position), eyeTranslation.Length, Values.DistanceEpsilon))
        {
            var target = Actor.Position - Target.Position;
            var percent = eyeTranslation.Length / Vector3.Distance(Actor.Position, Target.Position);
            
            moveToHere = target;

        }

        Actor.RotateAround(Target.Position, Vector3.Up, mouseInput.X * normalizedSpeed);
        Actor.RotateAround(Target.Position, Vector3.Right, mouseInput.Y * normalizedSpeed);
        
        if (LookTo != null)
            Actor.LookAt(LookTo.Position, Vector3.Up);




        //Actor.RotateAround(Target.Position, Actor.Transform.Up, normalizedSpeed);
    }

    public override void OnStart()
    {
        eyeTranslation = Actor.Position - Target.Position;

    }

    public void GetEyePosition()
    {

    }

    public override void OnDebugDrawSelected()
    {
        Debug.Log(Vector3.Distance(Target.Position, Actor.Position));
        DebugDraw.DrawWireSphere(new BoundingSphere { Center = moveToHere, Radius = 50f }, Color.AliceBlue);
    }


}
