#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Looks a key up in an authored table.
    /// </summary>
    /// <typeparam name="TKey">The type of the key being looked up.</typeparam>
    /// <typeparam name="TValue">The type of the value the key names.</typeparam>
    /// <remarks>
    /// Keys are matched with the type's own equality: for a string, ordinal and case-sensitive.
    /// A duplicate key is reported and answered by its first row.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Collection",
        Name = "Dictionary Lookup",
        Tooltip = "Looks a key up in an authored table")]
    public class DictionaryLookupConverter<TKey, TValue> : IConverter<TKey, TValue?>
    {
        [Tooltip("The value for each key. A duplicate key is reported, its first row wins.")]
        [SerializeField] private LookupEntry<TKey, TValue?>[] _map = Array.Empty<LookupEntry<TKey, TValue?>>();

        [Tooltip("Returned for a key the table does not list.")]
        [SerializeField] private TValue? _fallback;

        /// <remarks>Default: an empty table, answering every key with the type default.</remarks>
        public DictionaryLookupConverter() { }

        /// <param name="map">The value for each key. A duplicate key is reported, its first row wins. The array is copied.</param>
        /// <param name="fallback">Returned for a key <paramref name="map"/> does not list.</param>
        public DictionaryLookupConverter(
            LookupEntry<TKey, TValue?>[]? map,
            TValue? fallback = default)
        {
            _fallback = fallback;
            _map = map is null
                ? Array.Empty<LookupEntry<TKey, TValue?>>()
                : (LookupEntry<TKey, TValue?>[])map.Clone();
        }

        /// <summary>
        /// Looks the specified key up in the table.
        /// </summary>
        /// <param name="value">The key to look up.</param>
        /// <returns>The value for that key, or the fallback when it is not listed.</returns>
        public TValue? Convert(TKey value)
        {
            if (_map.Length is 0) return _fallback;
            var comparer = EqualityComparer<TKey>.Default;

            for (var i = 0; i < _map.Length; i++)
            {
                if (!comparer.Equals(_map[i].Key, value)) continue;

                for (var j = i + 1; j < _map.Length; j++)
                {
                    if (!comparer.Equals(_map[j].Key, value)) continue;

                    this.LogError(
                        problem: $"{value.Describe()} is listed more than once in the table",
                        consequence: "Using the first row that names it.");

                    break;
                }

                return _map[i].Value;
            }

            return _fallback;
        }
    }
}
