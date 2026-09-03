#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ValueOneWayBinder{T}"/> fixed to <see cref="BindMode.OneTime"/>: accepts a ViewModel value once.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
    [Serializable]
    [BindModeOverride(BindMode.OneTime)]
    public class ValueOneTimeBinder<T> : ValueOneWayBinder<T>
    {
        /// <param name="value">The initial value.</param>
        /// <param name="converter">The converter applied to the incoming value, or <see langword="null"/> to store it unchanged.</param>
        public ValueOneTimeBinder(
            T? value = default,
            IConverter<T?, T?>? converter = null)
            : base(value, converter, BindMode.OneTime) { }
    }
}
