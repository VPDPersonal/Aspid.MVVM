// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="EnumMatchConverter{TEnum}"/> tests a bound enum value.
    /// </summary>
    /// <remarks>Members are appended, never inserted: the order is the serialized value.</remarks>
    public enum EnumMatchMode
    {
        /// <summary>
        /// The value must equal the target.
        /// </summary>
        Equal,

        /// <summary>
        /// The value must differ from the target.
        /// </summary>
        NotEqual,

        /// <summary>
        /// The value must have every flag the target has.
        /// </summary>
        HasAllFlags,

        /// <summary>
        /// The value must have at least one flag the target has.
        /// </summary>
        HasAnyFlag,
    }
}
