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
    public class AudioSourceTimeBinder : TargetFloatBinder<AudioSource>
    {
        protected sealed override float Property
        {
            get => Target.time;
            set => Target.time = value;
        }

        public AudioSourceTimeBinder(AudioSource target, BindMode mode)
            : this(target, converter: null, mode) { }
        
        public AudioSourceTimeBinder(AudioSource target, Converter? converter = null, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }
    }
}