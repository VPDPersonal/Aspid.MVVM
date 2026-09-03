#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number with an explicit sign: "+15", "-3".
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Signed",
        Tooltip = "Formats a number with an explicit sign: '+15', '-3'")]
    public sealed class SignedNumberStringConverter :
        IConverter<float, string>,
        IConverter<int, string>,
        IConverter<long, string>,
        IConverter<double, string>
    {
        [Tooltip("A numeric format string for the magnitude.")]
        [SerializeField] private string _format = "0.##";

        [Tooltip("Show a plus on positive numbers.")]
        [SerializeField] private bool _alwaysShowSign = true;

        [Tooltip("Return an empty string for zero.")]
        [SerializeField] private bool _hideZero;

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: showing a plus on positive numbers.</remarks>
        public SignedNumberStringConverter() { }

        /// <param name="format">A numeric format string for the magnitude. One .NET refuses falls back to the general format.</param>
        /// <param name="hideZero">If <see langword="true"/>, returns an empty string for zero.</param>
        public SignedNumberStringConverter(
            string format,
            bool hideZero = false)
        {
            _format = format;
            _hideZero = hideZero;
        }

        /// <summary>
        /// Formats the specified number with its sign.
        /// </summary>
        /// <param name="value">The number to format.</param>
        /// <returns>The formatted number, or the general rendering when the format is unusable.</returns>
        public string Convert(float value) =>
            Write(Math.Abs(value), value < 0f, value is 0f);

        string IConverter<double, string>.Convert(double value) =>
            Write(Math.Abs(value), value < 0d, value is 0d);

        string IConverter<int, string>.Convert(int value) =>
            Write(Magnitude(value), value < 0, value == 0);

        string IConverter<long, string>.Convert(long value) =>
            Write(Magnitude(value), value < 0L, value == 0L);

        // Unsigned rather than negated: long.MinValue has no positive counterpart.
        private static ulong Magnitude(long value) =>
            value < 0 ? unchecked((ulong)-value) : (ulong)value;

        private string Write<TNumber>(TNumber magnitude, bool negative, bool zero)
            where TNumber : struct, IFormattable
        {
            if (_hideZero && zero) return string.Empty;

            var text = this.FormatOrGeneral(magnitude, _format, _culture.ToCultureInfo());
            if (negative) return "-" + text;

            return _alwaysShowSign ? "+" + text : text;
        }
    }
}
