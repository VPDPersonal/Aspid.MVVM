// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    /// <summary>
    /// Specifies whether a <see cref="IMonoBinderValidatable"/> reset also clears previous values.
    /// </summary>
    public enum MonoBinderResetMode
    {
        /// <summary>
        /// Clears the current value and keeps the previous one.
        /// </summary>
        Soft,

        /// <summary>
        /// Clears the current and the previous value.
        /// </summary>
        Hard,
    }
}
