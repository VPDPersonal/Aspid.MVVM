#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that sets the <see cref="UnityEngine.UI.Image.fillAmount"/> property on an <see cref="UnityEngine.UI.Image"/>
    /// when the bound ViewModel value changes.
    /// </summary>
    [Serializable]
    public class ImageFillBinder : TargetFloatBinder<Image>
    {
        protected sealed override float Property
        {
            get => Target.fillAmount;
            set => Target.fillAmount = value;
        }

        public ImageFillBinder(Image target, BindMode mode)
            : this(target, converter: null, mode) { }

        public ImageFillBinder(Image target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp01(base.GetConvertedValue(value));
    }
}