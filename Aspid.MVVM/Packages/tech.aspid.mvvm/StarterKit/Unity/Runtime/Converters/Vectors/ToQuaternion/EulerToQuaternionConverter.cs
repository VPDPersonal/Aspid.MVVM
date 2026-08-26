#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Turns Euler angles into a rotation.
    /// </summary>
    /// <remarks>
    /// The pair names the same rotation both ways, but not the same numbers: Unity reports Euler
    /// angles in 0..360, so -10° goes out and 350° comes back.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector/To Quaternion",
        Name = "Euler To Quaternion",
        Tooltip = "Turns Euler angles into a rotation")]
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
        /// <returns>The angles, in degrees, each folded into 0..360.</returns>
        public Vector3 ConvertBack(Quaternion value) => value.eulerAngles;
    }
}
