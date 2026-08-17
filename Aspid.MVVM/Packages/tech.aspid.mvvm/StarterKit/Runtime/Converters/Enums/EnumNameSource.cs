using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Where <see cref="EnumToStringConverter{TEnum}"/> takes the text it returns.
    /// </summary>
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
    }
}
