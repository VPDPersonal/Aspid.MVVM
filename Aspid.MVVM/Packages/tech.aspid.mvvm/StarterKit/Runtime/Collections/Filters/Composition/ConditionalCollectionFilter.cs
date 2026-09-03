#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICollectionFilter{T}"/> that applies the nested filter only while enabled.
    /// When disabled, or with an empty slot, everything passes.
    /// </summary>
    /// <typeparam name="T">The element type being filtered.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Conditional",
        Tooltip = "Applies the nested filter only while enabled")]
    public class ConditionalCollectionFilter<T> : ICollectionFilter<T>
    {
        [Tooltip("Whether the nested filter is applied. When off, everything passes.")]
        [SerializeField] private bool _isEnabled = true;

        [Tooltip("Filter applied while enabled.")]
        [TypeSelector]
        [SerializeReference] private ICollectionFilter<T>? _filter;

        protected ConditionalCollectionFilter() { }

        /// <param name="filter">The filter applied while enabled.</param>
        /// <param name="isEnabled">Whether the nested filter is applied.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="filter"/> is <see langword="null"/>.
        /// </exception>
        public ConditionalCollectionFilter(ICollectionFilter<T> filter, bool isEnabled = true)
        {
            _isEnabled = isEnabled;
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        /// <summary>
        /// Gets or sets whether the nested filter is applied.
        /// </summary>
        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        /// <inheritdoc/>
        public bool Matches(T item) =>
            !_isEnabled || _filter is null || _filter.Matches(item);
    }
}
