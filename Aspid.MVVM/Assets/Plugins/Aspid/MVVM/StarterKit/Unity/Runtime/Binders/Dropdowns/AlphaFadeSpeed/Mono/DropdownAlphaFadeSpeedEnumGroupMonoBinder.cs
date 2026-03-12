#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupFloatMonoBinder{TMP_Dropdown}"/> that sets the <see cref="TMP_Dropdown.alphaFadeSpeed"/>
    /// property on each <see cref="TMP_Dropdown"/> in the group based on the bound enum ViewModel value.
    /// </summary>
    /// <remarks>
    /// The applied value is clamped to a minimum of 0.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed EnumGroup")]
    public sealed class DropdownAlphaFadeSpeedEnumGroupMonoBinder : EnumGroupFloatMonoBinder<TMP_Dropdown>
    {
        /// <summary>
        /// Applies the resolved value to the <see cref="TMP_Dropdown.alphaFadeSpeed"/> property of a single <see cref="TMP_Dropdown"/>.
        /// Clamps the value to a minimum of 0.
        /// </summary>
        /// <param name="element">The dropdown component to update.</param>
        /// <param name="value">The float value to apply.</param>
        protected override void SetValue(TMP_Dropdown element, float value) =>
            element.alphaFadeSpeed = Mathf.Max(value, 0);
    }
}
#endif