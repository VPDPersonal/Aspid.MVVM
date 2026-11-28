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
        /// <param name="parameters">The query with a nonblank binding identifier.</param>
        /// <returns>
        /// A result containing the matching member, or an empty result if the identifier is unknown.
        /// </returns>
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters);
    }
}
