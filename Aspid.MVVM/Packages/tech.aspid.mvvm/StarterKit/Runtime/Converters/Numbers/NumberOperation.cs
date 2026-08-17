// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The arithmetic <see cref="ArithmeticNumberConverter"/> can apply.
    /// </summary>
    /// <remarks>
    /// New members are appended rather than inserted: the order is the serialized value, so moving
    /// one silently rewrites every converter already authored in a scene.
    /// </remarks>
    public enum NumberOperation
    {
        /// <summary>
        /// Add the coefficient.
        /// </summary>
        Plus,

        /// <summary>
        /// Subtract the coefficient.
        /// </summary>
        Minus,

        /// <summary>
        /// Divide by the coefficient. A zero coefficient returns the input unchanged.
        /// </summary>
        Division,

        /// <summary>
        /// Multiply by the coefficient.
        /// </summary>
        Multiply,

        /// <summary>
        /// The remainder after dividing by the coefficient, always non-negative — unlike C#'s
        /// <c>%</c>, which keeps the sign of the left operand. Cannot be undone.
        /// </summary>
        Modulo,

        /// <summary>
        /// Raise to the power of the coefficient.
        /// </summary>
        Power,

        /// <summary>
        /// Subtract from the coefficient — <c>c - x</c>, for "how much is left".
        /// </summary>
        ReverseSubtract,

        /// <summary>
        /// Divide the coefficient by the value — <c>c / x</c>. A zero value returns the input.
        /// </summary>
        ReverseDivide,
    }
}
