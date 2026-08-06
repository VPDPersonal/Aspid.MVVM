using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.FastTools.Types.Editors
{
    /// <summary>
    /// Builds the keyboard-hint line shown in the type selector footer from the current selection and the
    /// search/navigation state. Pure string composition with no view access, so it stays trivially testable.
    /// </summary>
    internal static class TypeSelectorFooterHint
    {
        internal static string Build(
            bool searchFocused,
            TreeNode selected,
            bool isSelectedSectionCollapsed,
            bool isSearching,
            bool searchChromeOpen,
            bool canNavigateBack,
            bool hasParentPage)
        {
            var parts = new List<string> { "↑↓ Navigate" };

            if (!searchFocused && selected is { IsSectionTitle: true })
                parts.Add(isSelectedSectionCollapsed ? "→ Expand" : "← Collapse");
            else if (!searchFocused && selected is { HasChildren: true } && !isSearching)
                parts.Add("→ Open");
            else if (selected is { IsSelectable: true })
                parts.Add("Enter Select");

            if (!searchFocused && selected is { IsType: true })
            {
                parts.Add(TypeSelectorPreferences.IsFavorite(selected.AssemblyQualifiedName)
                    ? TypeSelectorHelpers.StarFilled + " Space Unfavorite"
                    : TypeSelectorHelpers.StarEmpty + " Space Favorite");
            }

            if (isSearching)
            {
                parts.Add("Esc Clear");
            }
            else if (searchChromeOpen)
            {
                parts.Add("Esc Cancel");
            }
            else
            {
                if (!searchFocused && (canNavigateBack || hasParentPage)) parts.Add("← Back");

                parts.Add("Type to search");
                parts.Add("Esc Close");
            }

            // The " · " separator matches the dot-joined summaries elsewhere in the package and keeps the line
            // narrow enough to share the footer with the settings gear.
            return string.Join(" · ", parts);
        }
    }
}
