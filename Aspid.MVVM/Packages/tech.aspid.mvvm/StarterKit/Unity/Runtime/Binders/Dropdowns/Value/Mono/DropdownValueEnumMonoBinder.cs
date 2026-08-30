#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TComponent,TValue}">EnumMonoBinder&lt;TMP_Dropdown, int&gt;</see> that sets the <see cref="TMP_Dropdown.value"/>
    /// property to a value resolved from a bound enum ViewModel property.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value Enum")]
    public sealed class DropdownValueEnumMonoBinder : EnumMonoBinder<TMP_Dropdown, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.SetValueWithoutNotify(value);
    }
}
#endif