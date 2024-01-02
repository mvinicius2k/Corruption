using System;
using System.Collections.Generic;

using FlaxEngine;
using Game;


namespace Game
{
    /// <summary>
    /// EntityMovement Script.
    /// </summary>
    public class EntityMovement : Script, IMoveUpdater
    {
        /// <summary>
        /// 
        /// </summary>
        private const float MagicInertia = 50f; //Funciona

        [ShowInEditor, Serialize]
        private PipedFloat speed = new PipedFloat { BaseValue = 30000f };
        [ShowInEditor, Serialize]
        private PipedFloat jumpForce = new PipedFloat { BaseValue = 50000f };
        [ShowInEditor, Serialize]
        private MutableScript<IGroundDetector> groundDetector;
        public PipedFloat JumpForce => jumpForce;
        public PipedFloat Speed => speed;

        public float velocitySmooth = 1f;

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

        [ShowInEditor, ReadOnly]
        private ComposeValue<Vector3> moveVector = new();
        public ComposeValue<Vector3> MoveVector => moveVector;
        public bool Grounded => groundDetector.Value.Grounded.Value;
        [ShowInEditor, ReadOnly]
        private IPlatformSlider currentPlatformSlider;
        
        

        public override void OnAwake()
        {
            jumping = new Observable<bool>();
        }

        

        public void Jump()
        {

            jumping.Value = true;
            
            RigidBody.AddForce(new Vector3(0f, JumpForce.TotalValue, 0f), ForceMode.Impulse);
            
            //Debug.Log("Iniciando pulo");
        }
        public void AddImpulseInAir()
        {
            if (!jumping.Value)
                return;

            RigidBody.AddForce(new Vector3 { Y = JumpForce.TotalValue * JumpAirForceMultiplicator });
            jumpAirDurationCount -= Time.DeltaTime;
            //Debug.Log("Pulando");
            if (jumpAirDurationCount <= 0f)
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
            //Debug.Log("Deixando de pular");
        }


        /// <inheritdoc/>
        public override void OnEnable()
        {
            groundDetector.Value.CurrentGround.OnChange += OnGroundChange;
        }

        private void OnGroundChange(Collider groundCollider)
        {
            if(groundCollider == null)
            {
                if (currentPlatformSlider != null)
                    RigidBody.LinearVelocity += currentPlatformSlider.Displacement * MagicInertia;
                currentPlatformSlider = null;
                Debug.Log("Sem chaõ");
                return;
            }


            currentPlatformSlider = groundCollider.GetScriptInParent<IPlatformSlider>();
   
            if(currentPlatformSlider != null)
            {
               
                Debug.Log("Novo chãi: " + groundCollider.Name + " em " + currentPlatformSlider);
            }
            else
            {
                currentPlatformSlider = null;
                Debug.Log("Chão sem plataforma");
            }
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            groundDetector.Value.CurrentGround.OnChange -= OnGroundChange;
        }




        public override void OnUpdate()
        {
            


            var speedDelta = Speed.TotalValue * Time.DeltaTime;

            if (Grounded)
                RigidBody.AngularVelocity = Vector3.Zero;


            if (groundDetector.Value.CurrentGround is IPlatformSlider)
            {

            }
            if (Grounded)
            {
                RigidBody.LinearVelocity = new Vector3
                {
                    X = MoveVector.Value.X * speedDelta,
                    Y = RigidBody.LinearVelocity.Y,
                    Z = MoveVector.Value.Z * speedDelta
                };

            }

            //if (MoveVector.Value.IsZero && Grounded)
            //    RigidBody.LinearVelocity = new Vector3 { Y = RigidBody.LinearVelocity.Y, };
        }

        public override void OnLateUpdate()
        {
            if (currentPlatformSlider != null)
            {
                Debug.Log("Deslocamento " + currentPlatformSlider.Displacement);
                Actor.Position += currentPlatformSlider.Displacement;
            }
        }

    }
}
