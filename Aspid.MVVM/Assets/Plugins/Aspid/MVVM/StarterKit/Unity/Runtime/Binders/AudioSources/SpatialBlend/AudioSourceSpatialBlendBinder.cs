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
    public class AudioSourceSpatialBlendBinder : TargetFloatBinder<AudioSource>
    {
        protected sealed override float Property
        {
            get => Target.spatialBlend;
            set => Target.spatialBlend = value;
        }

        public AudioSourceSpatialBlendBinder(AudioSource target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public AudioSourceSpatialBlendBinder(AudioSource target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}