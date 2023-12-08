using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlaxEngine;
using FlaxEngine.GUI;

namespace Game;



/// <summary>
/// Test Script.
/// </summary>
/// 

public class Test : Script
{
    
    public MutableScript<IEffect> Effect;


    public Dictionary<string, MutableScript<IEffect>> dict;
    public override void OnStart()
    {


    }
    [EditorAction]
    public void Set()
    {

        //Effect.TrySetImplementor(typeof(TailEffect));
        //Effect.refHolder = Actor.GetScript<TailEffect>();
    
    
    }


    [EditorAction]
    public void View()
    {

        var msg = "null";
        if (Effect.Value != null)
        {
            msg = Effect.Value.ToString() + " " + Effect.Value.Name;
        }
        Debug.Log("Tipo: " + msg);
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
