using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Sliders
{
    [AddComponentMenu("UI/Binders/Slider/Slider Binder - Value")]
    public partial class SliderValueMonoBinder : ComponentMonoBinder<Slider>, INumberBinder, INumberReverseBinder
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;

        [Header("Parameter")]
        [SerializeField] private bool _isReverseEnabled;
        
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterFloatToFloat _converter;

        private bool _isNotifyValueChanged = true;        
        
        public bool IsReverseEnabled => _isReverseEnabled;

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

        [BinderLog]
        public void SetValue(int value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(long value) =>
            SetValue((float)value);

        [BinderLog]
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            
            if (IsReverseEnabled && !Mathf.Approximately(CachedComponent.value, value))
                _isNotifyValueChanged = false;
            
            CachedComponent.value = value;
        }
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);

        private void OnValueChanged(float value)
        {
            if (_isNotifyValueChanged)
            {
                IntValueChanged?.Invoke((int)value);
                LongValueChanged?.Invoke((long)value);
                FloatValueChanged?.Invoke(value);
                DoubleValueChanged?.Invoke(value);
            }

            _isNotifyValueChanged = true;
        }
    }
}