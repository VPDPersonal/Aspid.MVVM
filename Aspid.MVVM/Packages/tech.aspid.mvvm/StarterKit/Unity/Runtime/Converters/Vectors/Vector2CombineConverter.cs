#nullable enable
using System;
using UnityEngine;
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;

// The named converter aliases are [Obsolete]. The converters below keep implementing them for
// one release so that a [SerializeReference] field a project declares as one still
// deserializes; the base lists go with the aliases in the next major.
#pragma warning disable CS0618 // Type or member is obsolete

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Base class for converters that combine a bound 2D vector with one read from a scene component
    /// by selecting components. Supports optional pre- and post-conversion transformations.
    /// </summary>
    /// <remarks>
    /// Binding one axis and leaving the other where the designer put it: a bar that grows in width only,
    /// a marker that slides along X while its Y stays with the layout.
    /// <para>
    /// Breaking change: this class shipped <see langword="sealed"/> and concrete, and becoming a base
    /// class takes its <c>Default</c> property and its <c>Convert(Vector2, Vector2)</c> overload with it.
    /// Code that constructed one directly, or a <c>[SerializeReference]</c> field holding one, now names
    /// a type that cannot be instantiated: pick the concrete converter for the component the reference
    /// vector is read from — <see cref="TransformPosition2DCombineConverter"/>,
    /// <see cref="RectTransformAnchoredPosition2DCombineConverter"/>,
    /// <see cref="RectTransformSizeDeltaCombineConverter"/>,
    /// <see cref="BoxCollider2DSizeCombineConverter"/> or
    /// <see cref="BoxCollider2DOffsetCombineConverter"/>.
    /// </para>
    /// </remarks>
    [Serializable]
    public abstract class Vector2CombineConverter :
        IConverterVector2,
        IConverterVector3ToVector2
    {
        [Tooltip("Which components come from the bound vector; the rest come from the reference one.")]
        [SerializeField] private Mode _mode;

        [Tooltip("Applied to the bound vector before the components are selected.")]
        [SerializeReference] private Converter? _preConvertor;

        [Tooltip("Applied to the combined result.")]
        [SerializeReference] private Converter? _postConvertor;

        [NonSerialized] private bool _loggedMissingTarget;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2CombineConverter"/> class with XY mode.
        /// </summary>
        public Vector2CombineConverter() :
            this(Mode.XY) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2CombineConverter"/> class.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        public Vector2CombineConverter(Mode mode)
        {
            _mode = mode;
            _preConvertor = default;
            _postConvertor = default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2CombineConverter"/> class with conversion functions.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        /// <param name="preConvertor">Applied to the bound vector before the components are selected.</param>
        /// <param name="postConvertor">Applied to the combined result.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when either function is <see langword="null"/>. Use the converter overload with
        /// <see langword="null"/> to leave a stage out.
        /// </exception>
        public Vector2CombineConverter(
            Mode mode,
            Func<Vector2, Vector2> preConvertor,
            Func<Vector2, Vector2> postConvertor)
            : this(mode, preConvertor.ToConvert(), postConvertor.ToConvert()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2CombineConverter"/> class with converter interfaces.
        /// </summary>
        /// <param name="mode">The combination mode specifying which components to use.</param>
        /// <param name="preConvertor">
        /// Applied to the bound vector before the components are selected, or <see langword="null"/> to
        /// leave that stage out.
        /// </param>
        /// <param name="postConvertor">
        /// Applied to the combined result, or <see langword="null"/> to leave that stage out.
        /// </param>
        public Vector2CombineConverter(
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
        protected abstract Vector2 VectorTo { get; }

        /// <summary>
        /// Combines a <see cref="Vector2"/> with the reference vector by selecting components.
        /// </summary>
        /// <param name="value">The vector to convert.</param>
        /// <returns>The combined vector, or the input unchanged when <see cref="Target"/> is missing.</returns>
        public Vector2 Convert(Vector2 value) =>
            Combine(value);

        /// <summary>
        /// Combines a <see cref="Vector3"/> with the reference vector, dropping its Z.
        /// </summary>
        /// <param name="value">The 3D vector to convert.</param>
        /// <returns>The combined vector, or the narrowed input when <see cref="Target"/> is missing.</returns>
        public Vector2 Convert(Vector3 value) =>
            Combine(value);

        /// <summary>
        /// Reads the reference vector and combines with it, degrading to the input when the target
        /// reference was never assigned or has since been destroyed.
        /// </summary>
        private Vector2 Combine(Vector2 from)
        {
            // Unity's overloaded == is deliberate: `is null` reports false for a destroyed object,
            // whose managed reference is still alive. Same idiom as Vector3CombineConverter.
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
        private Vector2 Combine(Vector2 from, Vector2 to)
        {
            from = _preConvertor?.Convert(from) ?? from;

            from = _mode switch
            {
                Mode.X => new Vector2(from.x, to.y),
                Mode.Y => new Vector2(to.x, from.y),
                Mode.XY => new Vector2(from.x, from.y),
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
            XY,
        }
    }
}
