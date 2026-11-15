// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines a ViewModel whose bindable members a View looks up by identifier.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Finds the bindable member with the identifier in <paramref name="parameters"/>.
        /// </summary>
        /// <param name="parameters">The lookup query with a non-null identifier that is not empty or whitespace.</param>
        /// <returns>
        /// The lookup result; <see cref="FindBindableMemberResult.IsFound"/> indicates whether it contains a member.
        /// </returns>
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters);
    }
}
