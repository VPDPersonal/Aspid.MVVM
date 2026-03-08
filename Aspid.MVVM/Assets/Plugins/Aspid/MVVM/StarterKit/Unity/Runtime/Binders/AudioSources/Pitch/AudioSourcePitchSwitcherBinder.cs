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
    /// Binder that switches the <see cref="AudioSource.pitch"/> between two float values based
    /// on a bound boolean ViewModel property. The value is clamped to the range [-3, 3].
    /// </summary>
    [Serializable]
    public sealed class AudioSourcePitchSwitcherBinder : SwitcherBinder<AudioSource, float, Converter>
    {
        public AudioSourcePitchSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }

        public AudioSourcePitchSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }
        
        protected override void SetValue(float value) =>
            Target.pitch = value;
        
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: -3, max: 3);
    }
}