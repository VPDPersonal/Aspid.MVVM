#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Globalization;
using Aspid.MVVM.StarterKit.Converters;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<string?, string?>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class InputFieldBinder : TargetBinder<TMP_InputField>, IBinder<string?>, INumberBinder, IReverseBinder<string>, INumberReverseBinder
    {
        public event Action<string>? ValueChanged;
        public event Action<int>? IntValueChanged;
        public event Action<long>? LongValueChanged;
        public event Action<float>? FloatValueChanged;
        public event Action<double>? DoubleValueChanged;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter? _converter;
        
        private bool _isNotifyValueChanged = true;
        
        public bool IsReverseEnabled { get; }

        public InputFieldBinder(TMP_InputField target)
            : this(target, isReverseEnabled: true) { }
        
        public InputFieldBinder(TMP_InputField target, bool isReverseEnabled)
            : base(target)
        {
            _converter = null;
            IsReverseEnabled = isReverseEnabled;
        }
        
        public InputFieldBinder(TMP_InputField target, Func<string?, string> converter, bool isReverseEnabled = true)
            : this(target, converter.ToConvert(), isReverseEnabled) { }
        
        public InputFieldBinder(TMP_InputField target, Converter? converter = null, bool isReverseEnabled = true)
            : base(target)
        {
            _converter = converter;
            IsReverseEnabled = isReverseEnabled; }

        protected override void OnBound(in BindParameters parameters)
        {
            if (!IsReverseEnabled) return;
            Target.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound()
        {
            if (!IsReverseEnabled) return;
            Target.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void SetValue(string? value)
        {
            _isNotifyValueChanged = false;
            Target.text = _converter?.Convert(value) ?? value;
            _isNotifyValueChanged = true;
        }

        public void SetValue(int value) =>
            SetValue(value.ToString());
        
        public void SetValue(long value) =>
            SetValue(value.ToString());
        
        public void SetValue(float value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));
        
        public void SetValue(double value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));

        private void OnValueChanged(string value)
        {
            if (!_isNotifyValueChanged) return;
            
            ValueChanged?.Invoke(value);

            if (Target.contentType 
                is not (TMP_InputField.ContentType.IntegerNumber 
                or TMP_InputField.ContentType.DecimalNumber)) return;
           
            if (IntValueChanged != null || LongValueChanged != null)
            {
                if (!long.TryParse(value, out var integerValue)) return;

                if (integerValue is <= int.MaxValue and >= int.MinValue)
                    IntValueChanged?.Invoke((int)integerValue);

                LongValueChanged?.Invoke(integerValue);
            }
            
            if (FloatValueChanged != null || DoubleValueChanged != null)
            {
                if (!double.TryParse(value, out var decimalValue)) return;
                
                if (decimalValue is <= float.MaxValue and >= float.MinValue)
                    FloatValueChanged?.Invoke((float)decimalValue);

                DoubleValueChanged?.Invoke(decimalValue);
            }
        }
    }
}
#endif