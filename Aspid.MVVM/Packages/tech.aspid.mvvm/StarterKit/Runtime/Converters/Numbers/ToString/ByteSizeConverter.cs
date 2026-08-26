using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a byte count as a readable size.
    /// </summary>
    /// <remarks>
    /// A float or double input is counted as whole bytes: the fraction is truncated toward zero.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Byte Size",
        Tooltip = "Formats a byte count as a readable size")]
    public sealed class ByteSizeConverter :
        IConverter<long, string>,
        IConverter<int, string>,
        IConverter<float, string>,
        IConverter<double, string>
    {
        private static readonly string[] _binaryUnitNames = { "B", "KiB", "MiB", "GiB", "TiB" };
        private static readonly string[] _decimalUnitNames = { "B", "KB", "MB", "GB", "TB" };

        [Tooltip("Use 1024 and KiB-style units rather than 1000 and KB.")]
        [SerializeField] private bool _binaryUnits = true;

        [Tooltip("How many decimals to show.")]
        [SerializeField] [Min(0)] private int _decimals = 1;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: binary units with one decimal.</remarks>
        public ByteSizeConverter() { }

        /// <param name="binaryUnits">
        /// When <see langword="true"/>, uses 1024 and KiB-style units rather than 1000 and KB.
        /// </param>
        /// <param name="decimals">How many decimals to show.</param>
        public ByteSizeConverter(bool binaryUnits, int decimals = 1)
        {
            _binaryUnits = binaryUnits;
            _decimals = decimals;
        }

        /// <summary>
        /// Formats the specified byte count.
        /// </summary>
        /// <param name="value">The number of bytes.</param>
        /// <returns>The formatted size.</returns>
        public string Convert(long value)
        {
            var units = _binaryUnits ? _binaryUnitNames : _decimalUnitNames;
            var step = _binaryUnits ? 1024d : 1000d;

            // Negated as a double: long.MinValue has no positive counterpart of its own width.
            var magnitude = Math.Abs((double)value);
            var tier = 0;

            while (magnitude >= step && tier < units.Length - 1)
            {
                magnitude /= step;
                tier++;
            }

            // The decimals can carry the magnitude up to the next unit: 1 048 530 bytes written with
            // one of them is 1024.0 KiB, which belongs a unit higher.
            if (tier > 0 && tier < units.Length - 1 && Rounded(magnitude) >= step)
            {
                magnitude /= step;
                tier++;
            }

            // Bytes are whole, so the decimals start at the first scaled unit.
            var format = tier == 0 ? "F0" : NumericFormat.Fixed(_decimals);
            var text = magnitude.ToString(format, _culture.ToCultureInfo());

            return (value < 0 ? "-" : string.Empty) + text + " " + units[tier];
        }

        string IConverter<int, string>.Convert(int value) => Convert(value);

        string IConverter<float, string>.Convert(float value) => Convert(NumericSaturation.ToLong(value));

        string IConverter<double, string>.Convert(double value) => Convert(NumericSaturation.ToLong(value));

        // Math.Round takes at most 15 places, and the field is authored.
        private double Rounded(double value) =>
            Math.Round(value, Math.Min(15, Math.Max(0, _decimals)), MidpointRounding.AwayFromZero);
    }
}
