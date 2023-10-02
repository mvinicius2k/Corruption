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
        [Tooltip($"Do tipo {nameof(EntityMovementData)}")]
        public JsonAsset DataProps;
        private EntityMovementData props;
        public EntityMovementData Props => props;
        public Vector2 Direction;
        public RigidBody RigidBody;
        public float JumpAirForceMultiplicator = 0.25f;
        public float JumpAirDuration = 1f;
        public Actor AnchorTarget;

        private float jumpAirDurationCount = 0f;
        private bool Jumping;
        Transform transform;
        
        public event EventHandler OnMoveUpdate;

        public void Jump()
        {

            Jumping = true;
            RigidBody.AddForce(new Vector3(0f, props.JumpForce, 0f), ForceMode.Impulse);
            jumpAirDurationCount = JumpAirDuration;
        }
        public void AddImpulseInAir()
        {
            RigidBody.AddForce(new Vector3 { Y = props.JumpForce * JumpAirForceMultiplicator });
            jumpAirDurationCount -= Time.DeltaTime;

            Jumping = JumpAirDuration <= 0f;
        }

        /// <inheritdoc/>
        public override void OnStart()
        {
            props = DataProps.CreateInstance<EntityMovementData>();
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

            if (Direction != Vector2.Zero)
            {
                OnMoveUpdate.Invoke(this, EventArgs.Empty);
            }
            //if (Direction != Vector2.Zero)
            //{
            //    //var camMultiplicator = AnchorTarget.HasTag(Values.TagCamera) ? -1f : 1f; //Se for camera, a âncora se inverte
            //    //var pos = Actor.Position - AnchorTarget.Position;
            //    //var angle = Mathf.Atan2(pos.X, pos.Z) * Mathf.RadiansToDegrees;
            //    //Actor.EulerAngles = angle * Vector3.Up;
            //}
            var speedDelta = Props.Speed * Time.DeltaTime;


            var targetTRS = Actor.WorldToLocalMatrix;
            var euler = new Vector3
            {
                X = Mathf.Atan2(Direction.X, 1f),
                Y = 0f,
                Z = Mathf.Atan2(Direction.Y, 1f),
            } * Mathf.RadiansToDegrees;

            var matrix = Matrix.Transformation(Float3.One, Quaternion.Euler(euler), Float3.Zero);
            var targetMatrix = Matrix.Multiply(targetTRS, matrix);

            targetMatrix.Decompose(out transform);
            var outDirection = transform.Orientation.EulerAngles.Normalized;
            RigidBody.LinearVelocity = new Vector3
            {
                X = outDirection.X * speedDelta,
                Y = RigidBody.LinearVelocity.Y,
                Z = outDirection.Z * speedDelta
            };


            if (Direction == Vector2.Zero)
                RigidBody.LinearVelocity = new Vector3 { Y = RigidBody.LinearVelocity.Y, };
        }

        public override void OnDebugDraw()
        {
            //DebugDraw.DrawWireCone(Actor.Position, Quaternion.Identity, 1f, transform., 0f, Color.Blue)
        }
    }
}
