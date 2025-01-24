using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Slider/Slider Binder - Value")]
    public class SliderValueMonoBinder : ComponentMonoBinder<Slider>, INumberBinder, INumberReverseBinder
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;
      
        [Header("Parameter")]
        [SerializeField] private bool _isReverseEnabled = true;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;

        private bool _isNotifyValueChanged = true;
        
        public bool IsReverseEnabled => _isReverseEnabled;
        
        protected virtual void OnValidate()
        {
            if (!IsBind) return;
            
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
            
            if (_isReverseEnabled)
                CachedComponent.onValueChanged.AddListener(OnValueChanged);
        }
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValueInternal(value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValueInternal(value);

        [BinderLog]
        public void SetValue(float value) =>
            SetValueInternal(value);
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValueInternal((float)value);

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound()
        {
            if (!IsReverseEnabled) return;
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        protected void SetValueInternal(float value)
        {
            _isNotifyValueChanged = false;
            CachedComponent.value = _converter?.Convert(value) ?? value;
            _isNotifyValueChanged = true;
        }
        
        private void OnValueChanged(float value)
        {
            if (!_isNotifyValueChanged) return;
            
            IntValueChanged?.Invoke((int)value);
            LongValueChanged?.Invoke((long)value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
        }
    }
}