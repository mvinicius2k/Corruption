using EditorPlus;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomEditors;


[CustomEditor(typeof(IImplementation<>)), DefaultEditor]
public class ImplementationEditor : ObjectSwitcherEditor
{
    
    protected override OptionType[] Options
    {
        get
        {
            //The interface
            var genericArgument = Values.Type.Type.GenericTypeArguments[0];

            //All type that implements the interface
            var allImplementations = ReflectionUtils.GetTypesImplementations(genericArgument).ToArray();

            //Creating options
            var allOptions = new OptionType[allImplementations.Length + 1];
            allOptions[0] = new OptionType("null", null); //Null option

            //All concrete type options
            for (int i = 1; i < allOptions.Length; i++)
            {
                var implementation = allImplementations[i - 1];

                Type typeContainer;
                if (CheckIfIsFlax(implementation)) //Holds flax objects
                    typeContainer = typeof(FlaxObjectImplementation<,>).MakeGenericType(genericArgument, implementation);
                else //Holds common objects
                    typeContainer = typeof(ObjectImplementation<,>).MakeGenericType(genericArgument, implementation);

                
                allOptions[i] = new OptionType($"{implementation.Name} ({implementation.Namespace})", typeContainer);
            }

            return allOptions;

        }
    }

    /// <returns><see langword="true"/>if <paramref name="type"/> is flax object, otherwise <see langword="false"/></returns>
    public bool CheckIfIsFlax(Type type)
        => typeof(FlaxEngine.Object).IsAssignableFrom(type);

    
}

