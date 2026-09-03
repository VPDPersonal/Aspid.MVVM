#nullable enable
using System;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICollectionOrder{T}"/> that wraps a <see cref="Comparison{T}"/> or an
    /// <see cref="IComparer{T}"/> for code-built sort orders.
    /// </summary>
    /// <typeparam name="T">The element type being ordered.</typeparam>
    [TypeSelectorDisplay(Hidden = true)]
    public class ComparisonCollectionOrder<T> : ICollectionOrder<T>
    {
        private readonly IComparer<T?> _comparer;

        /// <param name="comparer">The comparer to wrap.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="comparer"/> is <see langword="null"/>.
        /// </exception>
        public ComparisonCollectionOrder(IComparer<T?> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        /// <param name="comparison">The comparison to wrap.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="comparison"/> is <see langword="null"/>.
        /// </exception>
        public ComparisonCollectionOrder(Comparison<T?> comparison)
            : this(Comparer<T?>.Create(comparison ?? throw new ArgumentNullException(nameof(comparison)))) { }

        /// <inheritdoc/>
        public int Compare(T? x, T? y) =>
            _comparer.Compare(x, y);
    }
}
