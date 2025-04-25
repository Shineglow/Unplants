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
            EndBinding();
            DIBindingBuilder<T> bindingBuilder = new DIBindingBuilder<T>();
            _bindingBuilderAbstract = bindingBuilder;
            return bindingBuilder;
        }

        private void EndBinding()
        {
            if (_bindingBuilderAbstract == null) return;
            var record = _bindingBuilderAbstract.GetRecord();
            _bindingBuilderAbstract = null;
            if (record.DIBindingParameters.typeOfInstance == null)
            {
                record.DIBindingParameters.typeOfInstance = record.Binding;
            }

            ConstructorInfo constructorInfo = record.DIBindingParameters.typeOfInstance.GetConstructors().First(i => i.IsPublic);
            ResolveCache cache = new ResolveCache()
            {
                constructorInfo = constructorInfo,
                parameters = constructorInfo.GetParameters(),
                bindingParameters = record.DIBindingParameters,
                cachedInstance = record.DIBindingParameters.asInstance ? record.Instance : null,
            };

            _resolveDictionary.Add(record.Binding, cache);
            if (cache.bindingParameters is { isSingle: true, createInstanceOnBind: true })
            {
                cache.cachedInstance = Resolve(cache.bindingParameters.typeOfInstance);
                _resolveDictionary[record.Binding] = cache;
            }
        }
        
        public T Resolve<T>() => (T)Resolve(typeof(T));
        
        private object Resolve(Type typeToResolve)
        {
            EndBinding();
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