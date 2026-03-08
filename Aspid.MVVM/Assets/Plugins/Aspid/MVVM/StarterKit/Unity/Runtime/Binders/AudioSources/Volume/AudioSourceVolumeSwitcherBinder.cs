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
    /// Binder that switches the <see cref="AudioSource.volume"/> between two float values based
    /// on a bound boolean ViewModel property. The value is clamped to the range [0, 1].
    /// </summary>
    [Serializable]
    public sealed class AudioSourceVolumeSwitcherBinder : SwitcherBinder<AudioSource, float, Converter>
    {
        public AudioSourceVolumeSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public AudioSourceVolumeSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }
        
        protected override void SetValue(float value) =>
            Target.volume = Mathf.Clamp(value, min: 0, max: 1);
    }
}