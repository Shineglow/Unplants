using System;
using Unplants.Scripts.General.Types;

// using Unplants.Scripts.General.Types;

namespace Unplants.General.Systems.Unplants.Scripts.General.Systems.DI
{
    public abstract class DIBindingBuilderAbstract
    {
        protected DIRecord _record;
        public event Action ChainEnded; 

        public DIRecord GetRecord()
        {
            var result = _record;
            _record = new();
            return result;
        }

        protected void EndChain()
        {
            ChainEnded?.Invoke();
        }
    }

    public struct DIRecord
    {
        public InitializableValue<Type> Binding;
        public InitializableValue<Type> To;
    }
}