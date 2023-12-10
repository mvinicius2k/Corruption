using FlaxEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IAttack
    {
        public void Attack();
    }


    public class EntityDefense : Script
    {
        public bool Blind;
        [DefaultValue(1000f)]
        public float CurrentLife = 1000f;
        public float MaxLife = 1000f;
        public Entity Entity;


        public readonly List<EffectBind> _effects = new List<EffectBind>();



        public void DigestAttack(Hit hit, bool ignoreEffect = false)
        {
            if (Blind)
                return;

            CurrentLife -= hit.Damage;


            if (hit.Effect.HasValue)
            {
                _effects.Add(new EffectBind { Effect = hit.Effect.Value, Timer = hit.Effect.Value.Duration });
                hit.Effect.Value.Start(this);
            }

        }

        public override void OnUpdate()
        {
            ProcessEffects(Time.DeltaTime);
        }
        public override void OnFixedUpdate()
        {

            

        }

        private void ProcessEffects(float deltaTime)
        {
           
            for (int i = _effects.Count - 1; i >= 0; i--)
            {
                Debug.Log(_effects[i].Timer + " - " + deltaTime);
                _effects[i].Timer -= deltaTime;
                
                if(_effects[i].Timer <= 0f)
                {
                    _effects[i].Effect.End(this);
                    _effects.RemoveAt(i);
                }
            }
           
        }
    }
}
