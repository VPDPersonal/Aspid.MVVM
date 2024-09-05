using UnityEngine;
using UnityEngine.Events;
using System.Globalization;
using UltimateUI.MVVM.Unity;
using UltimateUI.MVVM.Unity.Generation;
using UltimateUI.MVVM.StarterKit.Converters;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.UnityEvents
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - String")]
    public sealed partial class StringUnityEventMonoBinder : MonoBinder, IBinder<string>, INumberBinder
    {
        public event UnityAction<string> StringValueSet
        {
            add => _stringValueSet.AddListener(value);
            remove => _stringValueSet.RemoveListener(value);
        }
        
        [Header("Converter")]
#if ULTIMATE_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterStringToString _stringConverter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _stringValueSet;
        
        [BinderLog]
        public void SetValue(string value)
        {
            if (_stringConverter != null)
                value = _stringConverter.Convert(value);
                    
            _stringValueSet?.Invoke(value);
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
    }
}