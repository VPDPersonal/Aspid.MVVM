#nullable enable
using System;
using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Abstract base class for converters that combine a bound vector with one read from a scene
    /// component, taking each axis from whichever of the two the configured <see cref="Mode"/> names.
    /// Derived classes supply the component and the reference vector; this layer contributes the axis
    /// selection and the optional pre- and post-conversion stages around it.
    /// </summary>
    /// <remarks>
    /// Binding one axis and leaving the rest where the scene put them. The reference vector is re-read
    /// on every conversion, so the unbound axes keep tracking the component even when something else
    /// moves it. <see cref="Vector2CombineConverter"/> is the two-dimensional half of the pair.
    /// <para>
    /// One concrete member per component property the reference vector is read from:
    /// <see cref="TransformPositionCombineConverter"/>, <see cref="TransformScaleCombineConverter"/>,
    /// <see cref="TransformEulerAnglesCombineConverter"/>,
    /// <see cref="RectTransformAnchoredPositionCombineConverter"/>,
    /// <see cref="BoxColliderCentreCombineConverter"/>, <see cref="BoxColliderSizeCombineConverter"/>,
    /// <see cref="SphereColliderCentreCombineConverter"/> and
    /// <see cref="CapsuleColliderCentreCombineConverter"/>.
    /// </para>
    /// </remarks>
    [Serializable]
    public abstract class Vector3CombineConverter :
        IConverterVector3,
        IConverterVector2ToVector3
    {
        [Tooltip("Which components come from the bound vector; the rest come from the reference one.")]
        [SerializeField] private Mode _mode;

        [Tooltip("Applied to the bound vector before the components are selected.")]
        [SerializeReference] private Converter? _preConvertor;

        [Tooltip("Applied to the combined result.")]
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
        /// <param name="preConvertor">Applied to the bound vector before the components are selected.</param>
        /// <param name="postConvertor">Applied to the combined result.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when either function is <see langword="null"/>. Use the converter overload with
        /// <see langword="null"/> to leave a stage out.
        /// </exception>
        public Vector3CombineConverter(
            Mode mode,
            Func<Vector3, Vector3> preConvertor,
            Func<Vector3, Vector3> postConvertor)
            : this(mode, preConvertor.ToConvert(), postConvertor.ToConvert()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3CombineConverter"/> class with converter interfaces.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        /// <param name="preConvertor">
        /// Applied to the bound vector before the components are selected, or <see langword="null"/> to
        /// leave that stage out.
        /// </param>
        /// <param name="postConvertor">
        /// Applied to the combined result, or <see langword="null"/> to leave that stage out.
        /// </param>
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
        /// <remarks>
        /// Same stage order as <see cref="Convert(Vector3)"/>: pre-converter, then the axis selection,
        /// then the post-converter. The argument widens to <c>(x, y, 0)</c> before any of that runs, so
        /// a mode naming Z takes that zero rather than the reference vector's depth.
        /// </remarks>
        /// <param name="value">The 2D vector to convert.</param>
        /// <returns>The converted 3D vector.</returns>
        public Vector3 Convert(Vector2 value) =>
            Combine(value, VectorTo);

        /// <summary>
        /// Combines a <see cref="Vector3"/> with the reference vector by selecting components.
        /// </summary>
        /// <remarks>
        /// The pre-converter transforms the bound vector, the <see cref="Mode"/> then takes each axis
        /// from that result or from the reference vector, and the post-converter runs last. So the
        /// pre-converter never sees the reference vector, and the post-converter can still move an axis
        /// the mode took from it.
        /// </remarks>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The combined vector.</returns>
        public Vector3 Convert(Vector3 value) =>
            Combine(value, VectorTo);

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
            /// <summary>
            /// Takes X from the bound vector; Y and Z stay at the reference vector's.
            /// </summary>
            X,

            /// <summary>
            /// Takes Y from the bound vector; X and Z stay at the reference vector's.
            /// </summary>
            Y,

            /// <summary>
            /// Takes Z from the bound vector; X and Y stay at the reference vector's.
            /// </summary>
            Z,

            /// <summary>
            /// Takes X and Y from the bound vector; Z stays at the reference vector's.
            /// </summary>
            XY,

            /// <summary>
            /// Takes X and Z from the bound vector; Y stays at the reference vector's.
            /// </summary>
            XZ,

            /// <summary>
            /// Takes Y and Z from the bound vector; X stays at the reference vector's.
            /// </summary>
            YZ,

            /// <summary>
            /// Takes all three components from the bound vector, leaving the reference vector with no
            /// say — only the pre- and post-converters shape the result.
            /// </summary>
            XYZ,
        }
    }
}
