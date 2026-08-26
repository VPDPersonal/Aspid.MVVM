#if ASPID_MVVM_UNITY_LOCALIZATION_INTEGRATION
#nullable enable
using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;
using UnityEngine.Localization.Settings;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number with the culture of the selected locale.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Localization",
        Name = "Localized Number",
        Tooltip = "Formats a number with the culture of the selected locale")]
    public sealed class LocalizedNumberConverter :
        IConverter<double, string>,
        IConverter<int, string>,
        IConverter<long, string>,
        IConverter<float, string>
    {
        [Tooltip("A standard numeric format string.")]
        [SerializeField] private string _format = "N0";

        /// <remarks>Default: formatting with thousands separators.</remarks>
        public LocalizedNumberConverter() { }

        /// <param name="format">
        /// A standard numeric format string. One .NET refuses is reported as an error and the general
        /// format is used instead.
        /// </param>
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

            culture ??= CultureInfo.CurrentCulture;

            try
            {
                return value.ToString(_format, culture);
            }
            catch (FormatException exception)
            {
                this.LogError($"{_format.Describe()} is not a numeric format ({exception.Message})",
                    "Falling back to the general format.");

                return value.ToString(culture);
            }
        }

        string IConverter<int, string>.Convert(int value) => Convert(value);

        string IConverter<long, string>.Convert(long value) => Convert(value);

        string IConverter<float, string>.Convert(float value) => Convert(value);
    }
}
#endif
