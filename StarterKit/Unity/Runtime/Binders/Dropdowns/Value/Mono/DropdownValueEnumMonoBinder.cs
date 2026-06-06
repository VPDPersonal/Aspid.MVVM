#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumIntMonoBinder{TMP_Dropdown}"/> that sets the <see cref="TMP_Dropdown.value"/>
    /// property to a value resolved from a bound enum ViewModel property.
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