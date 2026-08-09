using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="IndexToValueConverter{T}"/> does with an index outside the array.
    /// </summary>
    public enum IndexMode
    {
        /// <summary>Use the nearest end of the array.</summary>
        Clamp,

        /// <summary>Wrap around, so one past the end is the first entry.</summary>
        Wrap,

        /// <summary>Return the fallback.</summary>
        Fallback,
    }

    /// <summary>
    /// Picks a value out of an authored array by index.
    /// </summary>
    /// <typeparam name="T">The type of the values in the array.</typeparam>
    /// <remarks>
    /// Level icons, ability slots, star ratings, difficulty badges — a number chooses one of a fixed
    /// set of authored assets. Doing it in the ViewModel means the ViewModel holds sprites.
    /// </remarks>
    [Serializable]
    public sealed class IndexToValueConverter<T> : IConverter<int, T>
    {
        [Tooltip("The values to pick from, in order.")]
        [SerializeField] private T[] _values = Array.Empty<T>();

        [Tooltip("What to do with an index outside the array.")]
        [SerializeField] private IndexMode _mode = IndexMode.Clamp;

        [Tooltip("Returned for an out-of-range index when the mode is Fallback, or when the array is empty.")]
        [SerializeField] private T _fallback = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexToValueConverter{T}"/> class with an empty array.
        /// </summary>
        public IndexToValueConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexToValueConverter{T}"/> class.
        /// </summary>
        /// <param name="values">The values to pick from, in order.</param>
        /// <param name="mode">What to do with an index outside the array.</param>
        /// <param name="fallback">Returned for an out-of-range index when <paramref name="mode"/> is <see cref="IndexMode.Fallback"/>.</param>
        public IndexToValueConverter(T[]? values, IndexMode mode = IndexMode.Clamp, T fallback = default!)
        {
            _values = values ?? Array.Empty<T>();
            _mode = mode;
            _fallback = fallback;
        }

        /// <summary>
        /// Picks the value at the specified index.
        /// </summary>
        /// <param name="value">The index to pick.</param>
        /// <returns>The value at that index, resolved through the configured out-of-range mode.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public T Convert(int value)
        {
            if (_values is null || _values.Length == 0) return _fallback;
            if (value >= 0 && value < _values.Length) return _values[value];

            return _mode switch
            {
                IndexMode.Clamp => _values[value < 0 ? 0 : _values.Length - 1],
                IndexMode.Wrap => _values[((value % _values.Length) + _values.Length) % _values.Length],
                IndexMode.Fallback => _fallback,
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };
        }
    }

    /// <summary>
    /// Substitutes an authored value for a <see langword="null"/> one.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <remarks>
    /// The reference-type counterpart of a placeholder string: a default sprite while an avatar
    /// loads, a neutral material for an unequipped slot. Without it the empty state has to be a
    /// second property on the ViewModel.
    /// </remarks>
    [Serializable]
    public sealed class NullCoalesceConverter<T> : IConverter<T?, T>
        where T : class
    {
        [Tooltip("Returned when the bound value is null.")]
        [SerializeField] private T _fallback = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullCoalesceConverter{T}"/> class with no fallback.
        /// </summary>
        public NullCoalesceConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullCoalesceConverter{T}"/> class.
        /// </summary>
        /// <param name="fallback">Returned when the bound value is <see langword="null"/>.</param>
        public NullCoalesceConverter(T fallback)
        {
            _fallback = fallback;
        }

        /// <summary>
        /// Returns the specified value, or the fallback when it is <see langword="null"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>The value, or the fallback.</returns>
        public T Convert(T? value) => value ?? _fallback;
    }

    /// <summary>
    /// Tests a bound value against an authored one.
    /// </summary>
    /// <typeparam name="T">The type of the values being compared.</typeparam>
    /// <remarks>
    /// The generalisation of <see cref="NumberToBoolConverter"/> beyond numbers: "is this the
    /// selected item?", "is this the equipped weapon?". Comparison uses the type's own equality.
    /// </remarks>
    [Serializable]
    public sealed class EqualityToBoolConverter<T> : IConverter<T, bool>
    {
        [Tooltip("The value the bound one is compared against.")]
        [SerializeField] private T _operand = default!;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualityToBoolConverter{T}"/> class with a default operand.
        /// </summary>
        public EqualityToBoolConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualityToBoolConverter{T}"/> class.
        /// </summary>
        /// <param name="operand">The value the bound one is compared against.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public EqualityToBoolConverter(T operand, bool isInvert = false)
        {
            _operand = operand;
            _isInvert = isInvert;
        }

        /// <summary>
        /// Compares the specified value with the authored one.
        /// </summary>
        /// <param name="value">The value to compare.</param>
        /// <returns>Whether the two are equal, inverted when configured.</returns>
        public bool Convert(T value)
        {
            var equal = EqualityComparer<T>.Default.Equals(value, _operand);
            return _isInvert ? !equal : equal;
        }
    }
}
