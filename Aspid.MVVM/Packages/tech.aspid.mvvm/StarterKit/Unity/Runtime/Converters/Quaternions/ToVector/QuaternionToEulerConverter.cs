#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads Euler angles off a rotation.
    /// </summary>
    /// <remarks>
    /// Unity reports Euler angles in 0..360, so an un-normalized read gives 359 where -1 is meant.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Quaternion/To Vector",
        Name = "To Euler",
        Tooltip = "Reads Euler angles off a rotation")]
    public sealed class QuaternionToEulerConverter : IConverter<Quaternion, Vector3>
    {
        [Tooltip("Report angles as -180..180 rather than Unity's 0..360.")]
        [SerializeField] private bool _normalizeToSigned180 = true;

        /// <remarks>Default: normalizing to ±180.</remarks>
        public QuaternionToEulerConverter() { }

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
            return !_normalizeToSigned180 
                ? euler
                : new Vector3(Signed(euler.x), Signed(euler.y), Signed(euler.z));

        }

        private static float Signed(float angle) => angle > 180f ? angle - 360f : angle;
    }
}
