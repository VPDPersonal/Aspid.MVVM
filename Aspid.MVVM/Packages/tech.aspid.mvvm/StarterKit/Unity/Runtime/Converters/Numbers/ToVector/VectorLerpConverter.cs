#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Moves between two vectors by a 0..1 amount.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Number/To Vector",
        Name = "Lerp",
        Tooltip = "Moves between two vectors by a 0..1 amount")]
    public sealed class VectorLerpConverter :
        IConverter<float, Vector3>,
        IConverter<float, Vector2>,
        IConverter<float, Vector4>
    {
        [Tooltip("The vector at 0. Only the components the bound vector carries are read.")]
        [SerializeField] private Vector4 _from;

        [Tooltip("The vector at 1. Only the components the bound vector carries are read.")]
        [SerializeField] private Vector4 _to = Vector4.one;

        [Tooltip("Shapes the amount before the move. Leave it empty for an even one.")]
        [SerializeField] private AnimationCurve? _curve;

        [Tooltip("Hold the incoming amount inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <remarks>Default: going zero to one.</remarks>
        public VectorLerpConverter() { }

        /// <param name="from">The vector at 0. Only the components the bound vector carries are read.</param>
        /// <param name="to">The vector at 1. Only the components the bound vector carries are read.</param>
        /// <param name="curve">Shapes the amount before the move, or <see langword="null"/> for an even one.</param>
        public VectorLerpConverter(Vector4 from, Vector4 to, AnimationCurve? curve = null)
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
        public Vector3 Convert(float value) => Move(value);

        Vector2 IConverter<float, Vector2>.Convert(float value) => Move(value);

        Vector4 IConverter<float, Vector4>.Convert(float value)
        {
            var amount = Amount(value);
            return _clamp ? Vector4.Lerp(_from, _to, amount) : Vector4.LerpUnclamped(_from, _to, amount);
        }

        private Vector3 Move(float value)
        {
            var amount = Amount(value);
            Vector3 from = _from;
            Vector3 to = _to;

            return _clamp ? Vector3.Lerp(from, to, amount) : Vector3.LerpUnclamped(from, to, amount);
        }

        // An unassigned curve deserializes as an empty one, and evaluating that returns zero.
        private float Amount(float value) =>
            _curve is null || _curve.length == 0 ? value : _curve.Evaluate(value);
    }
}
