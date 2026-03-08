using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base MonoBehaviour binder for binding a <see langword="bool"/> property on a Unity <see cref="UnityEngine.Component"/>.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value is sent back to the ViewModel.
    /// </summary>
    public abstract class ComponentBoolMonoBinder<TComponent> : ComponentMonoBinder<TComponent, bool>
        where TComponent : Component
    {
        [SerializeField] private bool _isInvert;
        
        protected override bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}