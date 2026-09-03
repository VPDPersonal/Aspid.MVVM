using TMPro;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="TMP_Dropdown.options"/>
    /// on each element.
    /// </summary>
    /// <remarks>
    /// The list is copied; the selection is kept where the new list still has room for it.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), serializePropertyNames: "m_Options", SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options EnumGroup")]
    public sealed class DropdownOptionsEnumGroupMonoBinder
        : EnumGroupMonoBinder<TMP_Dropdown, List<TMP_Dropdown.OptionData>>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Dropdown element, List<TMP_Dropdown.OptionData> value) =>
            element.SetOptions(value);
    }
}
