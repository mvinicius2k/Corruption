

using EditorPlus;
using FlaxEditor;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEditor.CustomEditors.Elements;
using FlaxEditor.CustomEditors.GUI;
using FlaxEditor.GUI.ContextMenu;
using FlaxEditor.Scripting;
using FlaxEditor.Utilities;
using FlaxEngine;
using FlaxEngine.Utilities;
using Game;
using Newtonsoft.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Utils = FlaxEditor.Utilities.Utils;

namespace CustomEditors;


/// <summary>
/// Process top level of script and apply a custom editor. Generally used when type cannot be serialized.
/// </summary>
[CustomEditor(typeof(Script)), DefaultEditor]
public class CustomScriptEditor : GenericEditor
{


    protected override void SpawnProperty(LayoutElementsContainer itemLayout, ValueContainer itemValues, ItemInfo item)
    {
        
        base.SpawnProperty(itemLayout, itemValues, item);
    }

    public override void Initialize(LayoutElementsContainer layout)
    {


        var type = Values[0].GetType();
        base.Initialize(layout);

        //Drawing buttons bellow properties
        CreateButtonsEditorAction(layout, type);

    }

    /// <summary>
    /// Draw a button foreach method that uses <seealso cref="EditorActionAttribute"/>
    /// </summary>
    private void CreateButtonsEditorAction(LayoutElementsContainer layout, Type type)
    {
        var methods = type.GetMethods();
        foreach (var method in methods)
        {
            if (method.CustomAttributes.Any(c => c.AttributeType == typeof(EditorActionAttribute)))
            {
                var button = layout.Button(Utils.GetPropertyNameUI(method.Name));
                button.Button.Clicked += () => method.Invoke(Values[0], null);
               
            }

            
        }
    }

    
}
