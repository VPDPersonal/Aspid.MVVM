#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using System;
using UnityEngine;
using System.Globalization;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Input
{
    [AddComponentMenu("UI/Binders/Text/Input Field Binder")]
    public partial class InputFieldBinder : InputFieldBinderBase, 
        IBinder<string>, IBinderNumber, IReverseBinder<string>, IReverseBinderNumber
    {
        public event Action<string> ValueChanged;
        
        public event Action<int> IntValueChanged;
        public event Action<long> LongValueChanged;
        public event Action<float> FloatValueChanged;
        public event Action<double> DoubleValueChanged;
        
        [Header("Parameters")]
        [SerializeField] private bool _isReverse;
        [SerializeField] private string _format;

#if UNITY_EDITOR
        private bool _isSubscribed;
#endif
        
        public bool IsReverseEnabled => _isReverse;
        
        protected string Format => _format;

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
            CachedInputField.onValueChanged.AddListener(OnValueChanged);
        }
        
        private void Unsubscribe()
        {
#if UNITY_EDITOR
            _isSubscribed = false;
#endif
            CachedInputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        [BinderLog]
        public void SetValue(string value) =>
            CachedInputField.text = string.IsNullOrEmpty(Format) ? value : string.Format(Format, value);

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
            ValueChanged?.Invoke(value);

            if (CachedInputField.contentType 
                is not (TMP_InputField.ContentType.IntegerNumber 
                or TMP_InputField.ContentType.DecimalNumber)) return;
           
            if (IntValueChanged != null || LongValueChanged != null)
            {
                if (long.TryParse(value, out var integerValue))
                {
                    if (integerValue is <= int.MaxValue and >= int.MinValue)
                        IntValueChanged?.Invoke((int)integerValue);

                    LongValueChanged?.Invoke(integerValue);
                }
            }
            
            if (FloatValueChanged != null || DoubleValueChanged != null)
            {
                if (double.TryParse(value, out var decimalValue))
                {
                    if (decimalValue is <= float.MaxValue and >= float.MinValue)
                        FloatValueChanged?.Invoke((float)decimalValue);

                    DoubleValueChanged?.Invoke(decimalValue);
                }
            }
        }
    }
}
#endif