using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Game
{
    public class GotoRoot : Script
    {
        [ShowInEditor, Serialize]
        private Actor root;
        public Actor Root => root;
    }
}
