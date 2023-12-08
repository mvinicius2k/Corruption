using System;
using System.Collections.Generic;
using FlaxEngine;


namespace Game
{
    /// <summary>
    /// EntityMovement Script.
    /// </summary>
    public class EntityMovement : Script, IMoveUpdater
    {
        public PipedFloat Speed = new PipedFloat { BaseValue = 20000f } ;
        public PipedFloat JumpForce = new PipedFloat { BaseValue = 10000f };
        



        public float TargetFront => Mathf.Atan2(MoveVector.X, MoveVector.Z) * Mathf.RadiansToDegrees;
        public bool PauseFront => MoveVector.IsZero;

        //public Vector2 Direction;
        public RigidBody RigidBody;
        public float JumpAirForceMultiplicator = 0.25f;
        public float JumpAirDuration = 1f;
        public Actor AnchorTarget;

        private float jumpAirDurationCount = 0f;
        private bool Jumping;
        public Vector3 MoveVector;
        Transform transform;

        public void Jump()
        {

            Jumping = true;
            RigidBody.AddForce(new Vector3(0f, JumpForce.TotalValue, 0f), ForceMode.Impulse);
            jumpAirDurationCount = JumpAirDuration;
        }
        public void AddImpulseInAir()
        {
            RigidBody.AddForce(new Vector3 { Y = JumpForce.TotalValue * JumpAirForceMultiplicator });
            jumpAirDurationCount -= Time.DeltaTime;

            Jumping = JumpAirDuration <= 0f;
        }

      
        /// <inheritdoc/>
        public override void OnEnable()
        {
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }
        /// <inheritdoc/>
        public override void OnUpdate()
        {

            var speedDelta = Speed.TotalValue * Time.DeltaTime;

            RigidBody.LinearVelocity = new Vector3
            {
                X = MoveVector.X * speedDelta,
                Y = RigidBody.LinearVelocity.Y,
                Z = MoveVector.Z * speedDelta
            };


            if (MoveVector.IsZero)
                RigidBody.LinearVelocity = new Vector3 { Y = RigidBody.LinearVelocity.Y, };
        }

    }
}
