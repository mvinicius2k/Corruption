using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// EntityMovementController Script.
    /// </summary>
    public class EntityMovementController : Script
    {
        public EntityMovement EntityMovement;


        /// <inheritdoc/>
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
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

            //Direção de acordo com um objeto
            var right = Camera.MainCamera.Transform.Right;
            var fowared = Vector3.ProjectOnPlane(Camera.MainCamera.Transform.Forward, Vector3.Up).Normalized;
            var xInput = Input.GetAxisRaw(Values.InputHorizontalAxis);
            var yInput = Input.GetAxisRaw(Values.InputVerticalAxis);
            EntityMovement.MoveVector.BaseValue = fowared * yInput + right * xInput;

            if (Input.GetActionState(Values.InputJump) == InputActionState.Press)
            {
                EntityMovement.Jump();
            }
            else if (Input.GetActionState(Values.InputJump) == InputActionState.Pressing)
            {
                EntityMovement.AddImpulseInAir();

            }
        }
    }
}
