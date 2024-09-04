#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using System.Globalization;
using UltimateUI.MVVM.ViewModels;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Input
{
    public class InputFieldBinder : Binder, IBinder<string>, INumberBinder, IReverseBinder<string>, INumberReverseBinder
    {
        public event Action<string> ValueChanged;
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;
        
        protected readonly TMP_InputField InputField;
        protected readonly IConverter<string, string> Converter;
        
        public bool IsReverseEnabled { get; }
        
        public InputFieldBinder(TMP_InputField inputField, bool isReverseEnabled = true)
        {
            Converter = null;
            InputField = inputField;
            IsReverseEnabled = isReverseEnabled;
        }
        
        public InputFieldBinder(TMP_InputField inputField, Func<string, string> converter, bool isReverseEnabled = true)
            : this(inputField, new GenericFuncConverter<string, string>(converter), isReverseEnabled) { }
        
        public InputFieldBinder(TMP_InputField inputField, IConverter<string, string> converter, bool isReverseEnabled = true)
        {
            Converter = converter;
            InputField = inputField;
            IsReverseEnabled = isReverseEnabled;
        }

        protected override void OnBound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            InputField.onValueChanged.AddListener(OnValueChanged);
        }

        protected override void OnUnbound(IViewModel viewModel, string id)
        {
            if (!IsReverseEnabled) return;
            InputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void SetValue(string value) =>
            InputField.text = Converter?.Convert(value) ?? value;

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
            ValueChanged?.Invoke(value);

            if (InputField.contentType 
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