#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;
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
    /// A ViewModel holds an id — an item key, a status code, a faction name — and the View wants the
    /// thing that id names: an icon, a label, a colour. Seated on any binder bound to that id, this
    /// converter carries the map. The enum-keyed form of the same table already ships as
    /// <see cref="EnumToValueConverter{TEnum, T}"/>, but a key that is a string or an int had nowhere
    /// to look, so the map ended up as a switch in the ViewModel.
    /// <para>
    /// Keys are matched with the type's own equality, which for a string is ordinal and
    /// case-sensitive: an id authored as "Fire" does not answer a pushed "fire".
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Collection", Name = "Dictionary Lookup", Tooltip = "Looks a key up in an authored table")]
    public sealed class DictionaryLookupConverter<TKey, TValue> : IConverter<TKey, TValue?>
    {
        [Tooltip("The value returned for each key. Keys not listed use the fallback.")]
        [SerializeField] private LookupEntry<TKey, TValue>[] _map = Array.Empty<LookupEntry<TKey, TValue>>();

        [Tooltip("Returned for a key the table does not list.")]
        [SerializeField] private TValue _fallback = default!;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryLookupConverter{TKey, TValue}"/>
        /// class with an empty table.
        /// </summary>
        public DictionaryLookupConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryLookupConverter{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="map">The value returned for each key.</param>
        /// <param name="fallback">Returned for a key <paramref name="map"/> does not list.</param>
        public DictionaryLookupConverter(LookupEntry<TKey, TValue>[]? map, TValue fallback = default!)
        {
            _map = map ?? Array.Empty<LookupEntry<TKey, TValue>>();
            _fallback = fallback;
        }

        /// <summary>
        /// Looks the specified key up in the table.
        /// </summary>
        /// <param name="value">The key to look up.</param>
        /// <returns>The value the table gives for that key, or the fallback when it lists no such key.</returns>
        public TValue? Convert(TKey value)
        {
            if (_map is null) return _fallback;

            // A linear scan for the same reason EnumToValueConverter uses one: an authored table is a
            // handful of rows, and a Dictionary would have to be rebuilt after every deserialization.
            for (var i = 0; i < _map.Length; i++)
                if (EqualityComparer<TKey>.Default.Equals(_map[i].Key, value))
                    return _map[i].Value;

            return _fallback;
        }
    }
}
