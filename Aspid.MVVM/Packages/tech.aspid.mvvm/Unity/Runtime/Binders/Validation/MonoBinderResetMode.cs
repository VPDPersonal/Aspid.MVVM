// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    /// <summary>
    /// How far a <see cref="IMonoBinderValidatable"/> reset goes.
    /// </summary>
    public enum MonoBinderResetMode
    {
        /// <summary>
        /// Clears the current value; the previous one is kept.
        /// </summary>
        Soft,

        /// <summary>
        /// Clears the current and the previous value.
        /// </summary>
        Hard,
    }
}
