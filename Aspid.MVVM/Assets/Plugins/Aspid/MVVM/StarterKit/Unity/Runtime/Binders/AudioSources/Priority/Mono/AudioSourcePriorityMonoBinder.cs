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
    [AddBinderContextMenu(typeof(AudioSource))]
    [AddComponentMenu("Aspid/MVVM/Binders/Audio/AudioSource/AudioSource Binder – Priority")]
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    public partial class AudioSourcePriorityMonoBinder : ComponentMonoBinder<AudioSource>, INumberBinder, INumberReverseBinder
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;
        
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        [BinderLog]
        public void SetValue(int value) =>
            CachedComponent.priority = GetConvertedValue(value);
        
        [BinderLog]
        public void SetValue(float value) =>
            SetValue((int)value);
        
        [BinderLog]
        public void SetValue(long value) =>
            SetValue((int)value);
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((int)value);
        
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
            {
                var value = GetConvertedValue(CachedComponent.priority);
                
                IntValueChanged?.Invoke(value);
                LongValueChanged?.Invoke(value);
                FloatValueChanged?.Invoke(value);
                DoubleValueChanged?.Invoke(value);
            }
        }
        
        private int GetConvertedValue(int value) =>
            _converter?.Convert(value) ?? value;
    }
}