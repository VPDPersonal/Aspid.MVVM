#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Remembers the last conversion and reuses it while the input is unchanged.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// Wrap only a pure converter. Inputs are compared by default equality, so a reference mutated
    /// in place counts as unchanged. The two directions cache separately.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Cached",
        Tooltip = "Remembers the last conversion and reuses it while the input is unchanged")]
    public class CachedConverter<TFrom, TTo> : ITwoWayConverter<TFrom?, TTo?>
    {
        [Tooltip("The converter to memoize. Required.")]
        [SerializeReference] private IConverter<TFrom?, TTo?>? _inner;

        [NonSerialized] private bool _hasCache;
        [NonSerialized] private TTo? _lastOutput;
        [NonSerialized] private TFrom? _lastInput;

        [NonSerialized] private bool _hasBackCache;
        [NonSerialized] private TFrom? _lastBackOutput;
        [NonSerialized] private TTo? _lastBackInput;

        protected CachedConverter() { }

        /// <param name="inner">The converter to memoize.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="inner"/> is <see langword="null"/>.</exception>
        public CachedConverter(IConverter<TFrom?, TTo?> inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        /// <summary>
        /// Converts the specified value, reusing the previous result when the input is unchanged.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value, or the default value when the inner converter is missing.</returns>
        public TTo? Convert(TFrom? value)
        {
            if (_inner is null)
            {
                ReportMissing();
                return default;
            }

            if (_hasCache && EqualityComparer<TFrom?>.Default.Equals(_lastInput, value))
                return _lastOutput;

            var output = _inner.Convert(value);

            _hasCache = true;
            _lastInput = value;
            _lastOutput = output;

            return output;
        }

        /// <summary>
        /// Converts the specified value back, reusing the previous result when the input is unchanged.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <returns>
        /// The value converted back, or the default value when the inner converter is missing or
        /// converts one way only.
        /// </returns>
        public TFrom? ConvertBack(TTo? value)
        {
            if (_inner is null)
            {
                ReportMissing();
                return default;
            }

            if (_inner is not ITwoWayConverter<TFrom?, TTo?> inner)
            {
                var converterName = _inner.GetType().GetTypeName();

                this.LogError(
                    problem: $"{converterName} converts one way only, so the conversion cannot be undone",
                    consequence: "Returning the default value.");

                return default;
            }

            if (_hasBackCache && EqualityComparer<TTo?>.Default.Equals(_lastBackInput, value))
                return _lastBackOutput;

            var output = inner.ConvertBack(value);

            _hasBackCache = true;
            _lastBackInput = value;
            _lastBackOutput = output;

            return output;
        }

        private void ReportMissing() => this.LogError(
            problem: "the inner converter is required, and it is missing",
            consequence: "Returning the default value.");
    }
}
