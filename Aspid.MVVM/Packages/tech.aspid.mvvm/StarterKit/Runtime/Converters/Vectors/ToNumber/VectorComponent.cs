using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="VectorToFloatConverter"/> measures. A narrower vector carries fewer of them.
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

        // Kept last: Unity stores the declaration index, so inserting before would repoint serialized fields.
        /// <summary>
        /// How far the vector reaches along an authored direction.
        /// </summary>
        Dot,

        /// <summary>
        /// The W component, which only a <see cref="Vector4"/> carries.
        /// </summary>
        W,
    }
}
