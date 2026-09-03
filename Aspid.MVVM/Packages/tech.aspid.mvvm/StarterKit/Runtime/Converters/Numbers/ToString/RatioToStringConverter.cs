#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Formats a number against a maximum: "35 / 100".
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To String",
        Name = "Ratio",
        Tooltip = "Formats a number against a maximum: '35 / 100'")]
    public sealed class RatioToStringConverter :
        IConverter<float, string>,
        IConverter<int, string>,
        IConverter<long, string>,
        IConverter<double, string>
    {
        private const string DefaultFormat = "{0} / {1}";

        [Tooltip("The value the number is shown against.")]
        [SerializeField] private float _max = 100f;

        [Tooltip("A composite format: {0} is the value, {1} the maximum.")]
        [SerializeField] private string _format = DefaultFormat;

        [Tooltip("Round both numbers to whole values. A half lands on the nearest even one.")]
        [SerializeField] private bool _round = true;

        [Tooltip("The culture the numbers are formatted with.")]
        [SerializeField] private CultureInfoMode _culture = CultureInfoMode.CurrentCulture;

        /// <remarks>Default: against 100.</remarks>
        public RatioToStringConverter() { }

        /// <param name="max">The value the number is shown against.</param>
        /// <param name="format">A composite format: <c>{0}</c> is the value, <c>{1}</c> the maximum. A blank or invalid one falls back to a slash.</param>
        public RatioToStringConverter(
            float max,
            string format = DefaultFormat)
        {
            _max = max;
            _format = format;
        }

        /// <summary>
        /// Formats the specified value against the authored maximum.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>The formatted ratio, or the default layout when the format is unusable.</returns>
        public string Convert(float value) => _round
            ? Write(Mathf.RoundToInt(value), Max())
            : Write(value, Max());

        string IConverter<int, string>.Convert(int value) =>
            Write(value, Max());

        string IConverter<long, string>.Convert(long value) =>
            Write(value, Max());

        string IConverter<double, string>.Convert(double value) => _round
            ? Write(NumericSaturation.ToLong(Math.Round(value)), Max())
            : Write(value, Max());

        private object Max() => _round
            ? Mathf.RoundToInt(_max)
            : (object)_max;

        private string Write(object left, object right)
        {
            var culture = _culture.ToCultureInfo();

            try
            {
                return string.Format(culture, Layout(), left, right);
            }
            catch (FormatException exception)
            {
                this.LogError(
                    problem: $"{_format.Describe()} is not a composite format ({exception.Message})",
                    consequence: "Writing the two numbers with a slash between them.");

                return string.Format(culture, DefaultFormat, left, right);
            }
        }

        private string Layout()
        {
            if (!string.IsNullOrWhiteSpace(_format)) return _format;

            this.LogError(
                problem: "the composite format is blank",
                consequence: "Writing the two numbers with a slash between them.");

            return DefaultFormat;
        }
    }
}
