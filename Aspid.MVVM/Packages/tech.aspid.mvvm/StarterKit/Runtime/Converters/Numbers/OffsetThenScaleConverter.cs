#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Adds a constant to a number and scales the sum.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number",
        Name = "Offset Then Scale",
        Tooltip = "Adds a constant to a number and scales the sum")]
    public sealed class OffsetThenScaleConverter : TwoWayNumberConverter
    {
        [Tooltip("Added to the value first.")]
        [SerializeField] private float _offset;

        [Tooltip("Multiplies the sum. A scale of zero cannot be reversed.")]
        [SerializeField] private float _scale = 1f;

        [Tooltip("Returned from Convert Back when the scale is zero.")]
        [UsedInModes(BindMode.TwoWay, BindMode.OneWayToSource)]
        [SerializeField] private ConverterFallback<double> _convertBackFallback = new(0d, ConverterFailureMode.ReturnInput);

        /// <remarks>Default: no offset and a scale of one.</remarks>
        public OffsetThenScaleConverter() { }

        /// <param name="offset">Added to the value first.</param>
        /// <param name="scale">Multiplies the sum. A scale of zero cannot be reversed.</param>
        /// <param name="convertBackFallback">
        /// Returned from <c>ConvertBack</c> when the scale is zero. When omitted, returns the input value unchanged.
        /// </param>
        public OffsetThenScaleConverter(
            float offset,
            float scale = 1f,
            ConverterFallback<double>? convertBackFallback = null)
        {
            _scale = scale;
            _offset = offset;
            _convertBackFallback = convertBackFallback ?? _convertBackFallback;
        }

        /// <summary>
        /// Adds the offset and scales the sum.
        /// </summary>
        /// <param name="value">The number to transform.</param>
        /// <returns>The transformed number.</returns>
        protected override double Apply(double value) =>
            (value + _offset) * _scale;

        /// <summary>
        /// Divides by the scale and removes the offset.
        /// </summary>
        /// <param name="value">The number to transform back.</param>
        /// <returns>The number the forward pass was given, or the fallback for a zero scale.</returns>
        protected override double Undo(double value)
        {
            if (_scale is not 0f) return value / _scale - _offset;

            return _convertBackFallback.Fail(
                converter: this,
                value: value,
                problem: "the scale is zero, which discards the value the forward pass was given");
        }
    }
}
