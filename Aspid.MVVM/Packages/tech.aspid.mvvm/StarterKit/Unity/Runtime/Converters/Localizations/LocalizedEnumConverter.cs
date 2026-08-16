#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using UnityEngine.Localization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Looks an enum member's name up in a localization table.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being localized.</typeparam>
    /// <remarks>
    /// State, rarity and difficulty names, keyed by member name with an authored prefix — so adding a
    /// member adds a key rather than a switch branch.
    /// </remarks>
    [Serializable]
    public sealed class LocalizedEnumConverter<TEnum> : IConverter<TEnum, string>
        where TEnum : struct, Enum
    {
        [Tooltip("The string table the keys are looked up in.")]
        [SerializeField] private LocalizedStringTable _table = new();

        [Tooltip("Placed before the member name to form the key.")]
        [SerializeField] private string _keyPrefix = string.Empty;

        [Tooltip("Show the member name when it has no entry.")]
        [SerializeField] private bool _fallbackToName = true;

        public LocalizedEnumConverter() { }

        /// <summary>
        /// Looks the specified member up.
        /// </summary>
        /// <param name="value">The member to localize.</param>
        /// <returns>The localized text, or the member name when it has no entry.</returns>
        public string Convert(TEnum value)
        {
            var name = Enum.GetName(typeof(TEnum), value) ?? value.ToString();
            var key = _keyPrefix + name;

            var table = _table.GetTable();
            var entry = table?.GetEntry(key);

            if (entry is not null) return entry.GetLocalizedString();

            return _fallbackToName ? name : key;
        }
    }
}
#endif
