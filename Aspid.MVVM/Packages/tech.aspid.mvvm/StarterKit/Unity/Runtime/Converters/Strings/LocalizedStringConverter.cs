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
    /// Looks a key up in a localization table.
    /// </summary>
    /// <remarks>
    /// Only compiled when <c>com.unity.localization</c> is installed.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Localization",
        Name = "Localized String",
        Tooltip = "Looks a key up in a localization table")]
    public sealed class LocalizedStringConverter : IConverter<string?, string?>
    {
        [Tooltip("The string table the key is looked up in.")]
        [SerializeField] private LocalizedStringTable _table = new();

        [Tooltip("Show the key itself when it has no entry, rather than the missing format.")]
        [SerializeField] private bool _fallbackToKey = true;

        [Tooltip("A composite format for a missing entry: {0} is the key.")]
        [SerializeField] private string _missingFormat = "#{0}#";

        /// <remarks>Default: showing the key itself when it has no entry.</remarks>
        public LocalizedStringConverter() { }

        /// <summary>
        /// Looks the specified key up.
        /// </summary>
        /// <param name="value">The key to look up.</param>
        /// <returns>
        /// The localized text, the key itself, or the missing format — whichever the settings call
        /// for. A blank key, spaces included, comes back unchanged. A lookup with no table assigned
        /// is reported as an error.
        /// </returns>
        public string? Convert(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var table = _table.GetTable();

            if (table is null)
            {
                var missing = Missing(value);

                this.LogError(
                    problem: "no string table is assigned",
                    consequence: $"Showing {missing.Describe()} instead.");

                return missing;
            }

            var entry = table.GetEntry(value);

            return entry is not null ? entry.GetLocalizedString() : Missing(value);
        }

        private string Missing(string value)
        {
            if (_fallbackToKey || string.IsNullOrWhiteSpace(_missingFormat)) return value;

            try
            {
                return string.Format(_missingFormat, value);
            }
            catch (FormatException exception)
            {
                this.LogError(
                    problem: $"{_missingFormat.Describe()} is not a composite format ({exception.Message})",
                    consequence: "Showing the key instead.");

                return value;
            }
        }
    }
}
#endif
