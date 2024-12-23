using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UnityEvent/UnityEvent Binder - Long")]
    public sealed partial class UnityEventLongMonoBinder : MonoBinder, IBinder<long>
    {
        public event UnityAction<long> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }
        
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<long, long> _converter;
#else
        private IConverterLong _converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<long> _set;

        [BinderLog]
        public void SetValue(long value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}