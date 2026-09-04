#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Base class for converters that combine a bound 2D vector with one read from a scene
    /// component, taking each axis from one side or the other.
    /// </summary>
    /// <remarks>
    /// The reference vector is re-read on every conversion, so the unbound axes keep tracking the
    /// component even when something else moves it.
    /// </remarks>
    [Serializable]
    public abstract class Vector2CombineConverter :
        IConverter<Vector2, Vector2>,
        IConverter<Vector3, Vector2>
    {
        [Tooltip("Which components come from the bound vector; the rest come from the reference one.")]
        [SerializeField] private Mode _mode;

        [Tooltip("Applied to the bound vector before the components are selected.")]
        [SerializeReference] private IConverter<Vector2, Vector2>? _preConverter;

        [Tooltip("Applied to the combined result.")]
        [SerializeReference] private IConverter<Vector2, Vector2>? _postConverter;

        /// <remarks>Default: both components from the bound vector, with no pre- or post-converter.</remarks>
        public Vector2CombineConverter()
            : this(Mode.XY) { }

        /// <param name="mode">Which components come from the bound vector.</param>
        public Vector2CombineConverter(Mode mode)
        {
            _mode = mode;
        }

        /// <param name="mode">Which components come from the bound vector.</param>
        /// <param name="preConverter">Applied to the bound vector before the components are selected.</param>
        /// <param name="postConverter">Applied to the combined result.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when either function is <see langword="null"/>. Use the converter overload with
        /// <see langword="null"/> to leave a stage out.
        /// </exception>
        public Vector2CombineConverter(
            Mode mode,
            Func<Vector2, Vector2> preConverter,
            Func<Vector2, Vector2> postConverter)
            : this(mode, preConverter.ToConverter(), postConverter.ToConverter()) { }

        /// <param name="mode">Which components come from the bound vector.</param>
        /// <param name="preConverter">
        /// Applied to the bound vector before the components are selected, or <see langword="null"/> to
        /// leave that stage out.
        /// </param>
        /// <param name="postConverter">
        /// Applied to the combined result, or <see langword="null"/> to leave that stage out.
        /// </param>
        public Vector2CombineConverter(
            Mode mode,
            IConverter<Vector2, Vector2>? preConverter,
            IConverter<Vector2, Vector2>? postConverter)
        {
            _mode = mode;
            _preConverter = preConverter;
            _postConverter = postConverter;
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
        /// <remarks>
        /// The pre-converter never sees the reference vector, and the post-converter runs after the
        /// axis selection, so it can still move an axis the mode took from the reference.
        /// </remarks>
        /// <param name="value">The vector to convert.</param>
        /// <returns>
        /// The combined vector, or the input unchanged when <see cref="Target"/> is missing or the
        /// mode is not a declared <see cref="Mode"/> value, an error is reported either way.
        /// </returns>
        public Vector2 Convert(Vector2 value) =>
            Combine(value);

        /// <summary>
        /// Combines a <see cref="Vector3"/> with the reference vector, dropping its Z.
        /// </summary>
        /// <param name="value">The 3D vector to convert.</param>
        /// <returns>
        /// The combined vector, or the narrowed input when <see cref="Target"/> is missing or the
        /// mode is not a declared <see cref="Mode"/> value, an error is reported either way.
        /// </returns>
        public Vector2 Convert(Vector3 value) =>
            Combine(value);

        private Vector2 Combine(Vector2 from)
        {
            if (Target == null)
            {
                this.LogError(
                    problem: "no target assigned",
                    consequence: "Returning the input value unchanged.");

                return from;
            }

            return Combine(from, VectorTo);
        }

        private Vector2 Combine(Vector2 from, Vector2 to)
        {
            from = _preConverter?.Convert(from) ?? from;

            from = _mode switch
            {
                Mode.X => new Vector2(from.x, to.y),
                Mode.Y => new Vector2(to.x, from.y),
                Mode.XY => new Vector2(from.x, from.y),
                _ => Undeclared(from)
            };

            return _postConverter?.Convert(from) ?? from;
        }

        private Vector2 Undeclared(Vector2 from)
        {
            this.LogError(
                problem: $"the mode {_mode.Describe()} is not a declared {nameof(Mode)}",
                consequence: "Returning the value unchanged.");

            return from;
        }

        /// <summary>
        /// Specifies which components to take from the first vector when combining.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// Takes X from the bound vector; Y stays at the reference vector's.
            /// </summary>
            X,

            /// <summary>
            /// Takes Y from the bound vector; X stays at the reference vector's.
            /// </summary>
            Y,

            /// <summary>
            /// Takes both components from the bound vector, leaving the reference vector with no
            /// say, only the pre- and post-converters shape the result.
            /// </summary>
            XY,
        }
    }
}
