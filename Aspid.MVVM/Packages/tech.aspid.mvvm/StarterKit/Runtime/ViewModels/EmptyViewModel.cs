// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="IViewModel"/> with no bindable members: every lookup fails, so bound binders stay at their defaults.
    /// </summary>
    public sealed class EmptyViewModel : IViewModel
    {
        /// <inheritdoc/>
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) =>
            default;
    }
}
