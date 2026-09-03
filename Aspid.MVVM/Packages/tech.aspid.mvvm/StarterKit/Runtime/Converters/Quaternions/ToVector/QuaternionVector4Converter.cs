#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reads a rotation as its four raw numbers, and builds one back out of them.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Quaternion/To Vector",
        Name = "To Vector4",
        Tooltip = "Reads a rotation as its four raw numbers, and builds one back out of them")]
    public sealed class QuaternionVector4Converter :
        ITwoWayConverter<Quaternion, Vector4>,
        ITwoWayConverter<Vector4, Quaternion>
    {
        [Tooltip("Scale a rotation built from four numbers back to unit length.")]
        [SerializeField] private bool _normalize = true;

        /// <remarks>Default: normalizing what it builds.</remarks>
        public QuaternionVector4Converter() { }

        /// <param name="normalize">
        /// Whether to scale a rotation built from four numbers back to unit length.
        /// </param>
        public QuaternionVector4Converter(bool normalize)
        {
            _normalize = normalize;
        }

        /// <summary>
        /// Reads the specified rotation as four numbers.
        /// </summary>
        /// <param name="value">The rotation to read.</param>
        /// <returns>The four numbers, in x, y, z, w order.</returns>
        public Vector4 Convert(Quaternion value) =>
            new(value.x, value.y, value.z, value.w);

        /// <summary>
        /// Builds a rotation out of the specified numbers.
        /// </summary>
        /// <param name="value">The four numbers, in x, y, z, w order.</param>
        /// <returns>The rotation, or the identity for four zeroes when normalizing.</returns>
        public Quaternion ConvertBack(Vector4 value)
        {
            var rotation = new Quaternion(value.x, value.y, value.z, value.w);
            if (!_normalize) return rotation;

            // Four zeroes have no direction to normalize, so they answer with the identity.
            return value.sqrMagnitude <= Mathf.Epsilon ? Quaternion.identity : rotation.normalized;
        }

        Quaternion IConverter<Vector4, Quaternion>.Convert(Vector4 value) =>
            ConvertBack(value);

        Vector4 ITwoWayConverter<Vector4, Quaternion>.ConvertBack(Quaternion value) =>
            Convert(value);
    }
}
