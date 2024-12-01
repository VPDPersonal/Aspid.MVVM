using UnityEngine;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Number")]
    public sealed partial class NumberUnityEventMonoBinder : MonoBinder, INumberBinder
    {
        public event UnityAction<int> IntValueSet
        {
            add => _intValueSet.AddListener(value);
            remove => _intValueSet.RemoveListener(value);
        }
        
        public event UnityAction<long> LongValueSet
        {
            add => _longValueSet.AddListener(value);
            remove => _longValueSet.RemoveListener(value);
        }
        
        public event UnityAction<float> FloatValueSet
        {
            add => _floatValueSet.AddListener(value);
            remove => _floatValueSet.RemoveListener(value);
        }
        
        public event UnityAction<double> DoubleValueSet
        {
            add => _doubleValueSet.AddListener(value);
            remove => _doubleValueSet.RemoveListener(value);
        }

        // Int Converter
        [Header("Converters")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<int, int> _intConverter;
#else
        [SerializeReference] private IConverterIntToInt _intConverter;
#endif
        
        // Long Converter
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<long, long> _longConverter;
#else
        [SerializeReference] private IConverterLongToLong _longConverter;
#endif
        
        // Float Converter
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<float, float> _floatConverter;
#else
        [SerializeReference] private IConverterFloatToFloat _floatConverter;
#endif
        
        // Double Converter
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<double, double> _doubleConverter;
#else
        [SerializeReference] private IConverterDoubleToDouble _doubleConverter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<int> _intValueSet;
        [SerializeField] private UnityEvent<long> _longValueSet;
        [SerializeField] private UnityEvent<float> _floatValueSet;
        [SerializeField] private UnityEvent<double> _doubleValueSet;

        [BinderLog]
        public void SetValue(int value)
        {
            value = _intConverter?.Convert(value) ?? value;
            _intValueSet?.Invoke(value);
        }

        [BinderLog]
        public void SetValue(long value)
        {
            value = _longConverter?.Convert(value) ?? value;
            _longValueSet?.Invoke(value);
        }

        [BinderLog]
        public void SetValue(float value)
        {
            value = _floatConverter?.Convert(value) ?? value;
            _floatValueSet?.Invoke(value);
        }

        [BinderLog]
        public void SetValue(double value)
        {
            value = _doubleConverter?.Convert(value) ?? value;
            _doubleValueSet?.Invoke(value);
        }
    }
}