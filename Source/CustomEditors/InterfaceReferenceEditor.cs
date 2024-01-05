using EditorPlus;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEditors;

[CustomEditor(typeof(IInterfaceReference<>)), DefaultEditor]
public class InterfaceReferenceEditor : GenericEditor
{

    private Type currentChoosedType;
    private Type genericArgument;

    public override void Initialize(LayoutElementsContainer layout)
    {
        genericArgument = Values.Type.Type.GenericTypeArguments[0];
        var btnSelectInstance = layout.Button("Select Instance");
        btnSelectInstance.Button.Clicked += OnBtnSelectInstanceClicked;
        if (Values[0] == null)
        {
            //Adding elements
            return;
        }


        var referenceType = Values[0].GetType();
        var referenceDefinition = referenceType.GetGenericTypeDefinition(); ;
        currentChoosedType = (Type)referenceType.GetProperty(nameof(IInterfaceReference<object>.ChoosedType)).GetValue(Values[0]);


        Debug.Log("Tipo: " + currentChoosedType);

        base.Initialize(layout);
    }

    protected override void OnUnDirty()
    {

        //For update UI when swap values is not between scripts
        if (Values[0] != null)
        {
            if (!CheckIfIsFlax(currentChoosedType))
                if (ParentEditor != null)
                    ParentEditor.RebuildLayout();
                else
                    RebuildLayout();


        }
        base.OnUnDirty();
    }

    private void OnBtnSelectInstanceClicked()
    {
        var searchWindow = new InterfaceSearchWindow();
        searchWindow.Show();
        searchWindow.Init(genericArgument);
        searchWindow.OnTypeChoosed += OnTypeChoosed;
    }

    private void OnTypeChoosed(Type choosedType)
    {
        //Same type, nothing to do
        if (choosedType == currentChoosedType)
            return;


        Debug.Log($"Tipo {choosedType.Name}");



        //Initializing object to show if is not script
        if (CheckIfIsFlax(choosedType))
        {
            var flaxReferenceType = typeof(FlaxObjectReference<,>).MakeGenericType(genericArgument, choosedType);
            var flaxReference = Activator.CreateInstance(flaxReferenceType);
            SetValue(flaxReference);
        }
        else
        {
            var objectReferenceType = typeof(ObjectReference<,>).MakeGenericType(genericArgument, choosedType);
            var objectReference = Activator.CreateInstance(objectReferenceType);
            SetValue(objectReference);

        }

        RebuildLayoutOnRefresh();


    }

    public bool CheckIfIsFlax(Type type)
    {
        return typeof(FlaxEngine.Object).IsAssignableFrom(type);
    }

}

