using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget}"/> that binds a <typeparamref name="TProperty"/> target property using its get/set accessors.
    /// Supports <see cref="BindMode.OneWay">OneWay</see> and <see cref="BindMode.OneTime">OneTime</see> for one-way binding.
    /// Supports <see cref="BindMode.OneWayToSource">OneWayToSource</see> for reverse binding: when binding is established,
    /// the current property value is sent back to the ViewModel.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the bound property.</typeparam>
    /// <typeparam name="TProperty">The type of the property being bound.</typeparam>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract class TargetBinder<TTarget, TProperty> : TargetBinder<TTarget>, IBinder<TProperty>, IReverseBinder<TProperty>
    {
        /// <inheritdoc/>
        public event Action<TProperty?>? ValueChanged;

        /// <summary>
        /// Gets or sets the target property that this binder reads from and writes to.
        /// </summary>
        protected abstract TProperty? Property { get; set; }

        /// <summary>
        /// Initializes a new instance of TargetBinder/>.
        /// </summary>
        /// <param name="target">The target object that owns the property.</param>
        /// <param name="mode">The binding mode to use.</param>
        protected TargetBinder(TTarget target, BindMode mode)
            : base(target, mode) { }

        /// <summary>
        /// Sets the bound property to <paramref name="value"/>, passing it through <see cref="GetConvertedValue"/> first.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        public void SetValue(TProperty? value) =>
            Property = GetConvertedValue(value);

        /// <summary>
        /// Called after binding is established.
        /// Sends the initial property value to the ViewModel when in <see cref="BindMode.OneWayToSource"/> mode.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.OnBound()</c> to preserve
        /// the <see cref="BindMode.OneWayToSource"/> initialization behavior.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                RaiseValueChanged();
        }
        
        /// <summary>
        /// Raises <see cref="ValueChanged"/> with the current <see cref="Property"/> value,
        /// passing it through <see cref="GetConvertedValue"/> first.
        /// </summary>
        protected void RaiseValueChanged() =>
            RaiseValueChanged(Property);

        /// <summary>
        /// Raises <see cref="ValueChanged"/> with <paramref name="value"/>,
        /// passing it through <see cref="GetConvertedValue"/> first.
        /// </summary>
        /// <param name="value">The value to send to the ViewModel.</param>
        protected void RaiseValueChanged(TProperty? value) =>
            ValueChanged?.Invoke(GetConvertedValue(value));

        /// <summary>
        /// Converts <paramref name="value"/> before it is applied to the target property or sent back to the ViewModel.
        /// Returns <paramref name="value"/> unchanged by default.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual TProperty? GetConvertedValue(TProperty? value) => value;
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget,TProperty}"/> that applies an optional <typeparamref name="TConverter"/> to values in both binding directions.
    /// Supports <see cref="BindMode.OneWay">OneWay</see> and <see cref="BindMode.OneTime">OneTime</see>: the converter transforms the incoming value before setting it on the target property.
    /// Supports <see cref="BindMode.OneWayToSource">OneWayToSource</see> for reverse binding: when binding is established,
    /// the current property value is converted and sent back to the ViewModel.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the bound property.</typeparam>
    /// <typeparam name="TProperty">The type of the property being bound.</typeparam>
    /// <typeparam name="TConverter">The converter type used to transform the bound value before applying it.</typeparam>
    public abstract class TargetBinder<TTarget, TProperty, TConverter> : TargetBinder<TTarget, TProperty>
        where TConverter : IConverter<TProperty?, TProperty?>
    {
        [Tooltip("Optional converter applied to the value in both binding directions.")]
        [SerializeReferenceDropdown]
        [SerializeReference] private TConverter? _converter;

        /// <summary>
        /// Initializes a new instance of TargetBinder/>.
        /// </summary>
        /// <param name="target">The target object that owns the property.</param>
        /// <param name="converter">
        /// An optional converter applied to each value before it is stored in the target property.
        /// Pass <see langword="null"/> to use the value unchanged.
        /// </param>
        /// <param name="mode">The binding mode to use.</param>
        protected TargetBinder(TTarget target, TConverter? converter, BindMode mode)
            : base(target, mode)
        {
            _converter = converter;
        }

        /// <inheritdoc/>
        protected override TProperty? GetConvertedValue(TProperty? value) =>
            _converter is not null ? _converter.Convert(value) : value;
    }
}