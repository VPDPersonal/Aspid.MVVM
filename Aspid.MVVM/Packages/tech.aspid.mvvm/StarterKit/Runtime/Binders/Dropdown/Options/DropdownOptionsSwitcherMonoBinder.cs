using TMPro;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent, T}"/> that switches <see cref="TMP_Dropdown.options"/>.
    /// </summary>
    /// <remarks>
    /// The list is copied; the selection is kept where the new list still has room for it.
    /// </remarks>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Options", SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options Switcher")]
    public sealed class DropdownOptionsSwitcherMonoBinder
        : SwitcherMonoBinder<TMP_Dropdown, List<TMP_Dropdown.OptionData>>
    {
        /// <inheritdoc/>
        protected override void SetValue(List<TMP_Dropdown.OptionData> value) =>
            CachedComponent.SetOptions(value);
    }
}
