using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;

public static class Extensions
{
    public static Vector3 LinearInterpolate(this Spline spline, float time)
    {
        return Vector3.Backward;
    }

    /// <summary>
    /// Verifica se a máscara tem a camada <paramref name="layer"/> marcada
    /// </summary>
    /// <param name="layerEnum"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool HasLayer(this LayerEnum layerEnum, int layer)
        => layerEnum.HasFlag((LayerEnum)layer);

    public static T GetScriptInParent<T>(this Actor actor, bool includeSelf = true) where T : class
    {

        var current = includeSelf ? actor : actor.Parent;
        while (current != null)
        {
            var found = current.GetScript<T>();
            if (found != null)
                return found;
            current = current.Parent;
        }
        return null;
       
    }

}
