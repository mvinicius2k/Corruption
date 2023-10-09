using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEditor.CustomEditors.GUI;
using FlaxEditor.Utilities;
using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game;

[CustomEditor(typeof(Script))]
public class RichEditor : GenericEditor
{
    public override void Initialize(LayoutElementsContainer layout)
    {
        var type = Values[0].GetType();
        var methods = type.GetMethods();
        base.Initialize(layout);

        foreach (var method in methods)
        {
            if (method.CustomAttributes.Any(c => c.AttributeType == typeof(EditorActionAttribute)))
            {
                //var paramsArray = method.GetParameters();
                
                
                
                var button = layout.Button(method.Name, Color.Green);
                button.Button.Clicked += () => method.Invoke(Values[0], null);
            }
        }


        //

        //layout.Space(20);
        

        //// Use Values[] to access the script or value being edited.
        //// It is an array, because custom editors can edit multiple selected scripts simultaneously.
        //button.Button.Clicked += () => Debug.Log("Button clicked! The speed is " + (IsSingleObject ? (Values[0] as MyScript).Speed : ""));
    }
}
