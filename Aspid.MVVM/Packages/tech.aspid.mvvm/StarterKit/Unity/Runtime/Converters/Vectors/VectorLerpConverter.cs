#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Moves between two vectors by a 0..1 amount.
    /// </summary>
    /// <remarks>
    /// A marker travelling along a track as progress advances. The easing curve is a field here
    /// rather than a separate <see cref="AnimationCurveConverter"/> chained in front, because it
    /// belongs to the move it shapes — an author changing the two ends should not have to remember
    /// that the shape of the travel lives somewhere else.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector Lerp", Tooltip = "Moves between two vectors by a 0..1 amount")]
    public sealed class VectorLerpConverter : IConverter<float, Vector3>
    {
        [Tooltip("The vector at 0.")]
        [SerializeField] private Vector3 _from;

        [Tooltip("The vector at 1.")]
        [SerializeField] private Vector3 _to = Vector3.one;

        [Tooltip("Shapes the amount before the move. Leave it empty for an even one.")]
        [SerializeField] private AnimationCurve? _curve;

        [Tooltip("Hold the incoming amount inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: going zero to one.</remarks>
        public VectorLerpConverter() { }

        /// <param name="from">The vector at 0.</param>
        /// <param name="to">The vector at 1.</param>
        /// <param name="curve">Shapes the amount before the move, or <see langword="null"/> for an even one.</param>
        public VectorLerpConverter(Vector3 from, Vector3 to, AnimationCurve? curve = null)
        {
            _from = from;
            _to = to;
            _curve = curve;
        }

        /// <summary>
        /// Reads the vector at the specified amount.
        /// </summary>
        /// <param name="value">The 0..1 amount.</param>
        /// <returns>The vector there.</returns>
        public Vector3 Convert(float value)
        {
            // An unassigned curve deserializes as an empty one rather than as null, and evaluating
            // an empty curve returns zero — which would pin the result at _from instead of leaving
            // the amount alone. Both spellings of "no curve" have to mean the same thing.
            var amount = _curve is null || _curve.length == 0 ? value : _curve.Evaluate(value);

            return _clamp ? Vector3.Lerp(_from, _to, amount) : Vector3.LerpUnclamped(_from, _to, amount);
        }
    }
}
