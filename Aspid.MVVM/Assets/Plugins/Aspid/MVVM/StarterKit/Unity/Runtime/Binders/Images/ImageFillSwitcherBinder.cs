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
    /// Binder that switches the <see cref="UnityEngine.UI.Image.fillAmount"/> property on an <see cref="UnityEngine.UI.Image"/>
    /// between two values based on a bound boolean ViewModel value.
    /// </summary>
    [Serializable]
    public sealed class ImageFillSwitcherBinder : SwitcherBinder<Image, float, Converter>
    {
        public ImageFillSwitcherBinder(
            Image target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        public ImageFillSwitcherBinder(
            Image target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(float value) =>
            Target.fillAmount = Mathf.Clamp01(value);
    }
}