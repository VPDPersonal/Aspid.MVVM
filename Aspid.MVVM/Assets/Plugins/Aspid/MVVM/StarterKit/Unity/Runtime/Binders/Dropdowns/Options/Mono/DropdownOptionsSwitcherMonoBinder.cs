#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options Switcher")]
    public sealed class DropdownOptionsSwitcherMonoBinder : SwitcherMonoBinder<TMP_Dropdown, List<TMP_Dropdown.OptionData>>
    {
        protected override void SetValue(List<TMP_Dropdown.OptionData> value) =>
            CachedComponent.options = value;
    }
}
#endif