using TMPro;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods that write validated values to a <see cref="TMP_Dropdown"/>.
    /// </summary>
    public static class DropdownExtensions
    {
        /// <summary>
        /// Replaces <see cref="TMP_Dropdown.options"/> with a copy of <paramref name="options"/>, keeping the selection
        /// where the new list still has room for it.
        /// </summary>
        /// <remarks>
        /// <see langword="null"/> clears the options. The list is copied, so the source is never mutated by the dropdown.
        /// </remarks>
        /// <param name="dropdown">The dropdown whose options are replaced.</param>
        /// <param name="options">The options to copy, or <see langword="null"/> to clear.</param>
        public static void SetOptions(this TMP_Dropdown dropdown, List<TMP_Dropdown.OptionData> options)
        {
            var selected = dropdown.value;

            dropdown.ClearOptions();
            if (options is not null) dropdown.AddOptions(options);

            if (dropdown.options.Count > 0)
                dropdown.SetValueWithoutNotify(Mathf.Clamp(selected, 0, dropdown.options.Count - 1));

            dropdown.RefreshShownValue();
        }
    }
}
