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
        [ShowInEditor, Serialize]
        private PipedFloat speed = new PipedFloat { BaseValue = 20000f } ;
        [ShowInEditor, Serialize]
        private PipedFloat jumpForce = new PipedFloat { BaseValue = 10000f };
        public MutableScript<IGroundDetector> GroundDetector;
        public PipedFloat JumpForce => jumpForce;
        public PipedFloat Speed => speed;


        public float TargetFront => Mathf.Atan2(MoveVector.Value.X, MoveVector.Value.Z) * Mathf.RadiansToDegrees;
        public bool PauseFront => MoveVector.Value.IsZero;

        //public Vector2 Direction;
        public RigidBody RigidBody;
        public float JumpAirForceMultiplicator = 0.25f;
        public float JumpAirDuration = 1f;
        [ShowInEditor, Serialize]
        private Vector3 gravityDirection = Vector3.Down;

        public Vector3 GravityDirection => gravityDirection.Normalized;

        private float jumpAirDurationCount = 0f;
        private IObservable<bool> jumping;
        public IObservable<bool> Jumping => jumping;

        private ComposeValue<Vector3> moveVector = new();
        public ComposeValue<Vector3> MoveVector => moveVector;
        public bool Grounded => GroundDetector.Value.Grounded.Value;


        public override void OnAwake()
        {
            jumping = new Observable<bool>();
        }

        public void Jump()
        {

            jumping.Value = true;
            RigidBody.AddForce(new Vector3(0f, JumpForce.TotalValue, 0f), ForceMode.Impulse);
            jumpAirDurationCount = JumpAirDuration;
            Debug.Log("Iniciando pulo");
        }
        public void AddImpulseInAir()
        {
            RigidBody.AddForce(new Vector3 { Y = JumpForce.TotalValue * JumpAirForceMultiplicator });
            jumpAirDurationCount -= Time.DeltaTime;
            Debug.Log("Pulando");
            if(jumpAirDurationCount <= 0f)
            {

                ReleaseJump();


            }
        }

        public bool TryJump()
        {
            if (Grounded)
            {
                Jump();
                return true;
            }
            return false;
        }

        public void ReleaseJump()
        {
            jumping.Value = false;
            Debug.Log("Deixando de pular");
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
                X = MoveVector.Value.X * speedDelta,
                Y = RigidBody.LinearVelocity.Y,
                Z = MoveVector.Value.Z * speedDelta
            };


            if (MoveVector.Value.IsZero)
                RigidBody.LinearVelocity = new Vector3 { Y = RigidBody.LinearVelocity.Y, };
        }

    }
}
