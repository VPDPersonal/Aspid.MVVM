#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatMonoBinder{TMP_Dropdown}"/> that switches the <see cref="TMP_Dropdown.alphaFadeSpeed"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The applied value is clamped to a minimum of 0.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "Switcher")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed Switcher")]
    public sealed class DropdownAlphaFadeSpeedSwitcherMonoBinder : SwitcherFloatMonoBinder<TMP_Dropdown>
    {
        /// <summary>
        /// Applies the selected value to the <see cref="TMP_Dropdown.alphaFadeSpeed"/> property.
        /// Clamps the value to a minimum of 0.
        /// </summary>
        /// <param name="value">The float value to apply.</param>
        protected override void SetValue(float value) =>
            CachedComponent.alphaFadeSpeed = Mathf.Max(value, 0);
    }
}
#endif