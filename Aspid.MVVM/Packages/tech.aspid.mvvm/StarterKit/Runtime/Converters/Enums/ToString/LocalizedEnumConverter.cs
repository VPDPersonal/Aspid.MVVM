#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using UnityEngine.Localization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Looks an enum member's name up in a localization table.
    /// </summary>
    /// <typeparam name="TEnum">The enum type being localized.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Localization",
        Name = "Localized Enum",
        Tooltip = "Looks an enum member's name up in a localization table")]
    public class LocalizedEnumConverter<TEnum> : IConverter<TEnum, string>
        where TEnum : struct, Enum
    {
        [Tooltip("The string table the keys are looked up in.")]
        [SerializeField] private LocalizedStringTable _table = new();

        [Tooltip("Placed before the member name to form the key.")]
        [SerializeField] private string _keyPrefix = string.Empty;

        [Tooltip("Show the member name when it has no entry; cleared, the key is shown instead.")]
        [SerializeField] private bool _fallbackToName = true;

        /// <remarks>
        /// For deserialization only: Unity assigns the fields itself.
        /// </remarks>
        protected LocalizedEnumConverter() { }

        /// <param name="table">The string table the keys are looked up in.</param>
        /// <param name="keyPrefix">Placed before the member name to form the key.</param>
        /// <param name="fallbackToName">
        /// When <see langword="true"/>, a member with no entry shows its name; otherwise it shows the
        /// key. Either way the miss is reported as an error.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="table"/> is <see langword="null"/>.</exception>
        public LocalizedEnumConverter(
            LocalizedStringTable table,
            string? keyPrefix = null,
            bool fallbackToName = true)
        {
            _fallbackToName = fallbackToName;
            _keyPrefix = keyPrefix ?? string.Empty;
            _table = table ?? throw new ArgumentNullException(nameof(table));
        }

        /// <summary>
        /// Looks the specified member up.
        /// </summary>
        /// <param name="value">The member to localize.</param>
        /// <returns>
        /// The localized text; otherwise the member name, or the key when the fallback to the name is
        /// off. Every miss is reported as an error, including the one where no table is assigned.
        /// </returns>
        public string Convert(TEnum value)
        {
            var name = Enum.GetName(typeof(TEnum), value) ?? value.ToString();
            var key = _keyPrefix + name;
            var fallback = _fallbackToName ? name : key;

            var table = _table.GetTable();

            if (table is null)
            {
                this.LogError(
                    problem: "no string table is assigned",
                    consequence: $"Showing {fallback.Describe()} instead.");

                return fallback;
            }

            var entry = table.GetEntry(key);
            if (entry is not null) return entry.GetLocalizedString();

            this.LogError(
                problem: $"the table holds no entry for {key.Describe()}",
                consequence: $"Showing {fallback.Describe()} instead.");

            return fallback;
        }
    }
}
#endif
