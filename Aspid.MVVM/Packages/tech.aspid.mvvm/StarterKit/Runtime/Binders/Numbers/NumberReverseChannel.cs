using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Holds the subscriptions behind the four numeric events of an <see cref="INumberReverseBinder"/>
    /// and raises them together.
    /// </summary>
    /// <remarks>
    /// Keep it as a mutable field and expose it through <see cref="INumberReverseBinder.Channel"/>.
    /// </remarks>
    public struct NumberReverseChannel
    {
        /// <summary>
        /// Raised with the View value for <see cref="int"/> subscribers.
        /// </summary>
        public event Action<int>? IntValueChanged;

        /// <summary>
        /// Raised with the View value for <see cref="long"/> subscribers.
        /// </summary>
        public event Action<long>? LongValueChanged;

        /// <summary>
        /// Raised with the View value for <see cref="float"/> subscribers.
        /// </summary>
        public event Action<float>? FloatValueChanged;

        /// <summary>
        /// Raised with the View value for <see cref="double"/> subscribers.
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
        public bool HasFloatingPointListeners =>
            FloatValueChanged is not null || DoubleValueChanged is not null;

        /// <summary>
        /// Raises all four events with <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void Raise(int value)
        {
            RaiseIntegers(value);
            RaiseFloatingPoint(value);
        }

        /// <summary>
        /// Raises all four events with <paramref name="value"/>, saturating at the <see cref="int"/> bounds.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void Raise(long value)
        {
            RaiseIntegers(value);
            RaiseFloatingPoint(value);
        }

        /// <summary>
        /// Raises all four events with <paramref name="value"/>, saturating at each type's bounds.
        /// Integer events receive the value truncated toward zero, or zero for a NaN.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void Raise(float value)
        {
            RaiseIntegers(value);
            RaiseFloatingPoint(value);
        }

        /// <summary>
        /// Raises all four events with <paramref name="value"/>, saturating at each type's bounds.
        /// Integer events receive the value truncated toward zero, or zero for a NaN.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void Raise(double value)
        {
            RaiseIntegers(value);
            RaiseFloatingPoint(value);
        }

        /// <summary>
        /// Raises only <see cref="IntValueChanged"/> and <see cref="LongValueChanged"/>, saturating at the <see cref="int"/> bounds.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void RaiseIntegers(long value)
        {
            IntValueChanged?.Invoke(NumericSaturation.ToInt(value));
            LongValueChanged?.Invoke(value);
        }

        /// <summary>
        /// Raises only <see cref="IntValueChanged"/> and <see cref="LongValueChanged"/>, saturating at each type's bounds.
        /// The value is truncated toward zero; a NaN arrives as zero.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void RaiseIntegers(double value)
        {
            IntValueChanged?.Invoke(NumericSaturation.ToInt(value));
            LongValueChanged?.Invoke(NumericSaturation.ToLong(value));
        }

        /// <summary>
        /// Raises only <see cref="FloatValueChanged"/> and <see cref="DoubleValueChanged"/>, saturating at the <see cref="float"/> bounds.
        /// </summary>
        /// <param name="value">The value read from the View.</param>
        public void RaiseFloatingPoint(double value)
        {
            FloatValueChanged?.Invoke(NumericSaturation.ToFloat(value));
            DoubleValueChanged?.Invoke(value);
        }
    }
}
