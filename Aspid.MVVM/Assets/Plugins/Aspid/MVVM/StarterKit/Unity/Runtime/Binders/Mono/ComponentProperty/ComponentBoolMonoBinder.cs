using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public abstract class ComponentBoolMonoBinder<TComponent> : ComponentMonoBinder<TComponent, bool>
        where TComponent : Component
    {
        [SerializeField] private bool _isInvert;
        
        protected override bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}