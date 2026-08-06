#nullable enable
using System;
using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types
{
    /// <summary>
    /// Supplies presentation metadata for a type shown in the type-selector window: a display name,
    /// a picker group, a tooltip and an icon.
    /// </summary>
    /// <example>
    /// Rename the type in the picker, place it under an explicit group and give it a tooltip and an icon:
    /// <code>
    /// [TypeSelectorDisplay(
    ///     Name = "Damage ×",
    ///     Group = "Combat/Modifiers",
    ///     Tooltip = "Scales incoming damage",
    ///     Icon = "d_ScriptableObject Icon")]
    /// public sealed class DamageModifier { }
    /// </code>
    /// </example>
    [Conditional(conditionString: "UNITY_EDITOR")]
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class TypeSelectorDisplayAttribute : Attribute
    {
        /// <summary>
        /// Display name shown instead of the type's short name — in the picker rows and in the closed
        /// dropdown's caption. Search keeps matching the real type name as well, and the hover tooltip
        /// still reveals the full <c>Namespace.Class, Assembly</c> identity. On a generic type the formatted
        /// arguments (or parameters) are appended after the custom name (<c>Mod&lt;T&gt;</c>, <c>Mod&lt;Single&gt;</c>).
        /// <see langword="null"/> or whitespace means no override.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Explicit picker path for the type, with <c>/</c> separating levels (e.g. <c>"Combat/Melee"</c>).
        /// The group <b>replaces</b> the type's namespace placement in the picker hierarchy — the type
        /// appears only under this path. Empty segments are ignored; <see langword="null"/> or whitespace
        /// means the type stays under its namespace.
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// Tooltip shown when hovering the type's row. <see langword="null"/> means no tooltip override.
        /// </summary>
        public string? Tooltip { get; set; }

        /// <summary>
        /// Editor icon to show left of the label. One of: an <c>EditorGUIUtility.IconContent</c> name
        /// (e.g. <c>"d_ScriptableObject Icon"</c>); a project-relative asset path with extension
        /// (e.g. <c>"Assets/Art/Icons/Damage.png"</c>, loaded via <c>AssetDatabase</c>); or a <c>Resources</c>
        /// texture path without extension (e.g. <c>"Icons/Damage"</c>). The editor resolves the value lazily;
        /// <see langword="null"/> means no icon.
        /// </summary>
        public string? Icon { get; set; }
    }
}
