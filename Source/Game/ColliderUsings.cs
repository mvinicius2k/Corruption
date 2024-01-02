using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ColliderUsings : Script
    {
        [ShowInEditor, Serialize]
        private List<Script> acess = new();
        public List<Script> Acess => acess;
        

        
    }
}
