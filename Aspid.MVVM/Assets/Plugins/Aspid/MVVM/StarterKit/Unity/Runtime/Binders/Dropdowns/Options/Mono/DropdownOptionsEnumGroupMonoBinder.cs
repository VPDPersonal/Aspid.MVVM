#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Options EnumGroup")]
    public sealed class DropdownOptionsEnumGroupMonoBinder : EnumGroupMonoBinder<TMP_Dropdown, List<TMP_Dropdown.OptionData>>
    {
        protected override void SetValue(TMP_Dropdown element, List<TMP_Dropdown.OptionData> value) =>
            element.options = value;
    }
}
#endif