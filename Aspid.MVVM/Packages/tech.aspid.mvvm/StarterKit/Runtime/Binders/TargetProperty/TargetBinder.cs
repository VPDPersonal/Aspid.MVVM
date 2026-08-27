using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget}"/> that binds a <typeparamref name="TProperty"/> target property using its get/set accessors.
    /// Supports <see cref="BindMode.OneWay"/> and <see cref="BindMode.OneTime"/>; in <see cref="BindMode.OneWayToSource"/>,
    /// the current property value is sent back to the ViewModel when binding is established.
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
        /// the <see cref="BindMode.OneWayToSource"/> initialization behavior. To change what is sent,
        /// override <see cref="SendInitialValueToSource"/> instead.
        /// </remarks>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                SendInitialValueToSource();
        }

        /// <summary>
        /// Sends the current <see cref="Property"/> value to the ViewModel on binding in
        /// <see cref="BindMode.OneWayToSource"/>. Override to broadcast through additional channels.
        /// </summary>
        /// <remarks>
        /// An override must route the value through <see cref="GetConvertedBackValue"/>: the forward
        /// conversion describes the ViewModel to View direction only.
        /// </remarks>
        protected virtual void SendInitialValueToSource() =>
            RaiseValueChanged();

        /// <summary>
        /// Raises <see cref="ValueChanged"/> with the current <see cref="Property"/> value,
        /// passing it through <see cref="GetConvertedBackValue"/> first.
        /// </summary>
        protected void RaiseValueChanged() =>
            RaiseValueChanged(Property);

        /// <summary>
        /// Raises <see cref="ValueChanged"/> with <paramref name="value"/>,
        /// passing it through <see cref="GetConvertedBackValue"/> first.
        /// </summary>
        /// <param name="value">The value to send to the ViewModel.</param>
        protected void RaiseValueChanged(TProperty? value) =>
            ValueChanged?.Invoke(GetConvertedBackValue(value));

        /// <summary>
        /// Converts <paramref name="value"/> before it is applied to the target property.
        /// Returns <paramref name="value"/> unchanged by default.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual TProperty? GetConvertedValue(TProperty? value) => value;

        /// <summary>
        /// Converts <paramref name="value"/> before it is sent back to the ViewModel.
        /// Returns <paramref name="value"/> unchanged by default.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>
        /// Separate from <see cref="GetConvertedValue"/> because the reverse direction is not the
        /// forward one: applying the forward conversion again compounds it, so a ×100 converter
        /// turns 0.75 into 7500 on the way back rather than into 0.75.
        /// </remarks>
        protected virtual TProperty? GetConvertedBackValue(TProperty? value) => value;
    }

    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget,TProperty}"/> that applies an optional <typeparamref name="TConverter"/> to the bound value.
    /// </summary>
    /// <remarks>
    /// The reverse direction only converts when the configured converter implements
    /// <see cref="ITwoWayConverter{TFrom, TTo}"/>; otherwise the value is sent back unchanged, and a
    /// binder bound in a reverse mode reports the one-way converter once.
    /// </remarks>
    /// <typeparam name="TTarget">The type of the target object that exposes the bound property.</typeparam>
    /// <typeparam name="TProperty">The type of the property being bound.</typeparam>
    /// <typeparam name="TConverter">The converter type used to transform the bound value before applying it.</typeparam>
    [Serializable]
    public abstract class TargetBinder<TTarget, TProperty, TConverter> : TargetBinder<TTarget, TProperty>
        where TConverter : IConverter<TProperty?, TProperty?>
    {
        [Tooltip("Converts the value; runs in reverse only via ITwoWayConverter.")]
        [SerializeReference] private TConverter? _converter;

        /// <param name="target">The target object that owns the property.</param>
        /// <param name="converter">
        /// An optional converter applied to each value before it is stored in the target property.
        /// Pass <see langword="null"/> to use the value unchanged. Runs in reverse only if it implements
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>.
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