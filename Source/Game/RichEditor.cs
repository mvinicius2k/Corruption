
//#if FLAX_EDITOR
//using FlaxEditor.CustomEditors;
//using FlaxEditor.CustomEditors.Editors;
//using FlaxEditor.CustomEditors.GUI;
//using FlaxEditor.Scripting;
//using FlaxEditor.Utilities;
//using FlaxEngine;
//using Game;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Editors;

//[CustomEditor(typeof(Script))]
//public class RichEditor : GenericEditor
//{
//    //private IEnumerable<FieldInfo> interfaceFields;



//    public override void Initialize(LayoutElementsContainer layout)
//    {


//        var type = Values[0].GetType();
//        var methods = type.GetMethods();
//        //interfaceFields = type.GetFields()
//        //    .Where(f => f.CustomAttributes.Any(c => c.AttributeType == typeof(ShowInterfaceAttribute)));

//        base.Initialize(layout);

//        foreach (var method in methods)
//        {
//            if (method.CustomAttributes.Any(c => c.AttributeType == typeof(EditorActionAttribute)))
//            {
//                var button = layout.Button(method.Name, Color.Green);
//                button.Button.Clicked += () => method.Invoke(Values[0], null);
//            }
//        }



//        //layout.Space(20);
//        //var btn = layout.Button("Click me", Color.Green);
//        //var clicked = interfaceFields.FirstOrDefault();
//        //btn.Button.Clicked += () =>
//        //{
//        //    var search = new InterfaceSearch { fieldInfo = clicked };
//        //    search.Init(clicked, Values[0], layout);
//        //    search.Show();
//        //    search.DrawClasses();
//        //};



//        //

//        //layout.Space(20);


//        //// Use Values[] to access the script or value being edited.
//        //// It is an array, because custom editors can edit multiple selected scripts simultaneously.
//        //button.Button.Clicked += () => Debug.Log("Button clicked! The speed is " + (IsSingleObject ? (Values[0] as MyScript).Speed : ""));
//    }
//    //
//    //    private void ComboBox_SelectedIndexChanged(FlaxEditor.GUI.ComboBox obj)
//    //    {

//    //        Debug.Log("Alterado");
//    //        var fieldRef = interfaceFields.FirstOrDefault(i => i.Name == "Effect");
//    //        fieldRef.SetValue(Values[0], new SpeedEffect());
//    //    }
//}
//#endif