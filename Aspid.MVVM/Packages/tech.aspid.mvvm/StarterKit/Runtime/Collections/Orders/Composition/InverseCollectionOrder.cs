#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICollectionOrder{T}"/> that runs the nested order in the opposite direction.
    /// An empty slot keeps the source order.
    /// </summary>
    /// <typeparam name="T">The element type being ordered.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Inverse",
        Tooltip = "Runs an order in the opposite direction")]
    public class InverseCollectionOrder<T> : ICollectionOrder<T>
    {
        [Tooltip("Order to run in the opposite direction.")]
        [SerializeReference] private ICollectionOrder<T>? _order;

        protected InverseCollectionOrder() { }

        /// <param name="order">The order to run in the opposite direction.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="order"/> is <see langword="null"/>.
        /// </exception>
        public InverseCollectionOrder(ICollectionOrder<T> order)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
        }

        /// <inheritdoc/>
        public int Compare(T x, T y) =>
            _order?.Compare(y, x) ?? 0;
    }
}
