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
        public override void OnUpdate()
        {

            //Direção de acordo com um objeto
            var right = Camera.MainCamera.Transform.Right;
            var entityUp = EntityMovement.Transform.Up;
            var fowared = Vector3.ProjectOnPlane(Camera.MainCamera.Transform.Forward, entityUp).Normalized;
            var xInput = Input.GetAxisRaw(Values.InputHorizontalAxis);
            var yInput = Input.GetAxisRaw(Values.InputVerticalAxis);
            
            
            EntityMovement.MoveVector.BaseValue = fowared * yInput + right * xInput;

            if (Input.GetActionState(Values.InputJump) == InputActionState.Press)
            {
                EntityMovement.TryJump();
            }
            else if (Input.GetActionState(Values.InputJump) == InputActionState.Pressing)
            {
                EntityMovement.AddImpulseInAir();

            }
            else if(Input.GetActionState(Values.InputJump) == InputActionState.Release)
            {
                EntityMovement.ReleaseJump();
            }
        }
    }
}
