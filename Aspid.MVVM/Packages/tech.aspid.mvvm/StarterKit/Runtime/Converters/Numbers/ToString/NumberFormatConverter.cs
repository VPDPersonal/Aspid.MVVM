using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number with a standard .NET format string.
    /// </summary>
    /// <remarks>
    /// The format is the specifier itself — <c>N0</c>, not the composite <c>{0:N0}</c>. A format string
    /// .NET refuses is reported as an error and the general format is used instead.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Format",
        Tooltip = "Formats a number with a standard .NET format string")]
    public sealed class NumberFormatConverter :
        IConverter<float, string>,
        IConverter<double, string>,
        IConverter<int, string>,
        IConverter<long, string>
    {
        [Tooltip("A standard numeric format string: N0, F2, P1, C2.")]
        [SerializeField] private string _format = "N0";

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: formatting with thousands separators.</remarks>
        public NumberFormatConverter() { }

        /// <param name="format">
        /// A standard numeric format string. One .NET refuses falls back to the general format and is
        /// reported as an error.
        /// </param>
        /// <param name="culture">The culture the number is formatted with.</param>
        public NumberFormatConverter(string format, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
        }

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(float value) => Format(value);

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(double value) => Format(value);

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(int value) => Format(value);

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(long value) => Format(value);

        // Constrained to a struct so the four overloads share one body without boxing the number.
        private string Format<TNumber>(TNumber value)
            where TNumber : struct, IFormattable
        {
            var culture = _culture.ToCultureInfo();

            try
            {
                return value.ToString(_format, culture);
            }
            catch (FormatException exception)
            {
                this.LogError(
                    problem: $"{_format.Describe()} is not a numeric format ({exception.Message})",
                    consequence: "Falling back to the general format.");

                // An empty format string is the general format, and IFormattable needs one.
                return value.ToString(string.Empty, culture);
            }
        }
    }
}
