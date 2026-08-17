// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// How <see cref="EnumToBoolConverter{TEnum}"/> tests a bound enum value.
    /// </summary>
    public enum EnumMatch
    {
        /// <summary>
        /// The value must equal the target.
        /// </summary>
        Equals,

        /// <summary>
        /// The value must differ from the target.
        /// </summary>
        NotEquals,

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
