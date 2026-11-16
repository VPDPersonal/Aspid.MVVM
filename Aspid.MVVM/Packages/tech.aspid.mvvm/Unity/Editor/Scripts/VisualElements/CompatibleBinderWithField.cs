// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Specifies binder drop compatibility in increasing preference order.
    /// </summary>
    public enum CompatibleBinderWithField
    {
        /// <summary>
        /// The binder cannot fill the field.
        /// </summary>
        None,
        /// <summary>
        /// The binder type matches, but its identifier differs.
        /// </summary>
        Type,
        /// <summary>
        /// Both the binder type and identifier match.
        /// </summary>
        TypeAndId,
    }
}
