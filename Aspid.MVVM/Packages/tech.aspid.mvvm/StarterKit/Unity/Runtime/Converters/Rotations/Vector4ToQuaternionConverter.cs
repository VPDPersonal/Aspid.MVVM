#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Builds a rotation out of four raw numbers.
    /// </summary>
    /// <remarks>
    /// The way back from <see cref="QuaternionToVector4Converter"/>, for data that arrives as a
    /// <see cref="Vector4"/> from a save file, a server or a shader.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Rotation", Name = "Vector4 To Quaternion", Tooltip = "Builds a rotation out of four raw numbers")]
    public sealed class Vector4ToQuaternionConverter : IConverter<Vector4, Quaternion>
    {
        [Tooltip("Scale the result back to unit length before handing it over.")]
        [SerializeField] private bool _normalize = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4ToQuaternionConverter"/> class that normalises.
        /// </summary>
        public Vector4ToQuaternionConverter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector4ToQuaternionConverter"/> class.
        /// </summary>
        /// <param name="normalize">Whether to scale the result back to unit length.</param>
        public Vector4ToQuaternionConverter(bool normalize)
        {
            _normalize = normalize;
        }

        /// <summary>
        /// Builds a rotation out of the specified numbers.
        /// </summary>
        /// <param name="value">The four numbers, in x, y, z, w order.</param>
        /// <returns>The rotation, or the identity for four zeroes when normalising.</returns>
        public Quaternion Convert(Vector4 value)
        {
            var rotation = new Quaternion(value.x, value.y, value.z, value.w);
            if (!_normalize) return rotation;

            // Numbers that came off a lerp, a text field or a lossy wire format are rarely unit
            // length, and a rotation that is not unit length scales and shears whatever it lands on
            // rather than only turning it. Four zeroes have no direction to scale back to at all;
            // normalized already answers them with the identity, and the branch says so here rather
            // than leaving the case to a Unity behaviour the reader has to go and confirm.
            return value.sqrMagnitude <= Mathf.Epsilon ? Quaternion.identity : rotation.normalized;
        }
    }
}
