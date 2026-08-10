using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{T1, T2}">ComponentMonoBinder&lt;TComponent, bool&gt;</see> that adds optional value inversion.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target <see langword="bool"/> property.</typeparam>
    public abstract class ComponentBoolMonoBinder<TComponent> : ComponentMonoBinder<TComponent, bool>
        where TComponent : Component
    {
        [Tooltip("When enabled, inverts the bound bool value before applying it.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Inverts <paramref name="value"/> when the Invert option is set; otherwise returns it unchanged.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        /// <returns>The value to apply to the component.</returns>
        /// <remarks>
        /// Documented rather than inherited: the base says it returns the value unchanged, which stops being
        /// true the moment the option is enabled.
        /// </remarks>
        protected override bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}