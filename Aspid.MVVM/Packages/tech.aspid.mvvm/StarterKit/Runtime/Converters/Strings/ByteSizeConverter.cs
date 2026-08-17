using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a byte count as a readable size.
    /// </summary>
    [Serializable]
    public sealed class ByteSizeConverter : IConverter<long, string>
    {
        [Tooltip("Use 1024 as the step and KiB-style units rather than 1000 and KB.")]
        [SerializeField] private bool _binaryUnits = true;

        [Tooltip("How many decimals to show.")]
        [SerializeField] private int _decimals = 1;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        private static readonly string[] BinaryUnits = { "B", "KiB", "MiB", "GiB", "TiB" };
        private static readonly string[] DecimalUnits = { "B", "KB", "MB", "GB", "TB" };

        /// <summary>
        /// Formats the specified byte count.
        /// </summary>
        /// <param name="value">The number of bytes.</param>
        /// <returns>The formatted size.</returns>
        public string Convert(long value)
        {
            var units = _binaryUnits ? BinaryUnits : DecimalUnits;
            var step = _binaryUnits ? 1024d : 1000d;

            var magnitude = (double)Math.Abs(value);
            var tier = 0;

            while (magnitude >= step && tier < units.Length - 1)
            {
                magnitude /= step;
                tier++;
            }

            var format = tier == 0 ? "F0" : "F" + Math.Max(0, _decimals);
            var text = magnitude.ToString(format, _culture.ToCultureInfo());

            return (value < 0 ? "-" : string.Empty) + text + " " + units[tier];
        }
    }
}
