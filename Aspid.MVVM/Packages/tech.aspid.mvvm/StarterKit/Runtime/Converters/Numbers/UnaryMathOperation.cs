// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The single-argument functions <see cref="UnaryMathConverter"/> can apply.
    /// </summary>
    public enum UnaryMathOperation
    {
        /// <summary>
        /// The distance from zero.
        /// </summary>
        Abs,

        /// <summary>
        /// The value with its sign flipped.
        /// </summary>
        Negate,

        /// <summary>
        /// -1, 0 or 1.
        /// </summary>
        Sign,

        /// <summary>
        /// The square root. A negative value yields zero rather than NaN.
        /// </summary>
        Sqrt,

        /// <summary>
        /// One divided by the value. Zero yields zero rather than infinity.
        /// </summary>
        Reciprocal,

        /// <summary>
        /// The natural logarithm. A non-positive value yields zero.
        /// </summary>
        Log,

        /// <summary>
        /// The base-10 logarithm. A non-positive value yields zero.
        /// </summary>
        Log10,

        /// <summary>
        /// e raised to the value.
        /// </summary>
        Exp,

        /// <summary>
        /// The sine, in radians.
        /// </summary>
        Sin,

        /// <summary>
        /// The cosine, in radians.
        /// </summary>
        Cos,

        /// <summary>
        /// The tangent, in radians.
        /// </summary>
        Tan,

        /// <summary>The base-2 logarithm. A non-positive value yields zero.</summary>
        Log2,

        /// <summary>The arcsine, in radians. The value is clamped to -1..1 first.</summary>
        Asin,

        /// <summary>The arccosine, in radians. The value is clamped to -1..1 first.</summary>
        Acos,

        /// <summary>The arctangent, in radians.</summary>
        Atan,
    }
}
