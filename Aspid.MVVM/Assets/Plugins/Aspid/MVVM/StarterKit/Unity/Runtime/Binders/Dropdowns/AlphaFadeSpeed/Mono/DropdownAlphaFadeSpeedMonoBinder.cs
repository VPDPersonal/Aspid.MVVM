#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="TMP_Dropdown.alphaFadeSpeed"/> property on a <see cref="TMP_Dropdown"/>
    /// when the bound ViewModel value changes.
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established the current value
    /// is sent back to the ViewModel.
    /// </summary>
    [AddBinderContextMenu(typeof(TMP_Dropdown))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed")]
    public class DropdownAlphaFadeSpeedMonoBinder : ComponentFloatMonoBinder<TMP_Dropdown>
    {
        protected sealed override float Property
        {
            get => CachedComponent.alphaFadeSpeed;
            set => CachedComponent.alphaFadeSpeed = value;
        }

        protected override float GetConvertedValue(float value) =>
            Mathf.Max(base.GetConvertedValue(value), 0);
    }
}
#endif