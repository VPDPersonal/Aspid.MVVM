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
        /// <param name="parameters">The lookup query.</param>
        /// <returns>
        /// A result holding the member; a result whose <see cref="FindBindableMemberResult.IsFound"/> is <see langword="false"/>
        /// when no member has the identifier.
        /// </returns>
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters);
    }
}
