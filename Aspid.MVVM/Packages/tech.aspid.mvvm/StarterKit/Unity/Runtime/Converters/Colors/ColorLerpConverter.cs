#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Moves between two colours by a 0..1 amount.
    /// </summary>
    /// <remarks>
    /// A two-stop gradient without a <see cref="Gradient"/> to author, with a curve shaping how the
    /// amount travels between the two colours.
    /// <para>
    /// Shaping and extrapolating are one choice rather than two. A curve answers with its end key past
    /// either end of its own range — no wrap mode extrapolates — so the clamp turns the curve on;
    /// clearing it hands the amount to the lerp as it arrived, and the colours carry past the two stops.
    /// </para>
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Colour", Name = "Color Lerp", Tooltip = "Moves between two colours by a 0..1 amount")]
    public sealed class ColorLerpConverter : IConverter<float, Color>
    {
        [Tooltip("The colour at 0.")]
        [SerializeField] private Color _from = Color.red;

        [Tooltip("The colour at 1.")]
        [SerializeField] private Color _to = Color.green;

        [Tooltip("Shapes the travel between the two colours. A straight line, or no curve at all, moves evenly. Read only while Clamp is on, because a curve cannot answer past its own ends.")]
        [SerializeField] private AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Tooltip("Hold the incoming amount inside 0..1 and shape it with the curve. Clear it to let an amount outside 0..1 carry past the two colours, which skips the curve.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: going red to green.</remarks>
        public ColorLerpConverter() { }

        /// <param name="from">The colour at 0.</param>
        /// <param name="to">The colour at 1.</param>
        /// <param name="curve">Shapes the travel between the two colours, while the amount is clamped. Leave it out to move evenly.</param>
        public ColorLerpConverter(Color from, Color to, AnimationCurve? curve = null)
        {
            _from = from;
            _to = to;

            if (curve is not null) _curve = curve;
        }

        /// <summary>
        /// Reads the colour at the specified amount.
        /// </summary>
        /// <param name="value">The 0..1 amount.</param>
        /// <returns>
        /// The colour there, after the curve has shaped the amount. With the clamp cleared the amount
        /// reaches the two colours as it arrived and the curve takes no part, so an amount outside
        /// 0..1 carries past them.
        /// </returns>
        public Color Convert(float value) => _clamp
            ? Color.Lerp(_from, _to, Ease(value))
            // Not through the curve: it answers with its end key past either end of its own range,
            // and no wrap mode extrapolates, so routing an unclamped amount through one would clamp
            // it here and leave the field meaning nothing.
            : Color.LerpUnclamped(_from, _to, value);

        // A curve field that was never authored deserializes as a curve with no keys rather than as
        // null, and evaluating that returns zero — which would pin every value to the first colour.
        private float Ease(float value) => _curve is { length: > 1 } ? _curve.Evaluate(value) : value;
    }
}
