using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomEditors;

public static class ReflectionUtils
{
    /// <summary>
    /// Gets all concrete types from all assemblies that implements <paramref name="typeInterface"/>
    /// </summary>
    /// <param name="typeInterface"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetTypesImplementations(Type typeInterface)
    {

        var assembly = AppDomain.CurrentDomain.GetAssemblies();
        var allTypes = assembly.SelectMany(assembly => assembly.GetTypes());

        var implementors = allTypes
        .Where(type => typeInterface.IsAssignableFrom(type) && !type.IsInterface);


        return implementors;
    }
}

