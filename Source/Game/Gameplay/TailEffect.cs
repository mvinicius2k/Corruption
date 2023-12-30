using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class TailEffect : Script, IEffect
    {
        public Trap Origin;
        public int Hello;
        public string Name;
        public float Duration => 10;

        string IEffect.Name => this.Name;

        public void End(EntityDefense defense)
        {
            
        }

        public void Start(EntityDefense defense)
        {
            
        }
    }
}
