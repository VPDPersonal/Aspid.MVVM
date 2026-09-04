#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Runs a two-way converter in the opposite direction.
    /// </summary>
    /// <typeparam name="TFrom">The type the wrapped converter converts from.</typeparam>
    /// <typeparam name="TTo">The type the wrapped converter converts to.</typeparam>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Inverse",
        Tooltip = "Runs a two-way converter in the opposite direction")]
    public class InverseConverter<TFrom, TTo> : ITwoWayConverter<TTo?, TFrom?>
    {
        [Tooltip("The two-way converter to run in the opposite direction. Required.")]
        [SerializeReference] private ITwoWayConverter<TFrom?, TTo?>? _converter;

        protected InverseConverter() { }

        /// <param name="converter">The two-way converter to run in the opposite direction.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="converter"/> is <see langword="null"/>.
        /// </exception>
        public InverseConverter(ITwoWayConverter<TFrom?, TTo?> converter)
        {
            _converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        /// <summary>
        /// Converts the specified value with the wrapped converter's reverse direction.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// What the wrapped converter's <c>ConvertBack</c> answers, or the default value when the
        /// converter is missing.
        /// </returns>
        public TFrom? Convert(TTo? value)
        {
            if (_converter is not null)
                return _converter.ConvertBack(value);

            ReportMissing();
            return default;
        }

        /// <summary>
        /// Converts a value back with the wrapped converter's forward direction.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <returns>
        /// What the wrapped converter's <c>Convert</c> answers, or the default value when the
        /// converter is missing.
        /// </returns>
        public TTo? ConvertBack(TFrom? value)
        {
            if (_converter is not null)
                return _converter.Convert(value);

            ReportMissing();
            return default;
        }

        private void ReportMissing() => this.LogError(
            problem: "the converter is required, and it is missing",
            consequence: "Returning the default value.");
    }
}
