using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Where <see cref="EnumToStringConverter{TEnum}"/> takes the text it returns.
    /// </summary>
    /// <remarks>Members are appended, never inserted: the order is the serialized value.</remarks>
    public enum EnumNameSource
    {
        /// <summary>
        /// The member name as written in code.
        /// </summary>
        Name,

        /// <summary>
        /// The <see cref="InspectorNameAttribute"/> on the member, falling back to its name.
        /// </summary>
        InspectorName,

        /// <summary>
        /// The <see cref="System.ComponentModel.DescriptionAttribute"/> on the member, falling back
        /// to its name.
        /// </summary>
        Description,

        // Named Raw rather than ToString: a member named ToString would shadow the method on the enum.
        /// <summary>
        /// The value's own <c>ToString</c>: a flag combination reads as a comma-separated list, an
        /// undeclared value as its number.
        /// </summary>
        Raw,
    }
}
