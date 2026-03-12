#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TMP_Dropdown}"/> that sets the <see cref="TMP_Dropdown.alphaFadeSpeed"/>
    /// property when the bound ViewModel value changes.
    /// </summary>
    /// <remarks>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current value
    /// is sent back to the ViewModel.
    /// The bound value is clamped to a minimum of 0 before being applied.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown))]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed")]
    public class DropdownAlphaFadeSpeedMonoBinder : ComponentFloatMonoBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => CachedComponent.alphaFadeSpeed;
            set => CachedComponent.alphaFadeSpeed = value;
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="TMP_Dropdown.alphaFadeSpeed"/> property.
        /// Clamps the converted value to a minimum of 0.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted and clamped value.</returns>
        protected override float GetConvertedValue(float value) =>
            Mathf.Max(base.GetConvertedValue(value), 0);
    }
}
#endif