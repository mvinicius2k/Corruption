using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlaxEngine;
using FlaxEngine.GUI;
using Game;
using Newtonsoft.Json.Linq;

namespace Game;



/// <summary>
/// Test Script.
/// </summary>
/// 

public class Test : Script
{
    
    public MutableScript<IEffect> Effect;
    public ComposeValue<Vector2> Input = new ComposeValue<Vector2> { BaseValue = Vector2.One };
    public MutableScript<IGroundDetector> Ground;
    public Dictionary<string, MutableScript<IEffect>> dict;
    public override void OnStart()
    {

        Input.Functions.Add(new FunctionNode<Vector2>
        {
            Order = 5,
            Function = (input) => Vector2.Zero
        });
        Input.Functions.Add(new FunctionNode<Vector2>
        {
            Order = 0,
            Function = (input) => input + Vector2.One
        });



    }
    //

    [EditorAction]
    public void Preview()
    {
        Debug.Log(Input.Value);
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

public class Fast : IGroundDetector
{
    public string OPla;
    public IObservable<bool> Grounded => throw new NotImplementedException();

    public IObservable<bool> Sliding => throw new NotImplementedException();
}