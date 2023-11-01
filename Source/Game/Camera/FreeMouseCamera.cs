using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// FloatCamera Script.
    /// </summary>
    public class FreeMouseCamera : Script
    {
        public float CameraSmoothing { get; set; } = 20.0f;
        public bool ActiveInput { get; set; } = true;
        public Vector2 PitchLimits;
        private float pitch;
        private float yaw;
        public override void OnStart()
        {
            //inicializando pitch e yaw de acordo com a orientação da camera
            var initialAngle = Actor.Orientation.EulerAngles;
            yaw = initialAngle.Y;
            pitch = initialAngle.Z;
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
            Screen.CursorVisible = false;
            Screen.CursorLock = CursorLockMode.Locked;

            if (ActiveInput)
            {
                var mouseInput = new Float2(Input.GetAxis(Values.InputMouseX), Input.GetAxis(Values.InputMouseY));
                pitch = Mathf.Clamp(pitch + mouseInput.Y, PitchLimits.MinValue, PitchLimits.MaxValue);
                yaw += mouseInput.X;
            }
        }

        public override void OnFixedUpdate()
        {
            var camTrans = Actor.Transform;
            var camFactor = Mathf.Saturate(CameraSmoothing * Time.DeltaTime);

            camTrans.Orientation = Quaternion.Lerp(camTrans.Orientation, Quaternion.Euler(pitch, yaw, 0), camFactor);
            Actor.Transform = camTrans;
        }

    }
}
