// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What <see cref="EnumMaskConverter{TEnum}"/> does with the flags it is given.
    /// </summary>
    /// <remarks>Members are appended, never inserted: the order is the serialized value.</remarks>
    public enum EnumMaskOperation
    {
        /// <summary>
        /// Keep only the flags the mask names.
        /// </summary>
        And,

        /// <summary>
        /// Add the flags the mask names.
        /// </summary>
        Or,

        /// <summary>
        /// Flip the flags the mask names.
        /// </summary>
        Xor,

        /// <summary>
        /// Remove the flags the mask names.
        /// </summary>
        Clear,
    }
}
