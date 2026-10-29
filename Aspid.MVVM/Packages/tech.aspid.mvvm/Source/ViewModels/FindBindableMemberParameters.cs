// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Represents the query passed to <see cref="IViewModel.FindBindableMember"/>.
    /// </summary>
    public readonly ref struct FindBindableMemberParameters
    {
        /// <summary>
        /// The identifier of the bindable member, as generated or set by <see cref="BindIdAttribute"/>.
        /// </summary>
        public readonly string Id;

        /// <summary>
        /// Creates a query for the member with identifier <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The identifier of the bindable member.</param>
        public FindBindableMemberParameters(string id)
        {
            Id = id;
        }
    }
}
