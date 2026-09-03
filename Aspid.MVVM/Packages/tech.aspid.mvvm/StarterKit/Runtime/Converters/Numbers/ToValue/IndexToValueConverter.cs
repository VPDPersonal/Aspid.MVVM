#nullable enable
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
    /// <remarks>A float or double index drops its fraction; a NaN names no position and is reported.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Value",
        Name = "Index To Value",
        Tooltip = "Picks a value out of an authored array by index")]
    public class IndexToValueConverter<T> :
        IConverter<int, T?>,
        IConverter<long, T?>,
        IConverter<float, T?>,
        IConverter<double, T?>
    {
        [Tooltip("The values to pick from, in order.")]
        [SerializeField] private T[] _values = Array.Empty<T>();

        [Tooltip("What to do with an index outside the array.")]
        [SerializeField] private IndexOutOfRangeMode _mode = IndexOutOfRangeMode.Clamp;

        [Tooltip("Returned for an out-of-range index under Fallback, or for an empty array.")]
        [SerializeField] private T? _fallback;

        protected IndexToValueConverter() { }

        /// <param name="values">The values to pick from, in order.</param>
        /// <param name="mode">What to do with an index outside the array.</param>
        /// <param name="fallback">
        /// Returned for an out-of-range index under <see cref="IndexOutOfRangeMode.Fallback"/>, or for an empty array.
        /// </param>
        public IndexToValueConverter(
            T[]? values,
            IndexOutOfRangeMode mode = IndexOutOfRangeMode.Clamp,
            T? fallback = default)
        {
            _mode = mode;
            _fallback = fallback;
            _values = values ?? Array.Empty<T>();
        }

        /// <summary>
        /// Picks the value at the specified index.
        /// </summary>
        /// <param name="value">The index to pick.</param>
        /// <returns>The value at that index, resolved through the mode. An empty array or an undeclared mode falls back.</returns>
        public T? Convert(int value) =>
            Pick(value);

        T? IConverter<long, T?>.Convert(long value) =>
            Pick(value);

        T? IConverter<float, T?>.Convert(float value) =>
            Pick(value);

        T? IConverter<double, T?>.Convert(double value) =>
            Pick(value);

        private T? Pick(long value)
        {
            if (_values is null or { Length: 0 }) return Empty();
            if (value >= 0 && value < _values.Length) return _values[value];

            return _mode switch
            {
                IndexOutOfRangeMode.Clamp => _values[value < 0 ? 0 : _values.Length - 1],
                IndexOutOfRangeMode.Wrap => _values[(int)((value % _values.Length + _values.Length) % _values.Length)],
                IndexOutOfRangeMode.Fallback => _fallback,
                _ => Undeclared()
            };
        }

        private T? Pick(double value)
        {
            if (double.IsNaN(value))
            {
                return this.UseFallback(
                    fallback: _fallback,
                    problem: value.Expected("an index"));
            }

            return Pick(NumericSaturation.ToLong(value));
        }

        private T? Empty() => this.UseFallback(
            fallback: _fallback,
            problem: "no values are authored to pick from");

        private T? Undeclared() => this.UseFallback(
            fallback: _fallback,
            problem: $"the mode {_mode.Describe()} is not a declared {nameof(IndexOutOfRangeMode)}");
    }
}
