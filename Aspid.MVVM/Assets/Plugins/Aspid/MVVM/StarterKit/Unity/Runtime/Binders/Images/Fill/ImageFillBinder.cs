#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="TargetFloatBinder{Image}"/> that sets the <see cref="Image.fillAmount"/> property.
    /// </summary>
    /// <remarks>
    /// The bound value is clamped to [0, 1] before being applied to <see cref="Image.fillAmount"/>.
    /// </remarks>
    /// <include file="XmlExampleDoc-Image-Fill-1.1.0.xml" path="doc//member[@name='ImageFillBinder']/*" />
    [Serializable]
    public class ImageFillBinder : TargetFloatBinder<Image>
    {
        protected sealed override float Property
        {
            get => Target.fillAmount;
            set => Target.fillAmount = value;
        }

        /// <inheritdoc/>
        public ImageFillBinder(Image target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Called when converting the bound value before applying it to the <see cref="Image.fillAmount"/> property.
        /// Clamps the converted value to the valid range of 0 to 1.
        /// </summary>
        /// <remarks>
        /// When overriding this method, always call <c>base.GetConvertedValue(value)</c> to preserve
        /// the clamping behavior.
        /// </remarks>
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp01(base.GetConvertedValue(value));
    }
}