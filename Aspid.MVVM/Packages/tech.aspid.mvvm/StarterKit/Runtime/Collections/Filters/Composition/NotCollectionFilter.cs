#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICollectionFilter{T}"/> that passes an element only when the nested filter rejects it.
    /// An empty slot passes everything.
    /// </summary>
    /// <typeparam name="T">The element type being filtered.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Not",
        Tooltip = "Passes an element only when the nested filter rejects it")]
    public class NotCollectionFilter<T> : ICollectionFilter<T>
    {
        [Tooltip("Filter whose verdict is inverted.")]
        [TypeSelector]
        [SerializeReference] private ICollectionFilter<T>? _filter;

        protected NotCollectionFilter() { }

        /// <param name="filter">The filter whose verdict is inverted.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="filter"/> is <see langword="null"/>.
        /// </exception>
        public NotCollectionFilter(ICollectionFilter<T> filter)
        {
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        /// <inheritdoc/>
        public bool Matches(T item) =>
            _filter is null || !_filter.Matches(item);
    }
}
