using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget,TProperty}"/> that applies an optional <see cref="IConverter{TFrom, TTo}"/> to the bound value.
    /// </summary>
    /// <remarks>
    /// The reverse direction only converts when the configured converter implements
    /// <see cref="ITwoWayConverter{TFrom, TTo}"/>; otherwise the value is sent back unchanged, and a
    /// binder bound in a reverse mode reports the one-way converter once.
    /// </remarks>
    /// <typeparam name="TTarget">The type of the target object that exposes the bound property.</typeparam>
    /// <typeparam name="TProperty">The type of the property being bound.</typeparam>
    [Serializable]
    public abstract class TargetBinderWithConverter<TTarget, TProperty> : TargetBinder<TTarget, TProperty>
    {
        [Tooltip("Converts the value; runs in reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<TProperty?, TProperty?>? _converter;

        /// <param name="target">The target object that owns the property.</param>
        /// <param name="converter">
        /// An optional converter applied to each value before it is stored in the target property.
        /// Pass <see langword="null"/> to use the value unchanged. Runs in reverse only if it implements
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode to use.</param>
        protected TargetBinderWithConverter(TTarget target, IConverter<TProperty?, TProperty?>? converter, BindMode mode)
            : base(target, mode)
        {
            _converter = converter;
        }

        /// <inheritdoc/>
        protected override TProperty? GetConvertedValue(TProperty? value) =>
            _converter is not null ? _converter.Convert(value) : value;

        /// <inheritdoc/>
        protected override TProperty? GetConvertedBackValue(TProperty? value) =>
            _converter is ITwoWayConverter<TProperty?, TProperty?> twoWay ? twoWay.ConvertBack(value) : value;

        /// <inheritdoc/>
        protected override void OnBound()
        {
            WarnAboutOneWayConverter();
            base.OnBound();
        }

        private void WarnAboutOneWayConverter()
        {
            if (Mode is not (BindMode.OneWayToSource or BindMode.TwoWay)) return;
            if (_converter is null or ITwoWayConverter<TProperty?, TProperty?>) return;

            Debug.LogWarning($"{GetType().Name} is bound as {Mode} with {_converter.GetType().Name}, which converts one way only. Values sent back to the ViewModel are not converted.");
        }
    }
}
