using System;
using UnityEngine;
using Aspid.UI.MVVM.StarterKit.Utilities;

namespace Aspid.UI.MVVM.StarterKit.Views.Initializers
{
    [Serializable]
    internal sealed class InitializeComponent<TInterface> : InitializeComponent
        where TInterface : class
    {
        public Resolve Resolve;
        public Component Mono;
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
        public SerializableMonoScript<TInterface> Type;
#endif
        
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] public TInterface References;
    }
    
    public abstract class InitializeComponent
    {
        public enum Resolve
        {
            Mono,
            References,
#if ASPID_UI_ZENJECT_INTEGRATION || ASPID_UI_VCONTAINER_INTEGRATION
            Di,
#endif
        }
    }
}