#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="TMP_Dropdown.value"/> property on a <see cref="TMP_Dropdown"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value Enum")]
    public sealed class DropdownValueEnumMonoBinder : EnumIntMonoBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.value = value;
    }
}
#endif