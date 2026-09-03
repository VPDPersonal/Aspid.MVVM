#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Runs another converter and substitutes a fallback value if it throws.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>Catches every exception: a throwing converter would stop the binders queued behind it.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Safe",
        Tooltip = "Runs another converter and substitutes a fallback value if it throws")]
    public class SafeConverter<TFrom, TTo> : ITwoWayConverter<TFrom?, TTo?>
    {
        [Tooltip("The converter to run. When empty, the fallback is returned.")]
        [TypeSelector]
        [SerializeReference] private IConverter<TFrom?, TTo?>? _inner;

        [Tooltip("Returned from Convert when the converter throws or is empty.")]
        [SerializeField] private TTo? _fallback;

        [Tooltip("Returned from Convert Back when the converter throws, is one-way or empty.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private TFrom? _convertBackFallback;

        protected SafeConverter() { }

        /// <param name="inner">The converter to run.</param>
        /// <param name="fallback">Returned from <see cref="Convert"/> when <paramref name="inner"/> throws.</param>
        /// <param name="convertBackFallback">
        /// Returned from <see cref="ConvertBack"/> when <paramref name="inner"/> throws or converts one way only.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="inner"/> is <see langword="null"/>.</exception>
        public SafeConverter(
            IConverter<TFrom?, TTo?> inner,
            TTo? fallback = default,
            TFrom? convertBackFallback = default)
        {
            _fallback = fallback;
            _convertBackFallback = convertBackFallback;
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        /// <summary>
        /// Converts the specified value, substituting the fallback if the wrapped converter throws.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value, or the fallback.</returns>
        public TTo? Convert(TFrom? value)
        {
            if (_inner is null)
            {
                return this.UseFallback(
                    fallback: _fallback,
                    problem: "the inner converter is required, and it is missing");
            }

            try
            {
                return _inner.Convert(value);
            }
            catch (Exception exception)
            {
                _inner.LogError(
                    exception: exception,
                    consequence: "Returning the fallback value.");

                return _fallback;
            }
        }

        /// <summary>
        /// Converts the specified value back, substituting the reverse fallback if the wrapped
        /// converter throws.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <returns>The value converted back, or the reverse fallback when the converter throws or converts one way only.</returns>
        public TFrom? ConvertBack(TTo? value)
        {
            if (_inner is null)
            {
                return this.UseFallback(
                    fallback: _convertBackFallback,
                    problem: "the inner converter is required, and it is missing");
            }

            if (_inner is not ITwoWayConverter<TFrom?, TTo?> inner)
            {
                var converterName = _inner.GetType().GetTypeName();

                return this.UseFallback(
                    fallback: _convertBackFallback,
                    problem: $"{converterName} converts one way only, so the conversion cannot be undone");
            }

            try
            {
                return inner.ConvertBack(value);
            }
            catch (Exception exception)
            {
                _inner.LogError(
                    exception: exception,
                    consequence: "Returning the fallback value.");

                return _convertBackFallback;
            }
        }
    }
}
