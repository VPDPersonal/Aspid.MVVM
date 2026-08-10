#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Adds a constant to a number and scales the sum.
    /// </summary>
    /// <remarks>
    /// <c>(x + a) * b</c> in one node. Reaching it before meant two
    /// <see cref="ArithmeticNumberConverter"/>s joined by a <see cref="ComposeConverter{TFrom, TMid, TTo}"/>,
    /// or a <see cref="RemapNumberConverter"/> whose four bounds had to be solved backwards from the
    /// offset and scale you actually had — three managed references and a calculation in the commit
    /// message, for an affine map.
    /// <para>
    /// The order is fixed and is the one the name states: the offset is applied first. <c>x * b + a</c>
    /// is the same shape with the offset divided by the scale.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Number", Name = "Sum Constant Then Scale", Tooltip = "Adds a constant to a number and scales the sum")]
    public sealed class SumConstantThenScaleConverter : ITwoWayConverter<float, float>
    {
        [Tooltip("Added to the value first.")]
        [SerializeField] private float _offset;

        [Tooltip("Multiplies the sum. A scale of zero flattens every value to nothing and cannot be undone.")]
        [SerializeField] private float _scale = 1f;

        /// <summary>
        /// Initializes a new instance of the <see cref="SumConstantThenScaleConverter"/> class leaving the value unchanged.
        /// </summary>
        public SumConstantThenScaleConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SumConstantThenScaleConverter"/> class.
        /// </summary>
        /// <param name="offset">Added to the value first.</param>
        /// <param name="scale">Multiplies the sum.</param>
        public SumConstantThenScaleConverter(float offset, float scale = 1f)
        {
            _offset = offset;
            _scale = scale;
        }

        /// <summary>
        /// Adds the offset to the specified value and scales the sum.
        /// </summary>
        /// <param name="value">The value to transform.</param>
        /// <returns>The transformed value.</returns>
        public float Convert(float value) => (value + _offset) * _scale;

        /// <summary>
        /// Reverses <see cref="Convert"/>.
        /// </summary>
        /// <param name="value">The value to transform back.</param>
        /// <returns>
        /// The value the forward pass was given, or <paramref name="value"/> unchanged when the scale
        /// is zero — the forward pass discarded the input, so there is nothing to recover it from.
        /// </returns>
        public float ConvertBack(float value) => _scale == 0f ? value : value / _scale - _offset;
    }
}
