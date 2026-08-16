using System;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reports whether a collection holds a particular item.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <remarks>"Has this achievement", "owns this item".</remarks>
    [Serializable]
    public sealed class CollectionContainsToBoolConverter<T> : IConverter<IEnumerable<T>?, bool>
    {
        [Tooltip("The item looked for.")]
        [SerializeField] private T _value = default!;

        [Tooltip("Invert the result.")]
        [SerializeField] private bool _isInvert;

        public CollectionContainsToBoolConverter() { }

        /// <param name="value">The item looked for.</param>
        /// <param name="isInvert">If <see langword="true"/>, inverts the result.</param>
        public CollectionContainsToBoolConverter(T value, bool isInvert = false)
        {
            _value = value;
            _isInvert = isInvert;
        }

        /// <summary>
        /// Looks for the configured item.
        /// </summary>
        /// <param name="value">The collection to search.</param>
        /// <returns>Whether the item is there, inverted when configured.</returns>
        public bool Convert(IEnumerable<T>? value)
        {
            var found = false;

            if (value is not null)
                foreach (var item in value)
                    if (EqualityComparer<T>.Default.Equals(item, _value))
                    {
                        found = true;
                        break;
                    }

            return _isInvert ? !found : found;
        }
    }
}
