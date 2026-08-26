using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Keeps a few items off one end of a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>
    /// The result is one <see cref="List{T}"/> refilled on every call — read it at once, do not cache it.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection",
        Name = "Take",
        Tooltip = "Keeps a few items off one end of a sequence")]
    public class CollectionTakeConverter<T> : IConverter<IEnumerable<T?>?, IEnumerable<T?>>
    {
        [Tooltip("How many items to keep. Zero or fewer keeps none of them.")]
        [SerializeField] [Min(0)] private int _count = 3;

        [Tooltip("Take from the end of the sequence rather than from its start.")]
        [SerializeField] private bool _fromEnd;

        [NonSerialized] private List<T?> _buffer = new();

        /// <remarks>Default: keeping the first three items.</remarks>
        public CollectionTakeConverter() { }

        /// <param name="count">How many items to keep. Zero keeps none of them.</param>
        /// <param name="fromEnd">Whether to take from the end rather than from the start.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="count"/> is negative.
        /// </exception>
        public CollectionTakeConverter(int count, bool fromEnd = false)
        {
            _fromEnd = fromEnd;
            _count = count >= 0 ? count : throw new ArgumentOutOfRangeException(nameof(count));
        }

        /// <summary>
        /// Keeps the configured items of the specified sequence.
        /// </summary>
        /// <param name="value">The sequence to shorten.</param>
        /// <returns>The kept items in their original order, in a list reused on the next call.</returns>
        public IEnumerable<T?> Convert(IEnumerable<T?>? value)
        {
            _buffer.Clear();
            if (_count <= 0) return _buffer;

            switch (value)
            {
                case null: return _buffer;
                case IReadOnlyList<T> list:
                    {
                        var start = _fromEnd ? Math.Max(0, list.Count - _count) : 0;
                        var end = _fromEnd ? list.Count : Math.Min(list.Count, _count);

                        for (var i = start; i < end; i++)
                            _buffer.Add(list[i]);

                        return _buffer;
                    }
            }

            foreach (var item in value)
            {
                _buffer.Add(item);
                if (!_fromEnd && _buffer.Count == _count) break;
            }

            // A sequence with no indexer gives no way to know where its last few items begin until it
            // has been walked, so the tail case buffers all of it and drops the front in one move.
            if (_fromEnd && _buffer.Count > _count)
                _buffer.RemoveRange(index: 0, count: _buffer.Count - _count);

            return _buffer;
        }
    }
}
