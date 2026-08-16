#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns between two rotations by a 0..1 amount.
    /// </summary>
    /// <remarks>
    /// Slerp rather than lerp, because interpolating the four numbers of a rotation directly makes the
    /// turn speed up in the middle. Both ends are authored as Euler angles: a serialized
    /// <see cref="Quaternion"/> shows four raw numbers in the Inspector, and nobody sets those by hand.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Rotation", Name = "Quaternion Slerp", Tooltip = "Turns between two rotations by a 0..1 amount")]
    public sealed class QuaternionSlerpConverter : IConverter<float, Quaternion>
    {
        [Tooltip("The rotation at 0, in Euler degrees.")]
        [SerializeField] private Vector3 _fromEuler;

        [Tooltip("The rotation at 1, in Euler degrees.")]
        [SerializeField] private Vector3 _toEuler;

        [Tooltip("Shapes the amount before the turn. Leave it empty for an even sweep.")]
        [SerializeField] private AnimationCurve? _curve;

        [Tooltip("Hold the incoming amount inside 0..1.")]
        [SerializeField] private bool _clamp = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionSlerpConverter"/> class turning nowhere.
        /// </summary>
        public QuaternionSlerpConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionSlerpConverter"/> class.
        /// </summary>
        /// <param name="fromEuler">The rotation at 0, in Euler degrees.</param>
        /// <param name="toEuler">The rotation at 1, in Euler degrees.</param>
        /// <param name="curve">Shapes the amount before the turn, or <see langword="null"/> for an even sweep.</param>
        public QuaternionSlerpConverter(Vector3 fromEuler, Vector3 toEuler, AnimationCurve? curve = null)
        {
            _fromEuler = fromEuler;
            _toEuler = toEuler;
            _curve = curve;
        }

        /// <summary>
        /// Reads the rotation at the specified amount.
        /// </summary>
        /// <param name="value">The 0..1 amount.</param>
        /// <returns>The rotation there.</returns>
        public Quaternion Convert(float value)
        {
            // An unassigned curve deserializes as an empty one rather than as null, and evaluating
            // an empty curve returns zero — which would pin the rotation at its starting end
            // instead of leaving the amount alone. Both spellings of "no curve" mean the same thing.
            var amount = _curve is null || _curve.length == 0 ? value : _curve.Evaluate(value);

            var from = Quaternion.Euler(_fromEuler);
            var to = Quaternion.Euler(_toEuler);

            return _clamp
                ? Quaternion.Slerp(from, to, amount)
                : Quaternion.SlerpUnclamped(from, to, amount);
        }
    }
}
