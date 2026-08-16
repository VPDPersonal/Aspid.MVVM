#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Widens a vector to four components.
    /// </summary>
    /// <remarks>
    /// A shader property is a <see cref="Vector4"/> whatever it holds, so binding a position, a
    /// direction or a tiling pair to one meant widening it in the ViewModel — which pushed a detail
    /// of the material into the model.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector3 To Vector4", Tooltip = "Widens a vector to four components")]
    public sealed class Vector3ToVector4Converter : IConverter<Vector3, Vector4>
    {
        [Tooltip("The value written into the fourth component.")]
        [SerializeField] private float _w;

        /// <remarks>Default: with a zero fourth component.</remarks>
        public Vector3ToVector4Converter() { }

        /// <param name="w">The value written into the fourth component.</param>
        public Vector3ToVector4Converter(float w)
        {
            _w = w;
        }

        /// <summary>
        /// Widens the specified vector.
        /// </summary>
        /// <param name="value">The vector to widen.</param>
        /// <returns>The four-component vector.</returns>
        public Vector4 Convert(Vector3 value) => new(value.x, value.y, value.z, _w);
    }
}
