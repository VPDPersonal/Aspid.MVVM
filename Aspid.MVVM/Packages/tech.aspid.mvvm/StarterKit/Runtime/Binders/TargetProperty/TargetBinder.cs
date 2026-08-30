using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget}"/> that binds a <typeparamref name="TProperty"/> target property
    /// using its get/set accessors and applies an optional <see cref="IConverter{TFrom, TTo}"/> to values in both
    /// binding directions; in <see cref="BindMode.OneWayToSource"/>, the current property value is converted before
    /// being sent back to the ViewModel.
    /// </summary>
    /// <typeparam name="TTarget">The type of the target object that exposes the bound property.</typeparam>
    /// <typeparam name="TProperty">The type of the property being bound.</typeparam>
    [Serializable]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public abstract class TargetBinder<TTarget, TProperty> : TargetBinder<TTarget>, IBinder<TProperty>, IReverseBinder<TProperty>
    {
        [Tooltip("Converts the value; runs in reverse only via ITwoWayConverter.")]
        [SerializeReference] private IConverter<TProperty?, TProperty?>? _converter;

        /// <summary>
        /// Gets or sets the target property that this binder reads from and writes to.
        /// </summary>
        protected abstract TProperty? Property { get; set; }

        /// <param name="target">The target object that owns the property.</param>
        /// <param name="converter">
        /// An optional converter applied to each value before it is stored in the target property.
        /// Pass <see langword="null"/> to use the value unchanged. Runs in reverse only if it implements
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </param>
        /// <param name="mode">The binding mode to use.</param>
        /// <remarks>
        /// For deserialization only: Unity builds a serialized instance without running a constructor's arguments and
        /// assigns the fields itself.
        /// </remarks>
        protected TargetBinder() { }

        protected TargetBinder(TTarget target, IConverter<TProperty?, TProperty?>? converter, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            _converter = converter;
        }

        /// <inheritdoc/>
        public event Action<TProperty?>? ValueChanged;

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
            WarnAboutOneWayConverter();

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
        /// Converts <paramref name="value"/> with the serialized converter before it is applied to the target property.
        /// Returns the value unchanged when no converter is set.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        protected virtual TProperty? GetConvertedValue(TProperty? value) =>
            _converter is not null ? _converter.Convert(value) : value;

        /// <summary>
        /// Converts <paramref name="value"/> before it is sent back to the ViewModel. Returns the value
        /// unchanged unless the serialized converter implements <see cref="ITwoWayConverter{TFrom, TTo}"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <remarks>
        /// Separate from <see cref="GetConvertedValue"/>: applying the forward conversion on the way back
        /// compounds it instead of undoing it.
        /// </remarks>
        protected virtual TProperty? GetConvertedBackValue(TProperty? value) =>
            _converter is ITwoWayConverter<TProperty?, TProperty?> twoWay ? twoWay.ConvertBack(value) : value;

        private void WarnAboutOneWayConverter()
        {
            if (Mode is not (BindMode.OneWayToSource or BindMode.TwoWay)) return;
            if (_converter is null or ITwoWayConverter<TProperty?, TProperty?>) return;

            this.LogWarning(
                problem: $"it is bound as {Mode} with {_converter.GetType().GetTypeName()}, which converts one way only",
                consequence: "Values sent back to the ViewModel are not converted.");
        }
    }
}
