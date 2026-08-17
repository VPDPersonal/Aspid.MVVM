// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What a converter does with a value it cannot convert.
    /// </summary>
    /// <remarks>
    /// This is about the <i>data</i>: a colour string that does not parse, a number outside a range.
    /// A misconfigured converter is a different matter and always reports itself.
    /// </remarks>
    public enum ConverterFailureMode
    {
        /// <summary>
        /// Return the converter's configured fallback value and report the failure.
        /// </summary>
        ReturnFallback,

        /// <summary>
        /// Return the incoming value unchanged and report the failure. Converters whose input
        /// and output types differ cannot honour this and treat it as
        /// <see cref="ReturnFallback"/>.
        /// </summary>
        ReturnInput,

        /// <summary>
        /// Throw. Note that a converter runs inside a binder's value push, and dispatch is a bare
        /// multicast — an exception here stops every binder queued behind this one. Wrap the
        /// converter in <see cref="SafeConverter{TFrom, TTo}"/> if the throw should stay local.
        /// </summary>
        Throw,
    }
}
