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
    public class AudioSourceMinMaxDistanceBinder : TargetBinder<AudioSource, Vector2>, INumberBinder
    {
        [SerializeField] private AudioSourceDistanceMode _distanceMode = AudioSourceDistanceMode.Range;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        protected sealed override Vector2 Property
        {
            get => new(Target.minDistance, Target.maxDistance);
            set => Target.SetMinMaxDistance(value, _distanceMode);
        }
        
        public AudioSourceMinMaxDistanceBinder(
            AudioSource target, 
            BindMode mode)
            : this(target, AudioSourceDistanceMode.Range, converter: null, mode) { }
        
        public AudioSourceMinMaxDistanceBinder(
            AudioSource target, 
            AudioSourceDistanceMode distanceMode, 
            BindMode mode)
            : this(target, distanceMode, converter: null, mode) { }
        
        public AudioSourceMinMaxDistanceBinder(
            AudioSource target, 
            Converter? converter,
            BindMode mode = BindMode.OneWay)
            : this(target, AudioSourceDistanceMode.Range, converter, mode) { }
        
        public AudioSourceMinMaxDistanceBinder(
            AudioSource target, 
            AudioSourceDistanceMode distanceMode = AudioSourceDistanceMode.Range, 
            Converter? converter = null,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfMatches(BindMode.TwoWay);
            _distanceMode = distanceMode;
            _converter = converter; 
        }
        
        public void SetValue(float value) =>
            SetValue(new Vector2(value, value));

        public void SetValue(int value) =>
            SetValue((float)value);

        public void SetValue(long value) =>
            SetValue((float)value);

        public void SetValue(double value) =>
            SetValue((float)value);
        
        protected override Vector2 GetConvertedValue(Vector2 value) =>
            _converter?.Convert(value) ?? value;
    }
}