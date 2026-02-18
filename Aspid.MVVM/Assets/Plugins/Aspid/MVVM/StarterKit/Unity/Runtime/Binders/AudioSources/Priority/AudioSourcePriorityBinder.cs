#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<int, int>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterInt;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public class AudioSourcePriorityBinder : TargetIntBinder<AudioSource>
    {
        protected sealed override int Property
        {
            get => Target.priority;
            set => Target.priority = value;
        }
        
        public AudioSourcePriorityBinder(AudioSource target, Converter? converter, BindMode mode = BindMode.OneWay)
            : base(target, converter, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
        }

        protected override int GetConvertedValue(int value) =>
            Mathf.Clamp(base.GetConvertedValue(value), min: 0, max: 256);
    }
}