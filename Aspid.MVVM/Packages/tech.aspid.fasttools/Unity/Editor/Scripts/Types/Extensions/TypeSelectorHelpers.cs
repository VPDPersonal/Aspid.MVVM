using System;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Shared constants and formatting helpers used by the type-selector UI
    /// (<see cref="TypeSelectorWindow"/>, <see cref="TypeField"/>, IMGUI drawers).
    /// </summary>
    internal static class TypeSelectorHelpers
    {
        /// <summary>
        /// Display string used for the "no type selected" option.
        /// </summary>
        public const string NoneOption = "<None>";

        /// <summary>
        /// Display string used for types that have no namespace (global namespace).
        /// </summary>
        public const string GlobalNamespace = "<Global>";

        /// <summary>
        /// Formats a caption for the type-selector dropdown.
        /// Returns the type's short name when <paramref name="value"/> is resolved,
        /// a <c>&lt;Missing ...&gt;</c> marker when the assembly-qualified name is non-empty
        /// but the type could not be resolved, or <see cref="NoneOption"/> when neither is provided.
        /// </summary>
        /// <param name="value">The resolved <see cref="Type"/>, or <see langword="null"/> if unresolved.</param>
        /// <param name="assemblyQualifiedName">
        /// The assembly-qualified name that was attempted. Pass non-null only when the type
        /// could not be resolved — passing it for a successfully resolved type forces the
        /// <c>&lt;Missing&gt;</c> branch.
        /// </param>
        public static string GetTypeSelectorTitle(Type value, string assemblyQualifiedName = null)
        {
            if (value is not null) return value.Name;

            return string.IsNullOrWhiteSpace(assemblyQualifiedName)
                ? NoneOption
                : $"<Missing {assemblyQualifiedName}>";
        }
    }
}
