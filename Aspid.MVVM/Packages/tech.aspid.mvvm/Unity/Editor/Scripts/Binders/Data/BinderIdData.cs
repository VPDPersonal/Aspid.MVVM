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
        /// Initializes a new entry for the field with the given id.
        /// </summary>
        /// <param name="id">The id of the view field.</param>
        public BinderIdData(string id)
        {
            Id = id;
        }
    }
}