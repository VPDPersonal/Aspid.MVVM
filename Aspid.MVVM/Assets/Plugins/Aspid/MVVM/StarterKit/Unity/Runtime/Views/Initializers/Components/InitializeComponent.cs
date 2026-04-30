using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public abstract class InitializeComponent<T>
        where T : class
    {
        [SerializeField] private ResolveType _resolve;
        
        [SerializeField] private Component _mono;
        [SerializeField] private ScriptableObject _scriptableObject;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private T _reference;
        
#if ASPID_MVVM_ZENJECT_INTEGRATION
        [field: NonSerialized]
        internal Zenject.DiContainer ZenjectContainer { get; set; }
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
        [field: NonSerialized]
        internal VContainer.IObjectResolver VContainerContainer { get; set; }
#endif

        protected ResolveType Resolve => _resolve;

        public T GetComponent()
        {
            switch (_resolve)
            {
                case ResolveType.Mono: return _mono as T;
                case ResolveType.References: return _reference;
                case ResolveType.ScriptableObject: return _scriptableObject as T;
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case ResolveType.Di:
                    var type = GetTypeForDi();
#if ASPID_MVVM_ZENJECT_INTEGRATION
                    var zenjectResult = ZenjectContainer?.TryResolve(type);
                    if (zenjectResult is T specificZenjectResult) return specificZenjectResult;
#endif
#if ASPID_MVVM_VCONTAINER_INTEGRATION
                    object specificVContainerResult = null;
                    if (VContainerContainer?.TryResolve(type, out specificVContainerResult) ?? false)
                        return specificVContainerResult as T;
#endif
                    throw new Exception("Unknown initialize component type: " + type);
#endif
                default: throw new ArgumentOutOfRangeException();
            }
        }

        public virtual void Validate()
        {
            switch (Resolve)
            {
                case ResolveType.Mono:
                    _reference = null;
                    _scriptableObject = null;
                    break;
                
                case ResolveType.References:
                    _mono = null;
                    _scriptableObject = null;
                    break;
                
                case ResolveType.ScriptableObject:
                    _mono = null;
                    _reference = null;
                    break;
                
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case ResolveType.Di:
                    _mono = null;
                    _reference = null;
                    _scriptableObject = null;
                    break;
#endif
                default: throw new ArgumentOutOfRangeException();
            }
        }

        protected abstract Type GetTypeForDi();
    }
}