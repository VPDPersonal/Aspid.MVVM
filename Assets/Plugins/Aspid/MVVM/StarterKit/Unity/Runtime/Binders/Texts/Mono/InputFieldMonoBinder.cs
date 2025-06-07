#if UNITY_2023_1_OR_NEWER || ASPID_MVVM_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;
using Aspid.MVVM.Unity;
using System.Globalization;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [BindModeOverride(IsAll = true)]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Text/InputField Binder - Text")]
    [AddPropertyContextMenu(typeof(TMP_InputField), "m_Text")]
    [AddComponentContextMenu(typeof(TMP_InputField),"Add InputField Binder/InputField Binder - Text")]
    public partial class InputFieldMonoBinder : ComponentMonoBinder<TMP_InputField>, 
        IBinder<string>, INumberBinder, IReverseBinder<string>, INumberReverseBinder
    {
        public event Action<string> ValueChanged;
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;
        
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        private bool _isNotifyValueChanged = true;
        
        protected override void OnBound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            
            CachedComponent.onValueChanged.AddListener(OnValueChanged);
            if (Mode is BindMode.OneWayToSource) OnValueChanged(CachedComponent.text);
        }

        protected override void OnUnbound()
        {
            if (Mode is not (BindMode.TwoWay or BindMode.OneWayToSource)) return;
            CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        [BinderLog]
        public void SetValue(string value)
        {
            _isNotifyValueChanged = false;
            CachedComponent.text = _converter?.Convert(value) ?? value;
            _isNotifyValueChanged = true;
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
            if (!_isNotifyValueChanged) return;
            
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