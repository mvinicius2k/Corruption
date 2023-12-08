using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Game;
public static class ReflectionUtils
{
    public static IEnumerable<Type> GetTypesImplementations(Type typeInterface)
    {

        var assembly = Assembly.GetExecutingAssembly();
        var allTypes = assembly.GetTypes();

        // Filtrar os tipos que implementam a interface desejada
        var implementors = allTypes
        .Where(type => typeInterface.IsAssignableFrom(type) && !type.IsGenericType);


        return implementors;
    }
}
