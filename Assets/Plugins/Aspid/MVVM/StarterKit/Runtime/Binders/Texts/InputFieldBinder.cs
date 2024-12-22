#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
#nullable enable
using TMPro;
using System;
using UnityEngine;
using System.Globalization;
using Aspid.MVVM.ViewModels;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public class InputFieldBinder : Binder, IBinder<string?>, INumberBinder, IReverseBinder<string>, INumberReverseBinder
    {
        public event Action<string>? ValueChanged;
        public event Action<int>? IntValueChanged;
        public event Action<long>? LongValueChanged;
        public event Action<float>? FloatValueChanged;
        public event Action<double>? DoubleValueChanged;

        [Header("Component")]
        private TMP_InputField _inputField;
        
#if UNITY_2023_1_OR_NEWER
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#endif
        private IConverter<string?, string?>? _converter;
        
        private bool _isNotifyValueChanged = true;
        
        public bool IsReverseEnabled { get; }
        
        public InputFieldBinder(TMP_InputField inputField, bool isReverseEnabled = true)
        {
            _converter = null;
            IsReverseEnabled = isReverseEnabled;
            _inputField = inputField ?? throw new ArgumentNullException(nameof(inputField));
        }
        
        public InputFieldBinder(TMP_InputField inputField, Func<string?, string> converter, bool isReverseEnabled = true)
            : this(inputField, converter.ToConvert(), isReverseEnabled) { }
        
        public InputFieldBinder(TMP_InputField inputField, IConverter<string?, string?>? converter = null, bool isReverseEnabled = true)
        {
            _converter = converter;
            IsReverseEnabled = isReverseEnabled;
            _inputField = inputField ?? throw new ArgumentNullException(nameof(inputField));
        }

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            _inputField.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound()
        {
            if (!IsReverseEnabled) return;
            _inputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void SetValue(string? value)
        {
            _isNotifyValueChanged = false;
            _inputField.text = _converter?.Convert(value) ?? value;
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

            if (_inputField.contentType 
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