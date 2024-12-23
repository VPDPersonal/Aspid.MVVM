using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UnityEvent/UnityEvent Binder - Vector3")]
    public sealed partial class UnityEventVector3MonoBinder : MonoBinder, IBinder<Vector3>
    {
        public event UnityAction<Vector3> Vector3ValueSet
        {
            add => _set.AddListener(value);
            remove => _set.RemoveListener(value);
        }

        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
        private IConverter<Vector3, Vector3> _converter;
#else
        private IConverterVector3 _converter;
#endif
        
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