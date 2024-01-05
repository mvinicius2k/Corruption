using EditorPlus;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEditor.GUI;
using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEditors;

[CustomEditor(typeof(ISerializebleInterface)), DefaultEditor]
public class SeriablizableInterfaceEditor : GenericEditor
{
    public override void Initialize(LayoutElementsContainer layout)
    {
        Debug.Log(".");
        var lb = layout.AddPropertyItem("Instance");
        var btn = layout.Button("Select Instance");
        lb.AddElement(btn);
    }
}
