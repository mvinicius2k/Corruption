using FlaxEditor;
using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Elements;
using FlaxEditor.GUI;
using FlaxEditor.Scripting;
using FlaxEngine;
using FlaxEngine.GUI;
using FlaxEngine.Utilities;
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
        public CustomElement<ClickableLabel> ClickableLabel;

    }

    class InterfaceSearchWindow : CustomEditorWindow
    {
        private Type[] avaliableTypes;
        private LayoutElementsContainer layout;
        private List<InterfaceOption> options;
        private InterfaceOption[] optionsFiltered;
        private InterfaceOption selectedOption;
        private TextBoxElement textbox;
        private CustomElementsContainer<Label> optionsContainer;



        public event Action<Type> OnTypeChoosed;


        public void Init(Type interfaceType)
        {
            optionsContainer = new();
            
            avaliableTypes = ReflectionUtils.GetTypesImplementations(interfaceType).Where(t => t != interfaceType).ToArray();

            if (avaliableTypes.Length == 0)
            {
                var lbNothing = new LabelElement();
                lbNothing.Label.Text.Value = $"No one implements {interfaceType.FullName}";
                optionsContainer.AddElement(lbNothing);
                return;
            }

            options = new(avaliableTypes.Length);

            foreach (var type in avaliableTypes)
            {
                var lbOption = new CustomElement<ClickableLabel>();
                lbOption.CustomControl.Text.Value = $"{type.Name} ({type.FullName})";
                lbOption.CustomControl.DoubleClick += Ok;
                lbOption.CustomControl.LeftClick += () => SelectOption(type);

                options.Add(new InterfaceOption
                {
                    ClickableLabel = lbOption,
                    Type = type,
                });
            }


            layout.AddElement(optionsContainer);


            var button = layout.Button("Select");
            button.Button.Clicked += Ok;

            DrawOptions();
        }

        public void SelectOption(Type toSelect)
        {
            if (selectedOption != null)
            {
                selectedOption.ClickableLabel.CustomControl.BackgroundColor = FlaxEngine.GUI.Style.Current.Background;
            }
            var found = options.FirstOrDefault(op => op.Type == toSelect);
            if (found != null)
            {
                selectedOption = found;
                selectedOption.ClickableLabel.CustomControl.BackgroundColor = FlaxEngine.GUI.Style.Current.BackgroundSelected;
            }
            else
                Debug.LogWarning($"{toSelect.FullName} not found");
        }

        public void Ok()
        {
            if (selectedOption != null)
                OnTypeChoosed?.Invoke(selectedOption.Type);
            this.Window.Close();
        }

        public override void Initialize(LayoutElementsContainer layout)
        {
            var txtSearch = layout.TextBox();
            txtSearch.TextBox.TooltipText = "Search...";
            txtSearch.TextBox.KeyUp += (_) => DrawOptions(txtSearch.Text);
            this.layout = layout;

        }

        private void DrawOptions(string startingWIth = "")
        {

            optionsContainer.ClearLayout();



            var toAdd = options.Where(lo => lo.ClickableLabel.CustomControl.Text.Value.StartsWith(startingWIth)).Select(interfaceOpt => interfaceOpt.ClickableLabel);
            foreach (var toOption in toAdd)
            {
                optionsContainer.AddElement(toOption);

            }
            

            

        }
    }
}
