using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TwoWayValue{T}"/> fixed to <see cref="BindMode.OneWayToSource"/>: pushes the current value to the ViewModel on binding.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class OneWayToSourceValue<T> : TwoWayValue<T>
    {
        /// <remarks>
        /// Starts with <see langword="default"/> and no converter.
        /// </remarks>
        public OneWayToSourceValue()
            : base(BindMode.OneWayToSource) { }

        /// <param name="value">The initial value.</param>
        public OneWayToSourceValue(T? value)
            : base(value, BindMode.OneWayToSource) { }

        /// <param name="value">The initial value.</param>
        /// <param name="converter">
        /// The converter applied to each value on its way to the ViewModel, or <see langword="null"/> to send it unchanged.
        /// Only an <see cref="ITwoWayConverter{TFrom, TTo}"/> takes effect, through its reverse conversion.
        /// </param>
        public OneWayToSourceValue(T? value, IConverter<T?, T?>? converter)
            : base(value, converter, BindMode.OneWayToSource) { }
    }
}
