#if UNITY_2023_1_OR_NEWER || ASPID_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;
using System.Globalization;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.Texts.Input
{
    [AddComponentMenu("UI/Binders/Text/Input Field Binder")]
    public partial class InputFieldMonoBinder : ComponentMonoBinder<TMP_InputField>, 
        IBinder<string>, INumberBinder, IReverseBinder<string>, INumberReverseBinder
    {
        public event Action<string> ValueChanged;
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
        [SerializeReference] private IConverterStringToString _converter;
        
        private bool _isNotifyValueChanged = true;
        
        public bool IsReverseEnabled => _isReverseEnabled;
        
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
        public void SetValue(string value)
        {
            if (value is not null) 
                value = _converter?.Convert(value) ?? value;
            
            if (IsReverseEnabled && CachedComponent.text != value)
                _isNotifyValueChanged = false;
            
            CachedComponent.text = value;
        }

        [BinderLog]
        public void SetValue(int value) =>
            SetValue(value.ToString());

        [BinderLog]
        public void SetValue(long value) =>
            SetValue(value.ToString());

        [BinderLog]
        public void SetValue(float value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));

        [BinderLog]
        public void SetValue(double value) =>
            SetValue(value.ToString(CultureInfo.InvariantCulture));

        private void OnValueChanged(string value)
        {
            if (!_isNotifyValueChanged)
            {
                _isNotifyValueChanged = true;
                return;
            }
            
            ValueChanged?.Invoke(value);

            if (CachedComponent.contentType 
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