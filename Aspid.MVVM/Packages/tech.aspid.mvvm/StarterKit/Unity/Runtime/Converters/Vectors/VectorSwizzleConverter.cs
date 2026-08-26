#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reorders the components of a vector.
    /// </summary>
    /// <remarks>
    /// A narrower vector reads only the slots it has, and a slot naming a component that width does
    /// not carry is reported and passed through unchanged.
    /// </remarks>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector",
        Name = "Swizzle",
        Tooltip = "Reorders the components of a vector")]
    public sealed class VectorSwizzleConverter :
        IConverter<Vector2, Vector2>, IConverter<Vector3, Vector3>, IConverter<Vector4, Vector4>
    {
        [Tooltip("Which incoming component the X of the result is read from.")]
        [SerializeField] private Vector4Component _x = Vector4Component.X;

        [Tooltip("Which incoming component the Y of the result is read from.")]
        [SerializeField] private Vector4Component _y = Vector4Component.Y;

        [Tooltip("Which incoming component the Z of the result is read from.")]
        [SerializeField] private Vector4Component _z = Vector4Component.Z;

        [Tooltip("Which incoming component the W of the result is read from.")]
        [SerializeField] private Vector4Component _w = Vector4Component.W;

        /// <remarks>Default: identity — each component keeps its own slot.</remarks>
        public VectorSwizzleConverter() { }

        /// <param name="x">
        /// Which incoming component the X of the result is read from. A component the bound vector
        /// does not carry is reported and X passes through unchanged.
        /// </param>
        /// <param name="y">
        /// Which incoming component the Y of the result is read from. A component the bound vector
        /// does not carry is reported and Y passes through unchanged.
        /// </param>
        /// <param name="z">
        /// Which incoming component the Z of the result is read from. A component the bound vector
        /// does not carry is reported and Z passes through unchanged.
        /// </param>
        /// <param name="w">
        /// Which incoming component the W of the result is read from. A component the bound vector
        /// does not carry is reported and W passes through unchanged.
        /// </param>
        public VectorSwizzleConverter(
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
        /// <returns>
        /// The reordered vector. A slot naming a component that is not a declared
        /// <see cref="Vector4Component"/> value reports an error and passes its own component
        /// through unchanged.
        /// </returns>
        public Vector4 Convert(Vector4 value) => new Vector4(
            Read(value, _x, "X", value.x, width: 4),
            Read(value, _y, "Y", value.y, width: 4),
            Read(value, _z, "Z", value.z, width: 4),
            Read(value, _w, "W", value.w, width: 4));

        Vector2 IConverter<Vector2, Vector2>.Convert(Vector2 value) => new Vector2(
            Read(value, _x, "X", value.x, width: 2),
            Read(value, _y, "Y", value.y, width: 2));

        Vector3 IConverter<Vector3, Vector3>.Convert(Vector3 value) => new Vector3(
            Read(value, _x, "X", value.x, width: 3),
            Read(value, _y, "Y", value.y, width: 3),
            Read(value, _z, "Z", value.z, width: 3));

        // The same source may be read into two destinations: broadcasting one number is a normal want.
        private float Read(Vector4 value, Vector4Component source, string slot, float unchanged, int width) =>
            source switch
            {
                Vector4Component.X => value.x,
                Vector4Component.Y => value.y,
                Vector4Component.Z when width > 2 => value.z,
                Vector4Component.W when width > 3 => value.w,
                Vector4Component.Z or Vector4Component.W => NotCarried(source, slot, width, unchanged),
                _ => Undeclared(source, slot, unchanged)
            };

        private float NotCarried(Vector4Component source, string slot, int width, float unchanged)
        {
            this.LogError(
                $"the {slot} slot reads {source.Describe()}, which a Vector{width} does not carry",
                $"Passing {slot} through unchanged.");

            return unchanged;
        }

        private float Undeclared(Vector4Component source, string slot, float unchanged)
        {
            this.LogError(
                $"the component {source.Describe()} is not a declared {nameof(Vector4Component)}",
                $"Passing {slot} through unchanged.");

            return unchanged;
        }
    }
}
