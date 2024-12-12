using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using System.Globalization;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
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
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<string, string> _converter;
#else
        [SerializeReference] private IConverterStringToString _converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _stringValueSet;
        
        [BinderLog]
        public void SetValue(string value)
        {
            if (_converter != null)
                value = _converter.Convert(value);
                    
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