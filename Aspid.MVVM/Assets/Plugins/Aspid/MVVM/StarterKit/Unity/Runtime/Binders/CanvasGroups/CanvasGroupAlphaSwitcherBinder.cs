#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Binder that switches the <see cref="CanvasGroup.alpha"/> property on a <see cref="CanvasGroup"/>
    /// between two values based on a bound boolean ViewModel value.
    /// </summary>
    [Serializable]
    public sealed class CanvasGroupAlphaSwitcherBinder : SwitcherBinder<CanvasGroup, float, Converter>
    {
        public CanvasGroupAlphaSwitcherBinder(
            CanvasGroup target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        public CanvasGroupAlphaSwitcherBinder(
            CanvasGroup target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }

        protected override void SetValue(float value) =>
            Target.alpha = Mathf.Clamp01(value);
    }
}