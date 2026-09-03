using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="NumericCastConverter"/> does with a number the target type cannot hold.
    /// </summary>
    public enum OverflowMode
    {
        /// <summary>
        /// Return the nearest value the target type can hold, or zero for a NaN on an integer target.
        /// </summary>
        Saturate,

        /// <summary>
        /// Convert the way a plain cast does: an integer keeps its low bits, an out-of-range floating-point value is undefined.
        /// </summary>
        Unchecked,

        /// <summary>
        /// Throw an <see cref="OverflowException"/> for a value too large for the target.
        /// </summary>
        /// <remarks>Wrap the converter in <see cref="SafeConverter{TFrom, TTo}"/> to keep the throw local.</remarks>
        Checked,
    }
}
