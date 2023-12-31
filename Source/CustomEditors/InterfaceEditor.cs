using FlaxEditor.CustomEditors;
using FlaxEditor.CustomEditors.Editors;
using FlaxEditor.CustomEditors.GUI;
using FlaxEditor.Scripting;
using FlaxEngine;
using FlaxEngine.GUI;
using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlaxEditor.GUI.ItemsListContextMenu;

namespace CustomEditors
{
    [CustomEditor(typeof(MutableScript<>)), DefaultEditor]
    public class InterfaceEditor : GenericEditor
    {

        private IMutableScript mutable;

        public override void Initialize(LayoutElementsContainer layout)
        {
            mutable = (IMutableScript) Values[0];
            mutable?.Refresh();
            Debug.Log("init");
            var lb = layout.AddPropertyItem(mutable?.TypeImplementor?.Name ?? "Instance");
            var btn = layout.Button("Select Instance");
            lb.AddElement(btn);
            btn.Button.Clicked += OnBtnSearchClicked;
            if (mutable == null || mutable.TypeImplementor == null)
            {

               
                return;

            }
            else
            {
                //
                if (mutable.IsScript)
                {
                    var custom = layout.Custom<FlaxObjectRefPickerControl>("Referencia");
                    custom.CustomControl.Type = new ScriptType(mutable.TypeImplementor);
                    custom.CustomControl.Value = mutable.RefHolder;
                    custom.CustomControl.ValueChanged += () =>
                    {

                        if (custom.CustomControl.Value == null)
                            mutable.SetScriptAsNull();


                        mutable.TrySetValue(custom.CustomControl.Value);
                        SetValue(mutable);
                        RebuildLayoutOnRefresh();
                    };
                    return;

                    //}

                }
                else
                {


                    
                    base.Initialize(layout);

                    return;
                }
               

            }


        }
        public Type DeclaredGeneric => Values.Type.Type.GenericTypeArguments[0];
        private void OnBtnSearchClicked()
        {

            var window = new InterfaceSearchWindow();
            window.Show();
            window.Init(DeclaredGeneric);
            window.OnTypeChoosed += OnChoosedType;

        }
        protected override bool OnDirty(CustomEditor editor, object value, object token = null)
        {
            Debug.Log("Dirty");
            return base.OnDirty(editor, value, token);
            
        }

        protected override void OnUnDirty()
        {
            
            
            //Necessário para atualizar a UI quando a troca de valores não for entre scripts
            if (!mutable.IsScript)
            {
                if (ParentEditor != null)
                    ParentEditor.RebuildLayout();
                else
                    RebuildLayout();

            }
            base.OnUnDirty();
        }

        private void OnChoosedType(Type choosedType)
        {
            
            //Criando instância mutable
            if (mutable == null)
            {
                mutable = (IMutableScript)CreateInstance();
            }
            else if (mutable.TypeImplementor == choosedType)
            {
                return;
            }
            else
            {
                
                mutable?.Dispose();
            }

            
            Debug.Log($"Tipo {choosedType.Name}");

            
            var res = mutable.TrySetImplementor(choosedType, true);
            
            SetValue(mutable);
            RebuildLayoutOnRefresh();
            


            //RebuildLayout(); //Por algum motivo, apaga tudo

            
        }
        public override void Refresh()
        {
            base.Refresh();
            if (mutable == null)
                return;
            
        }
        private object CreateInstance()
        {
            var mutableType = typeof(MutableScript<>);
            var mutableInstance = mutableType.MakeGenericType(DeclaredGeneric);
            var createdInstance = Activator.CreateInstance(mutableInstance);
            return createdInstance;
        }
    }

}
