using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Unplants.Scripts.General.Systems.DI
{
    public class DIContainerBase
    {
        private readonly DIContainerBase _parentContainer;
        
        private readonly Dictionary<Type, ResolveCache> _resolveDictionary = new(); // <binded, to>
        private DIBindingBuilderAbstract _bindingBuilderAbstract;

        public DIContainerBase() : this(null){}
        
        public DIContainerBase(DIContainerBase parentContainer)
        {
            _parentContainer = parentContainer;
        }
        
        public IDIBindingBuilder<T> AddBinding<T>()
        {
            AddBinding();
            DIBindingBuilder<T> bindingBuilder = new DIBindingBuilder<T>();
            _bindingBuilderAbstract = bindingBuilder;
            return bindingBuilder;
        }

        private void AddBinding()
        {
            if (_bindingBuilderAbstract == null) return;
            var record = _bindingBuilderAbstract.GetRecord();
            if (record.To != null)
            {
                record.To = record.Binding;
                record.DIBindingParameters.typeOfInstance = record.Binding;
            }
            ResolveCache cache = new ResolveCache()
            {
                constructorInfo = record.To.GetConstructors().First(i => i.IsPublic),
                parameters = record.To.GetConstructors().First(i => i.IsPublic).GetParameters(),
                bindingParameters = record.DIBindingParameters,
            };
            _resolveDictionary.Add(record.Binding, cache);
            _bindingBuilderAbstract = null;
            if (cache.bindingParameters is { isSingle: true, createInstanceOnBind: true })
            {
                cache.cachedInstance = Resolve(cache.bindingParameters.typeOfInstance);
                _resolveDictionary[record.Binding] = cache;
            }
        }
        
        public T Resolve<T>() => (T)Resolve(typeof(T));
        
        private object Resolve(Type typeToResolve)
        {
            AddBinding();
            object result;
            if (_resolveDictionary.TryGetValue(typeToResolve, out var resolveCache))
            {
                if (resolveCache.bindingParameters.isSingle && resolveCache.cachedInstance != null)
                {
                    return resolveCache.cachedInstance;
                }
                
                object[] resolvedParameters = new object[resolveCache.parameters.Length];
                for (var i = 0; i < resolveCache.parameters.Length; i++)
                {
                    resolvedParameters[i] = Resolve(resolveCache.parameters[i].ParameterType);
                }
                result = resolveCache.constructorInfo.Invoke(resolvedParameters);
                
                if (resolveCache.bindingParameters.isSingle)
                {
                    resolveCache.cachedInstance = result;
                    _resolveDictionary[typeToResolve] = resolveCache;
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
        public object cachedInstance;
    }
}