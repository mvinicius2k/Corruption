using System;

namespace EditorPlus;

/// <summary>
/// Holds a implementation of <typeparamref name="TInterface"/> and shows in editor options to choose one implementation.
/// </summary>
/// <typeparam name="TInterface"></typeparam>
public interface IImplementation<TInterface>
{
    public TInterface Instance { get; }
    public Type ChoosedType { get; }
    
}



