#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{TComponent,T}">SwitcherMonoBinder&lt;TMP_Dropdown, int&gt;</see> that switches the <see cref="TMP_Dropdown.value"/>
    /// property between two <see cref="int"/> values based on the bound boolean ViewModel value.
    /// </summary>
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value Switcher")]
    public sealed class DropdownValueSwitcherMonoBinder : SwitcherMonoBinder<TMP_Dropdown, int>
    {
        /// <inheritdoc/>
        protected override void SetValue(int value) =>
            CachedComponent.SetValueWithoutNotify(value);
    }
}
#endif