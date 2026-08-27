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
        /// Sets <see cref="Image.fillAmount"/> to <paramref name="value"/>, clamped to 0..1.
        /// </summary>
        /// <param name="value">The value received from the ViewModel.</param>
        protected override void SetValue(float value) =>
            Target.fillAmount = BinderMath.SafeClamp01(value);
    }
}