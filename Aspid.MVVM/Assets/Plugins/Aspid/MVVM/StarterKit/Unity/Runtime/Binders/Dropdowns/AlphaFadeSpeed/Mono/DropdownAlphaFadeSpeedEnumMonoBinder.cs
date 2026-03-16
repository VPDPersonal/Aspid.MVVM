#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumFloatMonoBinder{TMP_Dropdown}"/> that sets the <see cref="TMP_Dropdown.alphaFadeSpeed"/>
    /// property to a value resolved from a bound enum ViewModel property.
    /// </summary>
    /// <remarks>
    /// The applied value is clamped to a minimum of 0.
    /// </remarks>
    [AddBinderContextMenu(typeof(TMP_Dropdown), SubPath = "Enum")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – AlphaFadeSpeed Enum")]
    public sealed class DropdownAlphaFadeSpeedEnumMonoBinder : EnumFloatMonoBinder<TMP_Dropdown>
    {
        /// <summary>
        /// Applies the resolved enum value to the <see cref="TMP_Dropdown.alphaFadeSpeed"/> property.
        /// Clamps the value to a minimum of 0.
        /// </summary>
        /// <param name="value">The float value resolved from the bound enum.</param>
        protected override void SetValue(float value) =>
            CachedComponent.alphaFadeSpeed = Mathf.Max(value, 0);
    }
}
#endif