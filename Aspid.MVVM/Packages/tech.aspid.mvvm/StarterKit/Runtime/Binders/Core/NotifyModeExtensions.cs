// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Provides extension methods for <see cref="NotifyMode"/>.
    /// </summary>
    public static class NotifyModeExtensions
    {
        /// <summary>
        /// Returns whether a write should raise the change event of the bound component.
        /// </summary>
        /// <remarks>
        /// The ViewModel pushes its first value before the binder is marked bound, so a write made while
        /// <paramref name="isBound"/> is <see langword="false"/> counts as the write made while binding.
        /// </remarks>
        /// <param name="mode">The notify mode of the binder.</param>
        /// <param name="isBound">The binder's <c>IsBound</c> at the time of the write.</param>
        /// <returns><see langword="true"/> if the write should raise the event; otherwise, <see langword="false"/>.</returns>
        public static bool ShouldNotify(this NotifyMode mode, bool isBound) => mode switch
        {
            NotifyMode.Always => true,
            NotifyMode.AfterBind => isBound,
            _ => false,
        };
    }
}
