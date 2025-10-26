// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Interface for a ViewModel that supports data binding functionality.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Searches for a bindable member in the ViewModel based on the provided parameters.
        /// </summary>
        /// <param name="parameters">The parameters specifying the bindable member to find.</param>
        /// <returns>
        /// A <see cref="FindBindableMemberResult"/> that contains information about the bindable member search result.
        /// </returns>
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters);
    }
}