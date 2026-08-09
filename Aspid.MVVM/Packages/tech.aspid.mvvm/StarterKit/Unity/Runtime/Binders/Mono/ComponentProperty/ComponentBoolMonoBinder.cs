using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent, bool}"/> that adds optional value inversion.
    /// </summary>
    /// <typeparam name="TComponent">The type of <see cref="Component"/> that exposes the target <see langword="bool"/> property.</typeparam>
    public abstract class ComponentBoolMonoBinder<TComponent> : ComponentMonoBinder<TComponent, bool>
        where TComponent : Component
    {
        [Tooltip("When enabled, inverts the bound bool value before applying it.")]
        [SerializeField] private bool _isInvert;

        /// <inheritdoc/>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the inversion.
        /// </remarks>
        protected override bool GetConvertedValue(bool value) =>
            Invert(base.GetConvertedValue(value));

        /// <inheritdoc/>
        /// <remarks>
        /// Inversion is its own inverse, so the reverse direction mirrors the forward one: undo the
        /// inversion first, then let the base undo whatever it applied.
        /// </remarks>
        protected override bool GetConvertedBackValue(bool value) =>
            base.GetConvertedBackValue(Invert(value));

        private bool Invert(bool value) => _isInvert ? !value : value;
    }
}