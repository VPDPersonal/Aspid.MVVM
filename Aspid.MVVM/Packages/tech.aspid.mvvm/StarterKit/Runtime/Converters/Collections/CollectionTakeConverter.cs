#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Keeps a few items off one end of a sequence.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>
    /// A "top three" feed into a virtualized list, or the last five lines of a log — a shorter view of
    /// a bound collection, without a second projected property on the ViewModel that has to be rebuilt
    /// whenever the source changes.
    /// <para>
    /// The result is one <see cref="List{T}"/> reused between calls, because a binder pushes on every
    /// notification and a fresh list per push allocates once a frame while a value is being dragged.
    /// Read it inside the push it arrived on: the next conversion clears and refills the same list, so
    /// anything that stores it ends up holding a view of whatever came last.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Collection Take", Tooltip = "Keeps a few items off one end of a sequence")]
    public sealed class CollectionTakeConverter<T> : IConverter<IEnumerable<T>?, IEnumerable<T>>
    {
        [Tooltip("How many items to keep. Zero or fewer keeps none of them.")]
        [SerializeField] private int _count = 3;

        [Tooltip("Take from the end of the sequence rather than from its start.")]
        [SerializeField] private bool _fromEnd;

        [NonSerialized] private List<T>? _buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionTakeConverter{T}"/> class keeping the
        /// first three items.
        /// </summary>
        public CollectionTakeConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionTakeConverter{T}"/> class.
        /// </summary>
        /// <param name="count">How many items to keep.</param>
        /// <param name="fromEnd">Whether to take from the end rather than from the start.</param>
        public CollectionTakeConverter(int count, bool fromEnd = false)
        {
            _count = count;
            _fromEnd = fromEnd;
        }

        /// <summary>
        /// Keeps the configured items of the specified sequence.
        /// </summary>
        /// <param name="value">The sequence to shorten.</param>
        /// <returns>
        /// The kept items, in their original order, in a list this converter reuses on the next call.
        /// </returns>
        public IEnumerable<T> Convert(IEnumerable<T>? value)
        {
            _buffer ??= new List<T>();
            _buffer.Clear();

            if (value is null || _count <= 0) return _buffer;

            if (value is IReadOnlyList<T> list)
            {
                var start = _fromEnd ? Math.Max(0, list.Count - _count) : 0;
                var end = _fromEnd ? list.Count : Math.Min(list.Count, _count);

                for (var i = start; i < end; i++)
                    _buffer.Add(list[i]);

                return _buffer;
            }

            foreach (var item in value)
            {
                _buffer.Add(item);
                if (!_fromEnd && _buffer.Count == _count) break;
            }

            // A sequence with no indexer gives no way to know where its last few items begin until it
            // has been walked, so the tail case buffers all of it and drops the front in one move.
            if (_fromEnd && _buffer.Count > _count)
                _buffer.RemoveRange(0, _buffer.Count - _count);

            return _buffer;
        }
    }
}
