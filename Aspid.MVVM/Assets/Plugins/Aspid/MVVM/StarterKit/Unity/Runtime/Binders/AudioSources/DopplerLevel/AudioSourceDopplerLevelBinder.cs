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
    public class AudioSourceDopplerLevelBinder : TargetFloatBinder<AudioSource>
    {
        protected sealed override float Property
        {
            get => Target.dopplerLevel;
            set => Target.dopplerLevel = value;
        }
        
        public AudioSourceDopplerLevelBinder(AudioSource target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public AudioSourceDopplerLevelBinder(AudioSource target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}