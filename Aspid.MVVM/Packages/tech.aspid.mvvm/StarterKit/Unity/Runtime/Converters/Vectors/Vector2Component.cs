#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="Vector2ToFloatConverter"/> measures.
    /// </summary>
    /// <remarks>
    /// A separate enum from <see cref="VectorComponent"/> so the Inspector does not offer a Z axis
    /// on a value that has none.
    /// </remarks>
    public enum Vector2Component
    {
        /// <summary>
        /// The X axis.
        /// </summary>
        X,

        /// <summary>
        /// The Y axis.
        /// </summary>
        Y,

        /// <summary>
        /// The length of the vector.
        /// </summary>
        Magnitude,

        /// <summary>
        /// The squared length, which needs no square root.
        /// </summary>
        SqrMagnitude,

        /// <summary>
        /// How far the vector reaches along an authored direction.
        /// </summary>
        Dot,
    }
}
