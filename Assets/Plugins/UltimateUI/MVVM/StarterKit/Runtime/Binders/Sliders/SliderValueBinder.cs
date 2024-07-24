using System;
using UnityEngine;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.StarterKit.Converters.Number;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    [AddComponentMenu("UI/Binders/Slider/Slider Binder - Value")]
    public partial class SliderValueBinder : SliderBinderBase, IBinderNumber, IReverseBinderNumber
    {
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;

        [Header("Parameter")]
        [SerializeField] private bool _isReverse;
        [SerializeField] private bool _isConvert;
        [SerializeField] private FloatConverter _converter;
        
#if UNITY_EDITOR
        private bool _isSubscribed;
#endif
        
        public bool IsReverseEnabled => _isReverse;
        
        private void OnValidate()
        {
            if (!_isReverse) Unsubscribe();
            else if (!_isSubscribed) Subscribe();
        }
        
        private void OnEnable() => Subscribe();

        private void OnDisable() => Unsubscribe();

        private void Subscribe()
        {
            if (!IsReverseEnabled) return;
#if UNITY_EDITOR
            _isSubscribed = true;
#endif
            CachedSlider.onValueChanged.AddListener(OnValueChanged);
        }
        
        private void Unsubscribe()
        {
#if UNITY_EDITOR
            _isSubscribed = false;
#endif
            CachedSlider.onValueChanged.RemoveListener(OnValueChanged);
        }

        [BinderLog]
        public void SetValue(int value) =>
            CachedSlider.value = ConvertValue(value);
        
        [BinderLog]
        public void SetValue(long value) =>
            CachedSlider.value = ConvertValue(value);
        
        [BinderLog]
        public void SetValue(float value) =>
            CachedSlider.value = ConvertValue(value);
        
        [BinderLog]
        public void SetValue(double value) =>
            SetValue((float)value);

        protected float ConvertValue(float value) =>
            _isConvert ? _converter.Convert(value) : value;

        private void OnValueChanged(float value)
        {
            IntValueChanged?.Invoke((int)value);
            LongValueChanged?.Invoke((long)value);
            FloatValueChanged?.Invoke(value);
            DoubleValueChanged?.Invoke(value);
        }
    }
}