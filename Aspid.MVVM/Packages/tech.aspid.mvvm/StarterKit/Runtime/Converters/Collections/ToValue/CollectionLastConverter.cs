#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Takes the last item of a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>A sequence with no indexer is walked to its end.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To Value",
        Name = "Last",
        Tooltip = "Takes the last item of a sequence")]
    public class CollectionLastConverter<T> : IConverter<IEnumerable<T?>?, T?>
    {
        [Tooltip("Returned when the sequence is empty.")]
        [SerializeField] private T? _fallback;

        /// <remarks>Default: falling back to the type default.</remarks>
        public CollectionLastConverter() { }

        /// <param name="fallback">Returned when the sequence is empty.</param>
        public CollectionLastConverter(T? fallback)
        {
            _fallback = fallback;
        }

        /// <summary>
        /// Takes the last item of the specified sequence.
        /// </summary>
        /// <param name="value">The sequence to read.</param>
        /// <returns>The last item, or the fallback when there is none.</returns>
        public T? Convert(IEnumerable<T?>? value)
        {
            switch (value)
            {
                case null: return _fallback;
                case IReadOnlyList<T> list: return list.Count > 0 ? list[^1] : _fallback;
            }

            var last = _fallback;

            foreach (var item in value)
                last = item;

            return last;
        }
    }
}
