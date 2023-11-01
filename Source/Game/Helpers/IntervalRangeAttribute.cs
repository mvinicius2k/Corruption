using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;
public class IntervalRangeAttribute : Attribute
{
    public float Min { get; private set; }
    public float Max { get; private set; }

    public IntervalRangeAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }


}
