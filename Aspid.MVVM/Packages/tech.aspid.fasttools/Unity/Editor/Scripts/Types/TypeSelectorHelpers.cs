using System;
using System.Reflection;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Shared constants and formatting helpers for the type-selector UI.
    /// </summary>
    internal static class TypeSelectorHelpers
    {
        internal const string None = "○";
        internal const string Check = "✓";
        internal const string StarEmpty = "☆";
        internal const string StarFilled = "★";
        internal const string NoneOption = "<None>";
        internal const string GlobalNamespace = "<Global>";

        private static readonly Dictionary<Type, string> _customDisplayNames = new();

        /// <summary>
        /// The normalized <see cref="TypeSelectorDisplayAttribute.Name"/> override for <paramref name="value"/>,
        /// or <see langword="null"/> when the type declares none.
        /// </summary>
        /// <remarks>
        /// A whitespace-only value counts as none, as does a value equal to the <see cref="NoneOption"/> sentinel —
        /// a real type must not impersonate it. A generic keeps its formatted arguments after the custom base name
        /// (<c>Mod&lt;Single&gt;</c> for a constructed form, <c>Mod&lt;T&gt;</c> for the open definition), so closed
        /// forms stay distinguishable and an open definition still reads as generic.
        /// </remarks>
        internal static string GetCustomDisplayName(Type value)
        {
            if (value is null) return null;
            if (_customDisplayNames.TryGetValue(value, out var cached)) return cached;

            var attribute = value.GetCustomAttribute<TypeSelectorDisplayAttribute>(inherit: false);
            var name = string.IsNullOrWhiteSpace(attribute?.Name)
                ? null
                : attribute.Name.Trim();

            if (name == NoneOption)
                name = null;

            if (name is not null && value.IsGenericType)
            {
                var formatted = TypeUtility.FormatGenericName(value);
                name += formatted[formatted.IndexOf('<')..];
            }

            _customDisplayNames[value] = name;
            return name;
        }

        /// <summary>
        /// Formats a caption for the type-selector dropdown.
        /// Returns the type's <see cref="TypeSelectorDisplayAttribute.Name"/> override (or its short name)
        /// when <paramref name="value"/> is resolved, a <c>&lt;Missing ...&gt;</c> marker when the
        /// assembly-qualified name is non-empty but the type could not be resolved, or
        /// <see cref="NoneOption"/> when neither is provided.
        /// </summary>
        /// <param name="value">The resolved <see cref="Type"/>, or <see langword="null"/> if unresolved.</param>
        /// <param name="assemblyQualifiedName">
        /// The assembly-qualified name that was attempted; used only when <paramref name="value"/> is
        /// <see langword="null"/>, ignored otherwise.
        /// </param>
        internal static string GetTypeSelectorTitle(Type value, string assemblyQualifiedName = null)
        {
            if (value is not null)
                return GetCustomDisplayName(value) ?? TypeUtility.FormatGenericName(value);

            return string.IsNullOrWhiteSpace(assemblyQualifiedName)
                ? NoneOption
                : $"<Missing {assemblyQualifiedName}>";
        }

        /// <summary>
        /// Formats the hover tooltip for a resolved type in the type-selector dropdown — the full
        /// <c>Namespace.Class, Assembly</c> identity (generic arguments spelled out) — or
        /// <see langword="null"/> for no type.
        /// </summary>
        /// <remarks>
        /// The caption shows only the short name (or its <see cref="TypeSelectorDisplayAttribute.Name"/> override),
        /// so the tooltip is where the full identity stays readable.
        /// </remarks>
        internal static string GetTypeSelectorTooltip(Type value)
        {
            if (value is null) return null;

            var name = TypeUtility.FormatGenericName(value);
            var displayName = string.IsNullOrEmpty(value.Namespace) ? name : $"{value.Namespace}.{name}";

            return $"{displayName}, {value.Assembly.GetName().Name}";
        }
    }
}
