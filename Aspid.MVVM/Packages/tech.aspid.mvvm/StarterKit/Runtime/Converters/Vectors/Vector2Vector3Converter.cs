#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Maps a <see cref="Vector2"/>'s components onto two axes of a <see cref="Vector3"/>, filling
    /// the third with a constant, and reads the same two back.
    /// </summary>
    [Serializable]
    [TypeSelectorDisplay(
        Group = "Aspid/Vector",
        Name = "Vector2 To Vector3",
        Tooltip = "Maps a Vector2's components onto two axes of a Vector3, filling the third with a constant")]
    public sealed class Vector2Vector3Converter :
        ITwoWayConverter<Vector2, Vector3>,
        ITwoWayConverter<Vector3, Vector2>
    {
        [Tooltip("Which axes of the 3D vector the 2D components are written into.")]
        [SerializeField] private Mode _mode;

        [Tooltip("The constant written into the axis the mode leaves out.")]
        [SerializeField] private float _thirdValue;

        /// <remarks>Default: X and Y kept, with a zero Z.</remarks>
        public Vector2Vector3Converter()
            : this(Mode.XY) { }

        /// <param name="mode">Which axes of the 3D vector the 2D components are written into.</param>
        /// <param name="thirdValue">
        /// The constant written into the axis the mode leaves out. When omitted, zero.
        /// </param>
        public Vector2Vector3Converter(
            Mode mode,
            float thirdValue = 0)
        {
            _mode = mode;
            _thirdValue = thirdValue;
        }

        /// <summary>
        /// Maps the specified vector onto the configured axes.
        /// </summary>
        /// <param name="value">The 2D vector to convert.</param>
        /// <returns>
        /// The converted 3D vector. Reports an error and returns a zero vector when the mode is not
        /// a declared value.
        /// </returns>
        public Vector3 Convert(Vector2 value) => _mode switch
        {
            Mode.XY => new Vector3(value.x, value.y, _thirdValue),
            Mode.XZ => new Vector3(value.x, _thirdValue, value.y),
            Mode.YZ => new Vector3(_thirdValue, value.x, value.y),
            Mode.YX => new Vector3(value.y, value.x, _thirdValue),
            Mode.ZX => new Vector3(value.y, _thirdValue, value.x),
            Mode.ZY => new Vector3(_thirdValue, value.y, value.x),
            _ => Undeclared<Vector3>()
        };

        /// <summary>
        /// Reads the two mapped axes back out of the specified vector.
        /// </summary>
        /// <param name="value">The 3D vector to convert.</param>
        /// <returns>
        /// The two axes the mode names, in the order it names them; the constant axis is dropped.
        /// Reports an error and returns a zero vector when the mode is not a declared value.
        /// </returns>
        public Vector2 ConvertBack(Vector3 value) => _mode switch
        {
            Mode.XY => new Vector2(value.x, value.y),
            Mode.XZ => new Vector2(value.x, value.z),
            Mode.YZ => new Vector2(value.y, value.z),
            Mode.YX => new Vector2(value.y, value.x),
            Mode.ZX => new Vector2(value.z, value.x),
            Mode.ZY => new Vector2(value.z, value.y),
            _ => Undeclared<Vector2>()
        };

        Vector2 IConverter<Vector3, Vector2>.Convert(Vector3 value) =>
            ConvertBack(value);

        Vector3 ITwoWayConverter<Vector3, Vector2>.ConvertBack(Vector2 value) =>
            Convert(value);

        private T Undeclared<T>()
            where T : struct
        {
            this.LogError(
                problem: $"the mode {_mode.Describe()} is not a declared {nameof(Mode)}",
                consequence: "Returning a zero vector.");

            return default;
        }

        /// <summary>
        /// Specifies which components of the 2D vector to map to the 3D vector. The letters name the
        /// destination axes, in the order the 2D components are read.
        /// </summary>
        /// <remarks>
        /// New members are appended: Unity stores the declaration index, so inserting one would repoint serialized fields.
        /// </remarks>
        public enum Mode
        {
            /// <summary>
            /// The 2D X goes to X and the 2D Y to Y; the constant fills Z. The mode a new converter
            /// starts in.
            /// </summary>
            XY,

            /// <summary>
            /// The 2D X goes to X and the 2D Y to Z; the constant fills Y, laying a flat value on the
            /// ground plane.
            /// </summary>
            XZ,

            /// <summary>
            /// The 2D X goes to Y and the 2D Y to Z; the constant fills X.
            /// </summary>
            YZ,

            /// <summary>
            /// The 2D X goes to Y and the 2D Y to X; the constant fills Z.
            /// </summary>
            YX,

            /// <summary>
            /// The 2D X goes to Z and the 2D Y to X; the constant fills Y.
            /// </summary>
            ZX,

            /// <summary>
            /// The 2D X goes to Z and the 2D Y to Y; the constant fills X.
            /// </summary>
            ZY,
        }
    }
}
