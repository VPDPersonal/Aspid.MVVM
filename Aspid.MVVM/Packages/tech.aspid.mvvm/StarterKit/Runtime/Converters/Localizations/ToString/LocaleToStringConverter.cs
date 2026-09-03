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
    /// Writes the name of a locale.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Localization",
        Name = "Locale To String",
        Tooltip = "Writes the name of a locale")]
    public sealed class LocaleToStringConverter : IConverter<Locale?, string>
    {
        [Tooltip("Use the locale's native name rather than its English name. Without a culture, LocaleName is used.")]
        [SerializeField] private bool _nativeName = true;

        [Tooltip("Shown when there is no locale.")]
        [SerializeField] private string _fallback = string.Empty;

        /// <remarks>Default: the locale's own name for itself, and an empty string for no locale.</remarks>
        public LocaleToStringConverter() { }

        /// <param name="nativeName">
        /// Whether to use the locale's own name for itself rather than its English name. A locale with
        /// no culture behind it is named by its own <see cref="Locale.LocaleName"/> either way.
        /// </param>
        /// <param name="fallback">
        /// Shown when there is no locale, or <see langword="null"/> to show nothing.
        /// </param>
        public LocaleToStringConverter(
            bool nativeName,
            string? fallback = null)
        {
            _nativeName = nativeName;
            _fallback = fallback ?? string.Empty;
        }

        /// <summary>
        /// Writes the name of the specified locale.
        /// </summary>
        /// <param name="value">The locale to name.</param>
        /// <returns>
        /// Its native or English name; its <see cref="Locale.LocaleName"/> when no culture stands
        /// behind it; or the fallback when the locale is missing or destroyed.
        /// </returns>
        public string Convert(Locale? value)
        {
            if (value == null) return _fallback;

            var culture = value.Identifier.CultureInfo;
            if (culture is null) return value.LocaleName;

            return _nativeName
                ? culture.NativeName
                : culture.EnglishName;
        }
    }
}
#endif
