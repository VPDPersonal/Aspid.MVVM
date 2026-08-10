#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes the first item of a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>
    /// "Next in the queue", "the current objective" — the head of a bound sequence, shown on its own.
    /// <see cref="CollectionElementAtConverter{T}"/> answers the same question but needs an
    /// <see cref="IReadOnlyList{T}"/>, which an observable set, queue or stack is not: they stop at
    /// <see cref="IReadOnlyCollection{T}"/> and expose no indexer to read.
    /// <para>
    /// Only the first item is read, so the cost does not grow with the length of the sequence.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Collection First", Tooltip = "Takes the first item of a sequence")]
    public sealed class CollectionFirstConverter<T> : IConverter<IEnumerable<T>?, T?>
    {
        [Tooltip("Returned when the sequence is empty.")]
        [SerializeField] private T _fallback = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionFirstConverter{T}"/> class with no fallback.
        /// </summary>
        public CollectionFirstConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionFirstConverter{T}"/> class.
        /// </summary>
        /// <param name="fallback">Returned when the sequence is empty.</param>
        public CollectionFirstConverter(T fallback)
        {
            _fallback = fallback;
        }

        /// <summary>
        /// Takes the first item of the specified sequence.
        /// </summary>
        /// <param name="value">The sequence to read.</param>
        /// <returns>The first item, or the fallback when there is none.</returns>
        public T? Convert(IEnumerable<T>? value)
        {
            if (value is null) return _fallback;
            if (value is IReadOnlyList<T> list) return list.Count > 0 ? list[0] : _fallback;

            // foreach disposes the enumerator on the way out, which a bare MoveNext would not.
            foreach (var item in value) return item;

            return _fallback;
        }
    }
}
