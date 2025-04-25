using System;
using Unplants.Scripts.General.Types;

namespace Unplants.Scripts.General.Systems.DI
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
        public Type Binding;
        public Type To;
        public DIBindingParameters DIBindingParameters;
        public object Instance;
    }
    
    public struct DIBindingParameters
    {
        public Type typeOfInstance;
        public bool isSingle;
        public bool createInstanceOnBind;
        public bool asInstance;
    }
}