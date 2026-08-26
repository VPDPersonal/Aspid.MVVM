#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Moves between two colors by a 0..1 amount.
    /// </summary>
    /// <remarks>
    /// The curve applies only while the clamp is on: a curve cannot answer past its own ends.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Color",
        Name = "Lerp",
        Tooltip = "Moves between two colors by a 0..1 amount")]
    public sealed class ColorLerpConverter : IConverter<float, Color>, IConverter<double, Color>
    {
        [Tooltip("The color at 0.")]
        [SerializeField] private Color _from = Color.red;

        [Tooltip("The color at 1.")]
        [SerializeField] private Color _to = Color.green;

        [Tooltip("Shapes the travel between the two colors. Read only while Clamp is on, because a " +
            "curve cannot answer past its own ends.")]
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Tooltip("Hold the incoming amount inside 0..1 and shape it with the curve. Off, the amount " +
            "carries past the two colors and skips the curve.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: going red to green.</remarks>
        public ColorLerpConverter() { }

        /// <param name="from">The color at 0.</param>
        /// <param name="to">The color at 1.</param>
        /// <param name="curve">
        /// Shapes the travel between the two colors, while the amount is clamped. Leave it out to
        /// move evenly.
        /// </param>
        public ColorLerpConverter(Color from, Color to, AnimationCurve? curve = null)
        {
            _from = from;
            _to = to;

            if (curve is not null) _curve = curve;
        }

        /// <summary>
        /// Reads the color at the specified amount.
        /// </summary>
        /// <param name="value">The 0..1 amount.</param>
        /// <returns>
        /// The color there, after the curve has shaped the amount. With the clamp cleared the amount
        /// reaches the two colors as it arrived and the curve takes no part, so an amount outside
        /// 0..1 carries past them.
        /// </returns>
        public Color Convert(float value) => _clamp
            ? Color.Lerp(_from, _to, Ease(value))
            : Color.LerpUnclamped(_from, _to, value);

        // A curve that was never authored deserializes with no keys, and evaluating that returns zero,
        // pinning every value to the first color. One key is a constant, which pins it just as firmly.
        private float Ease(float value) => _curve is { length: > 1 } ? _curve.Evaluate(value) : value;

        // Unity's math is float, so the double overload carries a float's precision.
        Color IConverter<double, Color>.Convert(double value) =>
            Convert(NumericSaturation.ToFloat(value));
    }
}
