#nullable enable
using System;
using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector2, UnityEngine.Vector2>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector2;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    public sealed class AudioSourceMinMaxDistanceSwitcherBinder : SwitcherBinder<AudioSource, Vector2, Converter>
    {
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;
        
        public AudioSourceMinMaxDistanceSwitcherBinder(
            AudioSource target,
            Vector2 trueValue, 
            Vector2 falseValue,
            BindMode mode)
            : this(target, trueValue, falseValue, AudioSourceDistanceMode.Range, converter: null, mode) { }
        
        public AudioSourceMinMaxDistanceSwitcherBinder(
            AudioSource target,
            Vector2 trueValue, 
            Vector2 falseValue,
            AudioSourceDistanceMode distanceMode,
            BindMode mode)
            : this(target, trueValue, falseValue, distanceMode, converter: null, mode) { }
        
        public AudioSourceMinMaxDistanceSwitcherBinder(
            AudioSource target,
            Vector2 trueValue, 
            Vector2 falseValue,
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, trueValue, falseValue, AudioSourceDistanceMode.Range, converter, mode) { }
        
        public AudioSourceMinMaxDistanceSwitcherBinder(
            AudioSource target,
            Vector2 trueValue, 
            Vector2 falseValue,
            AudioSourceDistanceMode distanceMode = AudioSourceDistanceMode.Range,
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, converter, mode)
        {
            _distanceMode = distanceMode;
        }

        protected override void SetValue(Vector2 value) =>
            Target.SetMinMaxDistance(value, _distanceMode);
    }
}