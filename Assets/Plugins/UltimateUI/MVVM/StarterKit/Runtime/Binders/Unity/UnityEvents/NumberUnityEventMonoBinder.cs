using UnityEngine;
using UnityEngine.Events;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.UnityEvents
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

        [Header("Converters")]
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterIntToInt _intConverter;
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterLongToLong _longConverter;
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterFloatToFloat _floatConverter;
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterDoubleToDouble _doubleConverter;
        
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