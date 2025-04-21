using System;
using Unplants.Scripts.General.Types;

namespace Unplants.General.Systems.Unplants.Scripts.General.Systems.DI
{
    public abstract class DIBindingBuilderAbstract
    {
        protected DIRecord _record;

        public DIRecord GetRecord()
        {
            var result = _record;
            _record = new();
            return result;
        }
    }

    public struct DIRecord
    {
        public InitializableValue<Type> Binding;
        public InitializableValue<Type> To;
        public DIBindingParameters DIBindingParameters;
    }
    
    public struct DIBindingParameters
    {
        public Type typeOfInstance;
        public bool isSingle;
        public bool createInstanceOnBind;
    }
}