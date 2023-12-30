using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;

public class DashController : Script
{
    public Dash Dash;
    public float BufferDuration = 1f;
    public float DirectionInputMultiplier = 0.5f;
    private float bufferCount;

    public override void OnStart()
    {
        Dash.OnDash += OnDash;
        Dash.OnDashOut += OnDashOut;

    }

    public override void OnDestroy()
    {
        Dash.OnDash -= OnDash;
        Dash.OnDashOut -= OnDashOut;
        OnDashOut();
    }


    public void OnDash()
    {

        bufferCount = BufferDuration;
        //Dash.Entity.EntityMovement.XFactor.Multiplicators.Add(0.5f);
        //Dash.Entity.EntityMovement.ZFactor.Multiplicators.Add(0f);

    }
    public void OnDashOut()
    {
        bufferCount = 0f;
        //Dash.Entity.EntityMovement.XFactor.Multiplicators.Remove(0.5f);
        //Dash.Entity.EntityMovement.ZFactor.Multiplicators.Remove(0f);
    }

    public override void OnUpdate()
    {

        if(Input.GetActionState(Values.InputDash) == InputActionState.Press)
        {
            if (bufferCount <= 0f)
            {
                var dashed = Dash.TryDash();
                if (dashed)
                {
                    bufferCount = BufferDuration;

                }

            }

        }

        if (bufferCount >= 0f)
            bufferCount -= Time.DeltaTime;
    }

}

