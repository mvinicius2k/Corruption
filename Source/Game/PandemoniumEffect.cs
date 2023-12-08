using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    [Flags]
    public enum PandemoniumTargets
    {
        None = 0,
        Direction = 1 << 1,
        Combat = 1 << 2,
        Specials = 1 << 3,
    }
    public struct PandemoniumEffect : IEffect
    {

        public string Name;
        public float Duration;

        

        public PandemoniumTargets Targets;

        float IEffect.Duration => Duration;

        string IEffect.Name => Name;

        public void End(EntityDefense defense)
        {
            defense.Entity.EntityMovement.Speed.Multiplicators.Remove(-1f);
        }

        public void Start(EntityDefense defense)
        {
            defense.Entity.EntityMovement.Speed.Multiplicators.Add(-1f);
        }
    }
}
