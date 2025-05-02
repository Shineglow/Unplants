using System;
using System.Collections.Generic;

namespace Unplants.Scripts.General.Systems.DI
{
    public sealed class DIBindingBuilderMultipleBindings<T> : DIBindingBuilderMultipleBindingsAbstract, IDIBindingBuilderMultipleBindings<T>, ICreateOnBind<T>
    {
        private Type _cachedType;
        private Type[] _interfaces;
        private bool _bindSelf;
        private DIRecord _recordPrototype;

        public DIBindingBuilderMultipleBindings(bool bindSelf)
        {
            _cachedType = typeof(T);
            _interfaces = _cachedType.GetInterfaces();
            _bindSelf = bindSelf;
            records = new DIRecord[_interfaces.Length + (_bindSelf ? 1 : 0)];
            _recordPrototype = new DIRecord()
            {
                DIBindingParameters = new DIBindingParameters()
                {
                    typeOfInstance = _cachedType,
                },
                InstanceReference = new(),
            };
        }

        public override IReadOnlyList<DIRecord> GetRecords()
        {
            Array.Fill(records, _recordPrototype);
            for (int i = 0; i < _interfaces.Length; i++)
            {
                records[i].Binding = _interfaces[i];
            }
            if(_bindSelf)
            {
                records[^1].Binding = _cachedType;
            }
            return base.GetRecords();
        }

        public void AsInstance<T1>(T1 instance) where T1 : T
        {
            _recordPrototype.DIBindingParameters.asInstance = true;
            _recordPrototype.InstanceReference.Instance = instance;
        }

        public ICreateOnBind<T> AsSingle()
        {
            _recordPrototype.DIBindingParameters.isSingle = true;
            return this;
        }

        public void CreateOnBind()
        {
            _recordPrototype.DIBindingParameters.createInstanceOnBind = true;
        }
    }
}
