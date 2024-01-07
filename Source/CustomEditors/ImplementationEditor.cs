using EditorPlus;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEngine;
using System;
using System.Linq;

namespace CustomEditors;


[CustomEditor(typeof(IImplementation<>)), DefaultEditor]
public class ImplementationEditor : GenericEditor
{
    /// <summary>
    /// Implementation choosed to draw members
    /// </summary>
    private Type currentChoosedType;

    /// <summary>
    /// Interface to search implementations
    /// </summary>
    private Type genericArgument;


    private ImplementationOption[] allImplementations;

    public override void Initialize(LayoutElementsContainer layout)
    {
        genericArgument = Values.Type.Type.GenericTypeArguments[0];

        
        allImplementations = ReflectionUtils.GetTypesImplementations(genericArgument)
            .Select(t => new ImplementationOption
            {
                DisplayName = $"{t.Name} ({t.FullName})",
                Type = t
            })
            .ToArray();

        //Drawing options
        var cbImplementation = layout.ComboBox("Implementation");
        var cbOptions = allImplementations.Select(opt => opt.DisplayName);
        cbImplementation.ComboBox.AddItems(cbOptions);
        cbImplementation.ComboBox.SelectedIndexChanged += OnSelectImplementation;

        //No instance... exiting
        if (Values[0] == null)
            return;

        //Has instance, loading back choosed type
        var implementationType = Values[0].GetType();
        currentChoosedType = (Type)implementationType.GetProperty(nameof(IImplementation<object>.ChoosedType)).GetValue(Values[0]);
        cbImplementation.ComboBox.SelectedIndex = Array.FindIndex(allImplementations, opt => opt.Type == currentChoosedType);


        base.Initialize(layout);
    }

    private void OnSelectImplementation(FlaxEditor.GUI.ComboBox obj)
    {

        var choosedType = allImplementations[obj.SelectedIndex].Type;

        if (choosedType == currentChoosedType)
            return;


        //Initializing object to show if is not script
        if (CheckIfIsFlax(choosedType))
        {
            var flaxReferenceType = typeof(FlaxObjectImplementation<,>).MakeGenericType(genericArgument, choosedType);
            var flaxReference = Activator.CreateInstance(flaxReferenceType);
            SetValue(flaxReference);
        }
        else
        {
            var objectReferenceType = typeof(ObjectImplementation<,>).MakeGenericType(genericArgument, choosedType);
            var objectReference = Activator.CreateInstance(objectReferenceType);
            SetValue(objectReference);

        }

        

    }

    protected override void OnUnDirty()
    {

        //For update UI when swap values is not between scripts
        if (Values[0] != null)
        {
            if (ParentEditor != null)
                ParentEditor.RebuildLayout();
            else
                RebuildLayout();


        }
        base.OnUnDirty();
    }

    /// <returns><see langword="true"/>if <paramref name="type"/> is flax object, otherwise <see langword="false"/></returns>
    public bool CheckIfIsFlax(Type type)
    {
        return typeof(FlaxEngine.Object).IsAssignableFrom(type);
    }

}

