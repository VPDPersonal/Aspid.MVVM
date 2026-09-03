// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The arithmetic <see cref="ArithmeticNumberConverter"/> can apply.
    /// </summary>
    /// <remarks>Members are appended, never inserted: the order is the serialized value.</remarks>
    public enum NumberOperation
    {
        /// <summary>
        /// Add the coefficient.
        /// </summary>
        Add,

        /// <summary>
        /// Subtract the coefficient.
        /// </summary>
        Subtract,

        /// <summary>
        /// Divide by the coefficient. A zero coefficient falls back.
        /// </summary>
        Divide,

        /// <summary>
        /// Multiply by the coefficient.
        /// </summary>
        Multiply,

        /// <summary>
        /// The non-negative remainder after dividing by the coefficient. Cannot be undone.
        /// </summary>
        Modulo,

        /// <summary>
        /// Raise to the power of the coefficient.
        /// </summary>
        Power,

        /// <summary>
        /// Subtract from the coefficient: <c>c - x</c>.
        /// </summary>
        ReverseSubtract,

        /// <summary>
        /// Divide the coefficient by the value: <c>c / x</c>. A zero value falls back.
        /// </summary>
        ReverseDivide,
    }
}
