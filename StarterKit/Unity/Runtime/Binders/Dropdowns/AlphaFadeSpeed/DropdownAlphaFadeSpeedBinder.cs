#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{TMP_Dropdown}"/> that sets the <see cref="TMP_Dropdown.alphaFadeSpeed"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to a minimum of 0 before being applied.
    /// </remarks>
    /// <include file="XmlExampleDoc-Dropdown-AlphaFadeSpeed-1.1.0.xml" path="doc//member[@name='DropdownAlphaFadeSpeedBinder']/*" />
    [Serializable]
    public class DropdownAlphaFadeSpeedBinder : TargetFloatBinder<TMP_Dropdown>
    {
        /// <inheritdoc/>
        protected sealed override float Property
        {
            get => Target.alphaFadeSpeed;
            set => Target.alphaFadeSpeed = value;
        }
        
        /// <inheritdoc/>
        public DropdownAlphaFadeSpeedBinder(TMP_Dropdown target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

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