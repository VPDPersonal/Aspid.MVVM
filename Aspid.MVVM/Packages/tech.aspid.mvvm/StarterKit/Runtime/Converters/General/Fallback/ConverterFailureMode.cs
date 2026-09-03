// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What a converter does with a value it cannot convert.
    /// </summary>
    /// <remarks>The failure is always reported; the mode only decides what comes back.</remarks>
    public enum ConverterFailureMode
    {
        /// <summary>
        /// Return the configured fallback value.
        /// </summary>
        ReturnFallback,

        /// <summary>
        /// Return the incoming value unchanged, or the fallback when it does not fit the output type.
        /// </summary>
        ReturnInput,
    }
}
