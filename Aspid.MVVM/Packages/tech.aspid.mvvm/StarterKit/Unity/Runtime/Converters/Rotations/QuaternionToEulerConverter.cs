#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads Euler angles off a rotation.
    /// </summary>
    /// <remarks>
    /// Unity reports Euler angles in 0..360, so a needle a little past zero reads as 359 rather than
    /// -1 — which makes a "rotation below zero" test fail exactly when it matters. Normalising to
    /// ±180 is the option that removes the trap.
    /// </remarks>
    [Serializable]
    public sealed class QuaternionToEulerConverter : IConverter<Quaternion, Vector3>
    {
        [Tooltip("Report angles as -180..180 rather than Unity's 0..360.")]
        [SerializeField] private bool _normalizeToSigned180 = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionToEulerConverter"/> class normalising to ±180.
        /// </summary>
        public QuaternionToEulerConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionToEulerConverter"/> class.
        /// </summary>
        /// <param name="normalizeToSigned180">Whether to report angles as -180..180.</param>
        public QuaternionToEulerConverter(bool normalizeToSigned180)
        {
            _normalizeToSigned180 = normalizeToSigned180;
        }

        /// <summary>
        /// Reads the angles off the specified rotation.
        /// </summary>
        /// <param name="value">The rotation to read.</param>
        /// <returns>The angles, in degrees.</returns>
        public Vector3 Convert(Quaternion value)
        {
            var euler = value.eulerAngles;
            if (!_normalizeToSigned180) return euler;

            return new Vector3(Signed(euler.x), Signed(euler.y), Signed(euler.z));
        }

        private static float Signed(float angle) => angle > 180f ? angle - 360f : angle;
    }
}
