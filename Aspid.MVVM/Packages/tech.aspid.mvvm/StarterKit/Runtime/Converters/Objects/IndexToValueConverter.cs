using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks a value out of an authored array by index.
    /// </summary>
    /// <typeparam name="T">The type of the values in the array.</typeparam>
    /// <remarks>
    /// Level icons, ability slots, star ratings, difficulty badges — a number chooses one of a fixed
    /// set of authored assets. Doing it in the ViewModel means the ViewModel holds sprites.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Object", Name = "Index To Value", Tooltip = "Picks a value out of an authored array by index")]
    public sealed class IndexToValueConverter<T> : IConverter<int, T>
    {
        [Tooltip("The values to pick from, in order.")]
        [SerializeField] private T[] _values = Array.Empty<T>();

        [Tooltip("What to do with an index outside the array.")]
        [SerializeField] private IndexMode _mode = IndexMode.Clamp;

        [Tooltip("Returned for an out-of-range index when the mode is Fallback, or when the array is empty.")]
        [SerializeField] private T _fallback = default!;

        public IndexToValueConverter() { }

        /// <param name="values">The values to pick from, in order.</param>
        /// <param name="mode">What to do with an index outside the array.</param>
        /// <param name="fallback">Returned for an out-of-range index when <paramref name="mode"/> is <see cref="IndexMode.Fallback"/>.</param>
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
        /// <returns>The value at that index, resolved through the configured out-of-range mode.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the mode is not a declared value.</exception>
        public T Convert(int value)
        {
            if (_values is null || _values.Length is 0) return _fallback;
            if (value >= 0 && value < _values.Length) return _values[value];

            return _mode switch
            {
                IndexMode.Clamp => _values[value < 0 ? 0 : _values.Length - 1],
                IndexMode.Wrap => _values[(value % _values.Length + _values.Length) % _values.Length],
                IndexMode.Fallback => _fallback,
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };
        }
    }
}
