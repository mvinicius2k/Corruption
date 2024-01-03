

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


    public override void Initialize(LayoutElementsContainer layout)
    {


        var type = Values[0].GetType();
        var methods = type.GetMethods();

        base.Initialize(layout);

        foreach (var method in methods)
        {
            if (method.CustomAttributes.Any(c => c.AttributeType == typeof(EditorActionAttribute)))
            {
                var button = layout.Button(method.Name, Color.Green);
                button.Button.Clicked += () =>
                {

                    method.Invoke(Values[0], null);


                };
            }
        }



    }

}
