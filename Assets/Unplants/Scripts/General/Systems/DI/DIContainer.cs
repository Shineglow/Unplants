using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Unplants.Scripts.General.Systems.DI
{
    public class DIContainer
    {
        private readonly DIContainer _parentContainer;
        
        private readonly Dictionary<Type, ResolveCache> _resolveDictionary = new(); // <binded, to>
        private readonly Dictionary<Type, object> _instanceCaches = new(); // <type, instance>
        private DIBindingBuilderAbstract _bindingBuilderAbstract;
        private DIBindingBuilderMultipleBindingsAbstract _bindingBuilderMultiAbstract;

        public DIContainer() : this(null){}
        
        public DIContainer(DIContainer parentContainer)
        {
            _parentContainer = parentContainer;
        }
        
        public IDIBindingBuilder<T> Bind<T>()
        {
            EndBinding();
            DIBindingBuilder<T> bindingBuilder = new DIBindingBuilder<T>();
            _bindingBuilderAbstract = bindingBuilder;
            return bindingBuilder;
        }

        public IDIBindingBuilderMultipleBindings<T> BindToInterfacesAndSelf<T>() 
        {
            EndBinding();
            DIBindingBuilderMultipleBindings<T> bindingBuilder = new DIBindingBuilderMultipleBindings<T>(true);
            _bindingBuilderMultiAbstract = bindingBuilder;
            return bindingBuilder;
        }

        private void EndBinding()
        {
            if (_bindingBuilderAbstract != null)
            {
                EndSingleBinding();
            }
            else if (_bindingBuilderMultiAbstract != null)
            {
                EndMultipleBindings();
            }
        }

        private void EndMultipleBindings()
        {
            if (_bindingBuilderMultiAbstract == null)
                return;
            var records = _bindingBuilderMultiAbstract.GetRecords();
            _bindingBuilderMultiAbstract = null;
            foreach (var record in records)
            {
                RecordToBinding(record);
            }
        }

        private void EndSingleBinding()
        {
            if (_bindingBuilderAbstract == null)
                return;
            var record = _bindingBuilderAbstract.GetRecord();
            _bindingBuilderAbstract = null;
            RecordToBinding(record);
        }

        private void RecordToBinding(DIRecord record)
        {
            record.DIBindingParameters.typeOfInstance ??= record.Binding;

            ConstructorInfo constructorInfo = record.DIBindingParameters.typeOfInstance.GetConstructors().First(i => i.IsPublic);
            ResolveCache cache = new ResolveCache()
            {
                constructorInfo = constructorInfo,
                parameters = constructorInfo.GetParameters(),
                bindingParameters = record.DIBindingParameters,
            };
            _resolveDictionary.Add(record.Binding, cache);
            if (record.DIBindingParameters.asInstance && !_instanceCaches.ContainsKey(cache.bindingParameters.typeOfInstance))
            {
                _instanceCaches[cache.bindingParameters.typeOfInstance] = record.InstanceReference.Instance;
            }
            else if (cache.bindingParameters is { isSingle: true, createInstanceOnBind: true })
            {
                _instanceCaches[cache.bindingParameters.typeOfInstance] = Resolve(cache.bindingParameters.typeOfInstance);
            }
        }

        public T Resolve<T>() => (T)Resolve(typeof(T));
        
        private object Resolve(Type typeToResolve)
        {
            EndBinding();
            object result;
            if (_resolveDictionary.TryGetValue(typeToResolve, out var resolveCache))
            {
                if ((resolveCache.bindingParameters.isSingle || resolveCache.bindingParameters.asInstance) 
                    && _instanceCaches.TryGetValue(resolveCache.bindingParameters.typeOfInstance, out result))
                {
                    return result;
                }
                
                object[] resolvedParameters = new object[resolveCache.parameters.Length];
                for (var i = 0; i < resolveCache.parameters.Length; i++)
                {
                    resolvedParameters[i] = Resolve(resolveCache.parameters[i].ParameterType);
                }
                
                result = resolveCache.constructorInfo.Invoke(resolvedParameters);
                if (resolveCache.bindingParameters.isSingle)
                {
                    _instanceCaches[resolveCache.bindingParameters.typeOfInstance] = result;
                }
            }
            else if(_parentContainer != null)
            {
                result = _parentContainer.Resolve(typeToResolve);
            }
            else
            {
                throw new ArgumentException($"The container does not contain a bindings for the type {typeToResolve.FullName}");
            }
            return result;
        }
    }

    public struct ResolveCache
    {
        public DIBindingParameters bindingParameters;
        public ConstructorInfo constructorInfo;
        public ParameterInfo[] parameters;
    }

    public class CachedInstanceReference
    {
        public object Instance;
    }
}