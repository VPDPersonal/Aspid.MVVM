#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICollectionFilter{T}"/> that passes an element when at least one nested filter passes it.
    /// Empty slots are skipped; with no filter at all, everything passes.
    /// </summary>
    /// <typeparam name="T">The element type being filtered.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Or",
        Tooltip = "Passes an element when at least one nested filter passes it")]
    public class OrCollectionFilter<T> : ICollectionFilter<T>
    {
        [Tooltip("Filters of which at least one must pass an element. Empty slots are skipped.")]
        [SerializeReference] private ICollectionFilter<T>?[] _filters = Array.Empty<ICollectionFilter<T>>();

        protected OrCollectionFilter() { }

        /// <param name="filters">The filters of which at least one must pass an element. Empty slots are skipped.</param>
        public OrCollectionFilter(params ICollectionFilter<T>?[]? filters)
        {
            _filters = filters ?? Array.Empty<ICollectionFilter<T>>();
        }

        /// <inheritdoc/>
        public bool Matches(T item)
        {
            var hasFilter = false;

            foreach (var filter in _filters)
            {
                if (filter is null) continue;

                hasFilter = true;
                if (filter.Matches(item)) return true;
            }

            return !hasFilter;
        }
    }
}
