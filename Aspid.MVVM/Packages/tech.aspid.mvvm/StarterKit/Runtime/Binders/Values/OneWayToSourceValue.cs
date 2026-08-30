using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TwoWayValue{T}"/> pre-configured with <see cref="BindMode.OneWayToSource"/>,
    /// propagating the current value from the View back to the ViewModel on binding.
    /// </summary>
    /// <typeparam name="T">The type of the bindable value.</typeparam>
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class OneWayToSourceValue<T> : TwoWayValue<T>
    {
        public OneWayToSourceValue()
            : base(BindMode.OneWayToSource) { }

        /// <param name="value">The initial value.</param>
        public OneWayToSourceValue(T? value)
            : base(value, BindMode.OneWayToSource) { }

        /// <remarks>
        /// Only the forward conversion is unreachable here: it runs in <see cref="IBinder{T}.SetValue"/>, and
        /// <see cref="BindMode.OneWayToSource"/> has no ViewModel → View path. A one-way converter therefore
        /// never runs.
        /// </remarks>
        /// <param name="value">The initial value.</param>
        /// <param name="converter">
        /// The converter applied to each value on its way to the ViewModel; only an
        /// <see cref="ITwoWayConverter{TFrom, TTo}"/> takes effect, through its reverse conversion.
        /// </param>
        public OneWayToSourceValue(T? value, IConverter<T?, T?>? converter)
            : base(value, converter, BindMode.OneWayToSource) { }
    }
}