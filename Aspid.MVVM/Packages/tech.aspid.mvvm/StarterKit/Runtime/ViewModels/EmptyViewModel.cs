// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IViewModel"/> with no bindable members: every lookup fails, so bound binders stay at their defaults.
    /// </summary>
    public sealed class EmptyViewModel : IViewModel
    {
        /// <inheritdoc/>
        /// <exception cref="System.ArgumentNullException">The query identifier is <see langword="null"/>.</exception>
        /// <exception cref="System.ArgumentException">The query identifier is empty or whitespace.</exception>
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters)
        {
            parameters.EnsureValid();
            return default;
        }
    }
}
