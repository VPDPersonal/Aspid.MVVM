#nullable enable
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
        /// The key this row matches.
        /// </summary>
        [Tooltip("The key this row matches.")]
        public TKey Key;

        /// <summary>
        /// The value returned for <see cref="Key"/>.
        /// </summary>
        [Tooltip("The value returned for the key.")]
        public TValue Value;
    }
}
