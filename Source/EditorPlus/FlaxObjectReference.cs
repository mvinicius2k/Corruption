using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPlus;



public interface IInterfaceReference<TInterface>
{
    public TInterface Instance { get; }
    public Type ChoosedType { get; }
}

public class FlaxObjectReference<TInterface, TImplementor> : IInterfaceReference<TInterface> where TImplementor : FlaxEngine.Object, TInterface
{
    [ShowInEditor, Serialize]
    private TImplementor instance;
    public TInterface Instance => instance;

    public Type ChoosedType => typeof(TImplementor);
}

public class ObjectReference<TInterface, TImplementor> : IInterfaceReference<TInterface> where TImplementor : TInterface
{
    [ShowInEditor, Serialize]
    private TImplementor instance;
    public TInterface Instance => instance;
    public Type ChoosedType => typeof(TImplementor);

}



