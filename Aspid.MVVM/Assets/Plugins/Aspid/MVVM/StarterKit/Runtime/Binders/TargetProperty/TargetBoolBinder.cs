using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget,TProperty}"/> that adds optional value inversion.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the target <see langword="bool"/> property.</typeparam>
    [Serializable]
    public abstract class TargetBoolBinder<TTarget> : TargetBinder<TTarget, bool>
    {
        [Tooltip("When enabled, inverts the bound bool value before applying it.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of <see cref="TargetBoolBinder{TTarget}"/>.
        /// </summary>
        /// <param name="target">The target object whose boolean property is managed by this binder.</param>
        /// <param name="isInvert">
        /// When <see langword="true"/>, the ViewModel value is logically negated before being applied.
        /// </param>
        /// <param name="mode">The binding mode to use.</param>
        public TargetBoolBinder(TTarget target, bool isInvert, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            _isInvert = isInvert;
        }

        /// <inheritdoc/>
        protected override bool GetConvertedValue(bool value) =>
            _isInvert ? !value : value;
    }
}
