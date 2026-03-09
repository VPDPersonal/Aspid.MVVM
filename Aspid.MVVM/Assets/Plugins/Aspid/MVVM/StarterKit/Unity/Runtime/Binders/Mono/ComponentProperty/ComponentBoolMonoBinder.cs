using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent, TProperty}"/> that adds optional value inversion.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target <see langword="bool"/> property.</typeparam>
    public abstract class ComponentBoolMonoBinder<TComponent> : ComponentMonoBinder<TComponent, bool>
        where TComponent : Component
    {
        [Tooltip("When enabled, inverts the bound bool value before applying it.")]
        [SerializeField] private bool _isInvert;

        /// <inheritdoc/>
        protected override bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}