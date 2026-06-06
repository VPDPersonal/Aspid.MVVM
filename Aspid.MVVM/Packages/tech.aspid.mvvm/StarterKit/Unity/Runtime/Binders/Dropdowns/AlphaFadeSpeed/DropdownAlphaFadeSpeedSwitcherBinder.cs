#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatBinder{TMP_Dropdown}"/> that switches the <see cref="TMP_Dropdown.alphaFadeSpeed"/>
    /// property between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The applied value is clamped to a minimum of 0.
    /// </remarks>
    /// <include file="XmlExampleDoc-Dropdown-AlphaFadeSpeed-1.1.0.xml" path="doc//member[@name='DropdownAlphaFadeSpeedSwitcherBinder']/*" />
    [Serializable]
    public class DropdownAlphaFadeSpeedSwitcherBinder : SwitcherFloatBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        public DropdownAlphaFadeSpeedSwitcherBinder(
            TMP_Dropdown target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <inheritdoc/>
        protected override void SetValue(float value) =>
            Target.alphaFadeSpeed = value;

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="TMP_Dropdown.alphaFadeSpeed"/> property.
        /// Clamps the converted value to a minimum of 0.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call the base implementation to preserve
        /// the clamping behavior.
        /// </remarks>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted and clamped value.</returns>
        protected override float GetConvertedValue(float value) =>
            Mathf.Max(base.GetConvertedValue(value), 0);
    }
}
#endif