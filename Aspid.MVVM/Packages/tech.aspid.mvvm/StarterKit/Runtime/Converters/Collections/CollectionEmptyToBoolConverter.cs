using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reports whether a collection has anything in it.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>Empty-state placeholders, which previously needed their own boolean property.</remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Collection Empty To Bool", Tooltip = "Reports whether a collection has anything in it")]
    public sealed class CollectionEmptyToBoolConverter<T> : IConverter<IReadOnlyCollection<T>?, bool>
    {
        [Tooltip("Invert the result — true when the collection has items.")]
        [SerializeField] private bool _isInvert;

        public CollectionEmptyToBoolConverter() { }

        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public CollectionEmptyToBoolConverter(bool isInvert)
        {
            _isInvert = isInvert;
        }

        /// <summary>
        /// Tests whether the specified collection is empty.
        /// </summary>
        /// <param name="value">The collection to test.</param>
        /// <returns>
        /// <see langword="true"/> when it is <see langword="null"/> or empty, inverted when configured.
        /// </returns>
        public bool Convert(IReadOnlyCollection<T>? value)
        {
            var empty = value is null || value.Count == 0;
            return _isInvert ? !empty : empty;
        }
    }
}
