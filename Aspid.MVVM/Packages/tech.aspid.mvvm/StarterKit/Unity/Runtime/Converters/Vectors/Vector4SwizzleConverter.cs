#nullable enable
using Aspid.FastTools.Types;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reorders the components of a four-component vector.
    /// </summary>
    /// <remarks>
    /// Completes the permutation family the 2D and 3D converters already cover. Shader authors and
    /// importers order the four numbers however the tool that produced them saw fit, and reordering
    /// them in the ViewModel puts a format detail in the model.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(Group = "Aspid/Vector", Name = "Vector4 Swizzle", Tooltip = "Reorders the components of a four-component vector")]
    public sealed class Vector4SwizzleConverter : IConverter<Vector4, Vector4>
    {
        [Tooltip("Which incoming component the X of the result is read from.")]
        [SerializeField] private Vector4Component _x = Vector4Component.X;

        [Tooltip("Which incoming component the Y of the result is read from.")]
        [SerializeField] private Vector4Component _y = Vector4Component.Y;

        [Tooltip("Which incoming component the Z of the result is read from.")]
        [SerializeField] private Vector4Component _z = Vector4Component.Z;

        [Tooltip("Which incoming component the W of the result is read from.")]
        [SerializeField] private Vector4Component _w = Vector4Component.W;

        public Vector4SwizzleConverter() { }

        /// <param name="x">Which incoming component the X of the result is read from.</param>
        /// <param name="y">Which incoming component the Y of the result is read from.</param>
        /// <param name="z">Which incoming component the Z of the result is read from.</param>
        /// <param name="w">Which incoming component the W of the result is read from.</param>
        public Vector4SwizzleConverter(
            Vector4Component x,
            Vector4Component y,
            Vector4Component z,
            Vector4Component w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        /// <summary>
        /// Reorders the specified vector.
        /// </summary>
        /// <param name="value">The vector to reorder.</param>
        /// <returns>The reordered vector.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when a component is not a declared value.</exception>
        public Vector4 Convert(Vector4 value) => new(
            Read(value, _x),
            Read(value, _y),
            Read(value, _z),
            Read(value, _w));

        // Nothing stops the same source component being read into two destinations — a swizzle that
        // broadcasts one number across the vector is a normal thing to want, not a misconfiguration.
        private static float Read(Vector4 value, Vector4Component component) => component switch
        {
            Vector4Component.X => value.x,
            Vector4Component.Y => value.y,
            Vector4Component.Z => value.z,
            Vector4Component.W => value.w,
            _ => throw new ArgumentOutOfRangeException(nameof(component), component, null)
        };
    }
}
