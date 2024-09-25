using UnityEngine;
using UnityEngine.Events;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.UnityEvents
{
    [AddComponentMenu("UI/Binders/UnityEvent/UnityEvent Binder - Quaternion")]
    public sealed partial class QuaternionUnityEventMonoBinder : Aspid.UI.MVVM.Mono.MonoBinder, IBinder<Quaternion>
    {
        public event UnityAction<Quaternion> QuaternionValueSet
        {
            add => _quaternionValueSet.AddListener(value);
            remove => _quaternionValueSet.RemoveListener(value);
        }

        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterQuaternionToQuaternion _quaternionConverter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Quaternion> _quaternionValueSet;
        
        [BinderLog]
        public void SetValue(Quaternion value)
        {
            value = _quaternionConverter?.Convert(value) ?? value;
            _quaternionValueSet?.Invoke(value);
        }
    }
}