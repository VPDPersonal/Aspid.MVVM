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
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – DopplerLevel")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public partial class AudioSourceDopplerLevelMonoBinder : ComponentMonoBinder<AudioSource>, INumberBinder, INumberReverseBinder
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [BinderLog]
        public void SetValue(float value) =>
            CachedComponent.dopplerLevel = GetConvertedValue(value);

        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
            {
                var value = GetConvertedValue(CachedComponent.dopplerLevel);
                
                IntValueChanged?.Invoke((int)value);
                LongValueChanged?.Invoke((long)value);
                FloatValueChanged?.Invoke(value);
                DoubleValueChanged?.Invoke(value);
            }
        }
        
        private float GetConvertedValue(float value) =>
            _converter?.Convert(value) ?? value;
    }
}