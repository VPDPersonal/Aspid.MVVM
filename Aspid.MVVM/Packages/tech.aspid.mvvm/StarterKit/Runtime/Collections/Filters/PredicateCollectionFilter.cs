#nullable enable
using System;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICollectionFilter{T}"/> that wraps a <see cref="Predicate{T}"/> for code-built filters.
    /// </summary>
    /// <typeparam name="T">The element type being filtered.</typeparam>
    [TypeSelectorDisplay(Hidden = true)]
    public class PredicateCollectionFilter<T> : ICollectionFilter<T>
    {
        private readonly Predicate<T> _predicate;

        /// <param name="predicate">The predicate an element must satisfy.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="predicate"/> is <see langword="null"/>.
        /// </exception>
        public PredicateCollectionFilter(Predicate<T> predicate)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <inheritdoc/>
        public bool Matches(T item) =>
            _predicate(item);
    }
}
