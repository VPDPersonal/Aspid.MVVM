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
        /// <exception cref="ArgumentException">Thrown when <paramref name="mode"/> is <see cref="BindMode.TwoWay"/>.</exception>
        public ImageFillBinder(Image target, IConverter<float, float>? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        /// <summary>
        /// Clamps <paramref name="value"/> to 0..1 before it reaches <see cref="Image.fillAmount"/>.
        /// </summary>
        /// <remarks>Override calls must invoke the base implementation to preserve the clamping.</remarks>
        /// <param name="value">The value to convert.</param>
        protected override float GetConvertedValue(float value) =>
            BinderMath.SafeClamp01(base.GetConvertedValue(value));
    }
}