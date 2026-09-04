#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies multiple converters to a value in sequence.
    /// </summary>
    /// <typeparam name="T">The type of the value passing through the chain.</typeparam>
    /// <remarks>Empty slots in the chain are skipped, not treated as an error.</remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Sequence",
        Tooltip = "Applies multiple converters to a value in sequence")]
    public class SequenceConverter<T> : ITwoWayConverter<T?, T?>
    {
        [Tooltip("The converters applied in order. Empty slots are skipped.")]
        [SerializeReference] private IConverter<T?, T?>?[]? _converters = Array.Empty<IConverter<T?, T?>>();

        [Tooltip("Returned from Convert Back when a link in the chain converts one way only.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private ConverterFallback<T?> _convertBackFallback = new(default, ConverterFailureMode.ReturnInput);

        /// <remarks>Default: an empty chain, the value passes through.</remarks>
        public SequenceConverter() { }

        /// <param name="converters">The converters to apply in sequence. Empty slots are skipped. The array is copied.</param>
        public SequenceConverter(params IConverter<T?, T?>[]? converters)
            : this(convertBackFallback: null, converters) { }

        /// <param name="convertBackFallback">
        /// Returned from <see cref="ConvertBack"/> when a link in the chain converts one way only.
        /// When omitted, returns the input value unchanged.
        /// </param>
        /// <param name="converters">The converters to apply in sequence. Empty slots are skipped. The array is copied.</param>
        public SequenceConverter(
            ConverterFallback<T?>? convertBackFallback,
            params IConverter<T?, T?>[]? converters)
        {
            _convertBackFallback = convertBackFallback ?? _convertBackFallback;
            _converters = converters is null or { Length: 0 }
                ? converters
                : (IConverter<T?, T?>?[])converters.Clone();
        }

        /// <summary>
        /// Applies each converter in order.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value after the last converter.</returns>
        public T? Convert(T? value)
        {
            if (_converters is null) return value;

            foreach (var converter in _converters)
            {
                if (converter is not null)
                    value = converter.Convert(value);
            }

            return value;
        }

        /// <summary>
        /// Undoes each converter in reverse order.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <returns>The value with every link undone, or the fallback if any link converts one way only.</returns>
        /// <remarks>A one-way link is reported and nothing is undone.</remarks>
        public T? ConvertBack(T? value)
        {
            if (_converters is null) return value;
            var oneWay = FindOneWay();

            if (oneWay is not null)
            {
                var converterName = oneWay.GetType().GetTypeName();

                return _convertBackFallback.Fail(
                    converter: this,
                    value: value,
                    problem: $"{converterName} converts one way only, so the chain cannot be undone");
            }

            for (var i = _converters.Length - 1; i >= 0; i--)
            {
                if (_converters[i] is ITwoWayConverter<T?, T?> twoWay)
                    value = twoWay.ConvertBack(value);
            }

            return value;
        }

        private IConverter<T?, T?>? FindOneWay()
        {
            if (_converters is null) return null;

            foreach (var converter in _converters)
            {
                if (converter is not null and not ITwoWayConverter<T?, T?>)
                    return converter;
            }

            return null;
        }
    }
}
