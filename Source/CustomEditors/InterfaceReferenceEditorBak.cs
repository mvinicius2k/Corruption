using EditorPlus;
using FlaxEditor;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEditor.Scripting;
using FlaxEditor.Viewport.Modes;
using FlaxEngine;
using FlaxEngine.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEditors;

[CustomEditor(typeof(InterfaceReference<>)), DefaultEditor]
public class InterfaceReferenceEditorBak : GenericEditor
{
    

    //For handle object
    private IInterfaceReferenceOld interfaceRef;
//    private Type instanceType;
    private Type genericTypeArg;
    private static Type FlaxObjectTypeHolder;

    public bool TypeIsScript(Type type)
    {
        return typeof(FlaxEngine.Object).IsAssignableFrom(type);
    }


    public override void Initialize(LayoutElementsContainer layout)
    {


        interfaceRef = (IInterfaceReferenceOld)Values[0];

        //Loading back values ... nao precisa, interfaceRef tem tudo
        //if(FlaxObjectTypeHolder != null) //When type selected is script, the instance still null, revovering by static holder in here
        //    instanceType = FlaxObjectTypeHolder;
        //genericTypeArg = Values.Type.Type.GenericTypeArguments[0];

        //

        //Adding elements
        var label = layout.AddPropertyItem(interfaceRef?.ChoosedType?.Name ?? "Instance");
        var btnSelectInstance = layout.Button("Select Instance");
        label.AddElement(btnSelectInstance);
        btnSelectInstance.Button.Clicked += OnBtnSelectInstanceClicked;

        //Initial state. Only draw button
        if (interfaceRef == null || interfaceRef.ChoosedType == null)
            return;


        //FlaxObject behaviour
        if (TypeIsScript(interfaceRef.ChoosedType))
        {
            var objectRefPicker = layout.Custom<FlaxObjectRefPickerControl>("Instance");
            objectRefPicker.CustomControl.Type = new ScriptType(interfaceRef.ChoosedType);
            objectRefPicker.CustomControl.Value = (FlaxEngine.Object) interfaceRef.RawInstance;
            //Updating interface when change value 
            objectRefPicker.CustomControl.ValueChanged += () =>
            {
                var flaxObjectIntance = objectRefPicker.CustomControl.Value;
                interfaceRef.TrySetInstance(flaxObjectIntance);
                SetValue(flaxObjectIntance);
                RebuildLayoutOnRefresh();


            };
        }
        //Object behaviour
        else
        {
            //var info = new ScriptMemberInfo(instanceType);
            //var container = new ValueContainer(new ScriptMemberInfo(instanceType));

            //container.Add(interfaceRef);
            //layout.Object(container);
        }
            base.Initialize(layout);


        //
    }



    private void OnBtnSelectInstanceClicked()
    {
        var genericParam = Values.Type.Type.GenericTypeArguments[0];
        var searchWindow = new InterfaceSearchWindow();
        searchWindow.Show();
        searchWindow.Init(genericParam);
        searchWindow.OnTypeChoosed += OnTypeChoosed;
    }

    private void OnTypeChoosed(Type choosedType)
    {
        //Initializing script interface container
        if (interfaceRef == null)
        {
            //The interface type
            var genericArg = Values.Type.Type.GenericTypeArguments[0];
            //Infered InterfaceReference<T> type
            var structureType = typeof(InterfaceReference<>).MakeGenericType(genericArg);
            var structureInstance = Activator.CreateInstance(structureType);

            interfaceRef = (IInterfaceReferenceOld)structureInstance;


        }
        else if (interfaceRef.ChoosedType == choosedType)
            return; //Nothing to do

        Debug.Log($"Tipo {choosedType.Name}");



        //Initializing object to show if is not script
        if (!TypeIsScript(choosedType))
        {
            var instance = Activator.CreateInstance(choosedType);
            interfaceRef.TrySetInstance(instance);
        }
        else
        {
            interfaceRef.ChoosedType = choosedType;

        }

        //instanceType = choosedType;
        SetValue(interfaceRef);
        RebuildLayoutOnRefresh();


    }

    protected override void OnUnDirty()
    {

        //For update UI when swap values is not between scripts
        if (interfaceRef != null)
        {
            if (!TypeIsScript(interfaceRef.ChoosedType))
                if (ParentEditor != null)
                    ParentEditor.RebuildLayout();
                else
                    RebuildLayout();


        }
        base.OnUnDirty();
    }

    protected override void Deinitialize()
    {

        //var projectCache = Editor.Instance.ProjectCache;

        //projectCache.SetCustomData(TypeCacheKey, JsonSerializer.Serialize(referenceType.AssemblyQualifiedName));
        //if (flaxObjectIntance != null)
        //    projectCache.SetCustomData(FlaxObjectCacheKey, JsonSerializer.Serialize(flaxObjectIntance, referenceType));
        //else if (flaxObjectIntance != null)
        //    projectCache.SetCustomData(ObjectCacheKey, JsonSerializer.Serialize(objectInstance, referenceType));

        base.Deinitialize();
    }

}

