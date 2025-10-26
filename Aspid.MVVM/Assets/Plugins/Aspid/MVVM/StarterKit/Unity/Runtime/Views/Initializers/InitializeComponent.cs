using System;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class InitializeComponent<TInterface> : InitializeComponent
        where TInterface : class
    {
        public SerializableMonoScript<TInterface> Type;
        
        [SerializeReferenceDropdown]
        [SerializeReference] public TInterface References;
        
        public override void Validate()
        {
            switch (Resolve)
            {
                case ResolveType.Mono:
                    Type = null;
                    References = null;
                    Scriptable = null;
                    break;
                
                case ResolveType.References:
                    Type = null;
                    Mono = null;
                    Scriptable = null;
                    break;
                
                case ResolveType.ScriptableObject:
                    Type = null;
                    Mono = null;
                    References = null;
                    break;
                
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
                case ResolveType.Di:
                    Mono = null;
                    References = null;
                    Scriptable = null;
                    break;
#endif
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    [Serializable]
    public abstract class InitializeComponent
    {
        public ResolveType Resolve;
        
        public Component Mono;
        public ScriptableObject Scriptable;

        public abstract void Validate();
        
        public enum ResolveType
        {
            Mono,
            References,
            ScriptableObject,
#if ASPID_MVVM_ZENJECT_INTEGRATION || ASPID_MVVM_VCONTAINER_INTEGRATION
            Di,
#endif
        }
    }
}