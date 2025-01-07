using System;
using UnityEngine;
using Aspid.MVVM.StarterKit.Utilities;

namespace Aspid.MVVM.StarterKit.Views.Initializers
{
    [Serializable]
    internal sealed class InitializeComponent<TInterface> : InitializeComponent
        where TInterface : class
    {
        public Resolve Resolve;
        public Component Mono;
        public ScriptableObject Scriptable;
        public SerializableMonoScript<TInterface> Type;
        
        [SerializeReferenceDropdown]
        [SerializeReference] public TInterface References;
    }
    
    public abstract class InitializeComponent
    {
        public enum Resolve
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