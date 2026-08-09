#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using UnityEngine.Localization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes the name of a locale.
    /// </summary>
    /// <remarks>Labelling the entries of a language dropdown.</remarks>
    [Serializable]
    public sealed class LocaleToStringConverter : IConverter<Locale?, string>
    {
        [Tooltip("Use the locale's own name for itself rather than its English name.")]
        [SerializeField] private bool _nativeName = true;

        [Tooltip("Shown when there is no locale.")]
        [SerializeField] private string _fallback = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocaleToStringConverter"/> class.
        /// </summary>
        public LocaleToStringConverter() { }

        /// <summary>
        /// Writes the name of the specified locale.
        /// </summary>
        /// <param name="value">The locale to name.</param>
        /// <returns>Its name, or the fallback when there is none.</returns>
        public string Convert(Locale? value)
        {
            if (value == null) return _fallback;

            var culture = value.Identifier.CultureInfo;
            if (culture is null) return value.LocaleName;

            return _nativeName ? culture.NativeName : culture.EnglishName;
        }
    }
}
#endif
