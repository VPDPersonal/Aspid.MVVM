using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes one item out of a list by index.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To Value",
        Name = "Element At",
        Tooltip = "Takes one item out of a list by index")]
    public class CollectionElementAtConverter<T> : IConverter<IReadOnlyList<T?>?, T?>
    {
        [Tooltip("Which item to take.")]
        [SerializeField] [Min(0)] private int _index;

        [Tooltip("Count from the end rather than the start.")]
        [SerializeField] private bool _fromEnd;

        [Tooltip("Returned when the index is outside the list.")]
        [SerializeField] private T? _fallback;

        /// <remarks>Default: taking the first item.</remarks>
        public CollectionElementAtConverter() { }

        /// <param name="index">Which item to take. An index outside a non-empty list is reported.</param>
        /// <param name="fromEnd">Whether to count from the end.</param>
        /// <param name="fallback">
        /// Returned when the index is outside the list — silently for a <see langword="null"/> or
        /// empty list, with an error otherwise.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="index"/> is negative.
        /// </exception>
        public CollectionElementAtConverter(int index, bool fromEnd = false, T? fallback = default)
        {
            _fromEnd = fromEnd;
            _fallback = fallback;
            _index = index >= 0 ? index : throw new ArgumentOutOfRangeException(nameof(index));
        }

        /// <summary>
        /// Takes the configured item.
        /// </summary>
        /// <param name="value">The list to read.</param>
        /// <returns>
        /// The item, or the fallback when the index is outside the list. A <see langword="null"/> or
        /// empty list falls back silently; an index outside a non-empty list is reported first.
        /// </returns>
        public T? Convert(IReadOnlyList<T?>? value)
        {
            if (value is null || value.Count == 0) return _fallback;

            var index = _fromEnd ? value.Count - 1 - _index : _index;
            if (index >= 0 && index < value.Count) return value[index];

            this.LogError(
                problem: $"the index {_index}{(_fromEnd ? " from the end" : "")} is outside the list of {value.Count}",
                consequence: "Returning the fallback.");

            return _fallback;
        }
    }
}
