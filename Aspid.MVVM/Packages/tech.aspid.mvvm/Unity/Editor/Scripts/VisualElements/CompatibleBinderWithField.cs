// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// How well a dragged binder matches the view field it is being dropped on.
    /// </summary>
    /// <remarks>
    /// The order of the members carries meaning: the drop handler picks the best candidate with
    /// <c>Max()</c>, which works only while each member is declared after the one it beats. Reordering them
    /// changes which binder a multi-object drag ends up choosing, and nothing would report it.
    /// </remarks>
    public enum CompatibleBinderWithField
    {
        /// <summary>
        /// The binder cannot fill this field at all.
        /// </summary>
        None,
        /// <summary>
        /// The binder is of a type the field accepts, but its id does not match.
        /// </summary>
        Type,
        /// <summary>
        /// The binder matches the field by both type and id — the best match there is.
        /// </summary>
        TypeAndId,
    }
}
