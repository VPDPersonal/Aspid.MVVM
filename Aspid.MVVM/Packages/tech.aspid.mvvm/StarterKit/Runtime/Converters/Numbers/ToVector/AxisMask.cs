using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which axes a converter writes a number into.
    /// </summary>
    [Flags]
    public enum AxisMask
    {
        /// <summary>
        /// No axis.
        /// </summary>
        None = 0,

        /// <summary>
        /// The X axis.
        /// </summary>
        X = 1,

        /// <summary>
        /// The Y axis.
        /// </summary>
        Y = 2,

        /// <summary>
        /// The Z axis.
        /// </summary>
        Z = 4,

        /// <summary>
        /// The W axis, which only a <see cref="UnityEngine.Vector4"/> carries.
        /// </summary>
        W = 8,

        /// <summary>
        /// Every axis, so the value is uniform.
        /// </summary>
        All = X | Y | Z | W,
    }
}
