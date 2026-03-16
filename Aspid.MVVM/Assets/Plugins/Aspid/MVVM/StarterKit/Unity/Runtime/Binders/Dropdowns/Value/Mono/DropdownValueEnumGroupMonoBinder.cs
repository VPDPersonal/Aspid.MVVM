#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupIntMonoBinder{TMP_Dropdown}"/> that sets the <see cref="TMP_Dropdown.value"/>
    /// property on each element based on the bound enum ViewModel value.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value EnumGroup")]
    public sealed class DropdownValueEnumGroupMonoBinder : EnumGroupIntMonoBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        protected override void SetValue(TMP_Dropdown element, int value) =>
            element.value = value;
    }
}
#endif