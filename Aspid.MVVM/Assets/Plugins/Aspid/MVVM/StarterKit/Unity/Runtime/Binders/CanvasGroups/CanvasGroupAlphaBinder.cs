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
    [Serializable]
    public class CanvasGroupAlphaBinder : TargetFloatBinder<CanvasGroup>
    {
        protected sealed override float Property
        {
            get => Target.alpha;
            set => Target.alpha = value;
        }

        public CanvasGroupAlphaBinder(CanvasGroup target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public CanvasGroupAlphaBinder(CanvasGroup target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp01(base.GetConvertedValue(value));
    }
}