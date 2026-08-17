using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number with a standard .NET format string.
    /// </summary>
    /// <remarks>
    /// <see cref="GenericToString{TFrom}"/> takes a <i>composite</i> format, so <c>"N0"</c> comes
    /// back as the literal <c>N0</c> and the specifier has to be spelled <c>{0:N0}</c>. This takes
    /// the specifier everyone expects — the one on <see cref="int.ToString(string)"/>.
    /// </remarks>
    [Serializable]
    public sealed class NumberFormatConverter :
        IConverter<float, string>,
        IConverter<double, string>,
        IConverter<int, string>,
        IConverter<long, string>
    {
        [Tooltip("A standard numeric format string: N0 for thousands separators, F2 for two decimals, P1 for a percentage, C2 for currency.")]
        [SerializeField] private string _format = "N0";

        [Tooltip("The culture the number is formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: formatting with thousands separators.</remarks>
        public NumberFormatConverter() { }

        /// <param name="format">A standard numeric format string.</param>
        /// <param name="culture">The culture the number is formatted with.</param>
        public NumberFormatConverter(string format, CultureInfoMode culture = CultureInfoMode.CurrentCulture)
        {
            _format = format;
            _culture = culture;
        }

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(float value) => value.ToString(_format, _culture.ToCultureInfo());

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(double value) => value.ToString(_format, _culture.ToCultureInfo());

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(int value) => value.ToString(_format, _culture.ToCultureInfo());

        /// <inheritdoc cref="IConverter{TFrom,TTo}.Convert"/>
        public string Convert(long value) => value.ToString(_format, _culture.ToCultureInfo());
    }
}
