using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.Converters.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.Converters.IConverterVector3;
#endif

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder - Vector3")]
    public sealed partial class UnityEventVector3MonoBinder : MonoBinder, IBinder<Vector3>
    {
        public event UnityAction<Vector3> Vector3ValueSet
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }

        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [Header("Events")]
        [SerializeField] private UnityEvent<Vector3> _set;

        [BinderLog]
        public void SetValue(Vector3 value)
        {
            value = _converter?.Convert(value) ?? value;
            _set?.Invoke(value);
        }
    }
}