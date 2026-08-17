#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Which axes <see cref="FloatToVector3Converter"/> writes a number into.
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
        /// Every axis — a uniform value.
        /// </summary>
        All = X | Y | Z,
    }
}
