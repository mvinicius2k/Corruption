using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;

public class CameraAnchor : Script
{
    public Actor Anchor;
    public float Speed = 40f;

    public override void OnUpdate()
    {
        Screen.CursorVisible = false;
        Screen.CursorLock = CursorLockMode.Locked;

        float normalizedSpeed = Speed * Time.DeltaTime;
        Actor.Position = Vector3.Lerp(Actor.Position, Anchor.Position, normalizedSpeed);
        Actor.EulerAngles = new Float3
        {
            X = Mathf.LerpAngle(Actor.EulerAngles.X, Anchor.EulerAngles.X, normalizedSpeed),
            Y = Mathf.LerpAngle(Actor.EulerAngles.Y, Anchor.EulerAngles.Y, normalizedSpeed),
            Z = Mathf.LerpAngle(Actor.EulerAngles.Z, Anchor.EulerAngles.Z, normalizedSpeed),
        };
    }
}

