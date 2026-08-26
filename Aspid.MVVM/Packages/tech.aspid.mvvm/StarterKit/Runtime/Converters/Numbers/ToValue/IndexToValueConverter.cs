using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks a value out of an authored array by index.
    /// </summary>
    /// <typeparam name="T">The type of the values in the array.</typeparam>
    /// <remarks>
    /// A float or double index drops its fraction toward zero; a NaN names no position and is reported.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Value",
        Name = "Index To Value",
        Tooltip = "Picks a value out of an authored array by index")]
    public class IndexToValueConverter<T> :
        IConverter<int, T>,
        IConverter<long, T>,
        IConverter<float, T>,
        IConverter<double, T>
    {
        [Tooltip("The values to pick from, in order.")]
        [SerializeField] private T[] _values = Array.Empty<T>();

        [Tooltip("What to do with an index outside the array.")]
        [SerializeField] private IndexMode _mode = IndexMode.Clamp;

        [Tooltip("Returned when the mode is Fallback and the index is out of range, " +
            "or when the array is empty, which is also reported as an error.")]
        [SerializeField] private T _fallback = default!;

        protected IndexToValueConverter() { }

        /// <param name="values">The values to pick from, in order.</param>
        /// <param name="mode">What to do with an index outside the array.</param>
        /// <param name="fallback">
        /// Returned when <paramref name="mode"/> is <see cref="IndexMode.Fallback"/> and the index is
        /// out of range, or when the array is empty, which is also reported as an error.
        /// </param>
        public IndexToValueConverter(T[]? values, IndexMode mode = IndexMode.Clamp, T fallback = default!)
        {
            _mode = mode;
            _fallback = fallback;
            _values = values ?? Array.Empty<T>();
        }

        /// <summary>
        /// Picks the value at the specified index.
        /// </summary>
        /// <param name="value">The index to pick.</param>
        /// <returns>
        /// The value at that index, resolved through the configured mode. An empty array and an
        /// undeclared mode each report an error and return the fallback.
        /// </returns>
        public T Convert(int value) => Pick(value);

        T IConverter<long, T>.Convert(long value) => Pick(value);

        T IConverter<float, T>.Convert(float value) => Pick(value);

        T IConverter<double, T>.Convert(double value) => Pick(value);

        private T Pick(long value)
        {
            if (_values is null or { Length: 0 }) return Empty();
            if (value >= 0 && value < _values.Length) return _values[value];

            return _mode switch
            {
                IndexMode.Clamp => _values[value < 0 ? 0 : _values.Length - 1],
                IndexMode.Wrap => _values[(int)((value % _values.Length + _values.Length) % _values.Length)],
                IndexMode.Fallback => _fallback,
                _ => Undeclared()
            };
        }

        private T Pick(double value)
        {
            // A NaN is not a position, and saturating it to zero would quietly pick the first value.
            if (double.IsNaN(value)) return this.UseFallback(_fallback, value.Expected("an index"));

            return Pick(NumericSaturation.ToLong(value));
        }

        private T Empty() => this.UseFallback(
            fallback: _fallback,
            problem: "no values are authored to pick from");

        private T Undeclared() => this.UseFallback(
            fallback: _fallback,
            problem: $"the mode {_mode.Describe()} is not a declared {nameof(IndexMode)}");
    }
}
