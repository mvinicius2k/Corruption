using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPlus;



public interface IImplementation<TInterface>
{
    public TInterface Instance { get; }
    public Type ChoosedType { get; }
    
}

public class FlaxObjectImplementation<TInterface, TImplementor> : IImplementation<TInterface> where TImplementor : FlaxEngine.Object, TInterface
{
    [ShowInEditor, Serialize]
    private TImplementor instance;
    public TInterface Instance => instance;

    public Type ChoosedType => typeof(TImplementor);
}

public class ObjectImplementation<TInterface, TImplementor> : IImplementation<TInterface> where TImplementor : TInterface
{
    [ShowInEditor, Serialize]
    private TImplementor instance;
    public TInterface Instance => instance;
    public Type ChoosedType => typeof(TImplementor);

}



