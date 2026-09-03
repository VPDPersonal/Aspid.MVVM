using TMPro;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent, TValue}"/> that sets <see cref="TMP_Dropdown.options"/>.
    /// </summary>
    /// <remarks>
    /// The list is copied; the selection is kept where the new list still has room for it.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Options", SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options Enum")]
    public sealed class DropdownOptionsEnumMonoBinder : EnumMonoBinder<TMP_Dropdown, List<TMP_Dropdown.OptionData>>
    {
        /// <inheritdoc/>
        protected override void SetValue(List<TMP_Dropdown.OptionData> value) =>
            CachedComponent.SetOptions(value);
    }
}
