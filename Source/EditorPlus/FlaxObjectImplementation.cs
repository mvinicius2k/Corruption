﻿using FlaxEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPlus;

/// <summary>
/// When a Flax Object is choosed as implementation, this structure is assing to a <see cref="IImplementation{TInterface}"/>
/// </summary>
/// <typeparam name="TInterface">The interface</typeparam>
/// <typeparam name="TImplementor">Who implements</typeparam>
public class FlaxObjectImplementation<TInterface, TImplementor> : IImplementation<TInterface> where TImplementor : FlaxEngine.Object, TInterface
{
    [ShowInEditor, Serialize]
    private TImplementor instance;
    public TInterface Instance => instance;

    public Type ChoosedType => typeof(TImplementor);
}



