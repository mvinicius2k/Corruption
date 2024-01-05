using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EditorPlus;
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
    public FlaxEngine.Object Object;
    public MutableScript<IEffect> Effect;
    public ComposeValue<Vector2> Input = new ComposeValue<Vector2> { BaseValue = Vector2.One };
    public MutableScript<IGroundDetector> Ground;
    public Dictionary<string, MutableScript<IEffect>> dict;

    //public InterfaceReference<IEffect> OtherEffect;


    public IInterfaceReference<IEffect> Better;

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

    [ShowInEditor]
    public void Preview()
    {
        Debug.Log(Input.Value);
    }


    //[EditorAction]
    //public void ViewInterface()
    //{
    //    if (OtherEffect == null)
    //        Debug.Log("Não inicializado");
    //    else if (OtherEffect.Instance != null)
    //    {
    //        Debug.Log("Tipo: " + OtherEffect.Instance.ToString() + " " + OtherEffect.Instance.Name);
    //    }


    //}
    //[EditorAction]
    //public void View()
    //{

    //    var msg = "null";
    //    if (Effect.Instance != null)
    //    {
    //        msg = Effect.Instance.ToString() + " " + Effect.Instance.Name;
    //    }
    //    Debug.Log("Tipo: " + msg);
    //}
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




