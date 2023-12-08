using FlaxEditor;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Elements;
using FlaxEditor.GUI;
using FlaxEditor.Scripting;
using FlaxEngine;
using FlaxEngine.GUI;
using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomEditors
{
    public record InterfaceOption
    {
        public Type Type;
        public ClickableLabel Label;

    }

    class InterfaceSearchWindow : CustomEditorWindow
    {
        //public FieldInfo fieldInfo;
        //private object target;
        private Type[] avaliableTypes;
        private LayoutElementsContainer layout;
        private List<InterfaceOption> options;
        private InterfaceOption selectedOption;
        private TextBoxElement textbox;
     

        public event Action<Type> OnTypeChoosed;





        private object EmptyInstance(Type type)
        {
            if (type.IsClass)
            {





            }
            return Activator.CreateInstance(type);

        }

    

        public void Init(Type interfaceType)
        {

            avaliableTypes = ReflectionUtils.GetTypesImplementations(interfaceType).Where(t => t != interfaceType).ToArray();
            options = new(avaliableTypes.Length);

            foreach (var type in avaliableTypes)
            {
                var lb = layout.ClickableLabel($"{type.Name} ({type.FullName})");
                options.Add(new InterfaceOption
                {
                    Label = lb.CustomControl,
                    Type = type,
                });
                lb.CustomControl.DoubleClick += Ok;
                lb.CustomControl.LeftClick += () => SelectOption(type);
            }






            var button = layout.Button("Select");
            button.Button.Clicked += Ok;
        }

        public void SelectOption(Type toSelect)
        {
            if (selectedOption != null)
            {
                selectedOption.Label.BackgroundColor = FlaxEngine.GUI.Style.Current.Background;
            }
            var found = options.FirstOrDefault(op => op.Type == toSelect);
            if (found != null)
            {
                selectedOption = found;
                selectedOption.Label.BackgroundColor = FlaxEngine.GUI.Style.Current.BackgroundSelected;
            }
            else
                Debug.LogWarning($"{toSelect.FullName} not found");
        }

        public void Ok()
        {
            if(selectedOption != null)
                OnTypeChoosed?.Invoke(selectedOption.Type);
            this.Window.Close();
        }

        public override void Initialize(LayoutElementsContainer layout)
        {
            this.layout = layout;
            
        }
      

    }
}
