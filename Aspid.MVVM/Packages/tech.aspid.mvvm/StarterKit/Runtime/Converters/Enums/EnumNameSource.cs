using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Where <see cref="EnumToStringConverter{TEnum}"/> takes the text it returns.
    /// </summary>
    /// <remarks>
    /// New members are appended rather than inserted: the order is the serialized value, so moving
    /// one silently rewrites every converter already authored in a scene.
    /// </remarks>
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
        /// to its name. The attribute holds a sentence rather than a label, and is the one a
        /// non-Unity layer — a shared domain assembly, a generated contract — can already carry.
        /// </summary>
        Description,

        // Named Raw rather than ToString because a member named after an inherited method takes that
        // method out of lookup on the enum: `source.ToString()` would stop compiling for everyone.
        /// <summary>
        /// The value's own <c>ToString</c>: a flag combination reads as a comma-separated list, and
        /// a value that is not a declared member reads as its number.
        /// </summary>
        Raw,
    }
}
