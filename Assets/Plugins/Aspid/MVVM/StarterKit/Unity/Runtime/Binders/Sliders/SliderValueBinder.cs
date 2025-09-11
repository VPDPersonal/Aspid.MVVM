#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterFloat;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [Serializable]
    [BindModeOverride(IsAll = true)]
    public class SliderValueBinder : TargetBinder<Slider>, INumberBinder, INumberReverseBinder
    {
        public event Action<int>? IntValueChanged;
        public event Action<long>? LongValueChanged;
        public event Action<float>? FloatValueChanged;
        public event Action<double>? DoubleValueChanged;

        private bool _isNotifyValueChanged = true;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        public SliderValueBinder(Slider target, BindMode mode)
            : this(target, null, mode) { }
        
        public SliderValueBinder(Slider target, Converter? converter = null, BindMode mode = BindMode.TwoWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNone();
            _converter = converter;
        }

        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            
            Target.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(Target.value);
        }

        protected override void OnUnbound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            Target.onValueChanged.RemoveListener(OnValueChanged);
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
            
            _isNotifyValueChanged = false;
            Target.value = value;
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