#nullable enable

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="Vector3ToFloatConverter"/> measures.
    /// </summary>
    public enum VectorComponent
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
        /// The Z axis.
        /// </summary>
        Z,

        /// <summary>
        /// The length of the vector.
        /// </summary>
        Magnitude,

        /// <summary>
        /// The squared length, which needs no square root.
        /// </summary>
        SqrMagnitude,
    }
}
