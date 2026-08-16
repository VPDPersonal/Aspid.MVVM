#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Narrows a four-component vector to three by dropping one of them.
    /// </summary>
    /// <remarks>
    /// The way back from <see cref="Vector3ToVector4Converter"/>, for a shader property or a
    /// serialized record read back into something a transform can use.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector4 To Vector3", Tooltip = "Narrows a four-component vector to three by dropping one of them")]
    public sealed class Vector4ToVector3Converter : IConverter<Vector4, Vector3>
    {
        [Tooltip("Which component is left out. The other three keep their order.")]
        [SerializeField] private Vector4Component _drop = Vector4Component.W;

        /// <remarks>Default: dropping W.</remarks>
        public Vector4ToVector3Converter() { }

        /// <param name="drop">Which component is left out.</param>
        public Vector4ToVector3Converter(Vector4Component drop)
        {
            _drop = drop;
        }

        /// <summary>
        /// Narrows the specified vector.
        /// </summary>
        /// <param name="value">The vector to narrow.</param>
        /// <returns>The three-component vector.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the component is not a declared value.</exception>
        public Vector3 Convert(Vector4 value) => _drop switch
        {
            Vector4Component.X => new Vector3(value.y, value.z, value.w),
            Vector4Component.Y => new Vector3(value.x, value.z, value.w),
            Vector4Component.Z => new Vector3(value.x, value.y, value.w),
            Vector4Component.W => new Vector3(value.x, value.y, value.z),
            _ => throw new ArgumentOutOfRangeException(nameof(_drop), _drop, null)
        };
    }
}
