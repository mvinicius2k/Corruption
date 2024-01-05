using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("CustomEditors.CSharp")]
namespace EditorPlus;

public interface IInterfaceReference
{
    internal void TrySetInstance(object instance);
}
