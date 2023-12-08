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
    public class IEffectEditor : GenericEditor
    {

        private IMutableScript mutable;
        //protected override void SpawnProperty(LayoutElementsContainer itemLayout, ValueContainer itemValues, ItemInfo item)
        //{

        //    if(Values != null && Values.GetType() == typeof(MutableScript<>))
        //    {


        //    }
        //    base.SpawnProperty(itemLayout, itemValues, item);
        //}
        public override void Initialize(LayoutElementsContainer layout)
        {
            mutable = (IMutableScript) Values[0];
            mutable?.Refresh();
            //
            var lb = layout.AddPropertyItem(mutable?.TypeImplementor?.Name ?? "Instance");
            var btn = layout.Button("Select Instance");
            lb.AddElement(btn);
            btn.Button.Clicked += OnBtnSearchClicked;
            if (mutable == null || mutable.TypeImplementor == null)
            {

                
                //btn.Button.Clicked += () =>
                //{
                //    var init = new MutableScript<IEffect>(typeof(SpeedEffect));
                //    init.Value = new SpeedEffect();
                //    SetValue(init);

                //};
                return;

            }
            else
            {
                
                //if (!mutable.Initialized)
                //{
                if (mutable.IsScript)
                {
                    var custom = layout.Custom<FlaxObjectRefPickerControl>("Referencia");
                    custom.CustomControl.Type = new ScriptType(mutable.TypeImplementor);
                    Debug.Log(mutable.RefHolder?.ToString() ?? "null");
                    custom.CustomControl.Value = mutable.RefHolder;
                    custom.CustomControl.ValueChanged += () =>
                    {
                        //Debug.Log("Alterado");

                        mutable.TrySetValue((IEffect)custom.CustomControl.Value);
                        SetValue(mutable);
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

            //if (Values != null && Values[0] != null)
            //{
            //    var right = Values[0].GetType();
            //    var isScript = typeof(MutableScript<IEffect>) == right;
            //    var values = Values[0] as MutableScript<IEffect>;
            //    var mut = Values[0] as Mutable<IEffect>;
            //    if (values.IsScript)
            //    {
            //        Debug.Log("Prop");
            //        var custom = layout.Custom<FlaxObjectRefPickerControl>("Referencia");
            //        custom.CustomControl.Type = new ScriptType(typeof(TailEffect));
            //        custom.CustomControl.ValueChanged += () =>
            //        {
            //            Debug.Log("Alterado");

            //            mut.Value = (IEffect)custom.CustomControl.Value;
            //            SetValue(mut);
            //        };
            //    }
            //    else
            //    {
            //        mut.Value = new SpeedEffect();
            //        Values[0] = mut;
            //        Debug.Log("ALterando obj");
            //        SetValue(mut);
            //        base.Initialize(layout);
            //    }

            //}
            //else

            base.Initialize(layout);

        }
        public Type DeclaredGeneric => Values.Type.Type.GenericTypeArguments[0];
        private void OnBtnSearchClicked()
        {

            var window = new InterfaceSearchWindow();
            window.Show();
            window.Init(DeclaredGeneric);
            window.OnTypeChoosed += OnChoosedType;

        }

        private void OnChoosedType(Type choosedType)
        {
            //Criando instância mutable
            if (mutable == null)
            {
                mutable = (IMutableScript)CreateInstance();
            }
            else
            {

                mutable?.Dispose();
            }

            
            Debug.Log($"Tipo {choosedType.Name}");

            
            mutable.TrySetImplementor(choosedType, true);

            SetValue(mutable);

            RebuildLayoutOnRefresh();
            
            //RebuildLayout(); //Por algum motivo, apaga tudo

            //var stype = new ScriptType(type);
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
