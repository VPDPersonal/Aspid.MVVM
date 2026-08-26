// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The arithmetic <see cref="VectorArithmeticConverter"/> can apply.
    /// </summary>
    public enum VectorOperation
    {
        /// <summary>
        /// Add the operand.
        /// </summary>
        Add,

        /// <summary>
        /// Subtract the operand.
        /// </summary>
        Subtract,

        /// <summary>
        /// Multiply each axis by the operand's.
        /// </summary>
        Scale,

        /// <summary>
        /// Divide each axis by the operand's. A zero axis is left alone.
        /// </summary>
        Divide,

        /// <summary>
        /// Reflect off the operand as a normal. The normal is used raw, so one longer than unit
        /// scales the reflected part and a zero one leaves the vector alone.
        /// </summary>
        Reflect,
    }
}
