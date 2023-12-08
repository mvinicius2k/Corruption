using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;

public static class ReflectionExtensions
{
    public static bool IsSubclassOfRawGeneric(this Type child, Type superType)
    {
        while (child != null && child != typeof(object))
        {
            var tipoAtual = child.IsGenericType ? child.GetGenericTypeDefinition() : child;
            if (superType == tipoAtual)
                return true;
            child = child.BaseType;
        }
        return false;
    }


}

