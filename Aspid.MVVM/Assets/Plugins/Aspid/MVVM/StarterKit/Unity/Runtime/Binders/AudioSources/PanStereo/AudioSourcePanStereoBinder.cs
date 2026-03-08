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
    /// Binder that sets the <see cref="AudioSource.panStereo"/> property on an <see cref="AudioSource"/>
    /// when the bound ViewModel value changes. The value is clamped to the range [-1, 1].
    /// </summary>
    [Serializable]
    public class AudioSourcePanStereoBinder : TargetFloatBinder<AudioSource>
    {
        protected sealed override float Property
        {
            get => Target.panStereo;
            set => Target.panStereo = value;
        }

        public AudioSourcePanStereoBinder(AudioSource target, BindMode mode)
            : this(target, converter: null, mode) { }

        public AudioSourcePanStereoBinder(AudioSource target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
        
        protected override float GetConvertedValue(float value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: -1, max: 1);
    }
}