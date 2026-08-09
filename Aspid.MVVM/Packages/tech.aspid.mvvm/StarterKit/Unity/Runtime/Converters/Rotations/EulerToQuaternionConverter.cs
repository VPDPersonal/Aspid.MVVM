#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns Euler angles into a rotation.
    /// </summary>
    /// <remarks>
    /// A ViewModel naturally stores angles; a binder wants a <see cref="Quaternion"/>. Both
    /// directions are here because the pair round-trips within the ±180 convention.
    /// </remarks>
    [Serializable]
    public sealed class EulerToQuaternionConverter : ITwoWayConverter<Vector3, Quaternion>
    {
        /// <summary>
        /// Turns the specified angles into a rotation.
        /// </summary>
        /// <param name="value">The Euler angles, in degrees.</param>
        /// <returns>The rotation.</returns>
        public Quaternion Convert(Vector3 value) => Quaternion.Euler(value);

        /// <summary>
        /// Reads Euler angles off a rotation.
        /// </summary>
        /// <param name="value">The rotation to read.</param>
        /// <returns>The angles, in degrees.</returns>
        public Vector3 ConvertBack(Quaternion value) => value.eulerAngles;
    }
}
