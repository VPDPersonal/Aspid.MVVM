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
        /// A NaN or an infinity survives a double-to-float narrowing.
        /// </summary>
        /// <remarks>
        /// Deliberately the zero value. A <c>[SerializeReference]</c> instance restored from a scene
        /// written before this field existed reads whatever sits at zero, and that default must be
        /// the one that loses the least.
        /// </remarks>
        Saturate,

        /// <summary>
        /// Convert the way a plain <c>(int)</c> cast does: an integer keeps its low bits and can change
        /// sign, and an out-of-range floating-point value gives a result C# leaves undefined.
        /// </summary>
        Unchecked,

        /// <summary>
        /// Throw an <see cref="OverflowException"/> for a value too large for the target.
        /// </summary>
        /// <remarks>
        /// The throw happens inside a binder's value push and stops every binder queued behind this one;
        /// wrap the converter in <see cref="SafeConverter{TFrom, TTo}"/> to keep it local.
        /// Underflow is not reported: a double too small for a float still becomes zero silently.
        /// </remarks>
        Checked,
    }
}
