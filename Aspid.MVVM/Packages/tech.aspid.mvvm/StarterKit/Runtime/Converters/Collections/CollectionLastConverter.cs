#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes the last item of a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>
    /// "The most recent message", "the latest unlock" — the tail of a bound sequence, shown on its own.
    /// <see cref="CollectionElementAtConverter{T}"/> reaches the same item with index 0 counted from
    /// the end, but it needs an <see cref="IReadOnlyList{T}"/>; this one takes any
    /// <see cref="IEnumerable{T}"/>, which is what an observable set, queue or stack arrives as.
    /// <para>
    /// A sequence that exposes no indexer has to be walked to its end before its last item is known;
    /// there is no cheaper way to ask. A list or an array is read straight from its last index.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Collection Last", Tooltip = "Takes the last item of a sequence")]
    public sealed class CollectionLastConverter<T> : IConverter<IEnumerable<T>?, T?>
    {
        [Tooltip("Returned when the sequence is empty.")]
        [SerializeField] private T _fallback = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionLastConverter{T}"/> class with no fallback.
        /// </summary>
        public CollectionLastConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionLastConverter{T}"/> class.
        /// </summary>
        /// <param name="fallback">Returned when the sequence is empty.</param>
        public CollectionLastConverter(T fallback)
        {
            _fallback = fallback;
        }

        /// <summary>
        /// Takes the last item of the specified sequence.
        /// </summary>
        /// <param name="value">The sequence to read.</param>
        /// <returns>The last item, or the fallback when there is none.</returns>
        public T? Convert(IEnumerable<T>? value)
        {
            if (value is null) return _fallback;
            if (value is IReadOnlyList<T> list) return list.Count > 0 ? list[list.Count - 1] : _fallback;

            // Starting from the fallback answers the empty sequence as well: nothing overwrites it.
            var last = _fallback;

            foreach (var item in value)
                last = item;

            return last;
        }
    }
}
