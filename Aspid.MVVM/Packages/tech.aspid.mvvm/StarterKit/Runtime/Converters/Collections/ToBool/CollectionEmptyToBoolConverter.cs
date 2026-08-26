using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reports whether a collection has anything in it.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>A sequence carrying no count of its own is asked for one item, never for all of them.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection/To Bool",
        Name = "Is Empty",
        Tooltip = "Reports whether a collection has anything in it")]
    public class CollectionEmptyToBoolConverter<T> :
        IConverter<IEnumerable<T?>?, bool>,
        IConverter<IReadOnlyCollection<T?>?, bool>
    {
        [Tooltip("Invert the result — true when the collection has items.")]
        [SerializeField] private bool _isInvert;

        /// <remarks>Default: without inverting.</remarks>
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
        public bool Convert(IReadOnlyCollection<T?>? value)
        {
            var empty = value is null || value.Count is 0;
            return empty != _isInvert;
        }

        bool IConverter<IEnumerable<T?>?, bool>.Convert(IEnumerable<T?>? value) =>
            IsEmpty(value) != _isInvert;

        private static bool IsEmpty(IEnumerable<T?>? value)
        {
            switch (value)
            {
                case null: return true;
                case ICollection<T> collection: return collection.Count is 0;
                case IReadOnlyCollection<T> collection: return collection.Count is 0;
            }

            // using disposes the enumerator, which a bare MoveNext would not.
            using var enumerator = value.GetEnumerator();
            return !enumerator.MoveNext();
        }
    }
}
