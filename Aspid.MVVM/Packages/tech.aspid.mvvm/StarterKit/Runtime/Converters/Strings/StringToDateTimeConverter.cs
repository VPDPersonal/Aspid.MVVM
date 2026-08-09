using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a date out of text.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "String To Date Time", Tooltip = "Reads a date out of text")]
    public sealed class StringToDateTimeConverter : IConverter<string?, DateTime>
    {
        [Tooltip("The exact format expected. When empty, any format the culture understands is accepted.")]
        [SerializeField] private string _format = string.Empty;

        [Tooltip("The culture the text is read with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [Tooltip("Ticks of the date returned when the text is not one.")]
        [SerializeField] private long _fallbackTicks;

        /// <remarks>Default: accepting any format.</remarks>
        public StringToDateTimeConverter() { }

        /// <param name="format">The exact format expected.</param>
        /// <param name="fallback">Returned when the text is not a date.</param>
        public StringToDateTimeConverter(string format, DateTime fallback = default)
        {
            _format = format;
            _fallbackTicks = fallback.Ticks;
        }

        /// <summary>
        /// Reads a date out of the specified text.
        /// </summary>
        /// <param name="value">The text to read.</param>
        /// <returns>The date, or the fallback when the text is not one.</returns>
        public DateTime Convert(string? value)
        {
            var culture = _culture.ToCultureInfo();
            var fallback = new DateTime(_fallbackTicks);

            if (string.IsNullOrWhiteSpace(value)) return fallback;

            return string.IsNullOrWhiteSpace(_format)
                ? DateTime.TryParse(value, culture, DateTimeStyles.None, out var any) ? any : fallback
                : DateTime.TryParseExact(value, _format, culture, DateTimeStyles.None, out var exact) ? exact : fallback;
        }
    }
}
