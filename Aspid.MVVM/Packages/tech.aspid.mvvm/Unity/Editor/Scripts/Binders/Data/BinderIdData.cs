// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// One entry of the binder-id dropdown: the id of a field on the view that this binder may fill.
    /// </summary>
    public readonly struct BinderIdData
    {
        /// <summary>
        /// The id of the view field, as declared on it.
        /// </summary>
        public readonly string Id;
        
        /// <summary>
        /// Creates a dropdown entry for a binding identifier.
        /// </summary>
        /// <param name="id">The View field identifier.</param>
        public BinderIdData(string id)
        {
            Id = id;
        }
    }
}