#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using UnityEngine.Localization.Settings;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number with the culture of the selected locale.
    /// </summary>
    /// <remarks>
    /// <see cref="CultureInfoMode"/> can name the device's culture but not the game's — and a player
    /// who chose German inside a game running on an English device expects German numbers. This is
    /// the option that mode cannot express.
    /// </remarks>
    [Serializable]
    public sealed class LocalizedNumberConverter : IConverter<double, string>
    {
        [Tooltip("A standard numeric format string.")]
        [SerializeField] private string _format = "N0";

        public LocalizedNumberConverter() { }

        /// <param name="format">A standard numeric format string.</param>
        public LocalizedNumberConverter(string format)
        {
            _format = format;
        }

        /// <summary>
        /// Formats the specified number with the selected locale's culture.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The formatted number.</returns>
        public string Convert(double value)
        {
            var culture = LocalizationSettings.SelectedLocaleAsync.IsDone
                ? LocalizationSettings.SelectedLocale?.Identifier.CultureInfo
                : null;

            return value.ToString(_format, culture ?? System.Globalization.CultureInfo.CurrentCulture);
        }
    }
}
#endif
