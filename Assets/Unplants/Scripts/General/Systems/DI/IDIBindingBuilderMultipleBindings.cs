using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unplants.Scripts.General.Systems.DI
{
    public interface IDIBindingBuilderMultipleBindings<T> : IBindAsSingle<T>, IBindAsInstance<T> { }
}
