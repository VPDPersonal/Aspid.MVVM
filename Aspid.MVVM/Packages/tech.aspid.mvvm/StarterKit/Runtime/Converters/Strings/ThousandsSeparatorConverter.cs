using Aspid.FastTools.Types;
using System;
using UnityEngine;
using System.Globalization;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Groups the digits of a whole number: 1234567 becomes "1,234,567".
    /// </summary>
    /// <remarks>
    /// <see cref="NumberFormatConverter"/> with <c>"N0"</c> reaches the same place when the player's
    /// own separator is what is wanted. This exists for the case it cannot express: a separator the
    /// game picks — a thin space, an apostrophe — used whatever locale the player runs in, so a score
    /// reads the same on every machine and in every screenshot.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/String", Name = "Thousands Separator", Tooltip = "Groups the digits of a whole number: 1234567 becomes '1,234,567'")]
    public sealed class ThousandsSeparatorConverter : IConverter<long, string>, IConverter<int, string>
    {
        [Tooltip("Placed between groups of digits. When empty, the culture's own separator is used.")]
        [SerializeField] private string _separator = string.Empty;

        [Tooltip("The culture the number is formatted with. It supplies the separator when the field "
            + "above is empty, and decides how many digits go in a group either way.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        // Carrying an authored separator means cloning the culture's NumberFormatInfo, and a binder
        // pushes on every notification rather than on every change. The clone is kept and rebuilt
        // only when the culture or the separator it was made for is no longer the current one.
        [NonSerialized] private NumberFormatInfo? _format;
        [NonSerialized] private CultureInfo? _formatCulture;
        [NonSerialized] private string? _formatSeparator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThousandsSeparatorConverter"/> class using the culture's separator.
        /// </summary>
        public ThousandsSeparatorConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThousandsSeparatorConverter"/> class.
        /// </summary>
        /// <param name="separator">Placed between groups of digits. When empty, the culture's own separator is used.</param>
        /// <param name="culture">The culture the number is formatted with.</param>
        public ThousandsSeparatorConverter(string separator, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _separator = separator;
            _culture = culture;
        }

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(long value) => value.ToString("N0", Format());

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(int value) => value.ToString("N0", Format());

        private NumberFormatInfo Format()
        {
            var culture = _culture.ToCultureInfo();
            if (string.IsNullOrEmpty(_separator)) return culture.NumberFormat;

            if (_format is not null
                && ReferenceEquals(_formatCulture, culture)
                && string.Equals(_formatSeparator, _separator, StringComparison.Ordinal))
                return _format;

            // A NumberFormatInfo taken from a culture is read-only; its clone is not.
            var format = (NumberFormatInfo)culture.NumberFormat.Clone();
            format.NumberGroupSeparator = _separator;

            _format = format;
            _formatCulture = culture;
            _formatSeparator = _separator;

            return format;
        }
    }
}
