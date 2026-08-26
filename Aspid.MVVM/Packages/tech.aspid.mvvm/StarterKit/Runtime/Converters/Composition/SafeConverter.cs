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
    /// <remarks>
    /// Binder dispatch is a bare multicast: an exception from one converter cuts the subscriber list
    /// and stops the binders queued behind it.
    /// <para>
    /// It catches every exception on purpose — a containment boundary, not a filter.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Safe",
        Tooltip = "Runs another converter and substitutes a fallback value if it throws")]
    public class SafeConverter<TFrom, TTo> : ITwoWayConverter<TFrom?, TTo?>
    {
        [Tooltip("The converter to run. When empty, each direction returns its fallback.")]
        [TypeSelector]
        [SerializeReference] private IConverter<TFrom?, TTo?>? _inner;

        [Tooltip("Returned from Convert when the wrapped converter throws or is empty. Every failure is reported.")]
        [SerializeField] private TTo? _fallback;

        [Tooltip("Returned from Convert Back when the wrapped converter throws, converts one way only, or is empty. " +
            "Every failure is reported.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private TFrom? _convertBackFallback;

        protected SafeConverter() { }

        /// <param name="inner">The converter to run.</param>
        /// <param name="fallback">
        /// Returned from <see cref="Convert"/> when <paramref name="inner"/> throws. Every failure
        /// is reported.
        /// </param>
        /// <param name="convertBackFallback">
        /// Returned from <see cref="ConvertBack"/> when <paramref name="inner"/> throws or converts
        /// one way only. Every failure is reported.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="inner"/> is <see langword="null"/>. The empty shape belongs to
        /// the Inspector, which answers it with the fallback for the direction.
        /// </exception>
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
        /// <returns>
        /// The value converted back, or the reverse fallback — also returned when the wrapped
        /// converter converts one way only.
        /// </returns>
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
                var converterName = ConverterMessageText.GetTypeName(_inner.GetType());

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
