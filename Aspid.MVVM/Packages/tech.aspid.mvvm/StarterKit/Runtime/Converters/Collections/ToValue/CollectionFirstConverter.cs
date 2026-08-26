using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes the first item of a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>Takes any sequence, indexer or not — an observable set, queue or stack has no index.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To Value",
        Name = "First",
        Tooltip = "Takes the first item of a sequence")]
    public class CollectionFirstConverter<T> : IConverter<IEnumerable<T?>?, T?>
    {
        [Tooltip("Returned when the sequence is empty.")]
        [SerializeField] private T? _fallback;

        /// <remarks>Default: falling back to the type default.</remarks>
        public CollectionFirstConverter() { }

        /// <param name="fallback">Returned when the sequence is empty.</param>
        public CollectionFirstConverter(T? fallback)
        {
            _fallback = fallback;
        }

        /// <summary>
        /// Takes the first item of the specified sequence.
        /// </summary>
        /// <param name="value">The sequence to read.</param>
        /// <returns>The first item, or the fallback when there is none.</returns>
        public T? Convert(IEnumerable<T?>? value)
        {
            switch (value)
            {
                case null: return _fallback;
                case IReadOnlyList<T> list: return list.Count > 0 ? list[0] : _fallback;
            }

            // foreach disposes the enumerator on the way out, which a bare MoveNext would not.
            foreach (var item in value)
                return item;

            return _fallback;
        }
    }
}
