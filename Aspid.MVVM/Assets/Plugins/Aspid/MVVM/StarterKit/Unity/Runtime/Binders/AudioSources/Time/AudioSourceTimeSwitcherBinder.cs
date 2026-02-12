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
    public sealed class AudioSourceTimeSwitcherBinder : SwitcherBinder<AudioSource, float>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public AudioSourceTimeSwitcherBinder(
            AudioSource target,
            float trueValue, 
            float falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, converter: null, mode) { }
        
        public AudioSourceTimeSwitcherBinder(
            AudioSource target,
            float trueValue, 
            float falseValue,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode)
        {
            _converter = converter;
        }

        protected override void SetValue(float value) =>
            Target.time = _converter?.Convert(value) ?? value;
    }
}