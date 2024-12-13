using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using System.Globalization;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UnityEvent/UnityEvent Binder - String")]
    public sealed partial class UnityEventStringMonoBinder : MonoBinder, IBinder<string>, IBinder<object>, INumberBinder
    {
        public event UnityAction<string> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<string, string> _converter;
#else
        private IConverterStringToString _converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<string> _set;
        
        [BinderLog]
        public void SetValue(string value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
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

        [BinderLog]
        public void SetValue(object value) =>
            SetValue(value.ToString());
    }
}