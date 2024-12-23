using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UnityEvent/UnityEvent Binder - Quaternion")]
    public sealed partial class UnityEventQuaternionMonoBinder : MonoBinder, IBinder<Quaternion>
    {
        public event UnityAction<Quaternion> Set
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }

        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Quaternion, Quaternion> _converter;
#else
        private IConverterQuaternion_converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Quaternion> _set;
        
        [BinderLog]
        public void SetValue(Quaternion value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}