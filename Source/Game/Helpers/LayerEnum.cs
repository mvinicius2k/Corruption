using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;
[Flags]
public enum LayerEnum : uint
{
    Default = 1 << 0,
    Player = 1 << 1,
    World = 1 << 2,

    All = ~1u
}
