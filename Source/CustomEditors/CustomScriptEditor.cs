

using FlaxEditor;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEditor.CustomEditors.Elements;
using FlaxEditor.CustomEditors.GUI;
using FlaxEditor.Scripting;
using FlaxEditor.Utilities;
using FlaxEngine;
using FlaxEngine.Utilities;
using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static FlaxEditor.ProjectInfo;

namespace CustomEditors;



[CustomEditor(typeof(Script)), DefaultEditor]
public class CustomScriptEditor : GenericEditor
{
    private object toPass;
    private bool initialized;
    private ItemInfo target;
    private string tes = "nao";


    protected override void SynchronizeValue(object value)
    {
        //Debug.Log("Sync " + value);
        base.SynchronizeValue(value);
    }

    protected override bool OnDirty(CustomEditor editor, object value, object token = null)
    {
        //Debug.Log("Dirty " + value);
        return base.OnDirty(editor, value, token);
    }

    protected override void OnUnDirty()
    {
        //Debug.Log("UnDirty");
        base.OnUnDirty();
    }

    //protected override void SpawnProperty(LayoutElementsContainer itemLayout, ValueContainer itemValues, ItemInfo item)
    //{

    //    //

    //    var type = item.Info.ValueType.Type;
    //    if (type.IsSubclassOfRawGeneric(typeof(Mutable<>)))
    //    {

    //        var initializedValue = item.Info.GetValue(Values[0]);

    //        if (initializedValue == null)
    //        {
    //            Debug.Log("Nao iniciado");
    //            var btn = itemLayout.Button("+");
    //            btn.Button.Clicked += () =>
    //            {
    //                var window = new InterfaceSearchWindow();
    //                window.Show();
    //                window.Init(type.GenericTypeArguments[0]);
    //                window.OnTypeChoosed += (choosedType) =>
    //                {
    //                    Debug.Log($"Tipo {choosedType.Name}");
    //                    var mutableType = typeof(Mutable<>);
    //                    var mutableInstance = mutableType.MakeGenericType(type.GenericTypeArguments[0]);
    //                    var createdInstance = Activator.CreateInstance(mutableInstance);
    //                    item.Info.SetValue(Values[0], createdInstance);
    //                    initializedValue = item.Info.GetValue(Values[0]);
    //                    //var trap = Values[0] as Trap;
    //                    //trap.Effect = new Mutable<IEffect>();
    //                    var setImplMethod = type.GetMethod(nameof(Mutable<IEffect>.TrySetImplementor));
    //                    setImplMethod.Invoke(initializedValue, new object[] { choosedType });
    //                    //trap.Effect.SetImplementor<TailEffect>();
    //                    SetValue(Values[0]);
    //                    RebuildLayout();

    //                    //var stype = new ScriptType(type);

    //                };


    //            };
    //        }
    //        else
    //        {


    //            var propertyToGet = type.GetProperty(nameof(Mutable<IEffect>.Implementor));
    //            var propertyToGetRef = type.GetProperty(nameof(Mutable<IEffect>.RefHolder));
    //            var propertyToGetObject = type.GetProperty(nameof(Mutable<IEffect>.ObjectHolder));



    //            var targetedType = propertyToGet.GetValue(initializedValue) as Type;

    //            if (targetedType.IsAssignableTo(typeof(FlaxEngine.Object)))
    //            {
    //                Debug.Log("É script");
    //                var reference = itemLayout.Custom<FlaxObjectRefPickerControl>("Refrencia");
    //                reference.CustomControl.Type = new ScriptType(targetedType);
    //                reference.CustomControl.ValueChanged += () =>
    //                {

    //                    //var trap = Values[0] as Trap;
    //                    //trap.Effect.RefHolder = ;
    //                    propertyToGetRef.SetValue(initializedValue, reference.CustomControl.Value);
    //                    SetValue(Values[0]);


    //                };

    //                var refValue = propertyToGetRef.GetValue(initializedValue);

    //                if (refValue != null)
    //                    reference.CustomControl.Value = (FlaxEngine.Object)refValue;



    //            }
    //            else
    //            {

    //                Debug.Log("É objeto");
    //                var instance = Activator.CreateInstance(targetedType);
    //                propertyToGetObject.SetValue(initializedValue, instance);
    //                var objValue = propertyToGetObject.GetValue(initializedValue);
    //                var stype = new ScriptType(targetedType);
    //                //var ceditor = CustomEditorsUtil
    //                //var obj = itemLayout.Object("Objeto", new CustomValueContainer(stype,  );
    //                //var stype = new ScriptType(targetedType);
    //                SetValue(Values[0]);




    //            }






    //            //var trap = Values[0] as Trap;

    //            //trap.Effect = new Mutable<IEffect>(new SpeedEffect
    //            //{
    //            //    Amount = -50
    //            //}); ;
    //            //SetValue(trap);
    //            //itemValues = new ValueContainer(stype, itemValues);


    //        }
    //    }
    //    else
    //        base.SpawnProperty(itemLayout, itemValues, item);

    //}


    public override void Initialize(LayoutElementsContainer layout)
    {


        var type = Values[0].GetType();
        var methods = type.GetMethods();
        //interfaceFields = type.GetFields()
        //    .Where(f => f.CustomAttributes.Any(c => c.AttributeType == typeof(ShowInterfaceAttribute)));

        base.Initialize(layout);

        foreach (var method in methods)
        {
            if (method.CustomAttributes.Any(c => c.AttributeType == typeof(EditorActionAttribute)))
            {
                var button = layout.Button(method.Name, Color.Green);
                button.Button.Clicked += () =>
                {
                    //Debug.Log("Clickado");

                    method.Invoke(Values[0], null);


                };
            }
        }



        //layout.Space(20);
        //var btn = layout.Button("Click me", Color.Green);
        //var clicked = interfaceFields.FirstOrDefault();
        //btn.Button.Clicked += () =>
        //{
        //    var search = new InterfaceSearch { fieldInfo = clicked };
        //    search.Init(clicked, Values[0], layout);
        //    search.Show();
        //    search.DrawClasses();
        //};



        //

        //layout.Space(20);


        //// Use Values[] to access the script or value being edited.
        //// It is an array, because custom editors can edit multiple selected scripts simultaneously.
        //button.Button.Clicked += () => Debug.Log("Button clicked! The speed is " + (IsSingleObject ? (Values[0] as MyScript).Speed : ""));
    }
    //
    //    private void ComboBox_SelectedIndexChanged(FlaxEditor.GUI.ComboBox obj)
    //    {

    //        Debug.Log("Alterado");
    //        var fieldRef = interfaceFields.FirstOrDefault(i => i.Name == "Effect");
    //        fieldRef.SetValue(Values[0], new SpeedEffect());
    //    }
}
