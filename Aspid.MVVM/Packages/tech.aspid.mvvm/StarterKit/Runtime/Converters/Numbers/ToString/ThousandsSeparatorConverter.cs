#nullable enable
using System;
using UnityEngine;
using System.Globalization;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Groups the digits of a whole number: 1234567 becomes "1,234,567".
    /// </summary>
    /// <remarks>A float or double input is truncated to a whole number.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Thousands Separator",
        Tooltip = "Groups the digits of a whole number: 1234567 becomes '1,234,567'")]
    public sealed class ThousandsSeparatorConverter :
        IConverter<long, string>,
        IConverter<int, string>,
        IConverter<float, string>,
        IConverter<double, string>,
        ISerializationCallbackReceiver
    {
        [Tooltip("Placed between groups of digits. When empty, the culture's own separator is used.")]
        [SerializeField] private string _separator = string.Empty;

        [Tooltip("The culture the number is formatted with. Supplies the group size and the default separator.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        [NonSerialized] private NumberFormatInfo? _format;
        [NonSerialized] private CultureInfo? _formatCulture;

        /// <remarks>Default: using the culture's separator.</remarks>
        public ThousandsSeparatorConverter() { }

        /// <param name="separator">Placed between groups of digits. When empty, the culture's own is used.</param>
        /// <param name="culture">The culture the number is formatted with.</param>
        public ThousandsSeparatorConverter(
            string separator,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _culture = culture;
            _separator = separator;
        }

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(long value) =>
            value.ToString("N0", Format());

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(int value) =>
            value.ToString("N0", Format());

        string IConverter<float, string>.Convert(float value) =>
            Convert(NumericSaturation.ToLong(value));

        string IConverter<double, string>.Convert(double value) =>
            Convert(NumericSaturation.ToLong(value));

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        void ISerializationCallbackReceiver.OnAfterDeserialize() =>
            _format = null;

        private NumberFormatInfo Format()
        {
            var culture = _culture.ToCultureInfo();
            if (string.IsNullOrEmpty(_separator)) return culture.NumberFormat;

            if (_format is not null && ReferenceEquals(_formatCulture, culture)) return _format;

            var format = (NumberFormatInfo)culture.NumberFormat.Clone();
            format.NumberGroupSeparator = _separator;

            _format = format;
            _formatCulture = culture;

            return format;
        }
    }
}
