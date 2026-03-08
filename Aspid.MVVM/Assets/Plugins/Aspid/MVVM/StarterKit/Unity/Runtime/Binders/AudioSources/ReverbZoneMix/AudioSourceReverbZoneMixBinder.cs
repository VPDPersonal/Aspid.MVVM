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
    /// Binder that sets the <see cref="AudioSource.reverbZoneMix"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes. The value is clamped to the range [0, 1.1].
    /// </summary>
    [Serializable]
    public class AudioSourceReverbZoneMixBinder : TargetFloatBinder<AudioSource>
    {
        protected sealed override float Property
        {
            get => Target.reverbZoneMix;
            set => Target.reverbZoneMix = value;
        }
        
        public AudioSourceReverbZoneMixBinder(AudioSource target, BindMode mode)
            : this(target, converter: null, mode) { }

        public AudioSourceReverbZoneMixBinder(AudioSource target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 1.1f);
    }
}