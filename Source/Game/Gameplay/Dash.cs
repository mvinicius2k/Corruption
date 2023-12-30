using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlaxEngine;

namespace Game;

public enum Composition
{
    Absolute, Adiction, Multiply
}

public struct CompositionValue
{
    public float Value;
    public Composition Composition;

    public float With(float other)
    {
        switch (Composition)
        {
            case Composition.Absolute:
                return Value;
            case Composition.Adiction:
                return other + Value;
            case Composition.Multiply:
                return other * Value;
            default:
                return Value;
        }
    }

}

/// <summary>
/// Dash Script.
/// </summary>
public class Dash : Script
{
    public Entity Entity;
    public float Duration;
    public CompositionValue Speed;

    private float timeCount;
    private bool dashing;
    private Vector3 oldVelocity;
    private FunctionNode<Vector3> currentDashModifier;

    public event Action OnDash;
    public event Action OnDashOut;


    public bool TryDash()
    {
        

        if (!dashing && timeCount <= 0f)
        {
            dashing = true;
            timeCount = Duration;
            oldVelocity = Entity.EntityMovement.RigidBody.LinearVelocity;
            currentDashModifier = DirectionModifier;
            Entity.EntityMovement.MoveVector.Functions.Add(currentDashModifier);
            OnDash?.Invoke();
            return true;
        }
        return false;
    }

    private FunctionNode<Vector3> DirectionModifier
    {
        get
        {
            var front = Entity.Transform.Forward;
            Debug.Log($"Dash para {front.Normalized}");

            return new FunctionNode<Vector3>
            {
                Order = 1000,
                Function = (input) =>
                {
                    return front.Normalized;
                }
            };
        }
    }






    public override void OnUpdate()
    {
        if (dashing)
        {
            var speed = Speed.With(Entity.EntityMovement.Speed.TotalValue);
            Entity.EntityMovement.RigidBody.LinearVelocity = speed * Time.DeltaTime * Entity.Transform.Forward;
            timeCount -= Time.DeltaTime;

            if (timeCount <= 0f)
            {
                dashing = false;
                Entity.EntityMovement.RigidBody.LinearVelocity = oldVelocity;
                Entity.EntityMovement.MoveVector.Functions.Remove(currentDashModifier);
                OnDashOut?.Invoke();
            }
        }

    }




}
