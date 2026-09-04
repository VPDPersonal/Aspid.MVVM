#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICollectionOrder{T}"/> that applies multiple orders in sequence: the first one that tells
    /// two elements apart decides. Empty slots are skipped.
    /// </summary>
    /// <typeparam name="T">The element type being ordered.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Sequence",
        Tooltip = "Applies orders in sequence: the first one that tells two elements apart decides")]
    public class SequenceCollectionOrder<T> : ICollectionOrder<T>
    {
        [Tooltip("Orders applied in sequence. Empty slots are skipped.")]
        [SerializeReference] private ICollectionOrder<T>?[] _orders = Array.Empty<ICollectionOrder<T>>();

        protected SequenceCollectionOrder() { }

        /// <param name="orders">The orders applied in sequence. Empty slots are skipped.</param>
        public SequenceCollectionOrder(params ICollectionOrder<T>?[]? orders)
        {
            _orders = orders ?? Array.Empty<ICollectionOrder<T>>();
        }

        /// <inheritdoc/>
        public int Compare(T x, T y)
        {
            foreach (var order in _orders)
            {
                if (order is null) continue;

                var result = order.Compare(x, y);
                if (result != 0) return result;
            }

            return 0;
        }
    }
}
