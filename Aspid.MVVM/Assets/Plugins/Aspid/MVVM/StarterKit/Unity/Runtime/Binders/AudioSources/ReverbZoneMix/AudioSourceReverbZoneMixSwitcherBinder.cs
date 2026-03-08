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
    /// Binder that switches the <see cref="AudioSource.reverbZoneMix"/> between two float values based
    /// on a bound boolean ViewModel property. The applied value is clamped to [0, 1.1].
    /// </summary>
    [Serializable]
    public sealed class AudioSourceReverbZoneMixSwitcherBinder : SwitcherBinder<AudioSource, float, Converter>
    {
        public AudioSourceReverbZoneMixSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public AudioSourceReverbZoneMixSwitcherBinder(
            AudioSource target,
            float trueValue,
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode) { }
        
        protected override void SetValue(float value) =>
            Target.reverbZoneMix = Mathf.Clamp(value, min: 0, max: 1.1f);
    }
}