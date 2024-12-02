using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Quaternion")]
    public sealed partial class QuaternionUnityEventMonoBinder : MonoBinder, IBinder<Quaternion>
    {
        public event UnityAction<Quaternion> QuaternionValueSet
        {
            add => _quaternionValueSet.AddListener(value);
            remove => _quaternionValueSet.RemoveListener(value);
        }

        [Header("Converter")]
#if ASPID_MVVM_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<Quaternion, Quaternion> _converter;
#else
        [SerializeReference] private IConverterQuaternionToQuaternion _converter;
#endif
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Quaternion> _quaternionValueSet;
        
        [BinderLog]
        public void SetValue(Quaternion value)
        {
            value = _converter?.Convert(value) ?? value;
            _quaternionValueSet?.Invoke(value);
        }
    }
}