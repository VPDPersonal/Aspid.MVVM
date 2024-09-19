using System;
using UnityEngine;
using UnityEngine.UI;
using AspidUI.MVVM.ViewModels;
using AspidUI.MVVM.Unity.Generation;
using AspidUI.MVVM.StarterKit.Converters;

namespace AspidUI.MVVM.StarterKit.Binders.Mono.Sliders
{
    [AddComponentMenu("UI/Binders/Slider/Slider Binder - Value")]
    public partial class SliderValueMonoBinder : ComponentMonoBinder<Slider>, INumberBinder, INumberReverseBinder
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;

        [field: Header("Parameter")]
        [field: SerializeField]
        public bool IsReverseEnabled { get; private set; }
        
        [field: Header("Converter")]
        [field: SerializeReference]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterFloatToFloat Converter { get; private set; }

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        [BinderLog]
        public void SetValue(int value) =>
            CachedComponent.value = Converter?.Convert(value) ?? value;

        [BinderLog]
        public void SetValue(long value) =>
            CachedComponent.value = Converter?.Convert(value) ?? value;

        [BinderLog]
        public void SetValue(float value) =>
            CachedComponent.value = Converter?.Convert(value) ?? value;
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);

        private void OnValueChanged(float value)
        {
            IntValueChanged?.Invoke((int)value);
            LongValueChanged?.Invoke((long)value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
        }
    }
}