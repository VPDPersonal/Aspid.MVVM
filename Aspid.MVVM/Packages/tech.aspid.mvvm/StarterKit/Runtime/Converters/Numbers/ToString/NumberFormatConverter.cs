using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number with a standard .NET format string.
    /// </summary>
    /// <remarks>The format is the specifier itself: <c>N0</c>, not the composite <c>{0:N0}</c>.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Format",
        Tooltip = "Formats a number with a standard .NET format string")]
    public sealed class NumberFormatConverter :
        IConverter<float, string>,
        IConverter<double, string>,
        IConverter<int, string>,
        IConverter<long, string>,
        IConverter<decimal, string>
    {
        [Tooltip("A standard numeric format string: N0, F2, P1, C2.")]
        [SerializeField] private string _format = "N0";

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: formatting with thousands separators.</remarks>
        public NumberFormatConverter() { }

        /// <param name="format">A standard numeric format string. One .NET refuses falls back to the general format.</param>
        /// <param name="culture">The culture the number is formatted with.</param>
        public NumberFormatConverter(
            string format,
            CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
        }

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(float value) =>
            Format(value);

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(double value) =>
            Format(value);

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(int value) =>
            Format(value);

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(long value) =>
            Format(value);

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(decimal value) =>
            Format(value);

        private string Format<TNumber>(TNumber value)
            where TNumber : struct, IFormattable =>
            this.FormatOrGeneral(value, _format, _culture.ToCultureInfo());
    }
}
