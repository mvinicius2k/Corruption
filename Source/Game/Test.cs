using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game;

/// <summary>
/// Test Script.
/// </summary>
public class Test : Script
{
    public Collider Collider;
    public override void OnStart()
    {
        Collider.CollisionEnter += Lo;
        
    }
    
    public void Lo(Collision c)
    {
        Debug.Log("Colidiu com " + c.OtherActor);
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
        // Here you can add code that needs to be called every frame
    }
}
