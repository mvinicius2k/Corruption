using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;

    public interface IGroundDetector
    {
        public IObservable<bool> Grounded { get; }
        public IObservable<bool> Sliding { get; }
        
        
        public IObservable<Collider> CurrentGround { get; }

    }


