#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICollectionFilter{T}"/> that passes an element only when every nested filter passes it.
    /// Empty slots are skipped.
    /// </summary>
    /// <typeparam name="T">The element type being filtered.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "And",
        Tooltip = "Passes an element only when every nested filter passes it")]
    public class AndCollectionFilter<T> : ICollectionFilter<T>
    {
        [Tooltip("Filters that must all pass an element. Empty slots are skipped.")]
        [TypeSelector]
        [SerializeReference] private ICollectionFilter<T>?[] _filters = Array.Empty<ICollectionFilter<T>>();

        protected AndCollectionFilter() { }

        /// <param name="filters">The filters that must all pass an element. Empty slots are skipped.</param>
        public AndCollectionFilter(params ICollectionFilter<T>?[]? filters)
        {
            _filters = filters ?? Array.Empty<ICollectionFilter<T>>();
        }

        /// <inheritdoc/>
        public bool Matches(T item)
        {
            foreach (var filter in _filters)
            {
                if (filter is not null && !filter.Matches(item))
                    return false;
            }

            return true;
        }
    }
}
