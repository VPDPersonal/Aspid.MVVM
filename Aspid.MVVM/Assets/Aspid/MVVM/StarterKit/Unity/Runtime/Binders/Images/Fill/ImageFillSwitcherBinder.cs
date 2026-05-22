#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherFloatBinder{Image}"/> that switches the <see cref="Image.fillAmount"/> property
    /// between two <see cref="float"/> values based on the bound boolean ViewModel value.
    /// </summary>
    /// <remarks>
    /// The applied value is clamped to [0, 1] before being applied to <see cref="Image.fillAmount"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-Image-Fill-1.1.0.xml" path="doc//member[@name='ImageFillSwitcherBinder']/*" />
    [Serializable]
    public sealed class ImageFillSwitcherBinder : SwitcherFloatBinder<Image>
    {
        /// <inheritdoc/>
        public ImageFillSwitcherBinder(
            Image target,
            float trueValue,
            float falseValue,
            IConverter<float, float>? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        /// <summary>
        /// Called when applying the selected value to the <see cref="Image.fillAmount"/> property.
        /// Clamps the value to the valid range of 0 to 1.
        /// </summary>
        protected override void SetValue(float value) =>
            Target.fillAmount = Mathf.Clamp01(value);
    }
}