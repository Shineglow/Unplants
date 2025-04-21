using System;
using System.Collections.Generic;
using System.Linq;

namespace Unplants.General.Systems.Unplants.Scripts.General.Systems.DI
{
    public class DIContainerBase
    {
        private Dictionary<Type, Type> _resolveDictionary = new(); // <binded, to>
        private DIBindingBuilderAbstract _bindingBuilderAbstract;
        
        public DIBindingBuilder<T> AddBinding<T>()
        {
            AddBinding();
            DIBindingBuilder<T> bindingBuilder = new DIBindingBuilder<T>();
            _bindingBuilderAbstract = bindingBuilder;
            _bindingBuilderAbstract.ChainEnded += AddBinding;
            return bindingBuilder;
        }

        private void AddBinding()
        {
            if (_bindingBuilderAbstract == null) return;
            _bindingBuilderAbstract.ChainEnded -= AddBinding;
            var record = _bindingBuilderAbstract.GetRecord();
            if (!record.To.WasInitialized)
            {
                record.To.Value = record.Binding;
            }
            _resolveDictionary.Add(record.Binding, record.To);
            _bindingBuilderAbstract = null;
        }
        public T Resolve<T>() => (T)Resolve(typeof(T));
        private object Resolve(Type typeToResolve)
        {
            AddBinding();
            Type type = _resolveDictionary[typeToResolve];
            var constructor = type.GetConstructors().First(i => i.IsPublic);
            var parameters = constructor.GetParameters();
            var resolvedParameters = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                resolvedParameters[i] = Resolve(parameters[i].ParameterType);
            }
            var result = constructor.Invoke(resolvedParameters);
            return result;
        }
    }
}