using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="OneWayValue{T}"/> fixed to <see cref="BindMode.OneTime"/>: accepts a ViewModel value once.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
    [Serializable]
    [BindModeOverride(BindMode.OneTime)]
    public class OneTimeValue<T> : OneWayValue<T>
    {
        /// <remarks>
        /// Starts with <see langword="default"/> and no converter.
        /// </remarks>
        public OneTimeValue()
            : base(BindMode.OneTime) { }

        /// <param name="value">The initial value.</param>
        public OneTimeValue(T? value)
            : base(value, BindMode.OneTime) { }

        /// <param name="value">The initial value.</param>
        /// <param name="converter">The converter applied to the incoming value, or <see langword="null"/> to store it unchanged.</param>
        public OneTimeValue(T? value, IConverter<T?, T?>? converter)
            : base(value, converter, BindMode.OneTime) { }
    }
}
