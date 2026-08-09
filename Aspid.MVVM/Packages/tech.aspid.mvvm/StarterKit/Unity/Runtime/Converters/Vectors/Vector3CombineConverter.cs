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

        [SerializeReference] private Converter? _preConvertor;

        [SerializeReference] private Converter? _postConvertor;

        [NonSerialized] private bool _loggedMissingTarget;

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
        /// Gets the scene component <see cref="VectorTo"/> is read from. Derived classes must provide
        /// this value so an unassigned or destroyed Inspector reference can be detected before use.
        /// </summary>
        protected abstract Component? Target { get; }

        /// <summary>
        /// Gets the reference vector to combine with. Derived classes must provide this value.
        /// </summary>
        /// <remarks>Only read once <see cref="Target"/> is known to be alive.</remarks>
        protected abstract Vector3 VectorTo { get; }

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Vector3"/> by combining with the reference vector.
        /// </summary>
        /// <param name="value">The 2D vector to convert.</param>
        /// <returns>The converted 3D vector, or the widened input when <see cref="Target"/> is missing.</returns>
        public Vector3 Convert(Vector2 value) =>
            Combine(value);

        /// <summary>
        /// Combines a <see cref="Vector3"/> with the reference vector by selecting components.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The combined vector, or the input unchanged when <see cref="Target"/> is missing.</returns>
        public Vector3 Convert(Vector3 value) =>
            Combine(value);

        /// <summary>
        /// Reads the reference vector and combines with it, degrading to the input when the target
        /// reference was never assigned or has since been destroyed.
        /// </summary>
        private Vector3 Combine(Vector3 from)
        {
            // Unity's overloaded == is deliberate: `is null` reports false for a destroyed object,
            // whose managed reference is still alive. Same idiom as AddressableMonoBinder.
            if (Target == null)
            {
                LogMissingTarget();
                return from;
            }

            return Combine(from, VectorTo);
        }

        private void LogMissingTarget()
        {
            if (_loggedMissingTarget) return;
            _loggedMissingTarget = true;

            Debug.LogError($"{GetType().Name}: no target assigned. Returning the input value unchanged.");
        }

        /// <summary>
        /// Combines two vectors by selecting components from each based on the configured mode.
        /// </summary>
        private Vector3 Combine(Vector3 from, Vector3 to)
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