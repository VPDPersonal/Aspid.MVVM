#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders
{
    public class SliderValueBinder : Binder, INumberBinder, INumberReverseBinder
    {
        public event Action<int>? IntValueChanged;
        public event Action<long>? LongValueChanged;
        public event Action<float>? FloatValueChanged;
        public event Action<double>? DoubleValueChanged;

        private bool _isNotifyValueChanged = true;
        
        private readonly Slider _slider;
        private readonly IConverter<float, float>? _converter;
        
        public bool IsReverseEnabled { get; }

        public SliderValueBinder(Slider slider, bool isReverseEnabled)
        {
            _converter = null;
            IsReverseEnabled = isReverseEnabled;
            _slider = slider ?? throw new ArgumentNullException(nameof(slider));
        }
        
        public SliderValueBinder(Slider slider, Func<float, float> converter, bool isReverseEnabled = true) :
            this(slider, new GenericFuncConverter<float, float>(converter), isReverseEnabled) { }
        
        public SliderValueBinder(Slider slider, IConverter<float, float>?converter = null, bool isReverseEnabled = true)
        {
            _converter = converter;
            IsReverseEnabled = isReverseEnabled;
            _slider = slider ?? throw new ArgumentNullException(nameof(slider));
        }

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound()
        {
            if (!IsReverseEnabled) return;
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        public void SetValue(int value) =>
            SetValue((float)value);

        public void SetValue(long value) =>
            SetValue((float)value);
        
        public void SetValue(double value) =>
            SetValue((float)value);
        
        public void SetValue(float value)
        {
            value = _converter?.Convert(value) ?? value;
            
            if (IsReverseEnabled && !Mathf.Approximately(_slider.value, value))
                _isNotifyValueChanged = false;
            
            _slider.value = value;
        }

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