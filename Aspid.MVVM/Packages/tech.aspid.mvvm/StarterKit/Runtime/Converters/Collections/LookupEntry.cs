using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// One row of a <see cref="DictionaryLookupConverter{TKey, TValue}"/> table.
    /// </summary>
    /// <typeparam name="TKey">The type of the key being looked up.</typeparam>
    /// <typeparam name="TValue">The type of the value the key names.</typeparam>
    [Serializable]
    public struct LookupEntry<TKey, TValue>
    {
        /// <summary>
        /// Gets the key this row matches.
        /// </summary>
        [field: Tooltip("The key this row matches.")]
        [field: SerializeField]
        public TKey Key { get; private set; }

        /// <summary>
        /// Gets the value returned for <see cref="Key"/>.
        /// </summary>
        [field: Tooltip("The value returned for the key.")]
        [field: SerializeField]
        public TValue Value { get; private set; }

        /// <param name="key">The key this row matches.</param>
        /// <param name="value">The value returned for the key.</param>
        public LookupEntry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
