#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Applies two converters in sequence, converting through an intermediate type.
    /// </summary>
    /// <typeparam name="TFrom">The type of the input value.</typeparam>
    /// <typeparam name="TMid">The intermediate type the first converter produces.</typeparam>
    /// <typeparam name="TTo">The type of the converted output value.</typeparam>
    /// <remarks>
    /// Both links are required: the types on either side need not match, so a missing link leaves
    /// nothing meaningful to return.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Composition",
        Name = "Compose",
        Tooltip = "Applies two converters in sequence, converting through an intermediate type")]
    public class ComposeConverter<TFrom, TMid, TTo> : ITwoWayConverter<TFrom?, TTo?>
    {
        [Tooltip("Applied to the incoming value. Required.")]
        [TypeSelector]
        [SerializeReference] private IConverter<TFrom?, TMid?>? _first;

        [Tooltip("Applied to the result of the first link. Required.")]
        [TypeSelector]
        [SerializeReference] private IConverter<TMid?, TTo?>? _second;

        [Tooltip("Returned from Convert Back when either link converts one way only.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private ConverterFallback<TFrom?> _convertBackFallback = new(default, ConverterFailureMode.ReturnInput);

        protected ComposeConverter() { }

        /// <param name="first">The converter applied to the input value. Both links are required.</param>
        /// <param name="second">The converter applied to the result of <paramref name="first"/>. Both links are required.</param>
        /// <param name="convertBackFallback">
        /// Returned from <see cref="ConvertBack"/> when either link converts one way only.
        /// When omitted, returns the input value unchanged.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="first"/> or <paramref name="second"/> is <see langword="null"/>.
        /// </exception>
        public ComposeConverter(
            IConverter<TFrom?, TMid?> first,
            IConverter<TMid?, TTo?> second,
            ConverterFallback<TFrom?>? convertBackFallback = null)
        {
            _first = first ?? throw new ArgumentNullException(nameof(first));
            _second = second ?? throw new ArgumentNullException(nameof(second));
            _convertBackFallback = convertBackFallback ?? _convertBackFallback;
        }

        /// <summary>
        /// Converts the specified value through both links.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The result of the second converter, or the default value when either link is missing.</returns>
        public TTo? Convert(TFrom? value)
        {
            if (_first is not null && _second is not null)
                return _second.Convert(_first.Convert(value));

            this.LogError(
                problem: "both links are required, and one is missing",
                consequence: "Returning the default value.");

            return default;
        }

        /// <summary>
        /// Undoes the second link, then the first.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <returns>
        /// The value with both links undone, or the fallback when either one converts one way only
        /// or is missing.
        /// </returns>
        /// <remarks>A one-way link is reported and neither link is undone.</remarks>
        public TFrom? ConvertBack(TTo? value)
        {
            if (_first is ITwoWayConverter<TFrom?, TMid?> first
                && _second is ITwoWayConverter<TMid?, TTo?> second)
            {
                return first.ConvertBack(second.ConvertBack(value));
            }

            return _convertBackFallback.Fail(
                converter: this,
                value: value,
                problem: ReverseProblem());
        }

        private string ReverseProblem()
        {
            if (_first is null || _second is null)
                return "both links are required, and one is missing";

            IConverter oneWay = _first is ITwoWayConverter<TFrom?, TMid?>
                ? _second
                : _first;

            var typename = oneWay.GetType().GetTypeName();
            return $"{typename} converts one way only, so the composition cannot be undone";
        }
    }
}
