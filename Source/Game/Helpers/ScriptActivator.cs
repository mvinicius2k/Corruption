using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;

public enum WakeState
{
    Auto, Sleep, Wake
}

/// <summary>
/// Ativa ou desativa com <see cref="Script.Enabled"/> o script alvo
/// </summary>
public class ScriptActivator : Script
{
    [ShowInEditor, Serialize]
    private Collider collider;
    [ShowInEditor, Serialize]
    private FlaxEngine.Script target;
    public LayerEnum filterMask;
    public WakeState WakeState;
    public bool KeepAlive;

    public override void OnStart()
    {
        switch (WakeState)
        {
            case WakeState.Auto:
                break;
            case WakeState.Sleep:
                target.Enabled = false;
                break;
            case WakeState.Wake:
                target.Enabled = true;
                break;
            default:
                break;
        }
    }

    public override void OnEnable()
    {
        collider.CollisionEnter += EnableTarget;
        collider.CollisionExit += DisableTarget;
    }

    public override void OnDisable()
    {
        collider.CollisionEnter -= EnableTarget;
        collider.CollisionExit -= DisableTarget;
    }

    private void EnableTarget(Collision collision)
    {
        if(filterMask.HasLayer(collision.OtherActor.Layer))
            target.Enabled = true;
        if(!KeepAlive)
            Enabled = false;

    }

    private void DisableTarget(Collision collision)
    {
        if (filterMask.HasLayer(collision.OtherActor.Layer))
            target.Enabled = false;
        if (!KeepAlive)
            Enabled = false;
    }



}

