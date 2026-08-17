using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Counts the items in a collection.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>
    /// A badge showing "12" needed a <c>Count</c> property on every list-bearing ViewModel, kept in
    /// step with the list by hand.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Collection Count", Tooltip = "Counts the items in a collection")]
    public sealed class CollectionCountConverter<T> : IConverter<IReadOnlyCollection<T>?, int>
    {
        /// <summary>
        /// Counts the specified collection.
        /// </summary>
        /// <param name="value">The collection to count.</param>
        /// <returns>The number of items, or zero when the collection is <see langword="null"/>.</returns>
        public int Convert(IReadOnlyCollection<T>? value) => value?.Count ?? 0;
    }
}
