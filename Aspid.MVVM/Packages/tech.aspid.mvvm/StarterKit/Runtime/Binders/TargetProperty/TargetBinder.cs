using System;

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
}