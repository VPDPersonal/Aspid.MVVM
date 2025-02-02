#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<float, float>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterFloat;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class SliderValueBinder : TargetBinder<Slider>, INumberBinder, INumberReverseBinder
    {
        public event Action<int>? IntValueChanged;
        public event Action<long>? LongValueChanged;
        public event Action<float>? FloatValueChanged;
        public event Action<double>? DoubleValueChanged;

        private bool _isNotifyValueChanged = true;
        
        // ReSharper disable once MemberInitializerValueIgnored
        [Header("Parameters")]
        [SerializeField] private BindMode _mode = BindMode.TwoWay;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;

        public BindMode Mode => _mode;
        
        public SliderValueBinder(Slider target, BindMode mode)
            : base(target)
        {
            _mode = mode;
            _converter = null;
        }
        
        public SliderValueBinder(Slider target, Func<float, float> converter, BindMode mode = BindMode.TwoWay) :
            this(target, converter.ToConvert(), mode) { }
        
        public SliderValueBinder(Slider target, Converter? converter = null, BindMode mode = BindMode.TwoWay)
            : base(target)
        {
            _mode = mode;
            _converter = converter;
        }

        protected override void OnBound(in BindParameters parameters, bool isBound)
        {
            if (!isBound) return;
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