#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="TMP_Dropdown.value"/> property on a <see cref="TMP_Dropdown"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current value
    /// is sent back to the ViewModel.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value")]
    public class DropdownValueMonoBinder : ComponentIntMonoBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.value;
            set => CachedComponent.value = value;
        }
    }
}
#endif