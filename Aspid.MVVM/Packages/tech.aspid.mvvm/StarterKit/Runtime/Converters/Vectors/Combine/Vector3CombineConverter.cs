#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Base class for converters that combine a bound vector with one read from a scene component,
    /// taking each axis from whichever of the two the configured <see cref="Mode"/> names.
    /// </summary>
    /// <remarks>
    /// The reference vector is re-read on every conversion, so the unbound axes keep tracking the
    /// component even when something else moves it.
    /// </remarks>
    [Serializable]
    public abstract class Vector3CombineConverter :
        IConverter<Vector3, Vector3>,
        IConverter<Vector2, Vector3>
    {
        [Tooltip("Which components come from the bound vector; the rest come from the reference one.")]
        [SerializeField] private Mode _mode;

        [Tooltip("Applied to the bound vector before the components are selected.")]
        [TypeSelector]
        [SerializeReference] private IConverter<Vector3, Vector3>? _preConverter;

        [Tooltip("Applied to the combined result.")]
        [TypeSelector]
        [SerializeReference] private IConverter<Vector3, Vector3>? _postConverter;

        /// <remarks>Default: all three components from the bound vector, with no pre- or post-converter.</remarks>
        public Vector3CombineConverter()
            : this(Mode.XYZ) { }

        /// <param name="mode">Which components come from the bound vector.</param>
        public Vector3CombineConverter(Mode mode)
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
        public Vector3CombineConverter(
            Mode mode,
            Func<Vector3, Vector3> preConverter,
            Func<Vector3, Vector3> postConverter)
            : this(mode, preConverter.ToConverter(), postConverter.ToConverter()) { }

        /// <param name="mode">Which components come from the bound vector.</param>
        /// <param name="preConverter">
        /// Applied to the bound vector before the components are selected, or <see langword="null"/> to
        /// leave that stage out.
        /// </param>
        /// <param name="postConverter">
        /// Applied to the combined result, or <see langword="null"/> to leave that stage out.
        /// </param>
        public Vector3CombineConverter(
            Mode mode,
            IConverter<Vector3, Vector3>? preConverter,
            IConverter<Vector3, Vector3>? postConverter)
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
        protected abstract Vector3 VectorTo { get; }

        /// <summary>
        /// Converts a <see cref="Vector2"/> to a <see cref="Vector3"/> by combining with the reference vector.
        /// </summary>
        /// <remarks>
        /// The argument widens to <c>(x, y, 0)</c> before the axis selection runs, so a mode naming Z
        /// takes that zero rather than the reference vector's depth.
        /// </remarks>
        /// <param name="value">The 2D vector to convert.</param>
        /// <returns>
        /// The converted 3D vector, or the widened input when <see cref="Target"/> is missing or the
        /// mode is not a declared <see cref="Mode"/> value, an error is reported either way.
        /// </returns>
        public Vector3 Convert(Vector2 value) =>
            Combine(value);

        /// <summary>
        /// Combines a <see cref="Vector3"/> with the reference vector by selecting components.
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
        public Vector3 Convert(Vector3 value) =>
            Combine(value);

        private Vector3 Combine(Vector3 from)
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
                _ => Undeclared(from)
            };

            return _postConverter?.Convert(from) ?? from;
        }

        private Vector3 Undeclared(Vector3 from)
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
            /// say, only the pre- and post-converters shape the result.
            /// </summary>
            XYZ,
        }
    }
}
