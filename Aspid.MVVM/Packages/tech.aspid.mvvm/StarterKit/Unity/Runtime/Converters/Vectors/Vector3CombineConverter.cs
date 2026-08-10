#nullable enable
using System;
using UnityEngine;
using UnityEngine.Serialization;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Base class for converters that combine vector values by selecting components.
    /// Supports optional pre- and post-conversion transformations.
    /// </summary>
    [Serializable]
    public abstract class Vector3CombineConverter :
        IConverterVector3,
        IConverterVector2ToVector3
    {
        [Tooltip("Which components come from the bound vector; the rest come from the reference one.")]
        [SerializeField] private Mode _mode;

        [Tooltip("Applied to the bound vector before the components are selected.")]
        [FormerlySerializedAs("_preConvertor")]
        [SerializeReference] private Converter? _preConverter;

        [Tooltip("Applied to the combined result.")]
        [FormerlySerializedAs("_postConvertor")]
        [SerializeReference] private Converter? _postConverter;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3CombineConverter"/> class with XYZ mode.
        /// </summary>
        public Vector3CombineConverter()
            : this(Mode.XYZ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3CombineConverter"/> class.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        public Vector3CombineConverter(Mode mode)
        {
            _mode = mode;
            _preConverter = default;
            _postConverter = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3CombineConverter"/> class with conversion functions.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        /// <param name="preConverter">Applied to the bound vector before the components are selected.</param>
        /// <param name="postConverter">Applied to the combined result.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when either function is <see langword="null"/>. Use the converter overload with
        /// <see langword="null"/> to leave a stage out.
        /// </exception>
        public Vector3CombineConverter(
            Mode mode,
            Func<Vector3, Vector3> preConverter,
            Func<Vector3, Vector3> postConverter)
            : this(mode, preConverter.ToConvert(), postConverter.ToConvert()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3CombineConverter"/> class with converter interfaces.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        /// <param name="preConverter">
        /// Applied to the bound vector before the components are selected, or <see langword="null"/> to
        /// leave that stage out.
        /// </param>
        /// <param name="postConverter">
        /// Applied to the combined result, or <see langword="null"/> to leave that stage out.
        /// </param>
        public Vector3CombineConverter(
            Mode mode,
            Converter? preConverter,
            Converter? postConverter)
        {
            _mode = mode;
            _preConverter = preConverter;
            _postConverter = postConverter;
        }

        /// <summary>
        /// Gets the reference vector to combine with. Derived classes must provide this value.
        /// </summary>
        protected abstract Vector3 VectorTo { get; }

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Vector3"/> by combining with the reference vector.
        /// </summary>
        /// <param name="value">The 2D vector to convert.</param>
        /// <returns>The converted 3D vector.</returns>
        public Vector3 Convert(Vector2 value) =>
            Combine(value, VectorTo);

        /// <summary>
        /// Combines a <see cref="Vector3"/> with the reference vector by selecting components.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The combined vector.</returns>
        public Vector3 Convert(Vector3 value) =>
            Combine(value, VectorTo);

        private Vector3 Combine(Vector3 from, Vector3 to)
        {
            from = _preConverter?.Convert(from) ?? from;

            from = _mode switch
            {
                Mode.X => new Vector3(from.x, to.y, to.z),
                Mode.Y => new Vector3(to.x, from.y, to.z),
                Mode.Z => new Vector3(to.x, to.y, from.z),
                Mode.XY => new Vector3(from.x, from.y, to.z),
                Mode.XZ => new Vector3(from.x, to.y, from.z),
                Mode.YZ => new Vector3(to.x, from.y, from.z),
                Mode.XYZ => new Vector3(from.x, from.y, from.z),
                _ => throw new ArgumentOutOfRangeException(nameof(_mode), _mode, null)
            };

            return _postConverter?.Convert(from) ?? from;
        }

        /// <summary>
        /// Specifies which components to take from the first vector when combining.
        /// </summary>
        public enum Mode
        {
            X,
            Y,
            Z,
            XY,
            XZ,
            YZ,
            XYZ,
        }
    }
}
