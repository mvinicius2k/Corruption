using FlaxEngine;
using System;
using System.ComponentModel;

namespace Game;


public interface IMutableScript
{
    public bool IsScript { get; }
    public bool HasValue { get; }
    public Type TypeImplementor { get; }
    public bool TrySetValue<T>(T value);
    public bool TrySetImplementor(Type type, bool activate = false);


    public FlaxEngine.Object RefHolder { get; }
    public object ObjectHolder { get; }
    public void Refresh();
    public void Dispose();
    public void SetScriptAsNull();
}


public struct MutableScript<T> : IMutableScript
{
    [EditorDisplay(name: "Value"), ShowInEditor, Serialize]
    private object objectHolder;
    [Serialize, DefaultValue(null)]
    private FlaxEngine.Object refHolder;
    private Type typeImplementor;

    
    public bool IsScript => typeof(FlaxEngine.Object).IsAssignableFrom(typeImplementor);
    public FlaxEngine.Object RefHolder => refHolder;
    public Type TypeImplementor
    {
        get => typeImplementor;
        

    }



    public bool TrySetImplementor(Type type, bool activate)
    {
        if (typeof(T).IsAssignableFrom(type))
        {
            typeImplementor = type;
            if (!IsScript && activate)
                Value = (T)Activator.CreateInstance(typeImplementor);
            return true;
        }
        else
            return false;


    }

    public bool TrySetValue<T1>(T1 value)
    {
        
        if (value is T cast)
        {
            Value = cast;
            return true;
        }
        else
            return false;
    }

    public void Refresh()
    {
        if(typeImplementor == null)
        {
            typeImplementor = refHolder?.GetType() ?? objectHolder?.GetType();
        }
    }

    public void Dispose()
    {
        objectHolder = null;
        typeImplementor = null;
        refHolder = null;
    }

    public void SetScriptAsNull()
        => refHolder = null;

    [NoSerialize]
    public T Value
    {
        get
        {
            if (refHolder != null && refHolder is T scriptCast)
                return scriptCast;
            if (objectHolder != null && objectHolder is T objectCast)
                return objectCast;
            
            return default;
        }
        set
        {
            if (value is FlaxEngine.Object cast)
                refHolder = cast;
            else
                objectHolder = value;

        }
    }

    public bool HasValue => objectHolder != null || refHolder != null;

    public object ObjectHolder { get => objectHolder; }






}
