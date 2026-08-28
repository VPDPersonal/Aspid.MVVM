using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Holds the subscriptions behind the four numeric events of an <see cref="INumberReverseBinder"/>
    /// and raises them together.
    /// </summary>
    /// <remarks>
    /// The subscriptions have to live outside the binder: a binder cannot declare four events named
    /// <c>ValueChanged</c>, and an interface cannot hold a field. An implementor keeps this as a mutable
    /// field, hands it to <see cref="INumberReverseBinder.Channel"/>, and calls one <c>Raise</c> to reach
    /// every numeric type at once.
    /// </remarks>
    public struct NumberReverseChannel
    {
        /// <summary>
        /// Raised when the View value changes and should be propagated to an <see cref="int"/> binding target.
        /// </summary>
        public event Action<int>? IntValueChanged;

        /// <summary>
        /// Raised when the View value changes and should be propagated to a <see cref="long"/> binding target.
        /// </summary>
        public event Action<long>? LongValueChanged;

        /// <summary>
        /// Raised when the View value changes and should be propagated to a <see cref="float"/> binding target.
        /// </summary>
        public event Action<float>? FloatValueChanged;

        /// <summary>
        /// Raised when the View value changes and should be propagated to a <see cref="double"/> binding target.
        /// </summary>
        public event Action<double>? DoubleValueChanged;

        /// <summary>
        /// Indicates whether <see cref="IntValueChanged"/> or <see cref="LongValueChanged"/> has a subscriber.
        /// </summary>
        public bool HasIntegerListeners =>
            IntValueChanged is not null || LongValueChanged is not null;

        /// <summary>
        /// Indicates whether <see cref="FloatValueChanged"/> or <see cref="DoubleValueChanged"/> has a subscriber.
        /// </summary>
        public bool HasDecimalListeners =>
            FloatValueChanged is not null || DoubleValueChanged is not null;

        /// <summary>
        /// Raises all four events with <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void Raise(int value)
        {
            RaiseIntegers(value);
            RaiseDecimals(value);
        }

        /// <summary>
        /// Raises all four events with <paramref name="value"/>, saturating at <see cref="int"/>'s bounds
        /// on <see cref="IntValueChanged"/>.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void Raise(long value)
        {
            RaiseIntegers(value);
            RaiseDecimals(value);
        }

        /// <summary>
        /// Raises all four events with <paramref name="value"/>, saturating at each event type's bounds.
        /// The fraction is dropped toward zero on the integer events, and a NaN reaches them as zero.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void Raise(float value)
        {
            RaiseIntegers(value);
            RaiseDecimals(value);
        }

        /// <summary>
        /// Raises all four events with <paramref name="value"/>, saturating at each event type's bounds.
        /// The fraction is dropped toward zero on the integer events, and a NaN reaches them as zero.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void Raise(double value)
        {
            RaiseIntegers(value);
            RaiseDecimals(value);
        }

        /// <summary>
        /// Raises <see cref="IntValueChanged"/> and <see cref="LongValueChanged"/> only, saturating at
        /// <see cref="int"/>'s bounds.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        /// <remarks>
        /// For a binder that reaches the integer and the decimal events by separate routes — parsing text,
        /// for instance, where the two parses succeed independently.
        /// </remarks>
        public void RaiseIntegers(long value)
        {
            IntValueChanged?.Invoke(NumericSaturation.ToInt(value));
            LongValueChanged?.Invoke(value);
        }

        /// <summary>
        /// Raises <see cref="IntValueChanged"/> and <see cref="LongValueChanged"/> only, dropping the
        /// fraction toward zero and saturating at each type's bounds. A NaN reaches them as zero.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        /// <remarks>
        /// For a binder holding a decimal value that a ViewModel may bind as an integer — a text field
        /// whose contents are not whole, for instance.
        /// </remarks>
        public void RaiseIntegers(double value)
        {
            IntValueChanged?.Invoke(NumericSaturation.ToInt(value));
            LongValueChanged?.Invoke(NumericSaturation.ToLong(value));
        }

        /// <summary>
        /// Raises <see cref="FloatValueChanged"/> and <see cref="DoubleValueChanged"/> only, saturating at
        /// <see cref="float"/>'s bounds.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        /// <remarks>
        /// For a binder that reaches the integer and the decimal events by separate routes — parsing text,
        /// for instance, where the two parses succeed independently.
        /// </remarks>
        public void RaiseDecimals(double value)
        {
            FloatValueChanged?.Invoke(NumericSaturation.ToFloat(value));
            DoubleValueChanged?.Invoke(value);
        }
    }
}
