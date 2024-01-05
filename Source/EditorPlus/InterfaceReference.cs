using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace EditorPlus;

public class InterfaceReference<T> : IInterfaceReferenceOld
{
    //[NoSerialize, ShowInEditor]
    private T instance;
    [Serialize, ShowInEditor]
    private object objectHolder;
    [Serialize, ShowInEditor]
    private FlaxEngine.Object flaxObjectHolder;

    
    private Type choosedType;

    [Serialize, ShowInEditor]
    public T Instance
    {
        get => instance;
        set
        {
            ((IInterfaceReferenceOld)this).TrySetInstance(value);
        }
    }
    public object RawInstance => instance;
    public Type ArgType => typeof(T);

    Type IInterfaceReferenceOld.ChoosedType
    {
        get
        {
            choosedType ??= instance?.GetType();
            return choosedType;

        }
        set
        {
            choosedType = value;
        }
    }

    public void SetFlaxObjectInstance<TInstance>(TInstance newInstance) where TInstance : FlaxEngine.Object, T
    {
        flaxObjectHolder = newInstance;
        instance = newInstance;

        objectHolder = null;
        
    }
    public void SetObjectInstance<TInstance>(TInstance newInstance) where TInstance : T
    {
        instance = newInstance;
        objectHolder = newInstance;

        flaxObjectHolder = null;

    }


    bool IInterfaceReferenceOld.TrySetInstance<TNewInstance>(TNewInstance newInstance)
    {
        if (newInstance == null)
        {
            objectHolder = null;
            flaxObjectHolder = null;
            return true;
        }


        if (newInstance is not T cast)
        {
            return false;
        }

        instance = cast;
        if (instance is FlaxEngine.Object flaxInstance)
        {
            flaxObjectHolder = flaxInstance;
            //objectHolder = null;
        }
        else
        {
            //flaxObjectHolder = null;
            objectHolder = cast;
        }
        ((IInterfaceReferenceOld)this).ChoosedType = newInstance.GetType();

        return true;
    }
}

