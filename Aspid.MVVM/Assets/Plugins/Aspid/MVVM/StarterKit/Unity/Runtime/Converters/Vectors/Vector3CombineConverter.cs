#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

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
        [SerializeField] private Mode _mode;

        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _preConvertor;

        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _postConvertor;

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
            _preConvertor = default;
            _postConvertor = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3CombineConverter"/> class with conversion functions.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        /// <param name="preConvertor">Optional function to apply before combining vectors.</param>
        /// <param name="postConvertor">Optional function to apply after combining vectors.</param>
        public Vector3CombineConverter(
            Mode mode,
            Func<Vector3, Vector3> preConvertor,
            Func<Vector3, Vector3> postConvertor)
            : this(mode, preConvertor.ToConvert(), postConvertor.ToConvert()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3CombineConverter"/> class with converter interfaces.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        /// <param name="preConvertor">Optional converter to apply before combining vectors.</param>
        /// <param name="postConvertor">Optional converter to apply after combining vectors.</param>
        public Vector3CombineConverter(
            Mode mode,
            Converter? preConvertor,
            Converter? postConvertor)
        {
            _mode = mode;
            _preConvertor = preConvertor;
            _postConvertor = postConvertor;
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
            Convert(value, VectorTo);

        /// <summary>
        /// Combines a <see cref="Vector3"/> with the reference vector by selecting components.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The combined vector.</returns>
        public Vector3 Convert(Vector3 value) =>
            Convert(value, VectorTo);

        /// <summary>
        /// Combines two vectors by selecting components from each based on the configured mode.
        /// </summary>
        private Vector3 Convert(Vector3 from, Vector3 to)
        {
            from = _preConvertor?.Convert(from) ?? from;

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

            return _postConvertor?.Convert(from) ?? from;
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