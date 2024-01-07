using FlaxEngine;
using System;

namespace EditorPlus;

/// <summary>
/// When a common object is choosed as implementation, this structure is assing to a <see cref="IImplementation{TInterface}"/>
/// </summary>
/// <typeparam name="TInterface">The interface</typeparam>
/// <typeparam name="TImplementor">Who implements</typeparam>
public class ObjectImplementation<TInterface, TImplementor> : IImplementation<TInterface> where TImplementor : TInterface
{
    [ShowInEditor, Serialize]
    private TImplementor instance;
    public TInterface Instance => instance;
    public Type ChoosedType => typeof(TImplementor);

}



