#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ValueTwoWayBinder{T}"/> fixed to <see cref="BindMode.OneWayToSource"/>: pushes the current value to the ViewModel on binding.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
    [Serializable]
    [BindModeOverride(BindMode.OneWayToSource)]
    public class ValueOneWayToSourceBinder<T> : ValueTwoWayBinder<T>
    {
        /// <param name="value">The initial value.</param>
        /// <param name="converter">
        /// The converter applied to each value on its way to the ViewModel, or <see langword="null"/> to send it unchanged.
        /// Only an <see cref="ITwoWayConverter{TFrom, TTo}"/> takes effect, through its reverse conversion.
        /// </param>
        public ValueOneWayToSourceBinder(
            T? value  = default,
            IConverter<T?, T?>? converter = null)
            : base(value, converter, BindMode.OneWayToSource) { }
    }
}
