using System;
using UnityEngine.UI;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    public class SliderValueBinder : Binder, INumberBinder, INumberReverseBinder
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;
        
        protected readonly Slider Slider;
        protected readonly IConverter<float, float> Converter;
        
        public bool IsReverseEnabled { get; }

        public SliderValueBinder(Slider slider, bool isReverseEnabled)
        {
            Slider = slider;
            Converter = null;
            IsReverseEnabled = isReverseEnabled;
        }
        
        public SliderValueBinder(Slider slider, Func<float, float> converter, bool isReverseEnabled = true) :
            this(slider, new GenericFuncConverter<float, float>(converter), isReverseEnabled) { }
        
        public SliderValueBinder(Slider slider, IConverter<float, float> converter = null, bool isReverseEnabled = true)
        {
            Slider = slider;
            Converter = converter;
            IsReverseEnabled = isReverseEnabled;
        }

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            Slider.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            Slider.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        public void SetValue(int value) =>
            SetValue((float)value);

        public void SetValue(long value) =>
            SetValue((float)value);
        
        public void SetValue(double value) =>
            SetValue((float)value);
        
        public void SetValue(float value) =>
            Slider.value = Converter?.Convert(value) ?? value;

        private void OnValueChanged(float value)
        {
            IntValueChanged?.Invoke((int)value);
            LongValueChanged?.Invoke((long)value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
        }
    }
}