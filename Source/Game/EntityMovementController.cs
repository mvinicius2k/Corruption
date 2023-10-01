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
            EntityMovement.Direction = new Vector2
            {
                X = Input.GetAxisRaw(Values.InputHorizontalAxis),
                Y = Input.GetAxisRaw(Values.InputVerticalAxis),
            };

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
