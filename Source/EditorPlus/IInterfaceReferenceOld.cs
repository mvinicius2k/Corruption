using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("CustomEditors.CSharp")]
namespace EditorPlus;

public interface IInterfaceReferenceOld
{
    /// <summary>
    /// Instance as object, being Flax Object or common object
    /// </summary>
    public object RawInstance { get; }
    /// <summary>
    /// Verify automatically if <paramref name="newInstance"/> is FlaxObject or common object then set the instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="newInstance"></param>
    /// <returns><see langword="true"/> if sucess, <see langword="false"/> otherwise</returns>
    public bool TrySetInstance<T>(T newInstance);
    public Type ArgType { get; }
    internal Type ChoosedType { get; set; }//
}

