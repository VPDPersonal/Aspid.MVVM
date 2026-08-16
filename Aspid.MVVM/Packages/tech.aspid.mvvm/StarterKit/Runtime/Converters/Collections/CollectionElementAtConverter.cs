using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes one item out of a list by index.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>"The leaderboard leader", "next in queue" — one item out of a bound list.</remarks>
    [Serializable]
    public sealed class CollectionElementAtConverter<T> : IConverter<IReadOnlyList<T>?, T?>
    {
        [Tooltip("Which item to take.")]
        [SerializeField] private int _index;

        [Tooltip("Count from the end rather than the start.")]
        [SerializeField] private bool _fromEnd;

        [Tooltip("Returned when the index is outside the list.")]
        [SerializeField] private T _fallback = default!;

        /// <remarks>Default: taking the first item.</remarks>
        public CollectionElementAtConverter() { }

        /// <param name="index">Which item to take.</param>
        /// <param name="fromEnd">Whether to count from the end.</param>
        /// <param name="fallback">Returned when the index is outside the list.</param>
        public CollectionElementAtConverter(int index, bool fromEnd = false, T fallback = default!)
        {
            _index = index;
            _fromEnd = fromEnd;
            _fallback = fallback;
        }

        /// <summary>
        /// Takes the configured item.
        /// </summary>
        /// <param name="value">The list to read.</param>
        /// <returns>The item, or the fallback when the index is outside the list.</returns>
        public T? Convert(IReadOnlyList<T>? value)
        {
            if (value is null || value.Count == 0) return _fallback;

            var index = _fromEnd ? value.Count - 1 - _index : _index;
            return index >= 0 && index < value.Count ? value[index] : _fallback;
        }
    }
}
