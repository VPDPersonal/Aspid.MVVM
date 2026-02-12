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
    public sealed class AudioSourceTimeSamplesSwitcherBinder : SwitcherBinder<AudioSource, int>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public AudioSourceTimeSamplesSwitcherBinder(
            AudioSource target,
            int trueValue, 
            int falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public AudioSourceTimeSamplesSwitcherBinder(
            AudioSource target,
            int trueValue, 
            int falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(int value) =>
            Target.timeSamples = _converter?.Convert(value) ?? value;
    }
}