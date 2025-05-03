using Codice.CM.SEIDInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unplants.General.Systems.Assets.Unplants.Scripts.General.Systems.DI;

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

            ResolveCache cache = GetResolveCache(record);
            _resolveDictionary.Add(record.Binding, cache);
            if (record.DIBindingParameters.asInstance && !_instanceCaches.ContainsKey(cache.bindingParameters.typeOfInstance))
            {
                SetInitializable(cache.bindingParameters.typeOfInstance, cache, record.InstanceReference.Instance);
            }
            else if (cache.bindingParameters is { isSingle: true, createInstanceOnBind: true })
            {
                SetInitializable(cache.bindingParameters.typeOfInstance, cache, Resolve(cache.bindingParameters.typeOfInstance));
            }

            void SetInitializable(Type typeOfInstance, ResolveCache resolveCahce, object instance)
            {
                _instanceCaches[typeOfInstance] = instance;
                resolveCahce.initializable = instance as IInitializable;
            }
        }

        private static ResolveCache GetResolveCache(DIRecord record)
        {
            BindingFlags bf = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            ConstructorInfo constructorInfo = record.DIBindingParameters.typeOfInstance.GetConstructors(BindingFlags.Public).FirstOrDefault();
            var fields = record.DIBindingParameters.typeOfInstance.GetFields(bf).Where(i => i.IsDefined(typeof(BindingTargetAttribute)));
            var properties = record.DIBindingParameters.typeOfInstance.GetProperties(bf).Where(i => i.IsDefined(typeof(BindingTargetAttribute), false));
            var methods = record.DIBindingParameters.typeOfInstance.GetMethods(bf).Where(i => i.IsDefined(typeof(BindingTargetAttribute), false)).Select(r => (r, r.GetParameters()));

            ResolveCache cache = new ResolveCache()
            {
                constructorInfo = constructorInfo,
                parameters = constructorInfo?.GetParameters(),
                bindingParameters = record.DIBindingParameters,
                fields = fields,
                properties = properties,
                methods = methods,
            };
            return cache;
        }

        public T Resolve<T>() => (T)Resolve(typeof(T));
        
        private object Resolve(Type typeToResolve)
        {
            EndBinding();
            object result;
            if (_resolveDictionary.TryGetValue(typeToResolve, out var resolveCache))
            {
                // (not is single and not as instance) or not contains cached instance
                // inverted 'if' to support single method "return"
                if (!resolveCache.bindingParameters.isSingle && !resolveCache.bindingParameters.asInstance
                    || !_instanceCaches.TryGetValue(resolveCache.bindingParameters.typeOfInstance, out result))
                {
                    object[] resolvedParameters = GetResolvedParameters(resolveCache.parameters);

                    result = resolveCache.constructorInfo.Invoke(resolvedParameters);
                    if (resolveCache.bindingParameters.isSingle)
                    {
                        _instanceCaches[resolveCache.bindingParameters.typeOfInstance] = result;
                    }
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

            if((resolveCache != null || _resolveDictionary.TryGetValue(typeToResolve, out resolveCache)) && resolveCache.initializable != null)
            {
                resolveCache.initializable.Initialize();
            }

            ResolveAttributeMarked(result);

            return result;
        }

        private object[] GetResolvedParameters(ParameterInfo[] parameterInfos)
        {
            try
            {
                object[] resolvedParameters = new object[parameterInfos.Length];
                for (var i = 0; i < parameterInfos.Length; i++)
                {
                    resolvedParameters[i] = Resolve(parameterInfos[i].ParameterType);
                }

                return resolvedParameters;
            }
            catch (Exception e)
            { 
                return null; 
            }
        }

        public void ResolveAttributeMarked(object instance)
        {
            if(!_resolveDictionary.TryGetValue(instance.GetType(), out var cache))
            {
                DIRecord record = new()
                {
                    DIBindingParameters = new DIBindingParameters()
                    {
                        typeOfInstance = instance.GetType(),
                    }
                };
                cache = GetResolveCache(record);
            }
            foreach (var field in cache.fields)
            {
                field.SetValue(instance, Resolve(field.FieldType));
            }
            foreach (var property in cache.properties)
            {
                property.SetValue(instance, Resolve(property.PropertyType));
            }
            foreach (var (method, parameters) in cache.methods)
            {
                method.Invoke(instance, GetResolvedParameters(parameters));
            }
        }
    }

    public class ResolveCache
    {
        public DIBindingParameters bindingParameters;
        public ConstructorInfo constructorInfo;
        public ParameterInfo[] parameters;
        public IInitializable initializable;
        public IEnumerable<FieldInfo> fields;
        public IEnumerable<PropertyInfo> properties;
        internal IEnumerable<(MethodInfo r, ParameterInfo[])> methods;
    }

    public class CachedInstanceReference
    {
        public object Instance;
    }
}